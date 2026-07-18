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
        [Inject]
        private AiService AiService { get; set; } = default!;


        private DishGenerationRequest request = new();
        private bool isLoading = false; 
        private Dish? generatedDish = null;
        private string? saveMessage = null;

        

        private async Task GenerateDish()
        {
            isLoading = true;
            saveMessage = null;
            generatedDish = null;

            try
            {
                generatedDish = await AiService.GenerateDishAsync(request);
                Console.WriteLine($"Блюдо сгенерировано: {generatedDish?.Name ?? "NULL"}");
            }
            catch(Exception ex)
            {
                Console.WriteLine($"ОШИБКА: {ex.Message}");
            }            
            isLoading = false;
            StateHasChanged();
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
                request.MaxPrice = request.MinPrice;
            }
        }
    }
}
