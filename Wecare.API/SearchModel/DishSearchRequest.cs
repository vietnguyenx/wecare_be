namespace Wecare.API.SearchModel
{
    public class DishSearchRequest
    {
        public string DishName { get; set; }
        public string Ingredients { get; set; }
        public int Calories { get; set; }
        public decimal Carbs { get; set; }
        public decimal Protein { get; set; }
        public decimal Fat { get; set; }
    }
}
