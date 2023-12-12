using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Nop.Web.Framework.Mvc.Routing;

namespace Nop.Plugin.Widgets.RegionsOnRegisterPage.Infrastructure
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
            endpointRouteBuilder.MapControllerRoute("Register", "register/",
                new { controller = "RegionsOnRegisterPage", action = "Register" });
            endpointRouteBuilder.MapControllerRoute("Info", "customer/info/",
                new { controller = "RegionsOnRegisterPage", action = "Info" });
        }

        /// <summary>
        /// Gets a priority of route provider
        /// </summary>
        public int Priority => 1;
    }
}