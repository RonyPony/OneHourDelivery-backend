using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Nop.Plugin.Payments.Banrural.Helpers;
using Nop.Web.Framework;
using Nop.Web.Framework.Mvc.Routing;

namespace Nop.Plugin.Payments.Banrural.Infrastructure
{
    /// <summary>
    /// Represents plug-in route provider
    /// </summary>
    public sealed class RouteProvider : IRouteProvider
    {
        /// <summary>
        /// Gets a priority of route provider
        /// </summary>
        public int Priority => 0;

        /// <summary>
        /// Register routes
        /// </summary>
        /// <param name="endpointRouteBuilder">Route builder</param>
        public void RegisterRoutes(IEndpointRouteBuilder endpointRouteBuilder)
        {
            endpointRouteBuilder.MapControllerRoute(DefaultsInfo.ConfigurationRouteName, "Plugins/Banrural/Configure",
               new { controller = "Banrural", action = "Configure", area = AreaNames.Admin });

            endpointRouteBuilder.MapControllerRoute(DefaultsInfo.TransactionResultRouteName, "banrural/transaction-details",
                new { controller = "Banrural", action = "TransactionDetails" });

            endpointRouteBuilder.MapControllerRoute(DefaultsInfo.CompletedRouteName, "banrural/completed",
                new { controller = "Banrural", action = "Completed" });
        }
    }
}
