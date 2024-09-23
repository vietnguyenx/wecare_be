﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wecare.Repositories.Data.Entities;
using WeCare.Repositories.Data;
using Wecare.Repositories.Repositories.Base;
using Wecare.Repositories.Repositories.Repositories.Interface;

namespace Wecare.Repositories.Repositories.Repositories.Repository
{
    public class MenuDietPlanRepository : BaseRepository<MenuDietPlan>, IMenuDietPlanRepository
    {
        private readonly WeCareDbContext _context;

        public MenuDietPlanRepository(WeCareDbContext context) : base(context)
        {
            _context = context;
        }
    }
}