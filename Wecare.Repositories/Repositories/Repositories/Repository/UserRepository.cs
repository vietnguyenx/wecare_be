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

        public Task<User> FindUsernameOrEmail(User user)
        {
            throw new NotImplementedException();
        }

        public Task<List<User>> GetAllPagination(int pageNumber, int pageSize, string sortField, int sortOrder)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserByEmail(User user)
        {
            throw new NotImplementedException();
        }

        public Task<(List<User>, long)> Search(User user, int pageNumber, int pageSize, string sortField, int sortOrder)
        {
            throw new NotImplementedException();
        }
    }
}
