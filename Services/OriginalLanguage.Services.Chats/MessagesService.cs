using OriginalLanguage.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OriginalLanguage.Services.Chats.Models;
using OriginalLanguage.Common.Validator;
using AutoMapper;
using OriginalLanguage.Context.Entities.Chat;

namespace OriginalLanguage.Services.Chats;
public class MessagesService : IMessagesService
{
    private readonly IDbContextFactory<MainDbContext> dbContextFactory;
    private readonly IModelValidator<MessageModel> messageModelValidator;
    private readonly IMapper mapper;

    public MessagesService(
        IDbContextFactory<MainDbContext> dbContextFactory,
        IModelValidator<MessageModel> messageModelValidator,
        IMapper mapper)
    {
        this.dbContextFactory = dbContextFactory;
        this.messageModelValidator = messageModelValidator;
        this.mapper = mapper;
    }

    public async Task<MessageModel> AddMessage(MessageModel messageModel)
    {
        messageModelValidator.Check(messageModel);
        using var dbContext = await dbContextFactory.CreateDbContextAsync();
        var chatMessage = mapper.Map<ChatMessage>(messageModel);
        await dbContext.ChatMessages.AddAsync(chatMessage);
        dbContext.SaveChanges();

        return mapper.Map<MessageModel>(chatMessage);
    }

    public async Task<MessageResponse> ToMessageResponse(MessageModel messageModel)
    {
        var messageResponse = mapper.Map<MessageResponse>(messageModel);

        // Todo: return actual user data
        messageResponse.AvatarUrl = "https://via.placeholder.com/150";
        messageResponse.UserName = messageModel
            .UserId?
            .ToString()
            .Substring(0, 3) ?? "Anonymous";

        return messageResponse;
    }

    public async Task<IEnumerable<MessageResponse>> ToMessageResponses(
        IEnumerable<MessageModel> messageModels)
    {
        var tasks = messageModels.Select(ToMessageResponse);
        var messageResponses = await Task.WhenAll(tasks);
        return messageResponses;
    }

    public async Task<IEnumerable<MessageModel>> GetMessages(
        string groupId,
        int offet = 0,
        int limit = 100)
    {
        await using var dbContext = await dbContextFactory.CreateDbContextAsync();
        var chatMessages = dbContext
            .ChatMessages
            .Where(m => m.GroupId == groupId)
            .Skip(Math.Max(0, offet))
            .Take(Math.Min(Math.Max(0, limit), 1000));
        return (await chatMessages.ToListAsync())
            .Select(m => mapper.Map<MessageModel>(m));
    }
}
