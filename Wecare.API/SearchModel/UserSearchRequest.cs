using Wecare.Repositories.Data.Entities.Enum;

namespace Wecare.API.SearchModel
{
    public class UserSearchRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }
        public Gender Gender { get; set; }
    }
}
