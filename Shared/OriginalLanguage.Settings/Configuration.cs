namespace OriginalLanguage.Settings;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;

public abstract class Configuration
{
    /// <summary>
    /// Creates an object of the given type and binds configuration to it
    /// </summary>
    public static T? Load<T>(string key, IConfiguration? configuration = null)
    {
        T? settings = (T?)Activator.CreateInstance(typeof(T));

       (configuration ?? ConfigurationFactory.Create()).GetSection(key)
            .Bind(settings, (x) => { x.BindNonPublicProperties = true; });

        return settings;
    }
}