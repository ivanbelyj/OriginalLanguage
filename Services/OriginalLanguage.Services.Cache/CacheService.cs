using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OriginalLanguage.Common;

namespace OriginalLanguage.Services.Cache;
public class CacheService : ICacheService
{
    private readonly CacheSettings cacheSettings;
    private readonly string redisUri;
    private readonly TimeSpan defaultLifetime;
    
    private readonly IDatabase cacheDb;

    public CacheService(CacheSettings cacheSettings)
    {
        this.cacheSettings = cacheSettings;
        this.redisUri = cacheSettings.Uri;
        this.defaultLifetime = TimeSpan.FromMinutes(cacheSettings.CacheLifeTime);

        cacheDb = ConnectionMultiplexer.Connect(redisUri).GetDatabase();
    }
    public async Task<bool> Delete(string key)
    {
        return await cacheDb.KeyDeleteAsync(key);
    }

    public string GenerateKey()
    {
        return Guid.NewGuid().ToString();
    }

    public async Task<T?> Get<T>(string key, bool resetLifeTime = false)
    {
        try
        {
            string? cachedData = await cacheDb.StringGetAsync(key);
            if (cachedData == null)
                return default;

            T? data = cachedData.FromJsonString<T>();

            if (resetLifeTime)
                await SetStoreTime(key, defaultLifetime);

            return data;
        } catch (Exception ex) {
            throw new Exception($"Can`t get data from cache for {key}", ex);
        }
    }

    public async Task<bool> Put<T>(string key, T value, TimeSpan? storeTime = null)
    {
        return await cacheDb.StringSetAsync(key, value?.ToJsonString(),
            storeTime ?? defaultLifetime);
    }

    public async Task SetStoreTime(string key, TimeSpan? storeTime = null)
    {
        await cacheDb.KeyExpireAsync(key, storeTime ?? defaultLifetime);
    }
}
