using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

using Boilerplate.Utils.Logging;

namespace Boilerplate.IdentityServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<Program>>();

                logger.LogInformation("Initializing stores");

                try
                {
                    Stores.Initialize(services);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred while initializing the IdentityServer stores.");
                    throw;
                }

                logger.LogInformation("Initialized stores");
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        .UseStartup<Startup>()
                        .UseUrls("http://localhost:5100");
                })
                .ConfigureLogging();
    }
}
