using Cafe_AI.Core.Models;

namespace Cafe_AI.Services.Prompts
{
    public class DishPromptBuilder
    {
        public static string Build(DishGenerationRequest request)
        {
            var mealType = request.MealType switch
            {
                "Breakfast" => "завтрак",
                "Lunch" => "обед",
                "Dinner" => "ужин",
                "Dessert" => "десерт",
                "Drink" => "напиток",
                _ => "блюдо"
            };

            var cuisine = string.IsNullOrWhiteSpace(request.Cuisine) ? "авторская" : request.Cuisine;
            var special = string.IsNullOrWhiteSpace(request.SpecialRequirements) ? "нет" : request.SpecialRequirements;

            return $"Ты — креативный шеф-повар кафе \"Свои\". " +
                $"Придумай уникальное {mealType}. " +
                $"Параметры блюда: - Кухня: {cuisine} - Сезон:" +
                $" {request.Season} - Цена: от {request.MinPrice} до {request.MaxPrice} рублей - " +
                $"Особые требования: {special}." +
                $" Ответь ТОЛЬКО валидным JSON, без markdown и пояснений:" +
                $" {{ \"name\": \"Название блюда\", \"description\":" +
                $" \"Описание\", \"recipe\": \"Рецепт\"," +
                $" \"ingredients\": [\"1\", \"2\"], \"allergens\": [\"1\"]," +
                $" \"calories\": 350, \"weight\": 400, \"price\": 500, \"cookingTimeMinutes\": 30," +
                $" \"difficulty\": \"Средняя\" }}";
        }
    }
}
