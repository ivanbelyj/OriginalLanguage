using OriginalLanguage.Services.Chats.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Services.Chats;
public interface IMessagesService
{
    Task<MessageModel> AddMessage(AddMessageModel messageModel);
    Task<MessageModelRedundant> EnrichWithUserData(MessageModel messageModel);
    Task<IEnumerable<MessageModelRedundant>> EnrichWithUserData(
        IEnumerable<MessageModel> messageModels);
    Task<IEnumerable<MessageModelRedundant>> GetMessages(
        string groupId,
        int? idLimit = null,
        int limit = 100);
}
