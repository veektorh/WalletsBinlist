using DemoApp.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoApp.Helper
{
    public static class DbHelper
    {
        public static async Task Migrate(this IApplicationBuilder app)
        {
            try
            {
                var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                await context.Database.MigrateAsync();
                await context.Database.EnsureCreatedAsync();



            }
            catch (Exception ex)
            {
                var logger = app.ApplicationServices.GetRequiredService<ILogger<AppDbContext>>();
                logger.LogError(ex, "An error occurred initializing DB.");

            }
        }
    }
}
