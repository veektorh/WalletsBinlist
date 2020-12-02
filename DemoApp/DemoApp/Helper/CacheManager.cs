using DemoApp.Interfaces.Managers;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoApp.Helper
{
    public class CacheManager : ICacheManager
    {
        private readonly IMemoryCache _memoryCache;
        private readonly MemoryCacheEntryOptions memoryCacheEntryOptions;
        public CacheManager(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
            memoryCacheEntryOptions = new MemoryCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(20) };
        }


        public string Set(string key, string value)
        {
            return _memoryCache.Set<string>(key, value, memoryCacheEntryOptions);
        }

        public string Get(string key)
        {
            string value = "";
            _memoryCache.TryGetValue(key, out value);
            return value;
        }

        public bool Exists(string key)
        {
            object value;
            return _memoryCache.TryGetValue(key, out value);
        }

    }
}
