using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nop.Core.Infrastructure;

namespace Nop.Plugin.Synchronizers.CommonERPTables.Infrastructure
{
    /// <summary>
    /// Represents the class responsible for the startup configuration of this plugin.
    /// </summary>
    public class PluginNopStartup : INopStartup
    {
        /// <inheritdoc />
        void INopStartup.ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.ViewLocationExpanders.Add(new ViewLocationExpander());
            });
        }

        /// <inheritdoc />
        void INopStartup.Configure(IApplicationBuilder application) { }

        /// <summary>
        /// Represents order for this plugin.
        /// </summary>
        public int Order => 11;
    }
}