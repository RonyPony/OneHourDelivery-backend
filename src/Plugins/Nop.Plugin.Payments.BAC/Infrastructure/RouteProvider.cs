using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Nop.Plugin.Payments.BAC.Helpers;
using Nop.Web.Framework;
using Nop.Web.Framework.Mvc.Routing;

namespace Nop.Plugin.Payments.BAC.Infrastructure
{
    /// <summary>
    /// Represents plugin route provider
    /// </summary>
    public sealed class RouteProvider: IRouteProvider
    {
        /// <inheritdoc />
        void IRouteProvider.RegisterRoutes(IEndpointRouteBuilder endpointRouteBuilder)
        {
            endpointRouteBuilder.MapControllerRoute(DefaultsInfo.ConfigurationRouteName, "Plugins/BacPayment/Configure",
                new { controller = "BacPayment", action = "Configure", area = AreaNames.Admin });

            endpointRouteBuilder.MapControllerRoute(DefaultsInfo.TransactionResultRouteName, "BAC-payment/transaction-details",
                new { controller = "BacPayment", action = "TransactionDetails" });
        }

        /// <summary>
        /// Gets a priority of route provider
        /// </summary>
        public int Priority => 0;
    }
}
