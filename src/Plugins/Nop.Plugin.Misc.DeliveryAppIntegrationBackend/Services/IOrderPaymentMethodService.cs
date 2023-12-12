using Nop.Core.Domain.Orders;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Services
{
    /// <summary>
    /// Represents a contract for order payment method services.
    /// </summary>
    public interface IOrderPaymentMethodService
    {
        /// <summary>
        ///  Retrives the payment method for the order.
        /// </summary>
        /// <param name="order">An instance of <see cref="Order"/>.</param>
        /// <returns>The name of the payment method of the order or <see cref="string.Empty"/>.</returns>
        string GetOrderPaymentMethodName(Order order);
    }
}
