using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Nop.Plugin.Synchronizers.SAPProducts.Helpers;
using Nop.Web.Framework;
using Nop.Web.Framework.Mvc.Routing;

namespace Nop.Plugin.Synchronizers.SAPProducts.Infrastructure
{
    /// <summary>
    /// Represents the plugin route provider.
    /// </summary>
    public sealed class RouteProvider : IRouteProvider
    {
        private const int PRIORITY = 1;

        /// <summary>
        /// Register routes.
        /// </summary>
        /// <param name="endpointRouteBuilder">Route builder.</param>
        public void RegisterRoutes(IEndpointRouteBuilder endpointRouteBuilder)
        {
            endpointRouteBuilder.MapControllerRoute(ProductSyncDefaults.ConfigurationRouteName,
                "Plugins/Synchronizers/SAPProduct/Configure",
                new { controller = "SAPProductSynchronizer", action = "Configure", area = AreaNames.Admin });
        }

        /// <summary>
        /// Gets a priority of route provider.
        /// </summary>
        public int Priority => PRIORITY;
    }
}