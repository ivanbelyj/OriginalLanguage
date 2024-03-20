using AutoMapper;
using OriginalLanguage.Context.Entities.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Services.Chats.Models;
public class MessageModel
{
    public int Id { get; set; }
    public string Content { get; set; }
    public string? UserId { get; set; }
    public DateTime DateTime { get; set; }
    public string GroupId { get; set; }
}

public class MessageModelProfile : Profile
{
    public MessageModelProfile()
    {
        CreateMap<ChatMessage, MessageModel>();
        CreateMap<MessageModel, MessageModelRedundant>();
    }
}
