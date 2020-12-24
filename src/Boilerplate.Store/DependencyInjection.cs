using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace Boilerplate.Store
{
    public static class DependencyInjection
    {
        public static void AddStore(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<StoreContext>(options =>
                options
                    .UseNpgsql(configuration.GetConnectionString("StoreContext"))
            );
        }
    }
}
