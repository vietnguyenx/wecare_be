using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wecare.Repositories.Data.Entities.Enum;

namespace Wecare.Repositories.Data.Entities
{
    [Table("HealthMetric")]
    public class HealthMetric : BaseEntity
    {
        public Guid UserId { get; set; }  
        public DateTime DateRecorded { get; set; }
        public decimal? BloodSugar { get; set; }
        public decimal? UricAcid { get; set; }
        public decimal? Weight { get; set; }
        public string BloodPressure { get; set; }
        public string Note { get; set; }

        public virtual User User { get; set; }

        public bool IsHealthMetricSuitable(Menu menu)
        {

            if (menu.SuitableFor == SuitableFor.Diabetes && BloodSugar.HasValue && BloodSugar > 140)
            {
                return false; // not suitable for high blood sugar
            }
            if (menu.SuitableFor == SuitableFor.Gout && UricAcid.HasValue && UricAcid > 7)
            {
                return false; // not suitable for high uric acid
            }
            return true;
        }
    }
}
