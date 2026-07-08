namespace Cafe_AI.Core.Models
{
    public class DishGenerationRequest
    {
        public string MealType { get; set; } = "Lunch";
        public string Cuisine { get; set; } = string.Empty;
        public string Season { get; set; } = "all";
        public int MinPrice { get; set; } = 350;
        public int MaxPrice { get; set; } = 3000;
        public string SpecialRequirements { get; set; } = string.Empty; // Особые требования
    }
}
