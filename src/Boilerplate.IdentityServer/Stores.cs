using System;
using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Boilerplate.IdentityServer
{
    public static class Stores
    {
        public static void Initialize(IServiceProvider services)
        {
            var persistentGrantContext = services.GetRequiredService<PersistedGrantDbContext>();
            persistentGrantContext.Database.Migrate();

            var configurationContext = services.GetRequiredService<ConfigurationDbContext>();
            configurationContext.Database.Migrate();
        }
    }
}
