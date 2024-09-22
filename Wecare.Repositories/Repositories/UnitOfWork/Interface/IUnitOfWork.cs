using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wecare.Repositories.Repositories.Repositories.Interface;

namespace Wecare.Repositories.Repositories.UnitOfWork.Interface
{
    public interface IUnitOfWork : IBaseUnitOfWork
    {
        IDietitianRepository DietitianRepository { get; }
        IDietPlanRepository DietPlanRepository { get; }
        IDishRepository DishRepository { get; }
        IHealthMetricRepository HealthMetricRepository { get; }
        IMenuDishRepository MenuDishRepository { get; }
        IMenuRepository MenuRepository { get; }
        INotificationRepository NotificationRepository { get; }
        IMenuDietPlanRepository MenuDietPlanRepository { get; }
        IUserRepository UserRepository { get; }
    }
}
