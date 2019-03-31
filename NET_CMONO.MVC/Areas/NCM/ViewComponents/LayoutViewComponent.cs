using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NET_CMONO.Model;
using NET_CMONO.MVC.Models;
using NET_CMONO.Service;

namespace NET_CMONO.MVC.Areas.NCM.ViewComponents
{
    public class LayoutViewComponent : ViewComponent
    {
        private readonly IConfigurationService _configurationService;

        public LayoutViewComponent(ConfigurationService configurationService)
        {
            _configurationService = configurationService;
        }

        public async Task<IViewComponentResult> InvokeAsync(LayoutModule layoutModule, ExpandoObject extraObj = null)
        {

            var configs = await Task.FromResult(_configurationService.GetTypedConfigurationList((int)ConfigType.SITE));

            var dict = new Dictionary<string, string>();

            configs.ToList().ForEach(cfg =>
            {
                dict.Add(cfg.ConfigName, cfg.ConfigValue);
            });

            dict.Add("UserName", User.Identity.Name);

            return View(layoutModule.ToString(), dict);
        }

    }

}