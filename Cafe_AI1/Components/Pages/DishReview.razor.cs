using Cafe_AI.Core.Entities;
using Cafe_AI.Services;
using Microsoft.AspNetCore.Components;


namespace Cafe_AI1.Components.Pages
{
    public partial class DishReview
    {
        [Inject]
        private DishService DishService { get; set; } = default!;

        private List<Dish> pendingDishes = new(); // ожидающие блюда
        private Dish? selectedDish = null; // выбранное Блюдо
        private Dish? dishToReject = null; // блюдо Отклонить
        private string rejectReason = string.Empty; // Отклонить Причину
        private string? notificationMessage = null; // уведомление Сообщение
        private string notificationClass = "bg-success";
        private int approvedCount = 0; // Одобренные блюда 
        private int rejectedCount = 0; // Отклоненные блюда

        protected override async Task OnInitializedAsync()
        {
            pendingDishes = await DishService.GetPendingDishesAsyng();
        }
        private void ShowDetails(Dish dish)
        {
            selectedDish = null;
        }
        private void CloseDetails()
        {
            selectedDish = null;
        }
        private async Task ApproveDish(Dish dish)
        {
            await DishService.ApproveDishAsync(dish.Id);
            pendingDishes.Remove(dish);
            selectedDish = null;
            approvedCount++;
            ShowNotification($"Бдюдо \"{dish.Name}\" одобренно и добавлено в меню", "bg-success");
        }
        private async Task ConfirmReject() // Confirm - Подтверждать
        {
            if (dishToReject != null)
            {
                var dishName = dishToReject.Name;
                await DishService.ApproveDishAsync(dishToReject.Id);
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
