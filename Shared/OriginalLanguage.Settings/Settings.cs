namespace OriginalLanguage.Settings;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;

public abstract class Settings
{
    public static T? Load<T>(string key, IConfiguration? configuration = null)
    {
        T? settings = (T?)Activator.CreateInstance(typeof(T));

       (configuration ?? SettingsFactory.Create()).GetSection(key)
            .Bind(settings, (x) => { x.BindNonPublicProperties = true; });

        return settings;
    }
}