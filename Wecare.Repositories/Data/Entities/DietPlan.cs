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
        public Guid UserId { get; set; }
        public DateOnly DateAssigned { get; set; }
        public String Period { get; set; }
        public Status Status { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<MenuDietPlan> MenuDietPlans { get; set; }
    }
}
