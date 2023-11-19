using CleanDDDTemplate.Application.Services.ApplicationServices.Cache;
using CleanDDDTemplate.Application.Services.ApplicationServices.Logging;
using CleanDDDTemplate.Application.Services.DomainServices.Demo;
using CleanDDDTemplate.Application.Utility.Mapper;
using Mapster;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CleanDDDTemplate.Application.DependencyInjection
{
    public static class ApplicationLayerDependencyInjection
    {
        public static IServiceCollection InjectApplicationLayerDependencies(this IServiceCollection services)
        {
            SetMapperConfigurations();

            //Application Services
            services.AddTransient<ICacheService, CacheService>();
            services.AddSingleton<ILoggingService, LoggingService>();
            services.AddSingleton<IRegister>(new MapsterMappingProfile());

            //Domain Services
            services.AddTransient<IDemoService, DemoService>();

            return services;
        }

        private static void SetMapperConfigurations() => TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());
    }
}