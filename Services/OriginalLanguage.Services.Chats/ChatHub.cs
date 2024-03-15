using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Services.Chats;
public class ChatHub : Hub
{
    public async Task SendMessage(MessageModel message)
    {
        await Clients.All.SendAsync("ReceiveMessage", message);
        //await Clients.Group(groupName).SendAsync("Receive", message);
    }
}
