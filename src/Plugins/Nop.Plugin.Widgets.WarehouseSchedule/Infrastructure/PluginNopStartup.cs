using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nop.Core.Infrastructure;

namespace Nop.Plugin.Widgets.WarehouseSchedule.Infrastructure
{
    /// <summary>
    /// Start up of the plugin.
    /// </summary>
    public class PluginNopStartup : INopStartup
    {      
        
        /// <summary>
        ///<inheritdoc/>
        /// </summary>
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.ViewLocationExpanders.Add(new ViewLocationExpander());
            });
        }
        
        /// <summary>
        ///<inheritdoc/>
        /// </summary>
        public void Configure(IApplicationBuilder application)
        {
        }
        /// <summary>
        ///<inheritdoc/>
        /// </summary>
        public int Order => 11;
    }
}
