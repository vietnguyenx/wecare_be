using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wecare.Services.Model;

namespace Wecare.Services.Services.Interface
{
    public interface IDishService
    {
        Task<bool> Add(DishModel DishModel);
        Task<bool> Delete(Guid id);
        Task<List<DishModel>> GetAll();
        Task<DishModel?> GetById(Guid id);
        Task<bool> Update(DishModel DishModel);
        public Task<List<DishModel>?> GetAllPagination(int pageNumber, int pageSize, string sortField, int sortOrder);

        public Task<(List<DishModel>?, long)> Search(DishModel dishModel, int pageNumber, int pageSize, string sortField, int sortOrder);
        public Task<(List<DishModel>?, long)> GetAllPaginatiomByListId(List<Guid> guids, int pageNumber, int pageSize, string sortField, int sortOrder);

        public Task<long> GetTotalCount();
        Task<List<DishModel>> GetAllByMenuId(Guid menuId);
        Task<(List<DishModel>?, long)> GetAllPaginationByMenuId(Guid menuId, int pageNumber, int pageSize, string sortField, int sortOrder);
        Task<List<DishModel>?> GetAllExceptListId(List<Guid> guids);
    }
}
