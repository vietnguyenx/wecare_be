using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wecare.Repositories.Data.Entities;
using WeCare.Repositories.Data;
using Wecare.Repositories.Repositories.Base;
using Wecare.Repositories.Repositories.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace Wecare.Repositories.Repositories.Repositories.Repository
{
    public class MenuDietPlanRepository : BaseRepository<MenuDietPlan>, IMenuDietPlanRepository
    {
        private readonly WeCareDbContext _context;

        public MenuDietPlanRepository(WeCareDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<MenuDietPlan>> GetAllByMenuId(Guid id)
        {
            var queryable = GetQueryable(x => x.MenuId == id);

            if (queryable.Any())
            {
                queryable = queryable.Include(m => m.DietPlan).Include(m => m.Menu);
            }

            return await queryable.ToListAsync();
        }

        public async Task<MenuDietPlan?> GetByDietPlanIdAndMenuId(Guid dietPlanId, Guid menuId)
        {
            MenuDietPlan menuDietPlan = await _context.MenuDietPlans.Where(x => x.MenuId == menuId && x.DietPlanId == dietPlanId).Include(m => m.DietPlan).Include(m => m.Menu).SingleOrDefaultAsync();
            return menuDietPlan;
        }
    }
}
