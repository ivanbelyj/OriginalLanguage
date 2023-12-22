using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Services.Cache;
public class CacheSettings
{
    /// <summary>
    /// Time of the cache keeping (in minutes)
    /// </summary>
    public int CacheLifeTime { get; set; }

    /// <summary>
    /// Redis connection string
    /// </summary>
    public string Uri { get; set; }
}
