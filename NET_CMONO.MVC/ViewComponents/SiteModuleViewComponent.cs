using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NET_CMONO.Model;
using NET_CMONO.MVC.Models;
using NET_CMONO.Service;

namespace NET_CMONO.MVC.ViewComponents
{
    public class SiteModuleViewComponent : ViewComponent
    {
        private readonly IConfigurationService _configurationService;

        public SiteModuleViewComponent(ConfigurationService configurationService)
        {
            _configurationService = configurationService;
        }

        public async Task<IViewComponentResult> InvokeAsync(SiteModule siteModule, ConfigType configType = ConfigType.SITE)
        {
            var configs = await Task.FromResult(_configurationService.GetTypedConfigurationList((int)configType));

            return View(siteModule.ToString(), configs);
        }

        // public IViewComponentResult Invoke(string name)
        // {
        //     var tags = _tagService.GetList(name);
        //     // return View("MenuList", tags);
        //     return View(tags);
        // }
    }
}