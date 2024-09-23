using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wecare.Services.Model;

namespace Wecare.Services.Services.Interface
{
    public interface IDietPlanService
    {
        Task<bool> Add(DietPlanModel model);
        Task<bool> Update(DietPlanModel model);
        Task<bool> Delete(Guid id);
        Task<List<DietPlanModel>?> GetAll();
        Task<List<DietPlanModel>?> GetAllPagination(int pageNumber, int pageSize, string sortField, int sortOrder);
        Task<DietPlanModel?> GetById(Guid id);
        Task<DietPlanModel?> GetByUserId(Guid id);
        Task<long> GetTotalCount();
    }
}
