namespace Cafe_AI.Core.Entities
{
    public class Dish
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Recipe { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int CookingTimeMinutes { get; set; }
        public string MealType { get; set; } = string.Empty;
        public int Calories { get; set; }
        public int Weight { get; set; }
        public List<string> Ingredients { get; set; } = new();
        public List<string> Allergens { get; set; } = new();
        public bool IsAiGenerated { get; set; }
        public string? ImageUrl { get; set; }
        public string Difficulty { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsApproved { get; set; } = false;
        public bool IsRejected { get; set; } = false;
    }
}
