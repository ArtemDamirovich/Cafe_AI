using Cafe_AI.Core.Entities;
using Cafe_AI.Core.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe_AI.Services
{
    public class AiService
    {
        private readonly IConfiguration _configuration;

        public AiService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<Dish> GenerateDishAsync(DishGenerationRequest request)
        {
            await Task.Delay(new Random().Next(2000, 3000));
            return GenerateMockDish(request);
        }

        private Dish GenerateMockDish(DishGenerationRequest request)
        {
            var random = new Random();
            var dishes = new List<Dish>
        {
            new Dish
            {
                Name = "Сырники с ягодным соусом",
                Description = "Нежные творожные сырники с соусом из лесных ягод и сметаной.",
                Recipe = "1. Смешать творог с яйцом и мукой\n2. Сформировать сырники\n3. Обжарить до золотистой корочки\n4. Ягоды протереть с сахаром\n5. Подавать со сметаной",
                Price = random.Next(request.MinPrice, request.MaxPrice),
                CookingTimeMinutes = 25,
                MealType = request.MealType,
                Calories = 350,
                Ingredients = new List<string> { "Творог 300г", "Яйцо 1шт", "Мука 50г", "Ягоды 150г", "Сахар", "Сметана" },
                Allergens = new List<string> { "Молочные продукты", "Глютен", "Яйца" },
                IsAiGenerated = true,
                Difficulty = "Средняя"
            },
            new Dish
            {
                Name = "Крем-суп из шампиньонов",
                Description = "Бархатистый суп из свежих шампиньонов со сливками и гренками.",
                Recipe = "1. Обжарить лук и грибы\n2. Добавить бульон, варить 15 минут\n3. Пюрировать\n4. Добавить сливки\n5. Подавать с гренками",
                Price = random.Next(request.MinPrice, request.MaxPrice),
                CookingTimeMinutes = 30,
                MealType = request.MealType,
                Calories = 220,
                Ingredients = new List<string> { "Шампиньоны 400г", "Лук 1шт", "Сливки 100мл", "Бульон 500мл", "Гренки" },
                Allergens = new List<string> { "Молочные продукты", "Глютен" },
                IsAiGenerated = true,
                Difficulty = "Лёгкая"
            }
        };

            return dishes[random.Next(dishes.Count)];
        }
    }
}
