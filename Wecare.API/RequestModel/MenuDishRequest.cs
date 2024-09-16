namespace Wecare.API.RequestModel
{
    public class MenuDishRequest : BaseRequest
    {
        public Guid MenuId { get; set; }
        public Guid DishId { get; set; }

    }
}
