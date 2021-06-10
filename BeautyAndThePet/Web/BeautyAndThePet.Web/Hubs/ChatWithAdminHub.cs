using BeautyAndThePet.Data.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeautyAndThePet.Web.Hubs
{
    public class ChatWithAdminHub : Hub
    {
        public async Task Send(string message)
        {
            await this.Clients.All.SendAsync(
                "NewMessage",
                new ChatMessage { User = this.Context.User.Identity.Name, Text = message, });
        }
    }
}
