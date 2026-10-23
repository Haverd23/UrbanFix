using System;
using System.Collections.Generic;
using System.Text;

namespace UrbanFix.Core.Cache
{
    public interface ICacheService
    {
        Task<T> GetCachedDataAsync<T>(string key);
        Task SetCachedDataAsync<T>(string key, T data, TimeSpan expiration);
    }
}
