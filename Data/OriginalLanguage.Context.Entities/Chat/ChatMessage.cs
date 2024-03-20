using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Context.Entities.Chat;

public class ChatMessage : EntityBase
{
    public string Content { get; set; }
    public string? UserId { get; set; }
    public DateTime DateTime { get; set; }
    public string GroupId { get; set; }
}
