using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Areas.Admin.Factories;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Helpers;
using Nop.Web.Framework.Components;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Areas.Admin.Components
{
    /// <summary>
    /// Represents a component for the payment collection information.
    /// </summary>
    [ViewComponent(Name = "PaymentCollection")]
    public sealed class PaymentCollectionComponent : NopViewComponent
    {
        private readonly IDeliveryAppBaseAdminModelFactory _deliveryAppBaseAdminModelFactory;

        /// <summary>
        /// Initializes a new instance of <see cref="PaymentCollectionComponent"/>.
        /// </summary>
        /// <param name="deliveryAppBaseAdminModelFactory">An implementation of <see cref="IDeliveryAppBaseAdminModelFactory"/>.</param>
        public PaymentCollectionComponent(
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
            var model = _deliveryAppBaseAdminModelFactory.PrepareOrderPaymentCollectionModel(orderId);
            if (model is null) return Content(string.Empty);
            return View($"/{Defaults.OutputDir}/Areas/Admin/Views/DeliveryAppApiAdmin/_OrderDetails.PaymentCollection.cshtml", model);
        }
    }
}
