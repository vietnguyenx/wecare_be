using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wecare.Repositories.Data.Entities.Enum;

namespace Wecare.Services.Model
{
    public class DietPlanModel : BaseModel
    {
        public DateOnly DateAssigned { get; set; }
        public String Period { get; set; }
        public Status Status { get; set; }

        public virtual IList<MenuDietPlanModel> MenuDietPlans { get; set; }
    }
}
