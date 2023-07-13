using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelCostManagement.Contracts.RepositoryContracts;

namespace TravelCostManagement.InfrastructureData
{
    public class RepositoryCache : IRepositoryCache
    {
        private readonly IMemoryCache _memoryCache;

        public RepositoryCache(IMemoryCache memoryCache) 
        {
            _memoryCache = memoryCache;

        }

        public List<T> GetCache<T>(string key)
        {
            var response = _memoryCache.Get(key);

            return (List<T>)response;
        }

        public T GetCacheObject<T>(string key, T defaultResponse)
        {
            var response = _memoryCache.Get(key);

            if (response != null) return (T)response;

            return defaultResponse; 
        }

        public void SetCache<T>(string key, List<T> generic) 
        {
            _memoryCache.Set(key, generic);
        }

        public void SetCacheObject<T>(string key, T generic)
        {
            _memoryCache.Set(key, generic);
        }
    }
}
