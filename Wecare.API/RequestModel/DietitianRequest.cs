namespace Wecare.API.RequestModel
{
    public class DietitianRequest : BaseRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string? Specialization { get; set; }
        public string? ImageUrl { get; set; }
    }
}
