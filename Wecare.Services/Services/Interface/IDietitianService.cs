using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wecare.Services.Model;

namespace Wecare.Services.Services.Interface
{
    public interface IDietitianService
    {
        public Task<bool> Add(DietitianModel model);
        public Task<bool> Delete(DietitianModel model);
        public Task<bool> Update(DietitianModel model);
        public Task<List<DietitianModel>> GetAll();
        public Task<List<DietitianModel>> GetAllPagination(int pageNumber, int pageSize, string sortField, int sortOrder);
        public Task<DietitianModel> GetById(Guid id);
        public Task<(List<DietitianModel>?, long)> Search(DietitianModel dietitianModel, int pageNumber, int pageSize, string sortField, int sortOrder);
        public Task<long> GetTotalCount();
    }
}
