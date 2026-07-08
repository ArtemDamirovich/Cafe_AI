using Cafe_AI.Core.Entities;


namespace Cafe_AI1.Components.Pages
{
    public partial class DishReview
    {
        private List<Dish> pendingDishes = new(); // ожидающие блюда
        private Dish? selectedDish = null; // выбранное Блюдо
        private Dish? dishToReject = null; // блюдо Отклонить
        private string rejectReason = string.Empty; // Отклонить Причину
        private string? notificationMessage = null; // уведомление Сообщение
        private string notificationClass = "bg-success";
        private int approvedCount = 0; // Одобренные блюда 
        private int rejectedCount = 0; // Отклоненные блюда

        protected override void OnInitialized()
        {
            LoadMockData();
        }

        private void LoadMockData()
        {
            pendingDishes = new List<Dish>
            {
                new Dish
                {
                    Id = Guid.NewGuid(),
                    Name = "Кабачковые оладьи с муссом из козьего сыра",
                    Description = "Нежные оладьи из молодых кабачков с воздушным муссом из козьего сыра и карамелизированным луком. Идеальный летний завтрак.",
                    Recipe = "1. Натереть кабачки на крупной тёрке, посолить и оставить на 10 минут\n2. Отжать лишнюю влагу\n3. Добавить яйцо, муку и специи\n4. Обжарить на оливковом масле до золотистой корочки\n5. Для мусса: взбить козий сыр со сливками\n6. Подавать с карамелизированным луком",
                    Price = 480,
                    CookingTimeMinutes = 25,
                    MealType = "Breakfast",
                    Calories = 320,
                    Ingredients = new List<string> { "Кабачки 300г", "Козий сыр 150г", "Яйцо 2шт", "Мука 100г", "Сливки 50мл", "Лук красный 1шт" },
                    Allergens = new List<string> { "Глютен", "Яйца", "Молочные продукты" },
                    IsAiGenerated = true,
                    Difficulty = "Средняя"
                },
                new Dish
                {
                    Id = Guid.NewGuid(),
                    Name = "Поке с лососем и манго",
                    Description = "Освежающее гавайское блюдо с рисом, свежим лососем, спелым манго и пикантной заправкой.",
                    Recipe = "1. Отварить рис для суши\n2. Нарезать лосось кубиками\n3. Нарезать манго, огурец и авокадо\n4. Смешать соевый соус, кунжутное масло и имбирь\n5. Выложить рис, сверху рыбу и фрукты\n6. Полить заправкой",
                    Price = 620,
                    CookingTimeMinutes = 20,
                    MealType = "Lunch",
                    Calories = 450,
                    Ingredients = new List<string> { "Лосось свежий 200г", "Рис для суши 150г", "Манго 1шт", "Авокадо 1шт", "Огурец 1шт" },
                    Allergens = new List<string> { "Рыба", "Соя", "Кунжут" },
                    IsAiGenerated = true,
                    Difficulty = "Лёгкая"
                },
                new Dish
                {
                    Id = Guid.NewGuid(),
                    Name = "Тартар из тунца с авокадо",
                    Description = "Изысканное блюдо из свежего тунца с кремовым авокадо и соево-имбирной заправкой.",
                    Recipe = "1. Нарезать тунец мелкими кубиками\n2. Авокадо размять с лимонным соком\n3. Смешать соевый соус, имбирь и кунжутное масло\n4. Выложить через кольцо слоями\n5. Украсить кунжутом",
                    Price = 850,
                    CookingTimeMinutes = 20,
                    MealType = "Dinner",
                    Calories = 380,
                    Ingredients = new List<string> { "Тунец свежий 200г", "Авокадо 1шт", "Лимонный сок", "Соевый соус", "Имбирь" },
                    Allergens = new List<string> { "Рыба", "Соя" },
                    IsAiGenerated = true,
                    Difficulty = "Сложная"
                }
            };
        }
        private void ShowDetails(Dish dish)
        {
            selectedDish = null;
        }
        private void CloseDetails()
        {
            selectedDish = null;
        }
        private void ApproveDish(Dish dish)
        {
            pendingDishes.Remove(dish);
            selectedDish = null;
            approvedCount++;
            ShowNotification($"Бдюдо \"{dish.Name}\" одобренно и добавлено в меню", "bg-success");
        }
        private void ConfirmReject() // Confirm - Подтверждать
        {
            if (dishToReject != null)
            {
                var dishName = dishToReject.Name;
                pendingDishes.Remove(dishToReject);

                selectedDish = null;
                dishToReject = null;

                rejectedCount++;

                var reason = string.IsNullOrWhiteSpace(rejectReason) ? "Без указания причины" : rejectReason;

                ShowNotification($"❌ Блюдо \"{dishName}\" отклонено. Причина: {reason}", "bg-danger");
                rejectReason = string.Empty;
            }
        }

        private void ShowNotification(string message, string cssClass)
        {
            notificationMessage = message;
            notificationClass = cssClass;

            Task.Delay(3000).ContinueWith(_ =>
            {
                notificationMessage = null;
                InvokeAsync(StateHasChanged);
            });
        }
        private void ShowRejectModal(Dish dish)
        {
            dishToReject = dish;
            rejectReason = string.Empty;
        }
        private void CloseRejectModal()
        {
            dishToReject = null;
            rejectReason = string.Empty;
        }
        private string GetMealTypeName(string mealType)
        {
            return mealType switch
            {
                "Breakfast" => "Завтрак",
                "Lunch" => "Обед",
                "Dinner" => "Ужин",
                "Dessert" => "Десерт",
                "Drink" => "Напиток",
                _ => mealType
            };
        }
    }
}
