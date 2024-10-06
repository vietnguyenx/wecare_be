﻿using System;
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
        Task<User> FindUsernameOrEmail(User user);
        Task<List<User>> GetAllPagination(int pageNumber, int pageSize, string sortField, int sortOrder);
        Task<User> GetById(Guid id);
        Task<User> GetUserByEmail(User user);
        Task<(List<User>, long)> Search(User user, int pageNumber, int pageSize, string sortField, int sortOrder);
    }
}
