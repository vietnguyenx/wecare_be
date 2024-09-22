using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wecare.Repositories.Data.Entities;
using Wecare.Repositories.Repositories.Base;

namespace Wecare.Repositories.Repositories.Repositories.Interface
{
    public interface IDishRepository : IBaseRepository<Dish>
    {
        public Task<List<Dish>> SearchDish(string name);
        Task<List<Dish>> GetAllPagination(int pageNumber, int pageSize, string sortField, int sortOrder);
        Task<(List<Dish>, long)> Search(Dish Dish, int pageNumber, int pageSize, string sortField, int sortOrder);
        Task<(List<Dish>, long)> GetAllPaginationByListId(List<Guid> guids, int pageNumber, int pageSize, string sortField, int sortOrder);
        Task<Dish?> GetById(Guid id);
        Task<List<Dish>> GetAllExceptListId(List<Guid> guids);
    }
}
