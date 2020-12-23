using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetworkScanner.Infrastructure.Data;
using NetworkScanner.SharedKernel.Interfaces;

namespace NetworkScanner.Infrastructure
{
    public static class DependencyInjection
    {
        public static IConfigurationRoot Configuration;

        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfigurationRoot configuration)
        {
            Configuration = configuration;

            services.AddSingleton<IRepository, EfRepository>();
            services.AddTransient<NetworkContext>();
            return services;
        }
    }
}