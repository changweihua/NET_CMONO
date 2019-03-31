using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NET_CMONO.MVC.Areas.NCM.Controllers
{
    [Area(areaName: "NCM")]
    public class SPAController : NCMController
    {
        public IActionResult Index(int id = 0)
        {

            //他就是我们上面说的ClaimsPrincipal
            //此时，我掏出身份证（ClaimsIdentity），身份证上面有我的名称 （claim）
            var userId = User.FindFirst(ClaimTypes.Sid).Value;
            var userName = User.Identity.Name;
            ViewBag.LoginName = userName;

            //使用 HttpContext.Session来获取Session对象
            HttpContext.Session.SetString("userid","1000");

            return View();
        }
    }
}