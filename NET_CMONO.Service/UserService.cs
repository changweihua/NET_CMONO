using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using NET_CMONO.Framework;
using NET_CMONO.Framework.UnitOfWork;
using NET_CMONO.Model;

namespace NET_CMONO.Service
{
    public class UserService : IUserService
    {
        public IUnitOfWork unitOfWork { get; set; }

        // public IPagedList<ArticleModel> GetArticleList(string a)
        // {
        //     Func<IQueryable<ArticleModel>, IOrderedQueryable<ArticleModel>> orderBy = (b) => b.OrderBy(_ => _.ArticleTitle);
        //     return unitOfWork.GetRepository<ArticleModel>().GetPagedList(orderBy: orderBy);
        // }

        public UserModel DoLogin(string loginName, string loginPwd)
        {
            return unitOfWork.GetRepository<UserModel>().GetFirstOrDefault(predicate: (u) => u.LoginName == loginName && u.LoginPwd == loginPwd && !u.IsDeleted, include: (q) => q.Include(u => u.UserProfiles));//.FirstOrDefault();
        }
    }

    public interface IUserService : IDependency
    {
        // IPagedList<ArticleModel> GetArticleList(string a);

        UserModel DoLogin(string loginName, string loginPwd);
    }
}