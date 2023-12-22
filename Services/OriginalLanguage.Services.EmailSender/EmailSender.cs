using OriginalLanguage.Services.EmailSender.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Services.EmailSender;
public class EmailSender : IEmailSender
{
    private readonly ILogger logger;

    public EmailSender(ILogger logger)
    {
        this.logger = logger;
    }
    public async Task Send(EmailModel model)
    {
        // Todo:
        await Task.Delay(5000);
        logger.Information($"Email sended: {model.Content}");
    }
}
