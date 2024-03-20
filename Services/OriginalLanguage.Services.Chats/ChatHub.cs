using AutoMapper;
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
    private readonly IMessagesService messagesService;
    private readonly IMapper mapper;

    public ChatHub(
        IMessagesService messagesService,
        IMapper mapper)
    {
        this.messagesService = messagesService;
        this.mapper = mapper;
    }

    public async Task JoinGroup(string groupId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, groupId);
    }

    public async Task SendMessage(string groupId, string messageContent)
    {
        string? userId = GetUserId();

        var addMessageModel = new AddMessageModel()
        {
            Content = messageContent,
            DateTime = DateTime.UtcNow,
            GroupId = groupId,
            UserId = userId
        };

        var messageModel = await messagesService.AddMessage(addMessageModel);

        var messageResponse = await messagesService.EnrichWithUserData(messageModel);

        await Clients.Group(groupId).SendAsync("ReceiveMessage", messageResponse);
    }

    private string? GetUserId()
    {
        Claim? userIdClaim = Context
            .User
            ?.Claims
            .FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier);

        return userIdClaim?.Value;
    }
}
