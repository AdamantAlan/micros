using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PlatformService.Models;
using System;
using System.Linq;

namespace PlatformService.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app, bool isProd)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                SeedData(scope.ServiceProvider.GetService<AppDbContext>(), isProd);
            }
        }

        public static void SeedData(AppDbContext context, bool isProd)
        {
            if(isProd)
            {
                try
                {
                    context.Database.Migrate();
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Migrate is failed - {e.Message}");
                }
            }
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
