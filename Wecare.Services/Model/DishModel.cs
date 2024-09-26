using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wecare.Services.Model
{
    public class DishModel : BaseModel
    {
        public string DishName { get; set; }
        public string Description { get; set; }
        public float Calories { get; set; }
        public float Carbohydrates { get; set; }
        public float Protein { get; set; }
        public float Fat { get; set; }
        public float Fiber { get; set; }
        public float Sugar { get; set; }
        public float Purine { get; set; }
        public float Cholesterol { get; set; }
        public string ImageUrl { get; set; }

        public virtual IList<MenuDishModel> MenuDishes { get; set; }
    }
}
