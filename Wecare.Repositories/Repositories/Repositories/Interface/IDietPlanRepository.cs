using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wecare.Repositories.Data.Entities;
using Wecare.Repositories.Repositories.Base;
using static System.Collections.Specialized.BitVector32;

namespace Wecare.Repositories.Repositories.Repositories.Interface
{
    public interface IDietPlanRepository : IBaseRepository<DietPlan>
    {
        Task<List<DietPlan>> GetAllPagination(int pageNumber, int pageSize, string sortField, int sortOrder);
        Task<DietPlan?> GetById(Guid id);
        Task<DietPlan?> GetByUserId(Guid id);
    }
}
