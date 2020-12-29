using System.Collections.Generic;
using System.Linq;
using IdentityServer4.Models;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;

namespace Boilerplate.IdentityServer.SeedData
{
    public static class Extensions
    {
        public static void SeedConfiguration(this ConfigurationDbContext context)
        {
            // Add clients
            var clients = new List<Client>
            {
                // Machine to Machine client
                new Client
                {
                    ClientId = "client",
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = { "api1" },
                },
            };

            if (!context.Clients.Any())
            {
                foreach (var x in clients)
                {
                    context.Clients.Add(x.ToEntity());
                }
                context.SaveChanges();
            }

            // Add Identity Resources
            var identityResources = new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };

            if (!context.IdentityResources.Any())
            {
                foreach (var x in identityResources)
                {
                    context.IdentityResources.Add(x.ToEntity());
                }
                context.SaveChanges();
            }

            // Add API scopes
            var apiScopes = new List<ApiScope>
            {
                new ApiScope("api1", "My API")
            };

            if (!context.ApiScopes.Any())
            {
                foreach (var x in apiScopes)
                {
                    context.ApiScopes.Add(x.ToEntity());
                }
                context.SaveChanges();
            }
        }
    }
}
