using OriginalLanguage.Consts;
using OriginalLanguage.Services.EmailSender.Models;
using OriginalLanguage.Services.RabbitMq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Services.Actions;
public class SendEmailAction : ISendEmailAction
{
    private readonly IRabbitMq rabbitMq;

    public SendEmailAction(IRabbitMq rabbitMq) {
        this.rabbitMq = rabbitMq;
    }

    public async Task SendEmail(EmailModel email)
    {
        await rabbitMq.PushAsync(RabbitMqTaskQueueNames.SEND_EMAIL, email);
    }
}
