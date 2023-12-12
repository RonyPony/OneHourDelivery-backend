using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Nop.Plugin.Synchronizers.CommonERPTables.Helpers;
using Nop.Web.Framework.Mvc.Routing;

namespace Nop.Plugin.Synchronizers.CommonERPTables.Infrastructure
{
    /// <summary>
    /// Represents plugin route provider
    /// </summary>
    public sealed class RouteProvider: IRouteProvider
    {
        /// <inheritdoc />
        void IRouteProvider.RegisterRoutes(IEndpointRouteBuilder endpointRouteBuilder)
        {
            endpointRouteBuilder.MapControllerRoute(DefaultsInfo.CustomersMappingRouteName, "erp/customers",
                new { controller = "ErpCustomers", action = "Save"});

            endpointRouteBuilder.MapControllerRoute(DefaultsInfo.OrdersMappingRouteName, "erp/orders",
                new { controller = "ErpOrders", action = "Save"});

            endpointRouteBuilder.MapControllerRoute(DefaultsInfo.ProductsMappingRouteName, "erp/products",
                new { controller = "ErpProducts", action = "Save"});
        }

        /// <summary>
        /// Gets a priority of route provider
        /// </summary>
        public int Priority => 0;
    }
}
