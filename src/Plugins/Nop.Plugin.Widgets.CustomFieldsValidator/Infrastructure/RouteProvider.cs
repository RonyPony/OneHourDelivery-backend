using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Nop.Plugin.Widgets.CustomFieldsValidator.Helpers;
using Nop.Web.Framework;
using Nop.Web.Framework.Mvc.Routing;

namespace Nop.Plugin.Widgets.CustomFieldsValidator.Infrastructure
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
                new { controller = "FieldValidator", action = "Register"});
            endpointRouteBuilder.MapControllerRoute("CustomerInfo", "info/",
                new { controller = "FieldValidator", action = "Info" });

        }

        /// <summary>
        /// Gets a priority of route provider
        /// </summary>
        public int Priority => 1;
    }
}
