using Microsoft.AspNetCore.Mvc;
using NET_CMONO.Service;

namespace NET_CMONO.MVC.Controllers
{
    public class ProjectController : DTController
    {

        public ProjectService projectSerice { get; set; }

        public IActionResult Detail(int id = 0)
        {
            var project = projectSerice.GetProject(id);
            ViewData["Message"] = "Your contact page.";

            return View(project);
        }

    }
}