using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Services.Settings;
public class OpenApiSettings
{
    public bool Enabled { get; private set; }

    public string? OAuthClientId { get; private set; }
    public string? OAuthClientSecret { get; private set; }

    public OpenApiSettings()
    {
        Enabled = false;
    }
}
