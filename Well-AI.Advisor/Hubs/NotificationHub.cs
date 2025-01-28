using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace WellAI.Advisor.Hubs
{
    public class NotificationHub : Hub
    {
        public async Task SendMessage(object sender, string message)
        {
            await Clients.Others.SendAsync("broadcastMessage", sender, message);
        }
        public async Task SendTyping(object sender)
        {
            // Broadcast the typing notification to all clients except the sender.
            await Clients.Others.SendAsync("typing", sender);
        }
        public async Task SendMessageAll(string message)
        {
            // Broadcast the message to all clients except the sender.
            await Clients.Others.SendAsync("broadcastMessageAll",  message);
        }

        public async Task SendVideoNotification(string sender,string receiver, string roomName)
        {
            await Clients.Others.SendAsync("ReceiveVideoNotification", sender, receiver, roomName);
        }

        public void SendMessageNotification()
        {
             Clients.All.SendAsync("updateNotification");
        }

    }
}
