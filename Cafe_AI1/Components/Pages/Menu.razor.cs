using Cafe_AI.Core.Entities;

namespace Cafe_AI1.Components.Pages
{
    public partial class Menu
    {
        private string searchText = string.Empty; // поиск Текст
        private string selectedMealType = string.Empty; // выбранный тип еды(Фильтр)
        private Dish? selectedDish = null; // Блюдо - модельное окно скрыто

        private List<Dish> dishes = new()
        {
            new Dish
            {
                Id = Guid.NewGuid(),
                Name = "Салат Цезорь с креветками",
                Description = "Классический салат с солеными креветками, листьями романо, пармизаном и домашними гренками",
                Recipe = "1. Обжарить креветки с честнаком. 2) Нарезать листья салата. 3) Смешать с соусом. 4) Дабавить гренки и пармезан",
                Price = 650,
                CookingTimeMinutes = 20,
                MealType = "Lunch",
                Calories = 320,
                Ingredients = new List<string> {"Креветки", "Салат романо", "Пармезан", "Гренки", "Соус Цезарь"},
                Allergens = new List<string> {"Морепродукты", "Молочные продукты", "Глютен" },
                IsAiGenerated = false,
                ImageUrl = null
            },
            new Dish
            {
                Id = Guid.NewGuid(),
                Name = "Паста Карбонара",
                Description = "Традиционная итальянская паста с беконом, яичным желтком и пармезаном",
                Recipe = "1. Отварить спагетти\n2. Обжарить бекон\n3. Смешать желтки с сыром\n4. Соединить всё вместе",
                Price = 580,
                CookingTimeMinutes = 25,
                MealType = "Dinner",
                Calories = 450,
                Ingredients = new List<string> { "Спагетти", "Бекон", "Яичный желток", "Пармезан", "Чёрный перец" },
                Allergens = new List<string> { "Глютен", "Яйца", "Молочные продукты" },
                IsAiGenerated = false,
                ImageUrl = null
            },
            new Dish
            {
                Id = Guid.NewGuid(),
                Name = "Кабачковые оладьи с лососем",
                Description = "Нежные оладьи из молодых кабачков со слабосолёным лососем и сметанным соусом",
                Recipe = "1. Натереть кабачки\n2. Смешать с яйцом и мукой\n3. Обжарить до золотистой корочки\n4. Подавать с лососем и соусом",
                Price = 480,
                CookingTimeMinutes = 20,
                MealType = "Breakfast",
                Calories = 280,
                Ingredients = new List<string> { "Кабачки", "Яйцо", "Мука", "Лосось", "Сметана", "Укроп" },
                Allergens = new List<string> { "Глютен", "Яйца", "Рыба", "Молочные продукты" },
                IsAiGenerated = true,
                ImageUrl = null
            },
            new Dish
            {
                Id = Guid.NewGuid(),
                Name = "Тирамису",
                Description = "Классический итальянский десерт с маскарпоне, кофе и какао",
                Recipe = "1. Заварить крепкий кофе\n2. Взбить маскарпоне с сахаром\n3. Слоями выложить в форму\n4. Охладить 4 часа",
                Price = 420,
                CookingTimeMinutes = 30,
                MealType = "Dessert",
                Calories = 380,
                Ingredients = new List<string> { "Маскарпоне", "Савоярди", "Кофе", "Какао", "Яйца" },
                Allergens = new List<string> { "Молочные продукты", "Яйца", "Глютен" },
                IsAiGenerated = false,
                ImageUrl = null
            },
            new Dish
            {
                Id = Guid.NewGuid(),
                Name = "Домашний лимонад с мятой",
                Description = "Освежающий напиток из свежих лимонов с мятой и тростниковым сахаром",
                Recipe = "1. Выжать сок из лимонов\n2. Добавить сахар и воду\n3. Размять мяту\n4. Подавать со льдом",
                Price = 250,
                CookingTimeMinutes = 10,
                MealType = "Drink",
                Calories = 120,
                Ingredients = new List<string> { "Лимоны", "Мята", "Тростниковый сахар", "Лёд" },
                Allergens = new List<string>(),
                IsAiGenerated = true,
                ImageUrl = null
            }
        };
        private List<Dish> filteredDishes
        {
            get
            {
                var result = dishes.AsEnumerable();
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    result = result.Where(d => d.Name.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                    d.Description.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                    d.Ingredients.Any(i => i.Contains(searchText, StringComparison.OrdinalIgnoreCase))
                    );
                }
                if (string.IsNullOrEmpty(selectedMealType))
                {
                    result = result.Where(d => d.MealType == selectedMealType);
                }
                return result.ToList();
            }
        }
        private void ShowDishDetail(Dish dish)
        {
            selectedDish = dish;
        }
        private void CloseModal()
        {
            selectedDish = null;
        }
    }
}
