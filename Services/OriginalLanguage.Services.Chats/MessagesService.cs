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
    private readonly IModelValidator<AddMessageModel> messageModelValidator;
    private readonly IMapper mapper;

    public MessagesService(
        IDbContextFactory<MainDbContext> dbContextFactory,
        IModelValidator<AddMessageModel> messageModelValidator,
        IMapper mapper)
    {
        this.dbContextFactory = dbContextFactory;
        this.messageModelValidator = messageModelValidator;
        this.mapper = mapper;
    }

    public async Task<MessageModel> AddMessage(AddMessageModel addMessageModel)
    {
        messageModelValidator.Check(addMessageModel);
        using var dbContext = await dbContextFactory.CreateDbContextAsync();
        var chatMessage = mapper.Map<ChatMessage>(addMessageModel);
        await dbContext.ChatMessages.AddAsync(chatMessage);
        dbContext.SaveChanges();

        return mapper.Map<MessageModel>(chatMessage);
    }

    public async Task<IEnumerable<MessageModelRedundant>> GetMessages(
        string groupId,
        int? idLimit = null,
        int limit = 100)
    {
        // Todo: is it normally to base pagination on DateTime?
        await using var dbContext = await dbContextFactory.CreateDbContextAsync();
        var chatMessages = dbContext
            .ChatMessages
            .Where(m => m.GroupId == groupId)
            .OrderByDescending(x => x.DateTime)
            .Where(m => m.Id < (idLimit ?? int.MaxValue))
            .Take(Math.Min(Math.Max(0, limit), 1000));

        var messageModels = (await chatMessages.ToListAsync())
            .Select(m => mapper.Map<MessageModel>(m));
        return await EnrichWithUserData(messageModels);
    }

    public async Task<MessageModelRedundant> EnrichWithUserData(
        MessageModel messageModel)
    {
        var messageResponse = mapper.Map<MessageModelRedundant>(messageModel);

        // Todo: return actual user data
        messageResponse.AvatarUrl = "";
        messageResponse.UserName = messageModel
            .UserId?
            .ToString()
            .Substring(0, 3) ?? "Anonymous";

        return messageResponse;
    }

    public async Task<IEnumerable<MessageModelRedundant>> EnrichWithUserData(
        IEnumerable<MessageModel> messageModels)
    {
        var tasks = messageModels.Select(EnrichWithUserData);
        var messageResponses = await Task.WhenAll(tasks);
        return messageResponses;
    }
}
