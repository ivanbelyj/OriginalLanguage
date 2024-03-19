using AutoMapper;
using FluentValidation;
using OriginalLanguage.Context.Entities.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Services.Chats.Models;

#nullable disable
/// <summary>
/// Message model contains minimal data about a message
/// </summary>
public class MessageModel
{
    public string Content { get; set; }
    public Guid? UserId { get; set; }
    public DateTime DateTime { get; set; }
    public string GroupId { get; set; }
}

public class MessageModelValidator : AbstractValidator<MessageModel>
{
    public MessageModelValidator()
    {
        RuleFor(x => x.Content)
            .NotEmpty()
            .WithMessage("Content is required");
        RuleFor(x => x.GroupId)
            .NotEmpty()
            .WithMessage("GroupId is required");
    }
}

public class MessageModelProfile : Profile
{
    public MessageModelProfile()
    {
        CreateMap<MessageModel, ChatMessage>();
        CreateMap<ChatMessage, MessageModel>();
        CreateMap<MessageModel, MessageResponse>();
    }
}
