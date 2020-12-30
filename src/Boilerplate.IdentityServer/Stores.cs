using System;
using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

using Boilerplate.IdentityServer.Models;
using Boilerplate.IdentityServer.SeedData;

namespace Boilerplate.IdentityServer
{
    public class Stores
    {
        public static void Initialize(IServiceProvider services)
        {
            var logger = services.GetRequiredService<ILogger<Stores>>();

            // Persited Grant
            {
                logger.LogInformation("Migrating: {store}", nameof(PersistedGrantDbContext));
                var persistentGrantContext = services.GetRequiredService<PersistedGrantDbContext>();
                persistentGrantContext.Database.Migrate();
            }

            // Configuration
            {
                logger.LogInformation("Migrating: {store}", nameof(ConfigurationDbContext));
                var configurationContext = services.GetRequiredService<ConfigurationDbContext>();
                configurationContext.Database.Migrate();

                configurationContext.SeedConfiguration(logger);
            }

            // Application
            {
                logger.LogInformation("Migrating: {store}", nameof(ApplicationDbContext));
                var applicationDbContext = services.GetRequiredService<ApplicationDbContext>();
                applicationDbContext.Database.Migrate();

                applicationDbContext.SeedUsers(services.GetRequiredService<UserManager<ApplicationUser>>(), logger);
            }
        }
    }
}
