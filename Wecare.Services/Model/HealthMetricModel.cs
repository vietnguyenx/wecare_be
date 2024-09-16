using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wecare.Services.Model
{
    public class HealthMetricModel : BaseModel
    {
        public Guid UserId { get; set; }
        public DateTime DateRecorded { get; set; }
        public decimal? BloodSugar { get; set; }
        public decimal? UricAcid { get; set; }
        public decimal? Weight { get; set; }
        public string BloodPressure { get; set; }
        public string Note { get; set; }

        public UserModel User { get; set; }
    }
}
