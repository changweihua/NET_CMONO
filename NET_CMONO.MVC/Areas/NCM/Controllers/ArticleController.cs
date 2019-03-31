using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NET_CMONO.Model;
using NET_CMONO.DtoModel;
using NET_CMONO.MVC.Models;
using NET_CMONO.Service;
using Newtonsoft.Json;
using NET_CMONO.Framework.Util;
using System.Linq;

namespace NET_CMONO.MVC.Areas.NCM.Controllers
{
    [Area(areaName: "NCM")]
    public class ArticleController : NCMController
    {

        public IArticleCategoryService iArticleCategoryService { get; set; }
        public IArticleService iArticleService { get; set; }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ArticleList()
        {
            var pager = await Task.FromResult(iArticleService.GetArticleList(""));

            return Json(new LayTableResponseModel
            {
                code = 0,
                msg = "",
                count = pager.TotalCount,
                data = pager.Items
            });
        }

        public IActionResult Category()
        {
            // var configs = await Task.FromResult(iConfigurationService.GetTypedConfigurationList((int)ConfigType.SITE));

            return View();
        }

        [HttpPost]
        public IActionResult Category(ArticleCategoryEditModel model)
        {
            // var configs = await Task.FromResult(iConfigurationService.GetTypedConfigurationList((int)ConfigType.SITE));

            return Json(new
            {
                A = JsonConvert.SerializeObject(iArticleCategoryService.InsertArticleCategory(new ArticleCategoryModel
                {
                    CreatedBy = 1,
                    CategoryGroup = string.Join(", ", (EnumUtil.GetEnumValuesFromFlagsEnum<ArticleCategoryFor>(model.CategoryProject + model.CategoryArticle)).Select(e => e.ToString())),
                    CategoryName = model.CategoryName,
                    CategoryIcon = model.CategoryIcon,
                    CategoryRemark = model.CategoryRemark
                }))
            });
        }

        public async Task<IActionResult> CategoryList()
        {
            var pager = await Task.FromResult(iArticleCategoryService.GetArticleCategoryList(""));

            return Json(new LayTableResponseModel
            {
                code = 0,
                msg = "",
                count = pager.TotalCount,
                data = pager.Items
            });
        }
    }



}