using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wecare.Repositories.Data.Entities;
using Wecare.Repositories.Repositories.Base;

namespace Wecare.Repositories.Repositories.Repositories.Interface
{
    public interface IMenuRepository : IBaseRepository<Menu>
    {
        Task<List<Menu>> GetAllPagination(int pageNumber, int pageSize, string sortField, int sortOrder);
        Task<(List<Menu>, long)> Search(Menu Menu, int pageNumber, int pageSize, string sortField, int sortOrder);
        Task<Menu?> GetById(Guid id);
        Task<List<Menu>> GetAllMenuByDietitianId(Guid id);
        Task<List<Menu>> GetMenusByHealthMetrics(List<HealthMetric> healthMetrics);
    }
}
