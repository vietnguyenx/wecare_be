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
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }
        public Gender Gender { get; set; }
        public string ImageUrl { get; set; }

        public virtual ICollection<HealthMetric> HealthMetrics { get; set; }
        public virtual ICollection<UserDietPlan> UserDietPlans { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
    }
}
