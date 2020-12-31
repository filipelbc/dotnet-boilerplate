using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Samples.VanillaWebClient
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
                    webBuilder.UseStartup<Startup>()
                        .UseWebRoot("public")
                        .UseUrls("https://localhost:5100");
                });
    }
}
