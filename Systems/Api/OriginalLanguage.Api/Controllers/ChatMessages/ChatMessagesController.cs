using Microsoft.AspNetCore.Mvc;
using OriginalLanguage.Api.Controllers.Articles.Models;
using OriginalLanguage.Common.Responses;
using OriginalLanguage.Services.Chats;
using OriginalLanguage.Services.Chats.Models;

namespace OriginalLanguage.Api.Controllers.ChatMessages;

[ProducesResponseType(typeof(ErrorResponse), 400)]
[Route("api/v{version:apiVersion}/chat-messages")]
[ApiController]
[ApiVersion("1.0")]
[Produces("application/json")]
public class ChatMessagesController
{
    private readonly IMessagesService messagesService;

    public ChatMessagesController(IMessagesService messagesService)
    {
        this.messagesService = messagesService;
    }

    [ProducesResponseType(typeof(IEnumerable<MessageModelRedundant>), 200)]
    [HttpGet("")]
    public async Task<IEnumerable<MessageModelRedundant>> GetMessages(
        [FromQuery] string groupId,
        [FromQuery] int? idLimit = null,
        [FromQuery] int limit = 100)
    {
        return await messagesService.GetMessages(groupId, idLimit, limit);
    }
}
