using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wecare.Repositories.Data.Entities;
using Wecare.Repositories.Repositories.Base;

namespace Wecare.Repositories.Repositories.Repositories.Interface
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> GetById(Guid id);
        Task<User> GetUserByEmail(string email);
        Task<User> FindUsernameOrEmail(string username, string email);
        Task<List<User>> GetAllPagination(int pageNumber, int pageSize, string sortField, int sortOrder);
        Task<(List<User>, long)> Search(User searchCriteria, int pageNumber, int pageSize, string sortField, int sortOrder);
    }
}
