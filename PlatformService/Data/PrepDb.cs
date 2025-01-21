using Microsoft.EntityFrameworkCore;
using PlatformService.Models;

namespace PlatformService.Data
{
    public static class PrepDb
    {
        public static ILogger _logger;

        public static void InitializeLogger(IApplicationBuilder app)
        {
            using (var serviceScopw = app.ApplicationServices.CreateScope())
            {
                ILoggerFactory? loggerFactory = serviceScopw.ServiceProvider.GetService<ILoggerFactory>();

                if (loggerFactory != null)
                {
                    _logger = loggerFactory.CreateLogger(typeof(PrepDb).FullName!);
                }
            }
        }

        public static void PrepPopulation(IApplicationBuilder app, bool isProduction)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                AppDbContext? context = serviceScope.ServiceProvider.GetService<AppDbContext>();

                if (context == null)
                {
                    throw new Exception("service not found");
                }

                SeedData(context, isProduction);
            }
        }

        public static void SeedData(AppDbContext context, bool isProduction)
        {
            if (isProduction)
            {
                try
                {
                    _logger.LogInformation($"Starting migration");
                    context.Database.Migrate();
                }
                catch(Exception ex)
                {
                    _logger.LogError($"Couldnot run migration due to {ex.Message}");
                }

                
            }
            if (!context.Platforms.Any())
            {
                _logger.LogInformation("Seeding Data");

                context.Platforms.AddRange(
                    new Platform() { Name = ".Net", Publisher = "Microsoft", Cost = "Free" },
                    new Platform() { Name = "Microsoft Sql Server", Publisher = "Microsoft", Cost = "Free" },
                    new Platform() { Name = "Kubernetes", Publisher = "Cloud Native Computing Foundation", Cost = "Free" });

                context.SaveChanges();
            }
            else
            {
                _logger.LogInformation("Already has data");
            }
        }
    }
}
