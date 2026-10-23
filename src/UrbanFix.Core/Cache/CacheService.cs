using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace UrbanFix.Core.Cache
{
    public class CacheService : ICacheService
    {
        private readonly IDistributedCache _cache;

        public CacheService(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task<T> GetCachedDataAsync<T>(string key)
        {
            var data = await _cache.GetStringAsync(key);

            return data!= null ? JsonSerializer.Deserialize<T>(data) : default;
        }

        public async Task SetCachedDataAsync<T>(string key, T data, TimeSpan expiration)
        {
            var options = new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = expiration };
            var jsonData = JsonSerializer.Serialize(data);

            await _cache.SetStringAsync(key, jsonData, options);
        }

    }
}
