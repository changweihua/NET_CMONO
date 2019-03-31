using System;
using System.Collections.Generic;
using System.Linq;
using NET_CMONO.Framework;
using NET_CMONO.Framework.UnitOfWork;
using NET_CMONO.Model;

namespace NET_CMONO.Service
{
    public class MenuService : IMenuService
    {
        public IUnitOfWork unitOfWork { get; set; }

        public IList<MenuModel> GetMenuList(string a)
        {
            Func<IQueryable<MenuModel>, IOrderedQueryable<MenuModel>> orderBy = (b) => b.OrderBy(_ => _.MenuName);
            return unitOfWork.GetRepository<MenuModel>().Get(orderBy: orderBy).ToList();
        }
    }


    public interface IMenuService : IDependency
    {
        IList<MenuModel> GetMenuList(string a);
    }
}
