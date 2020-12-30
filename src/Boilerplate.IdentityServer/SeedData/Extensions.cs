using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using IdentityServer4.Models;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using static IdentityServer4.IdentityServerConstants;

using Boilerplate.IdentityServer.Models;

namespace Boilerplate.IdentityServer.SeedData
{
    public static class Extensions
    {
        public static void SeedConfiguration(this ConfigurationDbContext context, ILogger logger)
        {
            // Add API scopes
            var apiScopes = new List<ApiScope>
            {
                new ApiScope("api-1", "API 1"),
            };

            foreach (var x in apiScopes)
            {
                if (!context.ApiScopes.Any(y => y.Name == x.Name))
                {
                    logger.LogInformation("Adding scope: {scope}", x.Name);
                    context.ApiScopes.Add(x.ToEntity());
                    context.SaveChanges();
                }
            }

            // Add Identity Resources
            var identityResources = new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };

            foreach (var x in identityResources)
            {
                if (!context.IdentityResources.Any(y => y.Name == x.Name))
                {
                    logger.LogInformation("Adding identity resource: {identityResource}", x.Name);
                    context.IdentityResources.Add(x.ToEntity());
                    context.SaveChanges();
                }
            }

            // Add Clients
            var clients = new List<Client>
            {
                // Machine to Machine (m2m) client
                new Client
                {
                    ClientId = "m2m-client",
                    ClientSecrets = { new Secret("secret".Sha256()) },

                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    AllowedScopes = { "api-1" },
                },

                // Interactive Client
                new Client
                {
                    ClientId = "interactive-client",
                    ClientSecrets = { new Secret("secret".Sha256()) },

                    AllowedGrantTypes = GrantTypes.Code,

                    RedirectUris = { "https://localhost:5002/signin-oidc" },
                    PostLogoutRedirectUris = { "https://localhost:5002/signout-callback-oidc" },

                    AllowedScopes = new List<string>
                    {
                        StandardScopes.OpenId,
                        StandardScopes.Profile,
                        "api-1",
                    }
                }
            };

            foreach (var x in clients)
            {
                if (!context.Clients.Any(y => y.ClientId == x.ClientId))
                {
                    logger.LogInformation("Adding client: {client}", x.ClientId);
                    context.Clients.Add(x.ToEntity());
                    context.SaveChanges();
                }
            }
        }

        public static void SeedUsers(this ApplicationDbContext context, UserManager<ApplicationUser> userManager, ILogger logger)
        {
            var users = new List<ApplicationUser>
            {
                new ApplicationUser
                {
                    UserName = "alice",
                    Email = "AliceSmith@email.com",
                    EmailConfirmed = true,
                },
                new ApplicationUser
                {
                    UserName = "bob",
                    Email = "BobSmith@email.com",
                    EmailConfirmed = true
                },
            };

            foreach (var user in users)
            {
                if (userManager.FindByNameAsync(user.UserName).Result == null)
                {
                    logger.LogInformation("Creating user: {user}", user.UserName);

                    var result = userManager.CreateAsync(user, "Pass123$").Result;

                    if (!result.Succeeded)
                    {
                        logger.LogError("Failed to create user: {user}, {@errors}", user.UserName, result.Errors);
                        throw new Exception($"Failed to create user: {user.UserName}");
                    }
                }
            }
        }
    }
}
