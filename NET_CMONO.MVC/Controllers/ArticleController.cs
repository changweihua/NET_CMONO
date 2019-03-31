using Microsoft.AspNetCore.Mvc;
using NET_CMONO.Service;

namespace NET_CMONO.MVC.Controllers
{
    public class ArticleController : DTController
    {

        public ArticleService articleService { get; set; }

        public IActionResult Index(int id = 0)
        {
            var articles = articleService.GetArticleList("").Items;
            ViewData["Message"] = "Your contact page.";

            return View(articles);
        }

        public IActionResult Detail(int id = 0)
        {
            var article = articleService.GetArticle(id);
            ViewData["Message"] = "Your contact page.";

            return View(article);
        }

    }
}