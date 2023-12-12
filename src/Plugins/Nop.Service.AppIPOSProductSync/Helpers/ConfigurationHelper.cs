using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nop.Service.AppIPOSSync.Clients;
using Nop.Service.AppIPOSSync.Entities;
using System;

namespace Nop.Service.AppIPOSSync.Helpers
{
    /// <summary>
    /// Class used to get application configuration
    /// </summary>
    public static class ConfigurationHelper
    {
        /// <summary>
        /// Gets configuration from appsettings.json
        /// </summary>
        public static IConfiguration GetConfiguration()
        {
            return new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();
        }

        /// <summary>
        /// Configures services for dependency injection
        /// </summary>
        /// <param name="services">Service collection from service provider</param>
        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            return services.AddDbContext<AppIposContext>(options =>
                {
                    options.UseSqlServer(GetConfiguration().GetConnectionString("DefaultConnection"));
                })
                .AddSingleton<Service>()
                .AddScoped<CategoryClient>()
                .AddScoped<ManufacturerClient>()
                .AddScoped<ProductClient>()
                .AddScoped<OrderClient>()
                .AddScoped<CustomerClient>();
        }
    }
}