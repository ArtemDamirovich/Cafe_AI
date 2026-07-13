using Cafe_AI.Core.Entities;
using Cafe_AI.Services;
using Microsoft.AspNetCore.Components;

namespace Cafe_AI1.Components.Pages
{
    public partial class Menu
    {
        [Inject]
        private DishService DishService { get; set; } = default!;

        private string searchText = string.Empty;
        private string selectedMealType = string.Empty;
        private Dish? selectedDish = null;
        private List<Dish> dishes = new();

        protected override async Task OnInitializedAsync()
        {
            dishes = await DishService.GetMenuDishesAsync();
        }

        private List<Dish> filteredDishes
        {
            get
            {
                var result = dishes.AsEnumerable();

                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    result = result.Where(d =>
                        d.Name.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                        d.Description.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                        d.Ingredients.Any(i => i.Contains(searchText, StringComparison.OrdinalIgnoreCase))
                    );
                }

                if (!string.IsNullOrEmpty(selectedMealType))
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
