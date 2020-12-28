using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

using Boilerplate.Store;
using static Boilerplate.Utils.Monitoring.HealthDelegates;
using static Boilerplate.Utils.Json.JsonConfiguration;

namespace Boilerplate.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddStore(Configuration);

            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    ConfigureJsonOptions(options.JsonSerializerOptions);
                });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Boilerplate.Api", Version = "v1" });
            });

            services
                .AddHealthChecks()
                .AddDbContextCheck<StoreContext>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Boilerplate.Api v1"));

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health", new HealthCheckOptions()
                {
                    ResponseWriter = BuildHealthResponseWriter(GetJsonConfiguration())
                });
            });
        }
    }
}
