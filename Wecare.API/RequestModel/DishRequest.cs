namespace Wecare.API.RequestModel
{
    public class DishRequest : BaseRequest
    {
        public string DishName { get; set; }
        public string Description { get; set; }
        public float Calories { get; set; }
        public float Carbohydrates { get; set; }
        public float Protein { get; set; }
        public float Fat { get; set; }          
        public float Fiber { get; set; }         // chat xo
        public float Sugar { get; set; }
        public float Purine { get; set; }        // chi so purine cua gout
        public float Cholesterol { get; set; }
        public string ImageUrl { get; set; }
    }
}
