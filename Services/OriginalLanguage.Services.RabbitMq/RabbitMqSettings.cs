using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Services.RabbitMq;
public class RabbitMqSettings
{
    public string Uri { get; private set; }
    public string UserName { get; private set; }
    public string Password { get; private set; }
}
