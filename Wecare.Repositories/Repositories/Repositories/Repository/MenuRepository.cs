﻿using Microsoft.EntityFrameworkCore;
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
                .Include(m => m.DietPlans)
                .ToListAsync();
        }

        public async Task<List<Menu>> GetAllMenuByDietitianId(Guid id)
        {
            var queryable = await GetQueryable(x => x.DietitianId == id)
                .Include(m => m.Dietitian)
                .Include(m => m.MenuDishes)
                .Include(m => m.DietPlans).ToListAsync();

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
                .Include(m => m.DietPlans)
                .ToListAsync();

            return (pacakges, totalOrigin);
        }

        public async Task<Menu?> GetById(Guid id)
        {
            var query = GetQueryable(m => m.Id == id);
            var user = await query
                .Include(m => m.Dietitian)
                .Include(m => m.MenuDishes)
                .Include(m => m.DietPlans)
                .SingleOrDefaultAsync();

            return user;
        }
    }
}
