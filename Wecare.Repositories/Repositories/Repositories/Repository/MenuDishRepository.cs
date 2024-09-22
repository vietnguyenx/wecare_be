using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wecare.Repositories.Data.Entities;
using Wecare.Repositories.Repositories.Base;
using Wecare.Repositories.Repositories.Repositories.Interface;
using WeCare.Repositories.Data;

namespace Wecare.Repositories.Repositories.Repositories.Repository
{
    public class MenuDishRepository : BaseRepository<MenuDish>, IMenuDishRepository
    {
        private readonly WeCareDbContext _context;

        public MenuDishRepository(WeCareDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<MenuDish>> GetAllByMenuId(Guid id)
        {
            var queryable = GetQueryable(x => x.MenuId == id);

            if (queryable.Any())
            {
                queryable = queryable.Include(m => m.Dish).Include(m => m.Menu);
            }

            return await queryable.ToListAsync();
        }

        public async Task<MenuDish?> GetByDishIdAndMenuId(Guid dishId, Guid menuId)
        {
            MenuDish menuDish = await _context.MenuDishes.Where(x => x.MenuId == menuId && x.DishId == dishId).Include(m => m.Dish).Include(m => m.Menu).SingleOrDefaultAsync();
            return menuDish;
        }
    }
}
