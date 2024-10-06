namespace Wecare.API.RequestModel
{
    public class NotificationRequest : BaseRequest
    {
        public Guid UserId { get; set; }
        public string? Title { get; set; }
        public string? Message { get; set; }
        public bool? IsRead { get; set; }
    }
}
