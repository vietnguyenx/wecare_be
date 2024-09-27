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
        public Guid? DietitianId { get; set; }
        public string? MenuName { get; set; }
        public string? Description { get; set; }
        public SuitableFor? SuitableFor { get; set; }
        public string? ImageUrl { get; set; }
        public Status? Status { get; set; }
        public bool IsActive { get; set; }

        public virtual Dietitian? Dietitian { get; set; }
        public virtual ICollection<MenuDish>? MenuDishes { get; set; }
        public virtual ICollection<MenuDietPlan>? MenuDietPlans { get; set; }

        public float TotalCalories { get; set; }
        public float TotalCarbohydrates { get; set; }
        public float TotalProtein { get; set; }
        public float TotalFat { get; set; }
        public float TotalFiber { get; set; }
        public float TotalSugar { get; set; }
        public float TotalPurine { get; set; }
        public float TotalCholesterol { get; set; }

    }
}
