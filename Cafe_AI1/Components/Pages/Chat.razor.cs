using Cafe_AI.Core.Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

namespace Cafe_AI1.Components.Pages;

public partial class Chat : IAsyncDisposable
{
    [Inject] private NavigationManager Navigation { get; set; } = default!;

    private HubConnection? hubConnection;
    private string userName = string.Empty;
    private string chatMode = "general";
    private string? currentConnectionId;
    private List<ChatUser> onlineUsers = new();
    private ChatUser? selectedUser;
    private List<ChatMessage> messages = new();
    private string newMessage = string.Empty;
    private string nameInput = string.Empty;
    private string? notificationMessage;

    private List<ChatMessage> currentMessages => chatMode switch
    {
        "general" => messages.Where(m => m.Mode == "general").ToList(),
        "private" => messages.Where(m =>
            m.Mode == "private" &&
            (m.SenderName == userName && m.ReceiverName == selectedUser?.Name ||
             m.SenderName == selectedUser?.Name && m.ReceiverName == userName)).ToList(),
        _ => messages
    };

    protected override void OnInitialized()
    {
        // Ждём ввода имени — ничего не делаем
    }

    private async Task JoinChat()
    {
        if (string.IsNullOrWhiteSpace(nameInput)) return;

        userName = nameInput;

        hubConnection = new HubConnectionBuilder()
            .WithUrl(Navigation.ToAbsoluteUri("/chathub"))
            .Build();

        hubConnection.On<string, string>("ReceiveGeneralMessage", (user, msg) =>
        {
            if (user != userName)
            {
                messages.Add(new ChatMessage { Content = msg, SenderName = user, Mode = "general", SentAt = DateTime.Now });
                InvokeAsync(StateHasChanged);
            }
        });

        hubConnection.On<string, string>("ReceivePrivateMessage", (user, msg) =>
        {
            messages.Add(new ChatMessage { Content = msg, SenderName = user, ReceiverName = userName, Mode = "private", SentAt = DateTime.Now });

            if (chatMode != "private" || selectedUser?.Name != user)
                notificationMessage = $"📩 Новое сообщение от {user}";

            InvokeAsync(StateHasChanged);
        });

        hubConnection.On<string, string>("UserOnline", (user, connectionId) =>
        {
            if (!onlineUsers.Any(u => u.ConnectionId == connectionId))
                onlineUsers.Add(new ChatUser { Name = user, ConnectionId = connectionId, IsOnline = true, Role = "Сотрудник" });
            InvokeAsync(StateHasChanged);
        });

        hubConnection.On<string>("UserOffline", (connectionId) =>
        {
            onlineUsers.RemoveAll(u => u.ConnectionId == connectionId);
            InvokeAsync(StateHasChanged);
        });

        await hubConnection.StartAsync();
        currentConnectionId = hubConnection.ConnectionId;
        await hubConnection.SendAsync("UserConnected", userName);
        await hubConnection.SendAsync("RequestOnlineUsers");
        StateHasChanged();
    }

    private void SwitchToGeneral() { chatMode = "general"; selectedUser = null; notificationMessage = null; }
    private void SelectPrivateChat(ChatUser user) { chatMode = "private"; selectedUser = user; notificationMessage = null; }

    private async Task SendMessage()
    {
        if (string.IsNullOrWhiteSpace(newMessage)) return;

        if (chatMode == "general")
        {
            var msg = new ChatMessage { Content = newMessage, SenderName = userName, Mode = "general", SentAt = DateTime.Now };
            messages.Add(msg);
            await hubConnection!.SendAsync("SendToGeneral", userName, newMessage);
        }
        else if (chatMode == "private" && selectedUser != null)
        {
            var msg = new ChatMessage { Content = newMessage, SenderName = userName, ReceiverName = selectedUser.Name, Mode = "private", SentAt = DateTime.Now };
            messages.Add(msg);
            await hubConnection!.SendAsync("SendPrivate", selectedUser.ConnectionId, userName, newMessage);
        }

        newMessage = string.Empty;
        StateHasChanged();
    }

    public async ValueTask DisposeAsync()
    {
        if (hubConnection != null)
            await hubConnection.DisposeAsync();
    }
}
