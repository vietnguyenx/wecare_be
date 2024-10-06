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
    public class MenuRepository : BaseRepository<Menu>, IMenuRepository
    {
        private readonly WeCareDbContext _context;

        public MenuRepository(WeCareDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Menu>> GetAllPagination(int pageNumber, int pageSize, string sortField, int sortOrder)
        {
            var queryable = GetQueryable();
            queryable = base.ApplySort(queryable, sortField, sortOrder);

            // loc theo trang
            queryable = GetQueryablePagination(queryable, pageNumber, pageSize);

            return await queryable
                .Include(m => m.Dietitian)
                .Include(m => m.MenuDishes)
                .Include(m => m.MenuDietPlans)
                .ToListAsync();
        }

        public async Task<List<Menu>> GetAllMenuByDietitianId(Guid id)
        {
            var queryable = await GetQueryable(x => x.DietitianId == id)
                .Include(m => m.Dietitian)
                .Include(m => m.MenuDishes)
                .Include(m => m.MenuDietPlans).ToListAsync();

            return queryable;
        }

        public async Task<(List<Menu>, long)> Search(Menu Menu, int pageNumber, int pageSize, string sortField, int sortOrder)
        {
            var queryable = GetQueryable();
            queryable = base.ApplySort(queryable, sortField, sortOrder);

            // Điều kiện lọc từng bước
            if (queryable.Any())
            {
                if (!string.IsNullOrEmpty(Menu.MenuName))
                {
                    queryable = queryable.Where(m => m.MenuName.ToLower().Trim().Contains(Menu.MenuName.ToLower().Trim()));
                }

                if (Menu.DietitianId != Guid.Empty && Menu.DietitianId != null)
                {
                    queryable = queryable.Where(m => m.DietitianId == Menu.DietitianId);
                }
            }
            var totalOrigin = queryable.Count();

            // Lọc theo trang
            queryable = GetQueryablePagination(queryable, pageNumber, pageSize);

            var pacakges = await queryable
                .Include(m => m.Dietitian)
                .Include(m => m.MenuDishes)
                .Include(m => m.MenuDietPlans)
                .ToListAsync();

            return (pacakges, totalOrigin);
        }

        public async Task<Menu?> GetById(Guid id)
        {
            var query = GetQueryable(m => m.Id == id);
            var user = await query
                .Include(m => m.Dietitian)
                .Include(m => m.MenuDishes)
                .Include(m => m.MenuDietPlans)
                .SingleOrDefaultAsync();

            return user;
        }

        public async Task<List<Menu>> GetLatestMenus()
        {
            return await GetQueryable()
                .Include(m => m.Dietitian)
                .Include(m => m.MenuDishes)
                .OrderByDescending(m => m.CreatedDate) 
                .Take(5) 
                .ToListAsync();
        }

        public async Task<List<Menu>> GetMenusByHealthMetrics(List<HealthMetric> healthMetrics)
        {
            var queryable = GetQueryable();

            foreach (var healthMetric in healthMetrics)
            {
                if (healthMetric.BloodSugar.HasValue && healthMetric.BloodSugar > 100) // High blood sugar
                {
                    queryable = queryable.Where(menu => menu.MenuDishes.All(md =>
                        md.Dish.Carbohydrates <= 60 &&
                        md.Dish.Sugar <= 10 &&
                        md.Dish.Fiber >= 5 &&
                        md.Dish.Fat <= 20 &&
                        md.Dish.Calories <= 600
                    ));
                }

                if (healthMetric.UricAcid.HasValue && healthMetric.UricAcid > 7) // High uric acid
                {
                    queryable = queryable.Where(menu => menu.MenuDishes.All(md =>
                        md.Dish.Protein <= 50 &&
                        md.Dish.Purine <= 150 &&
                        md.Dish.Fat <= 20
                    ));
                }
            }

            return await queryable
                .Include(m => m.Dietitian)
                .Include(m => m.MenuDishes)
                .ThenInclude(md => md.Dish)
                .ToListAsync();
        }

    }
}
