using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NET_CMONO.DtoModel;
using NET_CMONO.Framework.Util;
using NET_CMONO.Service;
using Newtonsoft.Json;

namespace NET_CMONO.MVC.Areas.NCM.Controllers
{
    [Area(areaName: "NCM")]
    public class AccountController : Controller
    {

        public IUserService iUserService { get; set; }

        public IActionResult Login()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Login(LoginUserModel model)
        {
            var encryptedPwd = BCrypt.HashPassword(model.LoginPwd, BCrypt.GenerateSalt(8));
            // model.LoginPwd = encryptedPwd;

            var user = iUserService.DoLogin(model.LoginName, model.LoginPwd);

            if (user != null)
            {

                //使用 HttpContext.Session来获取Session对象
                HttpContext.Session.SetString("userid","10010");

                //ASP.NET Core 的验证模型是 claims-based authentication 。
                //Claim 是对被验证主体特征的一种表述，比如：登录用户名是xxx，email是xxx，其中的“登录用户名”，
                //“email”就是ClaimType.
                //一组claims构成了一个identity，具有这些claims的identity就是 ClaimsIdentity

                //创建一个身份认证
                var claims = new List<Claim>() {
                    new Claim(ClaimTypes.Sid, user.Id.ToString()), //用户ID
                    new Claim(ClaimTypes.Name, user.LoginName)  //用户名称
                };

                var identity = new ClaimsIdentity(claims, "UL-Login");

                //ClaimsIdentity的持有者就是 ClaimsPrincipal
                var userPrincipal = new ClaimsPrincipal(identity);

                //一个ClaimsPrincipal可以持有多个ClaimsIdentity，就比如一个人既持有驾照，又持有护照.
                await HttpContext.SignInAsync("NET-CMONO-CookieAuthenticationScheme", userPrincipal, new AuthenticationProperties
                {
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(20),
                    IsPersistent = false,
                    AllowRefresh = false
                });

                //理解了Claim, ClaimsIdentity, ClaimsPrincipal这三个概念，
                //就能理解生成登录Cookie为什么要用之前的代码。

                //要用Cookie代表一个通过验证的主体，必须包含Claim, ClaimsIdentity, ClaimsPrincipal这三个信息，
                //ClaimsPrincipal就是持有证件的人，ClaimsIdentity就是证件，"Login"就是证件类型（这里假设是驾照），
                //Claim就是驾照中的信息。
            }

            return Json(JsonConvert.SerializeObject(new
            {
                ori = model,
                res = user
            }));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("NET-CMONO-CookieAuthenticationScheme");
            return RedirectToAction("Index", "Home");
        }
    }



}