using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wecare.Repositories.Data.Entities
{
    [Table("MenuDish")]
    public class MenuDish : BaseEntity
    {
        public Guid MenuId { get; set; }
        public Guid DishId { get; set; }

        public virtual Menu Menu { get; set; }
        public virtual Dish Dish { get; set; }
    }
}
