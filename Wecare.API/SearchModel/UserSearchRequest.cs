using Wecare.Repositories.Data.Entities.Enum;

namespace Wecare.API.SearchModel
{
    public class UserSearchRequest
    {
        public string? Username { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public Gender? Gender { get; set; }
        public string? Phone { get; set; }
        public DiseaseType? DiseaseType { get; set; }
        public UserType? UserType { get; set; }

    }
}
