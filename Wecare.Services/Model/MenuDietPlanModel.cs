using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wecare.Services.Model
{
    public class MenuDietPlanModel : BaseModel
    {
        public Guid MenuId { get; set; }
        public Guid DietPlanId { get; set; }

        public MenuModel Menu { get; set; }
        public DietPlanModel DietPlan { get; set; }
    }
}
