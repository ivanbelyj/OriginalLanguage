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
using OriginalLanguage.Services.UserAccount;

namespace OriginalLanguage.Services.Chats;
public class MessagesService : IMessagesService
{
    private readonly IDbContextFactory<MainDbContext> dbContextFactory;
    private readonly IModelValidator<AddMessageModel> messageModelValidator;
    private readonly IMapper mapper;
    private readonly IUserAccountService usersService;

    public MessagesService(
        IDbContextFactory<MainDbContext> dbContextFactory,
        IModelValidator<AddMessageModel> messageModelValidator,
        IMapper mapper,
        IUserAccountService usersService)
    {
        this.dbContextFactory = dbContextFactory;
        this.messageModelValidator = messageModelValidator;
        this.mapper = mapper;
        this.usersService = usersService;
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
        await using var dbContext = await dbContextFactory.CreateDbContextAsync();
        var chatMessages = await dbContext
            .ChatMessages
            .Where(m => m.GroupId == groupId)
            .OrderByDescending(x => x.Id)
            .Where(m => m.Id < (idLimit ?? int.MaxValue))
            .Take(Math.Min(Math.Max(0, limit), 1000))
            .ToListAsync();

        var messageModels = chatMessages
            .Select(mapper.Map<MessageModel>)
            .ToList();
        return await EnrichWithUserData(messageModels);
    }

    public async Task<MessageModelRedundant> EnrichWithUserData(
        MessageModel messageModel)
    {
        var messageResponse = mapper.Map<MessageModelRedundant>(messageModel);

        var user = messageModel.UserId == null ?
            null
            : await usersService.GetUser(messageModel.UserId);
        
        messageResponse.AvatarUrl = ""; // Todo:
        messageResponse.UserName = user?.Name ?? "Anonymous";

        return messageResponse;
    }

    public async Task<IEnumerable<MessageModelRedundant>> EnrichWithUserData(
        IEnumerable<MessageModel> messageModels)
    {
        List<MessageModelRedundant> messages = new();
        foreach (var messageModel in messageModels)
        {
            messages.Add(await EnrichWithUserData(messageModel));
        }
        return messages;
    }
}
