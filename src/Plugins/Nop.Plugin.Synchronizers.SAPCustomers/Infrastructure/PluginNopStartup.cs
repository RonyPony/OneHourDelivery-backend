using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nop.Core.Infrastructure;

namespace Nop.Plugin.Synchronizers.SAPCustomers.Infrastructure
{
    /// <summary>
    /// Represents the class responsible for the startup configuration of this plugin.
    /// </summary>
    public sealed class PluginNopStartup : INopStartup
    {
        void INopStartup.ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.ViewLocationExpanders.Add(new ViewLocationExpander());
            });
        }

        void INopStartup.Configure(IApplicationBuilder application) { }

        /// <summary>
        /// Represents the display order.
        /// </summary>
        public int Order => 11;
    }
}