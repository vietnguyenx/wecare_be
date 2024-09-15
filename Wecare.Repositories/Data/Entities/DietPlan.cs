using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wecare.Repositories.Data.Entities.Enum;

namespace Wecare.Repositories.Data.Entities
{
    [Table("DietPlan")]
    public class DietPlan : BaseEntity
    {
        public Guid MenuId { get; set; }
        public Menu Menu { get; set; }
        public DateOnly DateAssigned { get; set; }
        public String Period { get; set; }
        public Status Status { get; set; }
        public virtual ICollection<UserDietPlan> UserDietPlans { get; set; }
    }
}
