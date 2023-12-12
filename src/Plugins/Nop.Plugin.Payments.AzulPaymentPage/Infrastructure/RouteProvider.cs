using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Nop.Plugin.Payments.AzulPaymentPage.Helpers;
using Nop.Web.Framework;
using Nop.Web.Framework.Mvc.Routing;

namespace Nop.Plugin.Payments.AzulPaymentPage.Infrastructure
{
    /// <summary>
    /// Represents plug-in route provider
    /// </summary>
    public sealed class RouteProvider : IRouteProvider
    {
        /// <summary>
        /// Register routes
        /// </summary>
        /// <param name="endpointRouteBuilder">Route builder</param>
        public void RegisterRoutes(IEndpointRouteBuilder endpointRouteBuilder)
        {
            endpointRouteBuilder.MapControllerRoute(DefaultsInfo.ConfigurationRouteName, "Plugins/AzulPaymentPage/Configure",
                new { controller = "AzulPayment", action = "Configure", area = AreaNames.Admin });

            endpointRouteBuilder.MapControllerRoute(DefaultsInfo.TransactionResultRouteName, "azul-payment-page/transaction-details",
                new { controller = "AzulPayment", action = "TransactionDetails" });
        }

        /// <summary>
        /// Gets a priority of route provider
        /// </summary>
        public int Priority => 0;
    }
}
