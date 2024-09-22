using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wecare.Repositories.Data.Entities;
using Wecare.Repositories.Data.Entities.Enum;

namespace Wecare.Services.Model
{
    public class UserModel : BaseModel
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string FullName { get; set; }
        public string? ImageUrl { get; set; }
        public string Email { get; set; }
        public DateTime? DOB { get; set; }
        public string? Address { get; set; }
        public Gender? Gender { get; set; }
        public string? Phone { get; set; }

        public Guid? HealthMetricId { get; set; }

        public Guid? DietPlanId { get; set; }

        public HealthMetricModel? HealthMetric { get; set; }
        public DietPlanModel? DietPlan { get; set; }

        public virtual IList<NotificationModel>? Notifications { get; set; }
    }
}
