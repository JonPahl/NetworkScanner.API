using Microsoft.Extensions.DependencyInjection;

namespace NetworkScanner.Infrastructure
{
    public static class StartupSetup
    {
        public static void AddDbContext(this IServiceCollection services, string connStr)
        {
            //services.AddDbContext<AppDbContext>(options => options.UseSqlite(connStr)); // will be created in web project root

            //services.AddDbContext<CongregationContext>(options 
            //  => options.UseSqlServer(connStr));
        }

    }
}
