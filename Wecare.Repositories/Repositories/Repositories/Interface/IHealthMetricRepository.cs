using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wecare.Repositories.Data.Entities;
using Wecare.Repositories.Repositories.Base;

namespace Wecare.Repositories.Repositories.Repositories.Interface
{
    public interface IHealthMetricRepository : IBaseRepository<HealthMetric>
    {
        Task<List<HealthMetric>> GetAllPagination(int pageNumber, int pageSize, string sortField, int sortOrder);
        Task<List<HealthMetric>> GetHealthMetricByUserId(Guid id);
        Task<HealthMetric> GetById(Guid id);
    }
}
