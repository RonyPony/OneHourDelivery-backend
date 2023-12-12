using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nop.Core.Infrastructure;

namespace Nop.Plugin.Misc.ImportProduct.Infrastructure
{
    /// <summary>
    /// Represents the class responsible for the startup configuration of this plugin.
    /// </summary>
    public class PluginNopStartup : INopStartup
    {
        /// <summary>
        /// Add and configure any of the middleware.
        /// </summary>
        /// <param name="services">An implementation of <see cref="IServiceCollection"/>.</param>
        /// <param name="configuration">An implementation of <see cref="IConfiguration"/>.</param>
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
        public void Configure(IApplicationBuilder application)
        {
        }
        /// <summary>
        /// Represents the display order.
        /// </summary>
        public int Order => 11;
    }
}