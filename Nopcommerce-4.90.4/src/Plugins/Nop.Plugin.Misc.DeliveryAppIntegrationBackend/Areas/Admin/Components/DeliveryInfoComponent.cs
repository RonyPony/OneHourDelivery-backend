using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Areas.Admin.Factories;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Helpers;
using Nop.Web.Framework.Components;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Areas.Admin.Components
{
    /// <summary>
    /// Represents a component for the delivery information.
    /// </summary>
    [ViewComponent(Name = "DeliveryInfo")]
    public sealed class DeliveryInfoComponent : NopViewComponent
    {
        private readonly IDeliveryAppBaseAdminModelFactory _deliveryAppBaseAdminModelFactory;

        /// <summary>
        /// Initializes a new instance of <see cref="DeliveryInfoComponent"/>.
        /// </summary>
        /// <param name="deliveryAppBaseAdminModelFactory">An implementation of <see cref="IDeliveryAppBaseAdminModelFactory"/>.</param>
        public DeliveryInfoComponent(
            IDeliveryAppBaseAdminModelFactory deliveryAppBaseAdminModelFactory)
        {
            _deliveryAppBaseAdminModelFactory = deliveryAppBaseAdminModelFactory;
        }

        /// <summary>
        /// Invokes the view component.
        /// </summary>
        /// <param name="orderId">An order id.</param>
        /// <returns>An implementation of <see cref="IViewComponentResult"/>.</returns>
        public IViewComponentResult Invoke(int orderId)
        {
            var model = _deliveryAppBaseAdminModelFactory.PrepareOrderDeliveryInfoModel(orderId);
            if (model is null) return Content(string.Empty);
            return View($"/{Defaults.OutputDir}/Areas/Admin/Views/DeliveryAppApiAdmin/_OrderDetails.DeliveryInfo.cshtml", model);
        }
    }
}
