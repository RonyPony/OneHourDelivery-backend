using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Nop.Plugin.Synchronizers.SAPCustomers.Helpers;
using Nop.Web.Framework;
using Nop.Web.Framework.Mvc.Routing;

namespace Nop.Plugin.Synchronizers.SAPCustomers.Infrastructure
{
    /// <summary>
    /// Represents plug-in route provider
    /// </summary>
    public sealed class RouteProvider : IRouteProvider
    {
        private const int PRIORITY = 1;

        void IRouteProvider.RegisterRoutes(IEndpointRouteBuilder endpointRouteBuilder)
        {
            endpointRouteBuilder.MapControllerRoute(DefaultsInfo.ConfigurationRouteName, "Plugins/Synchronizers/SAPCustomers/Configure",
                new { controller = "SAPCustomerSynchronizer", action = "Configure", area = AreaNames.Admin });
        }

        int IRouteProvider.Priority => PRIORITY;
    }
}
