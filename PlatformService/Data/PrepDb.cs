using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PlatformService.Data;
using PlatformService.Models;

namespace PlatformService
{
    public static class PrepDb
    {
        public static void PrepPopulation(this IApplicationBuilder builder, bool isProd)
        {
            using (var scope = builder.ApplicationServices.CreateScope())
            {
                SeedData(scope.ServiceProvider.GetRequiredService<AppDbContext>(), isProd);
            }
        }

        private static void SeedData(AppDbContext context, bool isProd)
        {
            if (isProd)
            {
                Console.WriteLine($"--> Attempting to apply migrations...");
                try
                {
                    context.Database.Migrate();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"--> Could not run migration: {ex.Message}");
                }
            }

            if (!context.Platforms.Any())
            {
                Console.WriteLine("--> Seeding data.");

                context.Platforms.AddRange
                (
                    new() { Name = "Dot net", Publisher = "Microsoft", Cost = "Free" },
                    new() { Name = "Sql Server", Publisher = "Microsoft", Cost = "Free" },
                    new() { Name = "Kubernetes", Publisher = "Cloud Native Computing Foundation", Cost = "Free" }
                );

                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("--> We already had data.");
            }
        }
    }
}