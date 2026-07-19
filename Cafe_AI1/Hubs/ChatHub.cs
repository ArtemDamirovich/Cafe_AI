using Microsoft.AspNetCore.SignalR;

namespace Cafe_AI1.Hubs
{
    public class ChatHub : Hub
    {
        private static readonly Dictionary<string, string> OnlineUsers = new();
        public async Task SendToGeneral(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveGeneralMessage", user, message);
        }

        public async Task SendPrivate(string receiverConnectionId, string user, string message)
        {
            await Clients.Client(receiverConnectionId).SendAsync("ReceivePrivateMessage", user, message);
        }

        public async Task UserConnected(string user)
        {
            OnlineUsers[Context.ConnectionId] = user;
            await Clients.All.SendAsync("UserOnline", user, Context.ConnectionId);
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            OnlineUsers.Remove(Context.ConnectionId);
            await Clients.All.SendAsync("UserOffline", Context.ConnectionId);
            await base.OnDisconnectedAsync(exception);
        }
        public async Task GetOnlineUsers()
        {
            await Clients.Caller.SendAsync("ReceiveOnlineUsers", "запрос");
        }
        public async Task RequestOnlineUsers()
        {
            foreach (var kvp in OnlineUsers)
            {
                if (kvp.Key != Context.ConnectionId)
                    await Clients.Caller.SendAsync("UserOnline", kvp.Value, kvp.Key);
            }
        }
    }
}
