using CleanDDDTemplate.Application.Context;
using CleanDDDTemplate.Application.DependencyInjection;
using CleanDDDTemplate.Application.Services.ApplicationServices.Cache;
using CleanDDDTemplate.Persistence.DependencyInjection;
using System.Text.Json.Serialization;

namespace CleanDDDTemplate.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services
                .AddEndpointsApiExplorer()
                .AddSwaggerGen()
                .AddControllers()
                .AddJsonOptions(j => j.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            builder.Services
                .InjectApplicationLayerDependencies()
                .InjectPersistenceLayerDependencies(builder.Configuration)
                .AddMemoryCache();

            var app = builder.Build();

            AddDomainCache(app);

#if DEBUG
            app.UseSwagger();
            app.UseSwaggerUI();
#endif

            app.UseHttpsRedirection();
            app.UseRouting();

            AddMiddlewares(app);

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }

        /// <summary>
        /// Gets all (a part of) database data and stores it in memory to lower database usage for better performance.
        /// NOTE: this will not be a good idea in a project with large amount of data.
        /// </summary>
        /// <param name="app"></param>
        private static void AddDomainCache(WebApplication app)
        {
            //Initilizing domain cache
            var cacheService = app.Services.GetService<ICacheService>();
            using var scope = app.Services.CreateScope();
            var cleanDDDTemplateDbContext = scope.ServiceProvider.GetRequiredService<ICleanDDDTemplateDbContext>();
            if (cacheService != null && cleanDDDTemplateDbContext != null)
                cleanDDDTemplateDbContext.GetAllData().ToList().ForEach(x => cacheService.InitializeCache(x.Key, x.Value));
        }

        private static void AddMiddlewares(WebApplication app)
        {
            //Add middlewares here.
            //app.UseMiddleware<CustomMiddleware>();
        }
    }
}