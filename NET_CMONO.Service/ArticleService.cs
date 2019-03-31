using System;
using System.Linq;
using NET_CMONO.Framework;
using NET_CMONO.Framework.UnitOfWork;
using NET_CMONO.Framework.UnitOfWork.PagedList;
using NET_CMONO.Model;

namespace NET_CMONO.Service
{
    public class ArticleService : IArticleService
    {
        public IUnitOfWork unitOfWork { get; set; }

        public IPagedList<ArticleModel> GetArticleList(string a)
        {
            Func<IQueryable<ArticleModel>, IOrderedQueryable<ArticleModel>> orderBy = (b) => b.OrderBy(_ => _.ArticleTitle);
            return unitOfWork.GetRepository<ArticleModel>().GetPagedList(orderBy: orderBy);
        }

        public ArticleModel GetArticle(int articleId = 0)
        {
            return unitOfWork.GetRepository<ArticleModel>().GetFirstOrDefault(predicate: (a) => a.Id == articleId);
        }
    }

    public interface IArticleService : IDependency
    {
        IPagedList<ArticleModel> GetArticleList(string a);

        ArticleModel GetArticle(int articleId = 0);
    }
}