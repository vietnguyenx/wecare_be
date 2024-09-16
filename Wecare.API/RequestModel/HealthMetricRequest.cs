namespace Wecare.API.RequestModel
{
    public class HealthMetricRequest : BaseRequest
    {
        public Guid UserId { get; set; }
        public DateTime DateRecorded { get; set; }
        public decimal? BloodSugar { get; set; }
        public decimal? UricAcid { get; set; }
        public decimal? Weight { get; set; }
        public string BloodPressure { get; set; }
        public string Note { get; set; }
    }
}
