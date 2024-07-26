using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace real_time_chat.Hubs;

public class ChatHub : Hub<IChatClient>
{
    private static readonly ConcurrentDictionary<string, string> Users = new ConcurrentDictionary<string, string>();
    
    public override Task OnDisconnectedAsync(Exception exception)
    {
        if (Users.TryRemove(Context.ConnectionId, out string userName))
        {
            Clients.All.UserLeft(userName);
            Clients.All.UpdateUserList(Users.Values.ToList());
        }

        return base.OnDisconnectedAsync(exception);
    }

    public async Task SendMessage(string user, string message)
    {
        await Clients.All.ReceiveMessage(user, message);
    }

    public async Task JoinChat(string user)
    {
        Users[Context.ConnectionId] = user;
        await Clients.All.UserJoined(user);
        await Clients.All.UpdateUserList(Users.Values.ToList());
    }

    public async Task LeaveChat(string user)
    {
        if (Users.TryRemove(Context.ConnectionId, out _))
        {
            await Clients.All.UserLeft(user);
            await Clients.All.UpdateUserList(Users.Values.ToList());
        }
    }
}

public interface IChatClient
{
    Task ReceiveMessage(string user, string message);
    Task UserJoined(string user);
    Task UserLeft(string user);
    Task UpdateUserList(List<string> users);
}