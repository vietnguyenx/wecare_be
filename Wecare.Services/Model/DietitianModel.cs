using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wecare.Services.Model
{
    public class DietitianModel : BaseModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; } 
        public string Specialization { get; set; }
        public string ImageUrl { get; set; }

        public virtual IList<MenuModel> Menus { get; set; }
    }
}
