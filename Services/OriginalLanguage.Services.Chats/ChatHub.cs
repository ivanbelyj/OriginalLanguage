using Microsoft.AspNetCore.SignalR;
using OriginalLanguage.Services.Chats.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Services.Chats;
public class ChatHub : Hub
{
    public async Task JoinGroup(string groupId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, groupId);
    }

    public async Task SendMessage(string groupId, SendMessageModel message)
    {
        Guid? userId = GetUserId();

        var messageModel = new MessageModel()
        {
            AvatarUrl = "https://via.placeholder.com/150",
            DateTime = DateTime.UtcNow,
            UserName = userId?.ToString().Substring(0, 3) ?? "Anonymous",
            Content = message.Content,
            GroupId = groupId
        };
        //await Clients.All.SendAsync("ReceiveMessage", messageModel);
        await Clients.Group(groupId).SendAsync("ReceiveMessage", messageModel);
    }

    private Guid? GetUserId()
    {
        Claim? userIdClaim = Context
            .User
            ?.Claims
            .FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier);

        var claims = Context.User?.Claims.ToList();

        Guid? userId = null;
        if (userIdClaim != null &&
            Guid.TryParse(userIdClaim.Value, out var parsedUserId))
        {
            userId = parsedUserId;
        }

        return userId;
    }
}
