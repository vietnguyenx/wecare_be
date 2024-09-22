using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wecare.Repositories.Repositories.Repositories.Interface;
using Wecare.Repositories.Repositories.UnitOfWork.Interface;
using WeCare.Repositories.Data;

namespace Wecare.Repositories.Repositories.UnitOfWork.Repository
{
    public class UnitOfWork : BaseUnitOfWork<WeCareDbContext>, IUnitOfWork
    {
        public UnitOfWork(WeCareDbContext context, IServiceProvider serviceProvider) : base(context, serviceProvider)
        {
        }

        public IDietitianRepository DietitianRepository => GetRepository<IDietitianRepository>();

        public IDietPlanRepository DietPlanRepository => GetRepository<IDietPlanRepository>();

        public IDishRepository DishRepository => GetRepository<IDishRepository>();

        public IHealthMetricRepository HealthMetricRepository => GetRepository<IHealthMetricRepository>();

        public IMenuDishRepository MenuDishRepository => GetRepository<IMenuDishRepository>();

        public IMenuRepository MenuRepository => GetRepository<IMenuRepository>();

        public INotificationRepository NotificationRepository => GetRepository<INotificationRepository>();

        public IMenuDietPlanRepository MenuDietPlanRepository => GetRepository<IMenuDietPlanRepository>();

        public IUserRepository UserRepository => GetRepository<IUserRepository>();
    }

}
