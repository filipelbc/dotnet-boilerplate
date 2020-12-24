using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

using Boilerplate.Utils.Logging;

namespace Boilerplate.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        .UseStartup<Startup>()
                        .UseUrls("http://localhost:5000");
                })
                .ConfigureLogging();
    }
}
