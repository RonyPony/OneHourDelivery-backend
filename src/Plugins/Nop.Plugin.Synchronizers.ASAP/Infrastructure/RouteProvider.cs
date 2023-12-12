using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Nop.Web.Framework.Mvc.Routing;

namespace Nop.Plugin.Synchronizers.ASAP.Infrastructure
{
    /// <summary>
    /// Represents plug-in route provider
    /// </summary>
    public sealed class RouteProvider : IRouteProvider
    {
        private const int PRIORITY = 1;

        /// <inheritdoc/>
        void IRouteProvider.RegisterRoutes(IEndpointRouteBuilder endpointRouteBuilder)
        {
            endpointRouteBuilder.MapControllerRoute("Plugin.Synchronizers.ASAP.Configure", "Plugins/Synchronizers/ASAP/Configure",
                new { controller = "AsapDelivery", action = "Configure", area = "Admin" });
        }

        /// <inheritdoc/>
        int IRouteProvider.Priority => PRIORITY;
    }
}
