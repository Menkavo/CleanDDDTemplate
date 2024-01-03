using CleanDDDTemplate.Application.InfrastructureInterfaces.Context;
using CleanDDDTemplate.Application.Utility;
using CleanDDDTemplate.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanDDDTemplate.Persistence.DependencyInjection
{
    public static class PersistenceLayerDependencyInjection
    {
        public static IServiceCollection InjectPersistenceLayerDependencies(this IServiceCollection services, IConfiguration configuration) => services
           .AddDbContext<CleanDDDTemplateDbContext>(option => option.UseSqlServer(AppSettingHelper.GetCleanDDDTemplateConnectionString(configuration)))
           .AddTransient<ICleanDDDTemplateDbContext, CleanDDDTemplateDbContext>();
    }
}