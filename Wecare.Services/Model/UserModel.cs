using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wecare.Repositories.Data.Entities.Enum;

namespace Wecare.Services.Model
{
    public class UserModel : BaseModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }
        public Gender Gender { get; set; }
        public string ImageUrl { get; set; }

        public virtual IList<HealthMetricModel> HealthMetrics { get; set; }
        public virtual IList<UserDietPlanModel> UserDietPlans { get; set; }
        public virtual IList<NotificationModel> Notifications { get; set; }
    }
}
