using Wecare.Repositories.Data.Entities.Enum;

namespace Wecare.API.RequestModel
{
    public class UserRequest : BaseRequest
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? FullName { get; set; }
        public string? ImageUrl { get; set; }
        public string? Email { get; set; }
        public DateTime? DOB { get; set; }
        public string? Address { get; set; }
        public Gender? Gender { get; set; }
        public string? Phone { get; set; }
        
    }
}
