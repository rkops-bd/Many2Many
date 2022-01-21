using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Many2Many
{
    public class Startup
    {
        private static IConfiguration _configuration;

        internal static IHost CreateHostBuilder(string[] args)
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();
            _configuration = builder.Build();

            var host = Host.CreateDefaultBuilder(args) // Initialising the Host 
                .ConfigureServices((context, services) =>
                { // Adding the DI container for configuration
                    ConfigureServices(services);
                })
                .Build(); // Build the Host

            return host;
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<MyContext>(options =>
                options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));
        }
    }
}
