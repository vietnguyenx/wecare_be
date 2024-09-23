using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wecare.Services.Model;

namespace Wecare.Services.Services.Interface
{
    public interface IMenuDietPlanService
    {
        Task<bool> Add(MenuDietPlanModel menuDietPlanModel);
        Task<bool> Delete(Guid idDietPlan, Guid idMenu);
        Task<List<MenuDietPlanModel>?> GetAllByMenuId(Guid menuId);
    }
}
