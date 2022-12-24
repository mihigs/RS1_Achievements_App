using backend_api.Models;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace backend_api.SignalR
{
    public class Notifier : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
        public async Task SendMessageAll(string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
        }
    }
}
