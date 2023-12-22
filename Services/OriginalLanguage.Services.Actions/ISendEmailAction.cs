using OriginalLanguage.Services.EmailSender.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Services.Actions;
public interface ISendEmailAction
{
    Task SendEmail(EmailModel email);
}
