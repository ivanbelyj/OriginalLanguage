using OriginalLanguage.Services.Chats.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Services.Chats;
public interface IMessagesService
{
    Task<MessageModel> AddMessage(MessageModel messageModel);
}
