using System;
using System.Collections.Generic;
using System.Linq;
using Autofac.Extras.DynamicProxy;
using NET_CMONO.Framework;
using NET_CMONO.Framework.UnitOfWork;
using NET_CMONO.Framework.UnitOfWork.PagedList;
using NET_CMONO.Model;
using NET_CMONO.Service.Interceptors;

namespace NET_CMONO.Service
{
    [Intercept(typeof(AOPTestInterceptor))]
    public class ConfigurationService : IConfigurationService
    {

        public IUnitOfWork unitOfWork { get; set; }

        public virtual IPagedList<ConfigurationModel> GetConfigurationList(string a)
        {
            Func<IQueryable<ConfigurationModel>, IOrderedQueryable<ConfigurationModel>> orderBy = (b) => b.OrderBy(_ => _.ConfigType).ThenBy(_ => _.ConfigName);
            return unitOfWork.GetRepository<ConfigurationModel>().GetPagedList(orderBy: orderBy);
        }

        public IList<ConfigurationModel> GetTypedConfigurationList(int configType)
        {
            Func<IQueryable<ConfigurationModel>, IOrderedQueryable<ConfigurationModel>> orderBy = (b) => b.OrderBy(_ => _.ConfigName);
            return unitOfWork.GetRepository<ConfigurationModel>().Get().ToList();
        }
    }

    public interface IConfigurationService : IDependency
    {
        IList<ConfigurationModel> GetTypedConfigurationList(int configType);
    }

}