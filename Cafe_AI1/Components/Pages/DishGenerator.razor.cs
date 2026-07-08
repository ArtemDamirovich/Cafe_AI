using Cafe_AI.Core.Entities;
using Cafe_AI.Core.Models;

namespace Cafe_AI1.Components.Pages
{
    public partial class DishGenerator
    {
        private DishGenerationRequest request = new();
        private bool isLoading = false; // Загрузка
        private Dish? generatedDish = null;
        private string? saveMessage = null;

        private Dish GenarateMockDish()
        {
            var mockDishes = new List<Dish>
            {
                new Dish
                {
                    Name = "Кабачковые оладьи с муссом из козьего сыра",
                    Description = "Нежные оладьи из молодых кабачков с воздушным муссом из козьего сыра и карамелизированным луком. Идеальный летний завтрак.",
                    Recipe = "1. Натереть кабачки на крупной тёрке, посолить и оставить на 10 минут\n2. Отжать лишнюю влагу\n3. Добавить яйцо, муку и специи\n4. Обжарить на оливковом масле до золотистой корочки\n5. Для мусса: взбить козий сыр со сливками\n6. Подавать с карамелизированным луком",
                    Price = 480,
                    CookingTimeMinutes = 25,
                    MealType = request.MealType,
                    Calories = 320,
                    Ingredients = new List<string> { "Кабачки 300г", "Козий сыр 150г", "Яйцо 2шт", "Мука 100г", "Сливки 50мл", "Лук красный 1шт", "Масло оливковое", "Соль", "Перец" },
                    Allergens = new List<string> { "Глютен", "Яйца", "Молочные продукты" },
                    IsAiGenerated = true,
                    Difficulty = "Средняя"
                },
                new Dish
                {
                    Name = "Поке с лососем и манго",
                    Description = "Освежающее гавайское блюдо с рисом, свежим лососем, спелым манго и пикантной заправкой.",
                    Recipe = "1. Отварить рис для суши\n2. Нарезать лосось кубиками\n3. Нарезать манго, огурец и авокадо\n4. Смешать соевый соус, кунжутное масло и имбирь для заправки\n5. Выложить рис, сверху рыбу и фрукты\n6. Полить заправкой, посыпать кунжутом",
                    Price = 620,
                    CookingTimeMinutes = 20,
                    MealType = request.MealType,
                    Calories = 450,
                    Ingredients = new List<string> { "Лосось свежий 200г", "Рис для суши 150г", "Манго 1шт", "Авокадо 1шт", "Огурец 1шт", "Соевый соус", "Кунжутное масло", "Имбирь", "Кунжут" },
                    Allergens = new List<string> { "Рыба", "Соя", "Кунжут" },
                    IsAiGenerated = true,
                    Difficulty = "Лёгкая"
                },
                new Dish
                {
                    Name = "Суп-пюре из запечённой тыквы",
                    Description = "Бархатистый суп с карамельным вкусом запечённой тыквы, имбирём и кокосовым молоком.",
                    Recipe = "1. Запечь тыкву с чесноком и тимьяном\n2. Обжарить лук и имбирь\n3. Добавить запечённую тыкву и бульон\n4. Варить 15 минут\n5. Пюрировать до однородности\n6. Добавить кокосовое молоко, прогреть\n7. Подавать с тыквенными семечками",
                    Price = 420,
                    CookingTimeMinutes = 35,
                    MealType = request.MealType,
                    Calories = 280,
                    Ingredients = new List<string> { "Тыква 500г", "Кокосовое молоко 200мл", "Лук 1шт", "Чеснок 3 зубчика", "Имбирь 20г", "Тимьян", "Бульон овощной 500мл", "Тыквенные семечки" },
                    Allergens = new List<string>(),
                    IsAiGenerated = true,
                    Difficulty = "Средняя"
                }
            };
            var random = new Random();
            return mockDishes[random.Next(mockDishes.Count)];
        }

        private async Task GenerateDish()
        {
            isLoading = true;
            saveMessage = null;
            generatedDish = null;

            await Task.Delay(2000);

            generatedDish = GenarateMockDish();

            isLoading = false;
        }
        private void SaveDish()
        {
            if (generatedDish != null)
            {
                saveMessage = $"Блюдо \"{generatedDish.Name}\"" +
                    $" отправленно на модерацию";
            }
        }
        private void ValidatePriceRange()
        {
            if (request.MinPrice > request.MaxPrice)
            {
                request.MinPrice = request.MaxPrice;
            }
        }
    }
}
