using Cafe_AI.Services;
using Microsoft.AspNetCore.Components;

namespace Cafe_AI1.Components.Pages
{
    public partial class Home
    {
        [Inject]
        private DishService DishService { get; set; } = default!;

        private int menuCount = 0;
        private int pendingCount = 0;

        protected override async Task OnInitializedAsync()
        {
            var menuDishes = await DishService.GetMenuDishesAsync();
            menuCount = menuDishes.Count;

            var pendingDishes = await DishService.GetPendingDishesAsyng();
            pendingCount = pendingDishes.Count;
        }
    }
}
