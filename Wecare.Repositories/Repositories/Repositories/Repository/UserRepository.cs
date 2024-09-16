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
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly WeCareDbContext _context;

        public UserRepository(WeCareDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User> GetById(Guid id)
        {
            var query = GetQueryable(m => m.Id == id);
            var user = await query
                .Include(m => m.HealthMetrics)
                .Include(m => m.UserDietPlans)
                .Include(m => m.Notifications)
                .SingleOrDefaultAsync();

            return user;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var queryable = base.GetQueryable();
            queryable = queryable.Where(entity => !entity.IsDeleted);

            if (!string.IsNullOrEmpty(email))
            {
                queryable = queryable.Where(entity => entity.Email.ToLower() == email.ToLower());
            }

            var result = await queryable
                .Include(m => m.HealthMetrics)
                .Include(m => m.UserDietPlans)
                .Include(m => m.Notifications)
                .SingleOrDefaultAsync();

            return result;
        }

        public async Task<User> FindUsernameOrEmail(string username, string email)
        {
            var queryable = base.GetQueryable();
            queryable = queryable.Where(entity => !entity.IsDeleted);

            if (!string.IsNullOrEmpty(username) || !string.IsNullOrEmpty(email))
            {
                queryable = queryable.Where(entity =>
                    (username != null && entity.Username.ToLower() == username.ToLower()) ||
                    (email != null && entity.Email.ToLower() == email.ToLower())
                );
            }

            var result = await queryable
                .Include(m => m.HealthMetrics)
                .Include(m => m.UserDietPlans)
                .SingleOrDefaultAsync();

            return result;
        }

        public async Task<List<User>> GetAllPagination(int pageNumber, int pageSize, string sortField, int sortOrder)
        {
            var queryable = GetQueryable();
            queryable = base.ApplySort(queryable, sortField, sortOrder);

            // loc theo trang
            queryable = GetQueryablePagination(queryable, pageNumber, pageSize);

            return await queryable
                .Include(m => m.HealthMetrics)
                .Include(m => m.UserDietPlans)
                .ToListAsync();
        }

        public async Task<(List<User>, long)> Search(User user, int pageNumber, int pageSize, string sortField, int sortOrder)
        {
            var queryable = base.GetQueryable().Where(entity => !entity.IsDeleted);
            queryable = base.ApplySort(queryable, sortField, sortOrder);

            if (!string.IsNullOrEmpty(user.Username))
            {
                queryable = queryable.Where(m => m.Username.ToLower().Trim() == user.Username.ToLower().Trim());
            }

            if (!string.IsNullOrEmpty(user.Email))
            {
                queryable = queryable.Where(m => m.Email.ToLower().Trim() == user.Email.ToLower().Trim());
            }

            if (!string.IsNullOrEmpty(user.FullName))
            {
                queryable = queryable.Where(m => m.FullName.ToLower().Trim().Contains(user.FullName.ToLower().Trim()));
            }

            if (user.Age > 0)
            {
                queryable = queryable.Where(m => m.Age == user.Age);
            }

            if (!string.IsNullOrEmpty(user.Phone))
            {
                queryable = queryable.Where(m => m.Phone.ToLower().Trim().Contains(user.Phone.ToLower().Trim()));
            }

            if (user.Gender != null)
            {
                queryable = queryable.Where(m => m.Gender == user.Gender);
            }


            var totalOrigin = await queryable.CountAsync();

            queryable = GetQueryablePagination(queryable, pageNumber, pageSize);

            var users = await queryable
                .Include(m => m.HealthMetrics)
                .Include(m => m.UserDietPlans)
                .Include(m => m.Notifications)
                .ToListAsync();

            return (users, totalOrigin);
        }

    }
}
