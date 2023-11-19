using CleanDDDTemplate.Application.Enums;
using CleanDDDTemplate.Domain.Models;

namespace CleanDDDTemplate.Application.Services.ApplicationServices.Cache
{
    public interface ICacheService
    {
        public void InitializeCache<T>(string key, T cacheValue);

        public void InitializeCache<T>(DomainEntityEnum key, T cacheValue);

        public T? GetCache<T>(string key);

        public void UpdateCache<T>(DomainEntityEnum key, T cacheValue);

        public IQueryable<DemoModel> GetDemos();
    }
}