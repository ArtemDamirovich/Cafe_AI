using Cafe_AI.Core.Entities;
using Microsoft.AspNetCore.Components.Web;

namespace Cafe_AI1.Components.Pages
{
    public partial class Chat
    {
        private List<User> onlineUsers = new();
        private User? selectedUser = null;
        private bool isAiChat = false;
        private string newMessage = string.Empty;
        private string currentUserId = "user-1";
        private string? currentUserName = "Igr";
        private List<ChatMessage> allMessages = new();

        private List<ChatMessage> currentMessages
        {
            get
            {
                if (isAiChat)
                {
                    return allMessages.Where(m => m.SenderId == "ai-assistant" || m.SenderId == currentUserId).
                        Where(m => m.ReceiverId == currentUserId || m.ReceiverId == "ai-assistant").ToList();
                }
                if (selectedUser != null)
                {
                    return allMessages.Where(m => (m.SenderId == currentUserId && m.ReceiverId == selectedUser.Id) ||
                    (m.SenderId == selectedUser.Id && m.ReceiverId == currentUserId)).ToList();
                }
                return allMessages.Where(m => string.IsNullOrEmpty(m.ReceiverId)).ToList();
            }
        }
        protected override void OnInitialized()
        {
            LoadMockData();
        }

        private void LoadMockData()
        {
            onlineUsers = new List<User>
            {
                new User  {Id = "user-1", Name = "Анна Петровна", Role = "Админ", IsOnline = true, UnreadCount = 0},
                new User { Id = "user-2", Name = "Иван Сидоров", Role = "Повар", IsOnline = true, UnreadCount = 2 },
                new User { Id = "user-3", Name = "Мария Иванова", Role = "Официант", IsOnline = false, UnreadCount = 0 },
                new User { Id = "user-4", Name = "Алексей Котов", Role = "Повар", IsOnline = true, UnreadCount = 1 },
            };
            allMessages = new List<ChatMessage>
            {
                new ChatMessage
                {
                    Id = Guid.NewGuid().ToString(),
                    Content = "Привет! Кто сегодня на смене?",
                    SenderId = "user-1",
                    SenderName = "Анна Петрова",
                    ReceiverId = "",
                    SentAt = DateTime.Now.AddHours(-2)
                },
                new ChatMessage
                {
                    Id = Guid.NewGuid().ToString(),
                    Content = "Я с 10 утра",
                    SenderId = "user-2",
                    SenderName = "Иван Сидоров",
                    ReceiverId = "",
                    SentAt = DateTime.Now.AddHours(-2).AddMinutes(5)
                },
                new ChatMessage
                {
                    Id = Guid.NewGuid().ToString(),
                    Content = "Я тоже буду к 10",
                    SenderId = "user-4",
                    SenderName = "Алексей Котов",
                    ReceiverId = "",
                    SentAt = DateTime.Now.AddHours(-2).AddMinutes(10)
                },
                new ChatMessage
                {
                    Id = Guid.NewGuid().ToString(),
                    Content = "Отлично! У нас новый десерт в меню, надо попробовать приготовить",
                    SenderId = "user-1",
                    SenderName = "Анна Петрова",
                    ReceiverId = "",
                    SentAt = DateTime.Now.AddHours(-1)
                },
                new ChatMessage
                {
                    Id = Guid.NewGuid().ToString(),
                    Content = "Уже видел! Тирамису выглядит отлично",
                    SenderId = "user-2",
                    SenderName = "Иван Сидоров",
                    ReceiverId = "",
                    SentAt = DateTime.Now.AddHours(-1).AddMinutes(3)
                }
            };
        }
        private void SelectUser(User user)
        {
            selectedUser = user;
            isAiChat = false;
            user.UnreadCount = 0;
        }
        private void SwitchToPeopleChat()
        {
            isAiChat = false;
        }
        private void SwitchToAiChat()
        {
            isAiChat = true;
            selectedUser = null;
        }
        private void SendMessage()
        {
            if (string.IsNullOrWhiteSpace(newMessage))
            {
                return;
            }
            var receiverId = isAiChat ? "ia-assistant" : selectedUser?.Id ?? "";
            var message = new ChatMessage
            {
                Id = Guid.NewGuid().ToString(),
                Content = newMessage,
                SenderName = currentUserName,
                ReceiverId = receiverId,
                SentAt = DateTime.Now
            };
            allMessages.Add(message);
            newMessage = string.Empty;

            if (isAiChat)
            {
                GenerateAiResponse(message.Content);
            }
        }

        private void GenerateAiResponse(string userQuestion)
        {
            string response;

            if (userQuestion.Contains("меню", StringComparison.OrdinalIgnoreCase))
            {
                response = "📋 Сегодня в меню:\n" +
                           "• Салат Цезарь с креветками — 650₽\n" +
                           "• Паста Карбонара — 580₽\n" +
                           "• Тирамису — 420₽\n" +
                           "• Домашний лимонад — 250₽\n\n" +
                           "Рекомендую попробовать новый десерт! 🍰";
            }
            else if (userQuestion.Contains("блюдо дня", StringComparison.OrdinalIgnoreCase) ||
                     userQuestion.Contains("предложи", StringComparison.OrdinalIgnoreCase))
            {
                response = "🍽️ Блюдо дня: Поке с лососем и манго — 620₽\n\n" +
                           "Освежающее гавайское блюдо с рисом, свежим лососем, спелым манго и пикантной заправкой.\n\n" +
                           "Готовится всего за 20 минут! 👨‍🍳";
            }
            else if (userQuestion.Contains("популярн", StringComparison.OrdinalIgnoreCase))
            {
                response = "📊 Самые популярные блюда за неделю:\n\n" +
                           "🥇 Паста Карбонара — 47 заказов\n" +
                           "🥈 Салат Цезарь — 38 заказов\n" +
                           "🥉 Тирамису — 29 заказов\n\n" +
                           "Тренд: растёт спрос на летние напитки 🥤";
            }
            else
            {
                response = "🤖 Спасибо за вопрос! Я пока учусь и могу ответить на вопросы:\n" +
                           "• 📋 Что в меню?\n" +
                           "• 🍽️ Предложить блюдо дня\n" +
                           "• 📊 Популярность блюд\n\n" +
                           "В будущем я смогу помогать с рецептами и аналитикой!";
            }

            allMessages.Add(new ChatMessage
            {
                Id = Guid.NewGuid().ToString(),
                Content = response,
                SenderId = "ai-assistant",
                SenderName = "ИИ-ассистент",
                ReceiverId = currentUserId,
                SentAt = DateTime.Now
            });
        }
        private void AskAiQuickQuestion(string question)
        {
            newMessage = question;
            SendMessage();
        }
        private void HandleKeyPress(KeyboardEventArgs e)
        {
            if (e.Key == "Enter" && !string.IsNullOrWhiteSpace(newMessage))
            {
                SendMessage();
            }
        }
    }
}
