using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wecare.Repositories.Data.Entities;
using Wecare.Repositories.Repositories.Base;

namespace Wecare.Repositories.Repositories.Repositories.Interface
{
    public interface IMenuDishRepository : IBaseRepository<MenuDish>
    {
        Task<List<MenuDish>> GetAllByMenuId(Guid id);

        Task<MenuDish> GetByDishIdAndMenuId(Guid dishId, Guid menuId);
    }
}
