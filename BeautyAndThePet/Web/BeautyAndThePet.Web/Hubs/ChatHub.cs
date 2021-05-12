namespace SignalRChat.Hubs
{
    using System.Threading.Tasks;
    using BeautyAndThePet.Data.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.SignalR;


    [Authorize]
    public class ChatHub : Hub
    {
        public async Task Send(string message)
        {
            await this.Clients.All.SendAsync(
                "NewMessage",
                new ChatMessage { User = this.Context.User.Identity.Name, Text = message, });
        }
    }
}