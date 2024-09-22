using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wecare.Services.Model;

namespace Wecare.Services.Services.Interface
{
    public interface IHealthMetricService
    {
        Task<bool> Add(HealthMetricModel model);
        Task<bool> Update(HealthMetricModel model);
        Task<bool> Delete(Guid id);
        Task<List<HealthMetricModel>?> GetAll();
        Task<List<HealthMetricModel>?> GetAllPagination(int pageNumber, int pageSize, string sortField, int sortOrder);
        Task<HealthMetricModel?> GetById(Guid id);
        Task<List<HealthMetricModel>> GetHealthMetricByUserId(Guid id);
        Task<long> GetTotalCount();
    }
}
