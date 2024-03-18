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
}
