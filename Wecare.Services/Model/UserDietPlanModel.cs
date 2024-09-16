using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wecare.Services.Model
{
    public class UserDietPlanModel : BaseModel
    {
        public Guid UserId { get; set; }
        public Guid DietPlanId { get; set; }

        public UserModel User { get; set; }
        public DietPlanModel DietPlan { get; set; }
    }
}
