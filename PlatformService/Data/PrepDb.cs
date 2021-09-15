using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using PlatformService.Data;
using PlatformService.Models;

namespace PlatformService
{
    public static class PrepDb
    {
        public static void PrepPopulation(this IApplicationBuilder builder)
        {
            using (var scope = builder.ApplicationServices.CreateScope())
            {
                SeedData(scope.ServiceProvider.GetRequiredService<AppDbContext>());
            }
        }

        private static void SeedData(AppDbContext context)
        {
            if(!context.Platforms.Any())
            {
                Console.WriteLine("--> Seeding data.");
                
                context.Platforms.AddRange
                (
                    new () { Name = "Dot net", Publisher = "Microsoft", Cost = "Free" },
                    new () { Name = "Sql Server", Publisher = "Microsoft", Cost = "Free" },
                    new () { Name = "Kubernetes", Publisher = "Cloud Native Computing Foundation", Cost = "Free" }
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