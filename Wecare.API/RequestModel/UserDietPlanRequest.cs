namespace Wecare.API.RequestModel
{
    public class UserDietPlanRequest : BaseRequest
    {
        public Guid UserId { get; set; }
        public Guid DietPlanId { get; set; }
    }
}
