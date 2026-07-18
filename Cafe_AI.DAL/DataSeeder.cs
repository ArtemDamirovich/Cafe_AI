using Cafe_AI.Core.Entities;

namespace Cafe_AI.DAL;

public static class DataSeeder
{
    public static void Seed(AppDbContext context)
    {
        if (context.Dishes.Any())
            return; // Данные уже есть

        var dishes = new List<Dish>
        {
            new Dish
            {
                Id = Guid.NewGuid(),
                Name = "Салат Цезарь с креветками",
                Description = "Классический салат с сочными креветками, листьями романо, пармезаном и домашними гренками",
                Recipe = "1. Обжарить креветки\n2. Нарвать салат\n3. Добавить соус\n4. Украсить пармезаном",
                Price = 650,
                CookingTimeMinutes = 20,
                MealType = "Lunch",
                Calories = 320,
                Ingredients = new List<string> { "Креветки", "Салат романо", "Пармезан", "Гренки", "Соус Цезарь" },
                Allergens = new List<string> { "Морепродукты", "Молочные продукты", "Глютен" },
                IsAiGenerated = false,
                IsApproved = true,
                Difficulty = "Средняя",
                CreatedAt = DateTime.UtcNow
            },
            new Dish
            {
                Id = Guid.NewGuid(),
                Name = "Паста Карбонара",
                Description = "Традиционная итальянская паста с беконом, яичным желтком и пармезаном",
                Recipe = "1. Отварить спагетти\n2. Обжарить бекон\n3. Смешать желтки с сыром\n4. Соединить всё",
                Price = 580,
                CookingTimeMinutes = 25,
                MealType = "Dinner",
                Calories = 450,
                Ingredients = new List<string> { "Спагетти", "Бекон", "Яичный желток", "Пармезан", "Чёрный перец" },
                Allergens = new List<string> { "Глютен", "Яйца", "Молочные продукты" },
                IsAiGenerated = false,
                IsApproved = true,
                Difficulty = "Средняя",
                CreatedAt = DateTime.UtcNow
            },
            new Dish
            {
                Id = Guid.NewGuid(),
                Name = "Кабачковые оладьи с лососем",
                Description = "Нежные оладьи из кабачков со слабосолёным лососем и сметанным соусом. Создано ИИ.",
                Recipe = "1. Натереть кабачки\n2. Отжать влагу\n3. Добавить яйцо и муку\n4. Обжарить\n5. Подавать с лососем",
                Price = 480,
                CookingTimeMinutes = 20,
                MealType = "Breakfast",
                Calories = 280,
                Ingredients = new List<string> { "Кабачки", "Яйцо", "Мука", "Лосось", "Сметана", "Укроп" },
                Allergens = new List<string> { "Глютен", "Яйца", "Рыба", "Молочные продукты" },
                IsAiGenerated = true,
                IsApproved = false,
                Difficulty = "Лёгкая",
                CreatedAt = DateTime.UtcNow
            }
        };

        context.Dishes.AddRange(dishes);
        context.SaveChanges();
    }
}