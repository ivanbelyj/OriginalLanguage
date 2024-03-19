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
    Task<MessageResponse> ToMessageResponse(MessageModel messageModel);
    Task<IEnumerable<MessageResponse>> ToMessageResponses(
        IEnumerable<MessageModel> messageModels);
    Task<IEnumerable<MessageModel>> GetMessages(
        string groupId,
        int offset = 0,
        int limit = 100);
}
