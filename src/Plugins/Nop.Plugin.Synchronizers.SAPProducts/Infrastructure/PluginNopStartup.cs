using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nop.Core.Infrastructure;

namespace Nop.Plugin.Synchronizers.SAPProducts.Infrastructure
{
    /// <summary>
    /// Represents the class responsible for the startup configuration of this plugin.
    /// </summary>
    public sealed class PluginNopStartup : INopStartup
    {
        /// <summary>
        /// Add and configure any of the middleware.
        /// </summary>
        /// <param name="services">Collection of service descriptors.</param>
        /// <param name="configuration">Configuration of the application.</param>
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.ViewLocationExpanders.Add(new ViewLocationExpander());
            });
        }

        /// <summary>
        /// Configure the using of added middleware.
        /// </summary>
        /// <param name="application">Builder for configuring an application's request pipeline.</param>
        public void Configure(IApplicationBuilder application) { }

        /// <summary>
        /// Represents the display order.
        /// </summary>
        public int Order => 1;
    }
}