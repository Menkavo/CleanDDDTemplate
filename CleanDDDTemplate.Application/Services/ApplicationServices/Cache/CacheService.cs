using CleanDDDTemplate.Application.Enums;
using CleanDDDTemplate.Domain.Models;
using Microsoft.Extensions.Caching.Memory;

namespace CleanDDDTemplate.Application.Services.ApplicationServices.Cache
{
    public class CacheService : ICacheService
    {
        private IMemoryCache _cache { get; }

        public CacheService(IMemoryCache cache) => _cache = cache;

        public void InitializeCache<T>(string key, T cacheValue) => _cache.Set(key, cacheValue, DateTimeOffset.MaxValue);

        public void InitializeCache<T>(DomainEntityEnum key, T cacheValue) => InitializeCache(key.ToString(), cacheValue);

        public T GetCache<T>(string key)
        {
            var cacheValue = _cache.Get(key);
            return cacheValue != null ? (T)cacheValue : default;
        }

        public void UpdateCache<T>(DomainEntityEnum key, T cacheValue)
        {
            //TODO
            throw new NotImplementedException();
        }

        
        #region Domain Cache

        public IQueryable<DemoModel> GetDemos() => GetCache<List<DemoModel>>(nameof(DomainEntityEnum.Demo))?.AsQueryable() ?? new List<DemoModel>().AsQueryable();

        #endregion
    }
}