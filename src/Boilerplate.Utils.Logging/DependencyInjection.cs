using System;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Boilerplate.Utils.Logging
{
    public static class DependencyInjection
    {
        public static IHostBuilder ConfigureLogging(this IHostBuilder builder)
        {
            return builder.UseSerilog(Configure);
        }

        static void Configure(HostBuilderContext hostBuilderContext, IServiceProvider serviceProvider, LoggerConfiguration loggerConfiguration)
        {
            loggerConfiguration
                .ReadFrom.Configuration(hostBuilderContext.Configuration);

            Serilog.Debugging.SelfLog.Enable(Console.Error);
        }
    }
}
