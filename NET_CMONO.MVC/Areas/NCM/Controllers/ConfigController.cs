using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NET_CMONO.Model;
using NET_CMONO.Service;

namespace NET_CMONO.MVC.Areas.NCM.Controllers
{
    [Area(areaName: "NCM")]
    public class ConfigController : NCMController
    {

        public IConfigurationService iConfigurationService { get; set; }


        public async Task<IActionResult> Site()
        {
            var configs = await Task.FromResult(iConfigurationService.GetTypedConfigurationList((int)ConfigType.SITE));

            return View(configs);
        }
    }
}