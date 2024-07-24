using Microsoft.AspNetCore.SignalR;

namespace BlazorSignalRApp.Hubs;

public class ChatHub : Hub<IChatClient>
{
	public async Task SendMessage(string user, string message)
	{
		await Clients.All.ReceiveMessage(user, message);

		await base.OnConnectedAsync();
	}

	public async Task JoinChat(string user)
	{
        await Clients.All.UserJoined(user);

        await base.OnConnectedAsync();
    }

	public async Task LeaveChat(string user)
	{
        await Clients.All.UserLeft(user);

        await base.OnDisconnectedAsync(new Exception("User left the chat"));
    }
}

public interface IChatClient
{
    Task ReceiveMessage(string user, string message);
	Task UserJoined(string user);
	Task UserLeft(string user);
}