using System;
using System.Collections.Generic;
using System.Linq;
using NET_CMONO.Framework;
using NET_CMONO.Framework.UnitOfWork;
using NET_CMONO.Framework.UnitOfWork.PagedList;
using NET_CMONO.Model;

namespace NET_CMONO.Service
{
    public class ArticleCategoryService : IArticleCategoryService
    {
        public IUnitOfWork unitOfWork { get; set; }

        public IPagedList<ArticleCategoryModel> GetArticleCategoryList(string a)
        {
            Func<IQueryable<ArticleCategoryModel>, IOrderedQueryable<ArticleCategoryModel>> orderBy = (b) => b.OrderBy(_ => _.CategoryGroup).ThenBy(_ => _.CategoryName);
            return unitOfWork.GetRepository<ArticleCategoryModel>().GetPagedList(orderBy: orderBy);
        }


        public ArticleCategoryModel InsertArticleCategory(ArticleCategoryModel articleCategory)
        {
            unitOfWork.GetRepository<ArticleCategoryModel>().Insert(articleCategory);
            unitOfWork.SaveChanges();
            return articleCategory;
        }
    }


    public interface IArticleCategoryService : IDependency
    {
        IPagedList<ArticleCategoryModel> GetArticleCategoryList(string a);

        ArticleCategoryModel InsertArticleCategory(ArticleCategoryModel articleCategory);

    }
}
