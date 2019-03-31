using Microsoft.AspNetCore.Mvc;
using NET_CMONO.Service;

namespace NET_CMONO.MVC.Controllers
{
    public class AutoDIController : DTController
    {
        // GET: AutoDI
        public IActionResult Index()
        {

//  HttpContext.RequestServices.GetService<ITest>();   

            ViewBag.date = testService.GetList("Name");
            return View();
        }
    }
}