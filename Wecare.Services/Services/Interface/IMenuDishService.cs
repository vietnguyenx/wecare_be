using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wecare.Services.Model;

namespace Wecare.Services.Services.Interface
{
    public interface IMenuDishService
    {
        Task<bool> Add(MenuDishModel menuDishModel);
        Task<bool> Delete(Guid idDish, Guid idMenu);
        Task<List<MenuDishModel>?> GetAllByMenuId(Guid menuId);
    }
}
