using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nop.Core.Infrastructure;
using Nop.Plugin.Widgets.GoogleMapsIntegration.Factories;
using Nop.Plugin.Widgets.GoogleMapsIntegration.Services;

namespace Nop.Plugin.Widgets.GoogleMapsIntegration.Infrastructure
{
    /// <summary>
    /// Plug-in dependency registrar.
    /// </summary>
    public class DependencyRegistrar : INopStartup
    {
        /// <summary>
        /// Registers plugin dependencies.
        /// </summary>
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAddressGeoCoordinatesService, AddressGeoCoordinatesService>();
            services.AddScoped<ICheckoutModelCustomFactory, CheckoutModelCustomFactory>();
        }

        /// <summary>
        /// Configures plugin middleware.
        /// </summary>
        public void Configure(IApplicationBuilder application)
        {
        }

        /// <summary>
        /// Represents the display order.
        /// </summary>
        public int Order => 1;
    }
}
