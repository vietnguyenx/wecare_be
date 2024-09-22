namespace Wecare.API.SearchModel
{
    public class HealthMetricSearchRequest
    {
        public Guid UserId { get; set; }
        public DateTime DateRecorded { get; set; }
        public decimal? BloodSugar { get; set; }
        public decimal? UricAcid { get; set; }
        public decimal? Weight { get; set; }
        public string BloodPressure { get; set; }
    }
}
