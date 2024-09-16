using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wecare.Services.Model
{
    public class MenuDishModel : BaseModel
    {
        public Guid MenuId { get; set; }
        public Guid DishId { get; set; }

        public MenuModel Menu { get; set; }
        public DishModel Dish { get; set; }
    }
}
