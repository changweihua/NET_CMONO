using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NET_CMONO.Service;

namespace NET_CMONO.MVC.ViewComponents
{
    public class MenuViewComponent : ViewComponent
    {
        private readonly IMenuService _menuService;

        public MenuViewComponent(MenuService menuService)
        {
            _menuService = menuService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string name)
        {
            var menus = await Task.FromResult(_menuService.GetMenuList(name));

            return View("NavMenu", menus);
        }

        // public IViewComponentResult Invoke(string name)
        // {
        //     var tags = _tagService.GetList(name);
        //     // return View("MenuList", tags);
        //     return View(tags);
        // }
    }
}