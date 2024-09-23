using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wecare.Repositories.Data.Entities;
using Wecare.Repositories.Repositories.Base;

namespace Wecare.Repositories.Repositories.Repositories.Interface
{
    public interface IMenuDietPlanRepository : IBaseRepository<MenuDietPlan>
    {
        Task<List<MenuDietPlan>> GetAllByMenuId(Guid id);

        Task<MenuDietPlan> GetByDietPlanIdAndMenuId(Guid dietPlanId, Guid menuId);
    }
}
