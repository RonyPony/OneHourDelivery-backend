using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nop.Core.Infrastructure;
using Nop.Plugin.Widgets.WarehouseSchedule.Factories;
using Nop.Plugin.Widgets.WarehouseSchedule.Services;

namespace Nop.Plugin.Widgets.WarehouseSchedule.Infrastructure
{
    /// <summary>
    /// Plugin dependency registrar
    /// </summary>
    public class DependencyRegistrar : INopStartup
    {
        /// <summary>
        /// Register the plugin dependencies.
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        /// <param name="configuration">Configuration of the application</param>
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IWarehouseScheduleService, WarehouseScheduleService>();
            services.AddScoped<IWarehouseScheduleMappingFactory, WarehouseScheduleMappingFactory>();
        }

        /// <summary>
        /// Configure the using of added middleware
        /// </summary>
        /// <param name="application">Builder for configuring an application's request pipeline</param>
        public void Configure(IApplicationBuilder application)
        {
        }

        /// <summary>
        /// Gets order of this startup configuration implementation
        /// </summary>
        public int Order => 1;
    }
}
