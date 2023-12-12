using Microsoft.AspNetCore.Mvc;
using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Shipping;
using Nop.Data;
using Nop.Services.Orders;
using Nop.Web.Areas.Admin.Models.Orders;
using Nop.Web.Framework.Components;
using Nop.Web.Framework.Infrastructure;

namespace Nop.Plugin.Synchronizers.ASAP.Areas.Admin.Components
{
    /// <summary>
    /// Shipment Button View Component
    /// </summary>
    [ViewComponent(Name = "CustomAdminWidget")]
    public partial class AdminWidgetViewComponent : NopViewComponent
    {
        private readonly IOrderService _orderService;
        /// <summary>
        /// An implementation of <see cref="AdminWidgetViewComponent"/>
        /// </summary>
        /// <param name="orderService">An implementation of <see cref="IOrderService"/></param>
        public AdminWidgetViewComponent(IOrderService orderService)
        {
            _orderService = orderService;
        }
        /// <summary>
        /// Invokes the view as the condition is met
        /// </summary>
        /// <param name="widgetZone">An implementation of <see cref="IWidgetRepository"/></param>
        /// <param name="additionalData">An implementation of <see cref="additionalData"/></param>
        /// <returns>An implementation of <see cref="IViewComponentResult"/></returns>
        public IViewComponentResult Invoke(string widgetZone, ShipmentModel additionalData)
        {
            Order order = _orderService.GetOrderById(additionalData.OrderId);

            if (widgetZone.Equals(AdminWidgetZones.OrderShipmentDetailsButtons)
                && order.ShippingStatus == ShippingStatus.NotYetShipped
                && order.ShippingRateComputationMethodSystemName.Equals(AsapDeliveryDefaults.SystemName))
            {
                return View("~/Plugins/Synchronizers.ASAP/Areas/Admin/Views/Order/_OrderShipmentDetailsButtons.cshtml", additionalData);
            }

            return Content(string.Empty);
        }
    }
}
