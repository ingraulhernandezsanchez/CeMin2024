using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CeMin2024.Server.Options
{
    public static class OptionsConfiguration
    {
        public static IServiceCollection AddServerOptions(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<CorsOptions>(
                configuration.GetSection("Cors"));

            services.Configure<DatabaseOptions>(
                configuration.GetSection("DatabaseOptions"));

            return services;
        }
    }
}