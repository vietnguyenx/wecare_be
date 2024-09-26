using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wecare.Repositories.Data.Entities.Enum;

namespace Wecare.Services.Model
{
    public class MenuModel : BaseModel
    {
        public Guid? DietitianId { get; set; }
        public string? MenuName { get; set; }
        public string? Description { get; set; }
        public SuitableFor? SuitableFor { get; set; }
        public string? ImageUrl { get; set; }
        public Status? Status { get; set; }
        public bool? IsActive { get; set; }

        public float TotalCalories { get; set; }
        public float TotalCarbohydrates { get; set; }
        public float TotalProtein { get; set; }
        public float TotalFat { get; set; }
        public float TotalFiber { get; set; }
        public float TotalSugar { get; set; }
        public float TotalPurine { get; set; }
        public float TotalCholesterol { get; set; }

        public DietitianModel Dietitian { get; set; }
        public IList<MenuDishModel> MenuDishes { get; set; }
        public IList<MenuDietPlanModel> MenuDietPlans { get; set; }
    }
}
