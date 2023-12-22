using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Services.Cache;
public interface ICacheService
{
    string GenerateKey();
    Task<bool> Put<T>(string key, T value, TimeSpan? storeTime = null);
    Task SetStoreTime(string key, TimeSpan? storeTime);
    Task<T?> Get<T>(string key, bool resetLifeTime = false);
    Task<bool> Delete(string key);
}
