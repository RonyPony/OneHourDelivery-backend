using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Nop.Web.Framework;
using Nop.Web.Framework.Mvc.Routing;

namespace Nop.Plugin.Misc.Alanube.Infrastructure;

/// <summary>
/// Represents the Alanube plugin route provider.
/// </summary>
public sealed class RouteProvider : IRouteProvider
{
    public void RegisterRoutes(IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.MapControllerRoute(
            name: AlanubeDefaults.ConfigurationRouteName,
            pattern: "Admin/Alanube/Configure",
            defaults: new { controller = "AlanubeAdmin", action = "Configure", area = AreaNames.ADMIN });

        endpointRouteBuilder.MapControllerRoute(
            name: AlanubeDefaults.CompaniesRouteName,
            pattern: "Admin/Alanube/Companies",
            defaults: new { controller = "AlanubeAdmin", action = "Companies", area = AreaNames.ADMIN });
        endpointRouteBuilder.MapControllerRoute(name: AlanubeDefaults.OfficesRouteName, pattern: "Admin/Alanube/Offices", defaults: new { controller = "AlanubeAdmin", action = "Offices", area = AreaNames.ADMIN });
        endpointRouteBuilder.MapControllerRoute(name: "Plugin.Alanube.DgiCatalogs", pattern: "Admin/Alanube/DgiCatalogs", defaults: new { controller = "AlanubeAdmin", action = "DgiCatalogs", area = AreaNames.ADMIN });
        endpointRouteBuilder.MapControllerRoute(name: "Plugin.Alanube.ProductMapping", pattern: "Admin/Alanube/ProductMapping/{productId}", defaults: new { controller = "AlanubeAdmin", action = "ProductMapping", area = AreaNames.ADMIN });
        endpointRouteBuilder.MapControllerRoute(name: AlanubeDefaults.WebhookRouteName, pattern: "plugins/alanube/webhooks/documents", defaults: new { controller = "AlanubeWebhook", action = "Documents" });
    }

    public int Priority => 0;
}
