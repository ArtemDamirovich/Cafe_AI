using Cafe_AI.Core.Entities;
using Cafe_AI.Core.Models;
using Cafe_AI.Services.Prompts;
using OllamaSharp;
using System.Text;
using System.Text.Json;


namespace Cafe_AI.Services
{
    public class AiService
    {
        private readonly OllamaApiClient _client;

        public AiService()
        {
            _client = new OllamaApiClient("http://localhost:11434", "gemma2:9b"); ;
        }

        public async Task<Dish> GenerateDishAsync(DishGenerationRequest request)
        {
            var prompt = DishPromptBuilder.Build(request);

            var chatRequest = new OllamaSharp.Models.Chat.ChatRequest
            {
                Model = "gemma2:9b",
                Stream = false,
                Messages = new[]
                {
                new OllamaSharp.Models.Chat.Message
                {
                    Role = "user",
                    Content = prompt
                }
            }
            };

            // Получаем IAsyncEnumerable и собираем все ответы
            var responseStream = _client.ChatAsync(chatRequest);

            var fullResponse = new StringBuilder();
            await foreach (var response in responseStream)
            {
                if (response?.Message?.Content != null)
                    fullResponse.Append(response.Message.Content);
            }

            var text = fullResponse.ToString()
                .Replace("```json", "")
                .Replace("```", "")
                .Trim();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var dish = JsonSerializer.Deserialize<AiDish>(text, options)
                ?? throw new Exception($"Не удалось разобрать ответ ИИ: {text}");

            var price = Math.Clamp(dish.Price, request.MinPrice, request.MaxPrice);
            var cookingTime = Math.Clamp(dish.CookingTimeMinutes, 10, 30);

            var result = new Dish
            {
                Name = dish.Name,
                Description = dish.Description,
                Recipe = dish.Recipe,
                Price = price,
                CookingTimeMinutes = cookingTime,
                MealType = request.MealType,
                Calories = dish.Calories,
                Weight = dish.Weight,
                Ingredients = dish.Ingredients,
                Allergens = dish.Allergens ?? new List<string>(),
                Difficulty = dish.Difficulty ?? "Средняя",
                IsAiGenerated = true
            };
            return result;
        }
    }
}
