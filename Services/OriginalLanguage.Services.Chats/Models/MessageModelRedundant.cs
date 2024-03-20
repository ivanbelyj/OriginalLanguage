using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Services.Chats.Models;

#nullable disable
/// <summary>
/// Message response contains full data required for message rendering so
/// it is optimal for client-server communications
/// </summary>
public class MessageModelRedundant : MessageModel
{
    //public int Id { get; set; }
    //public string Content { get; set; }

    // Redudant data for optimization
    public string UserName { get; set; }
    public string AvatarUrl { get; set; }

    //public DateTime DateTime { get; set; }
    //public string GroupId { get; set; }
}
