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
        public DiseaseType? DiseaseType { get; set; }
        public UserType? UserType { get; set; }
        public UserRole? UserRole { get; set; }

        public HealthMetricModel? HealthMetric { get; set; }
        public IList<DietPlanModel>? DietPlans { get; set; }

        public IList<NotificationModel>? Notifications { get; set; }
    }
}
