using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wecare.Repositories.Data.Entities
{
    [Table("MenuDietPlan")]
    public class MenuDietPlan : BaseEntity
    {
        public Guid MenuId { get; set; }
        public Guid DietPlanId { get; set; }

        public virtual Menu Menu { get; set; }
        public virtual DietPlan DietPlan { get; set; }
    }
}
