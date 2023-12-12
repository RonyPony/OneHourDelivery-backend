using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nop.Core.Infrastructure;

namespace Nop.Plugin.Widgets.CustomFieldsValidator.Infrastructure
{
    /// <summary>
    /// Plug-in startup class
    /// </summary>
    
    public class PluginNopStartup : INopStartup
    {
        /// <inheritdoc/>
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.ViewLocationExpanders.Add(new ViewLocationExpander());
            });
        }

        /// <inheritdoc/>
        public void Configure(IApplicationBuilder application)
        {
        }

        /// <inheritdoc/>
        public int Order => 11;
    }
}