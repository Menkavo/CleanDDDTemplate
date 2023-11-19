using CleanDDDTemplate.Application.Context;
using CleanDDDTemplate.Application.Services.ApplicationServices.Cache;

namespace CleanDDDTemplate.Application.Services.DomainServices.Demo
{
    public class DemoService : IDemoService
    {
        private readonly ICacheService _cacheService;
        private readonly ICleanDDDTemplateDbContext _cleanDDDTemplateDbContext;

        public DemoService(ICacheService cacheService, ICleanDDDTemplateDbContext cleanDDDTemplateDbContext)
        {
            _cacheService = cacheService;
            _cleanDDDTemplateDbContext = cleanDDDTemplateDbContext;
        }
    }
}
