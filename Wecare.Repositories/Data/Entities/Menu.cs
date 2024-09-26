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

        public void CalculateTotalNutrition()
        {
            TotalCalories = MenuDishes?.Sum(md => md.Dish.Calories) ?? 0;
            TotalCarbohydrates = MenuDishes?.Sum(md => md.Dish.Carbohydrates) ?? 0;
            TotalProtein = MenuDishes?.Sum(md => md.Dish.Protein) ?? 0;
            TotalFat = MenuDishes?.Sum(md => md.Dish.Fat) ?? 0;
            TotalFiber = MenuDishes?.Sum(md => md.Dish.Fiber) ?? 0;
            TotalSugar = MenuDishes?.Sum(md => md.Dish.Sugar) ?? 0;
            TotalPurine = MenuDishes?.Sum(md => md.Dish.Purine) ?? 0;
            TotalCholesterol = MenuDishes?.Sum(md => md.Dish.Cholesterol) ?? 0;
        }
    }
}
