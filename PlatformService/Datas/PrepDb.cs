using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using PlatformService.Models;
using System.Linq;

namespace PlatformService.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                SeedData(scope.ServiceProvider.GetService<AppDbContext>());
            }
        }

        public static void SeedData(AppDbContext context)
        {
            if (!context.Platforms.Any())
            {
                context.Platforms.AddRange(
                    new Platform() { Name = "Dot Net", Publisher = "Microsoft", Cost = "Free" },
                    new Platform() { Name = "Sql Server Express", Publisher = "Microsoft", Cost = "Free" },
                    new Platform() { Name = "Kubernetes", Publisher = "Could Native", Cost = "Free" }
                    );
                context.SaveChanges();
            }
        }
    }
}
