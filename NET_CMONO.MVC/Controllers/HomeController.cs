using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NET_CMONO.Framework.UnitOfWork;
using NET_CMONO.Model;
using NET_CMONO.MVC.Models;
using NET_CMONO.Service;

namespace NET_CMONO.MVC.Controllers
{
    public class HomeController : DTController
    {
        public ILogger<HomeController> _logger;

        // private readonly ILogger<HomeController> _logger;

        // private readonly ITestService testService;

        // public HomeController(ILogger<HomeController> logger, ITestService tService)
        // {
        //     _logger = logger;
        //     testService=tService;
        // }

        public IActionResult Index()
        {

            // var list = cfgService.GetConfigurationList("").Items;
            // //  _logger.LogInformation("你访问了首页");
            // // _logger.LogWarning("警告信息");
            // // _logger.LogError("错误信息");
            // ViewBag.date = cfgService.GetConfigurationList("").Items;

            var user = HttpContext.User;
// if(user != null){
//             logger.LogInformation(Newtonsoft.Json.JsonConvert.SerializeObject(user.Identity.IsAuthenticated));
// }
            return View();
        }


        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
