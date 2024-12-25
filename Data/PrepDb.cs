using PlatformService.Models;

namespace PlatformService.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                AppDbContext? context = serviceScope.ServiceProvider.GetService<AppDbContext>();

                if (context == null)
                {
                    throw new Exception("service not found");
                }

                SeedData(context);
            }
        }

        public static void SeedData(AppDbContext context)
        {
            if (!context.Platforms.Any())
            {
                Console.WriteLine("Seeding Data");

                context.Platforms.AddRange(
                    new Platform() { Name = ".Net", Publisher = "Microsoft", Cost = "Free" },
                    new Platform() { Name = "Microsoft Sql Server", Publisher = "Microsoft", Cost = "Free" },
                    new Platform() { Name = "Kubernetes", Publisher = "Cloud Native Computing Foundation", Cost = "Free" });

                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("Already has data");
            }
        }
    }
}
