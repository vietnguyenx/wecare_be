namespace Wecare.API.RequestModel
{
    public class MenuDietPlanRequest : BaseRequest
    {
        public Guid MenuId { get; set; }
        public Guid DietPlanId { get; set; }
    }
}
