using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NET_CMONO.Service;

namespace NET_CMONO.MVC.ViewComponents
{
    // [ViewComponent(Name = "BannerList")]
    public class BannerViewComponent : ViewComponent
    {
        private readonly ITestService _tagService;

        public BannerViewComponent(ITestService tagService)
        {
            _tagService = tagService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string name)
        {
            var tags = await Task.FromResult(_tagService.GetList(name));

            return View("BannerList", tags);
        }
    }
}