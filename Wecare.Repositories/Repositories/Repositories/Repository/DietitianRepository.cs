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
    public class DietitianRepository : BaseRepository<Dietitian>, IDietitianRepository
    {
        private readonly WeCareDbContext _context;

        public DietitianRepository(WeCareDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Dietitian>> GetAllPagination(int pageNumber, int pageSize, string sortField, int sortOrder)
        {
            var queryable = GetQueryable();
            queryable = base.ApplySort(queryable, sortField, sortOrder);

            queryable = GetQueryablePagination(queryable, pageNumber, pageSize);

            return await queryable.ToListAsync();
        }

        public async Task<(List<Dietitian>, long)> Search(Dietitian dietitian, int pageNumber, int pageSize, string sortField, int sortOrder)
        {
            var queryable = GetQueryable();
            queryable = base.ApplySort(queryable, sortField, sortOrder);

            if (queryable.Any())
            {
                if (!string.IsNullOrEmpty(dietitian.Name))
                {
                    queryable = queryable.Where(m => m.Name.ToLower().Trim().StartsWith(dietitian.Name.ToLower().Trim()));
                }

                if (!string.IsNullOrEmpty(dietitian.Email))
                {
                    queryable = queryable.Where(m => m.Email.ToLower().Trim().StartsWith(dietitian.Email.ToLower().Trim()));
                }

                if (!string.IsNullOrEmpty(dietitian.Phone))
                {
                    queryable = queryable.Where(m => m.Phone.ToLower().Trim().StartsWith(dietitian.Phone.ToLower().Trim()));
                }

                if (!string.IsNullOrEmpty(dietitian.Specialization))
                {
                    queryable = queryable.Where(m => m.Specialization.ToLower().Trim().StartsWith(dietitian.Specialization.ToLower().Trim()));
                }
            }

            var totalOrigin = queryable.Count();

            queryable = GetQueryablePagination(queryable, pageNumber, pageSize);

            var dietitians = await queryable.Include(m => m.Menus).ToListAsync();

            return (dietitians, totalOrigin);
        }

        public async Task<Dietitian> GetById(Guid id)
        {
            var query = GetQueryable(m => m.Id == id);
            var dietitian = await query
                .Include(m => m.Menus)
                .SingleOrDefaultAsync();

            return dietitian;
        }
    }
}
