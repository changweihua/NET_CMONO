using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NET_CMONO.Service;

namespace NET_CMONO.MVC.ViewComponents
{
    public class TopWidgetViewComponent : ViewComponent
    {

        private readonly IProjectService _projectService;

        public TopWidgetViewComponent(ProjectService projectService)
        {
            _projectService = projectService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string widgetType, int count = 5, string viewName = "Default")
        {

            switch (widgetType)
            {
                case "":

                    break;
                default:
                    break;
            }

            var projects = await Task.FromResult(_projectService.GetProjectList());

            return View(viewName, projects);
        }
    }
}