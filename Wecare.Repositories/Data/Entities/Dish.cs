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
        public string? Description { get; set; }
        public float? Calories { get; set; }    
        public float? Carbohydrates { get; set; } 
        public float? Protein { get; set; }     
        public float? Fat { get; set; }         
        public float? Fiber { get; set; }         // chat xo
        public float? Sugar { get; set; }        
        public float? Purine { get; set; }        // chi so purine cua gout
        public float? Cholesterol { get; set; }
        public string? ImageUrl { get; set; }

        public virtual ICollection<MenuDish> MenuDishes { get; set; }
    }
}
