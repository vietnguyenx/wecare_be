using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wecare.Repositories.Data.Entities.Enum;

namespace Wecare.Repositories.Data.Entities
{
    [Table("Menu")]
    public class Menu : BaseEntity
    {
        public Guid DietitianId { get; set; }
        public string MenuName { get; set; }
        public string Description { get; set; }
        public SuitableFor SuitableFor { get; set; }
        public string ImageUrl { get; set; }

        public Dietitian Dietitian { get; set; }
        public virtual ICollection<MenuDish> MenuDishes { get; set; }
        public virtual ICollection<DietPlan> DietPlans { get; set; }
    }
}
