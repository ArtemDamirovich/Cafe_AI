using Cafe_AI.Core.Entities;
using Cafe_AI.Core.Models;
using Cafe_AI.Services;
using Microsoft.AspNetCore.Components;

namespace Cafe_AI1.Components.Pages
{
    public partial class DishGenerator
    {
        [Inject]
        private DishService DishService { get; set; } = default!;
        private DishGenerationRequest request = new();
        private bool isLoading = false; 
        private Dish? generatedDish = null;
        private string? saveMessage = null;

        [Inject]
        private AiService AiService { get; set; } = default!;

        private async Task GenerateDish()
        {
            isLoading = true;
            saveMessage = null;
            generatedDish = null;

            await Task.Delay(2000);

            generatedDish = await AiService.GenerateDishAsync(request);

            isLoading = false;
        }
        private async Task SaveDish()
        {
            if (generatedDish != null)
            {
                await DishService.CreateDishAsync(generatedDish);
                saveMessage = $"Блюдо \"{generatedDish.Name}\" отправленно на модерацию";
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
