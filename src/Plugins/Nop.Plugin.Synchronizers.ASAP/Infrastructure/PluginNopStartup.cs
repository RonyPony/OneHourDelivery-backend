using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nop.Core.Infrastructure;

namespace Nop.Plugin.Synchronizers.ASAP.Infrastructure
{
    /// <summary>
    /// Plugin Nop Startup
    /// </summary>
    public class PluginNopStartup : INopStartup
    {
        /// <inheritdoc/>
        void INopStartup.ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.ViewLocationExpanders.Add(new ViewLocationExpander());
            });
        }

        /// <inheritdoc/>
        void INopStartup.Configure(IApplicationBuilder application)
        {
        }

        /// <inheritdoc/>
        int INopStartup.Order => 11;
    }
}