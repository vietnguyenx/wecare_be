using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wecare.Repositories.Data.Entities;
using Wecare.Repositories.Repositories.Base;

namespace Wecare.Repositories.Repositories.Repositories.Interface
{
    public interface IDietitianRepository : IBaseRepository<Dietitian>
    {
        Task<List<Dietitian>> GetAll();
        Task<List<Dietitian>> GetAllPagination(int pageNumber, int pageSize, string sortField, int sortOrder);
        Task<(List<Dietitian>, long)> Search(Dietitian dietitian, int pageNumber, int pageSize, string sortField, int sortOrder);
        Task<Dietitian> GetById(Guid id);
    }
}
