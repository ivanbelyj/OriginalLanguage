using OriginalLanguage.Services.EmailSender.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Services.EmailSender;
public interface IEmailSender
{
    Task Send(EmailModel model);
}
