using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
    public class HealthMetricRepository : BaseRepository<HealthMetric>, IHealthMetricRepository
    {
        private readonly WeCareDbContext _context;

        public HealthMetricRepository(WeCareDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<HealthMetric>> GetAllPagination(int pageNumber, int pageSize, string sortField, int sortOrder)
        {
            var queryable = GetQueryable();
            queryable = base.ApplySort(queryable, sortField, sortOrder);

            queryable = GetQueryablePagination(queryable, pageNumber, pageSize);

            return await queryable.Include(m => m.User).ToListAsync();
        }

        public async Task<List<HealthMetric>> GetHealthMetricByUserId(Guid id)
        {
            var queryable = base.GetQueryable(m => m.UserId == id);

            return await queryable.Include(m => m.User).ToListAsync();
        }

        public async Task<HealthMetric> GetById(Guid id)
        {
            var query = GetQueryable(m => m.Id == id);
            var healthMetric = await query
                .Include(m => m.User).SingleOrDefaultAsync();
            return healthMetric;
        }
    }
}
