using Microsoft.AspNetCore.Mvc;
using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Payments;
using Nop.Services.Orders;
using Nop.Web.Framework.Components;
using Nop.Web.Models.Checkout;

namespace Nop.Plugin.Payments.CyberSource.Components
{
    /// <summary>
    /// A view component for payment status message on checkout completed page.
    /// </summary>
    [ViewComponent(Name = "CheckoutCompletedPaymentStatus")]
    public sealed class CheckoutCompletedPaymentStatusViewComponent : NopViewComponent
    {
        private readonly IOrderService _orderService;

        /// <summary>
        /// Initializes a new iunstance of <see cref="CheckoutCompletedPaymentStatusViewComponent"/>.
        /// </summary>
        /// <param name="orderService">An implemenbtation of <see cref="IOrderService"/>.</param>
        public CheckoutCompletedPaymentStatusViewComponent(IOrderService orderService)
        {
            _orderService = orderService;
        }

        /// <summary>
        /// Invokes the view component.
        /// </summary>
        /// <returns>An implementation of <see cref="IViewComponentResult"/>.</returns>
        public IViewComponentResult Invoke(CheckoutCompletedModel additionalData)
        {
            Order order = _orderService.GetOrderById(additionalData.OrderId);

            if (order is null || order.PaymentStatus == PaymentStatus.Paid)
                return Content(string.Empty);

            return View("~/Plugins/Payments.CyberSource/Views/CheckoutCompletedPaymentStatus.cshtml", order);
        }
    }
}
