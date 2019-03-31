using System;
using System.Collections.Generic;
using System.Linq;
using NET_CMONO.Framework;
using NET_CMONO.Framework.UnitOfWork;
using NET_CMONO.Model;

namespace NET_CMONO.Service
{
    public class ProjectService : IProjectService
    {
        public IUnitOfWork unitOfWork { get; set; }

        public IList<ProjectModel> GetProjectList()
        {
            Func<IQueryable<ProjectModel>, IOrderedQueryable<ProjectModel>> orderBy = (b) => b.OrderBy(_ => _.ProjectName);
            return unitOfWork.GetRepository<ProjectModel>().Get(orderBy: orderBy).ToList();
        }

        public ProjectModel GetProject(int projectId = 0)
        {
            return unitOfWork.GetRepository<ProjectModel>().GetFirstOrDefault(predicate: (p) => p.Id == projectId);
        }
    }

    public interface IProjectService : IDependency
    {
        IList<ProjectModel> GetProjectList();

        ProjectModel GetProject(int projectId = 0);

    }

}