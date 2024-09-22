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
        public string Ingredients { get; set; }
        public int Calories { get; set; }
        public decimal Carbs { get; set; }
        public decimal Protein { get; set; }
        public decimal Fat { get; set; }
        public string ImageUrl { get; set; }

        public virtual IList<MenuDishModel> MenuDishes { get; set; }
    }
}
