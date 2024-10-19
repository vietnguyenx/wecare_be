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
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly WeCareDbContext _context;

        public UserRepository(WeCareDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User> FindUsernameOrEmail(User user)
        {
            var queryable = base.GetQueryable();
            queryable = queryable.Where(entity => !entity.IsDeleted);

            if (!string.IsNullOrEmpty(user.Username) || !string.IsNullOrEmpty(user.Email))
            {
                queryable = queryable.Where(entity => user.Username.ToLower() == entity.Username.ToLower()
                                            || user.Email.ToLower() == entity.Email.ToLower()
                );
            }

            var result = await queryable
                .Include(m => m.HealthMetric)
                .Include(m => m.DietPlans).SingleOrDefaultAsync();

            return result;
        }

        public async Task<User> GetUserByEmail(User user)
        {
            var queryable = base.GetQueryable();
            queryable = queryable.Where(entity => !entity.IsDeleted);

            if (!string.IsNullOrEmpty(user.Email))
            {
                queryable = queryable.Where(entity => user.Email.ToLower() == entity.Email.ToLower()
                );
            }

            var result = await queryable
                .Include(m => m.HealthMetric)
                .Include(m => m.DietPlans).SingleOrDefaultAsync();

            return result;
        }

        public async Task<List<User>> GetAllPagination(int pageNumber, int pageSize, string sortField, int sortOrder)
        {
            var queryable = GetQueryable();
            queryable = base.ApplySort(queryable, sortField, sortOrder);

            // loc theo trang
            queryable = GetQueryablePagination(queryable, pageNumber, pageSize);

            return await queryable
                .Include(m => m.HealthMetric)
                .Include(m => m.DietPlans).ToListAsync();
        }

        public async Task<User> GetById(Guid id)
        {
            var query = GetQueryable(m => m.Id == id);
            var user = await query.Include(m => m.HealthMetric).Include(m => m.DietPlans).ThenInclude(dp => dp.MenuDietPlans).SingleOrDefaultAsync();

            return user;
        }

        public async Task<(List<User>, long)> Search(User user, int pageNumber, int pageSize, string sortField, int sortOrder)
        {
            var queryable = GetQueryable();
            queryable = base.ApplySort(queryable, sortField, sortOrder);

            // dieu kien loc tung buoc
            if (queryable.Any())
            {
                if (!string.IsNullOrEmpty(user.Username))
                {
                    queryable = queryable.Where(m => m.Username.ToLower().Trim() == user.Username.ToLower().Trim());
                }

                if (!string.IsNullOrEmpty(user.FullName))
                {
                    queryable = queryable.Where(m => m.FullName.ToLower().Trim().Contains(user.FullName.ToLower().Trim()));
                }

                if (!string.IsNullOrEmpty(user.Email))
                {
                    queryable = queryable.Where(m => m.Email.ToLower().Trim() == user.Email.ToLower().Trim());
                }

                if (user.DOB.HasValue)
                {
                    queryable = queryable.Where(m => m.DOB.Value.Date == user.DOB.Value.Date);
                }

                if (!string.IsNullOrEmpty(user.Address))
                {
                    queryable = queryable.Where(m => m.Address.ToLower().Trim().Contains(user.Address.ToLower().Trim()));
                }

                if (user.Gender.HasValue) 
                {
                    queryable = queryable.Where(m => m.Gender == user.Gender); 
                }

                if (!string.IsNullOrEmpty(user.Phone))
                {
                    queryable = queryable.Where(m => m.Phone.ToLower().Trim().Contains(user.Phone.ToLower().Trim()));
                }

            }

            var totalOrigin = queryable.Count();

            // loc theo trang
            queryable = GetQueryablePagination(queryable, pageNumber, pageSize);

            var users = await queryable
                .Include(m => m.HealthMetric)
                .Include(m => m.DietPlans).ToListAsync();

            return (users, totalOrigin);
        }
    }
}
