using Cafe_AI.Core.Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

namespace Cafe_AI1.Components.Pages
{
    public partial class Chat : IAsyncDisposable
    {
        [Inject]
        private NavigationManager Navigation { get; set; } = default!;

        private HubConnection? hubConnection;
        private List<ChatMessage> messages = new();        
        private string newMessage = string.Empty;
        private string userName = "Анна";
        private bool isAiChat = false;

        private List<User> onlineUsers = new();
        private User? selectedUser = null;
        private string? notificationMessage = null;

        protected override async Task OnInitializedAsync()
        {
            hubConnection = new HubConnectionBuilder().WithUrl(Navigation.ToAbsoluteUri("/chathub")).Build();

            hubConnection.On<string, string>("ReceiveMessage", (user, msg) =>
            {
                messages.Add(new ChatMessage
                {
                    Id = Guid.NewGuid().ToString(),
                    Content = msg,
                    SenderName = user,
                    SentAt = DateTime.Now
                });
                InvokeAsync(StateHasChanged);
            });
            await hubConnection.StartAsync();

            LoadMockUser();
            LoadMockMessages();
        }

        private void LoadMockMessages()
        {
            messages.Add(new ChatMessage { Content = "Привет!", SenderName = "Иван", SentAt = DateTime.Now.AddMinutes(-5) });
        }

        private void LoadMockUser()
        {
            onlineUsers = new List<User>
            {
                new User { Name = "Анна", Role = "Админ", IsOnline = true },
                new User { Name = "Иван", Role = "Повар", IsOnline = true }
            };
        }

        private async Task SendMessage()
        {
            if(!string.IsNullOrWhiteSpace(newMessage) && hubConnection != null)
            {
                if(isAiChat)
                {
                    messages.Add(new ChatMessage
                    {
                        Id = Guid.NewGuid().ToString(),
                        Content = newMessage,
                        SenderName = userName,
                        SentAt = DateTime.Now
                    });
                    GenerateAiResponce(newMessage);
                }
                else
                {
                    await hubConnection.SendAsync("SendMessage", userName, newMessage);
                }
                newMessage = string.Empty;
            }
        }

        private void GenerateAiResponce(string question)
        {
            messages.Add(new ChatMessage
            {
                Id = Guid.NewGuid().ToString(),
                Content = "🤖 Ответ ИИ: " + question,
                SenderName = "ИИ-ассистент",
                SentAt = DateTime.Now
            });
        }
        private void SwitchToPeopleChat() => isAiChat = false;
        private void SwitchToAiChat()
        {
            isAiChat = true; 
            selectedUser = null;
        }
        public async ValueTask DisposeAsync()
        {
            if (hubConnection != null)
            {
                await hubConnection.DisposeAsync();
            }
        }
    }
}
