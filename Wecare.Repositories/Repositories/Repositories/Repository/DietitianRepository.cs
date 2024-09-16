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
    public class DietitianRepository : BaseRepository<Dietitian>, IDietitianRepository
    {
        private readonly WeCareDbContext _context;

        public DietitianRepository(WeCareDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
