using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wecare.Repositories.Data.Entities
{
    [Table("Dish")]
    public class Dish : BaseEntity
    {
        public string DishName { get; set; }
        public string Ingredients { get; set; }
        public int Calories { get; set; }
        public decimal Carbs { get; set; }
        public decimal Protein { get; set; }
        public decimal Fat { get; set; }
        public string ImageUrl { get; set; }

        public virtual ICollection<MenuDish> MenuDishes { get; set; }
    }
}
