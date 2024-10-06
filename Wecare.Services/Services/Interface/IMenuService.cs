using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wecare.Services.Model;

namespace Wecare.Services.Services.Interface
{
    public interface IMenuService
    {
        Task<bool> Add(MenuModel MenuModel);
        Task<bool> Delete(Guid id);
        Task<List<MenuModel>> GetAll();
        Task<MenuModel?> GetById(Guid id);
        Task<bool> Update(MenuModel PackageModel);

        public Task<List<MenuModel>?> GetAllPagination(int pageNumber, int pageSize, string sortField, int sortOrder);

        public Task<(List<MenuModel>?, long)> Search(MenuModel menuModel, int pageNumber, int pageSize, string sortField, int sortOrder);

        public Task<long> GetTotalCount();
        Task<List<MenuModel>?> GetAllMenuByDietitianId(Guid idDietitian);
        Task<List<MenuModel>> GetLatestMenus();
        Task<List<MenuModel>?> GetMenusByHealthMetrics(List<HealthMetricModel> healthMetrics);
    }
}
    