using System.Dynamic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NET_CMONO.MVC.Models;

namespace NET_CMONO.MVC.ViewComponents
{
    public class PageViewComponent : ViewComponent
    {
        public PageViewComponent()
        {
        }

        // public async Task<IViewComponentResult> InvokeAsync(Page page = Page.HOLDER)
        // {

        //     return View("NavMenu", menus);
        // }

        public IViewComponentResult Invoke(Page page = Page.HOLDER, ExpandoObject extraObj = null)
        {
            return View(page.ToString(), extraObj);
        }
    }
}