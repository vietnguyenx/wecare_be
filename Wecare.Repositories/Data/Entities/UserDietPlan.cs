using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wecare.Repositories.Data.Entities
{
    [Table("UserDietPlan")]
    public class UserDietPlan : BaseEntity
    {
        public Guid UserId { get; set; }
        public Guid DietPlanId { get; set; }

        public User User { get; set; }
        public DietPlan DietPlan { get; set; }
    }
}
