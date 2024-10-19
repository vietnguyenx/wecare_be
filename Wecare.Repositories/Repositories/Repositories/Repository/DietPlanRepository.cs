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
using static System.Collections.Specialized.BitVector32;

namespace Wecare.Repositories.Repositories.Repositories.Repository
{
    public class DietPlanRepository : BaseRepository<DietPlan>, IDietPlanRepository
    {
        private readonly WeCareDbContext _context;

        public DietPlanRepository(WeCareDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<DietPlan>> GetAllPagination(int pageNumber, int pageSize, string sortField, int sortOrder)
        {
            var queryable = GetQueryable();
            queryable = base.ApplySort(queryable, sortField, sortOrder);

            queryable = GetQueryablePagination(queryable, pageNumber, pageSize);
            return await queryable.Include(m => m.User)
            .ToListAsync();
        }

        public async Task<DietPlan?> GetById(Guid id)
        {
            var query = GetQueryable(m => m.Id == id);
            var session = await query.Include(m => m.User).SingleOrDefaultAsync();

            return session;
        }

        public async Task<DietPlan?> GetByUserId(Guid id)
        {
            var query = GetQueryable(m => m.UserId == id);
            var session = await query.Include(m => m.User).SingleOrDefaultAsync();

            return session;
        }

        public async Task<bool> IsDietPlanExists(Guid userId, DateOnly dateAssigned)
        {
            return await _context.DietPlans.AnyAsync(dp => dp.UserId == userId && dp.DateAssigned == dateAssigned);
        }

    }
}
