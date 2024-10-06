using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Wecare.Repositories.Data.Entities.Enum;

namespace Wecare.Repositories.Data.Entities
{
    [Table("User")]
    public class User : BaseEntity
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

        public virtual HealthMetric? HealthMetric { get; set; }  
        public virtual ICollection<DietPlan>? DietPlans { get; set; }
        public virtual ICollection<Notification>? Notifications { get; set; }
    }
}

