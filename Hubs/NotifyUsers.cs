using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Planning_Poker.Hubs
{
    public class NotifyUsers: Hub
    {
        public async Task NotifyUser(string user, string message)
        {
            await Clients.All.SendAsync("VotesReceive", user, message);
        }
    }
}
