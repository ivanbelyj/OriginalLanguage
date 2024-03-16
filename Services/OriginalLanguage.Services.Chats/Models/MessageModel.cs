using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Services.Chats.Models;

#nullable disable
public class MessageModel
{
    public string Content { get; set; }
    public string UserName { get; set; }
    public string AvatarUrl { get; set; }
    public DateTime DateTime { get; set; }
    public string GroupId { get; set; }
}
