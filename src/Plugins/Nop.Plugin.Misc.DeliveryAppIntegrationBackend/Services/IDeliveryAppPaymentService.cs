using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Helpers;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Services
{
    /// <summary>
    /// Represents a contract for the delivery order payment services.
    /// </summary>
    public interface IDeliveryAppPaymentService
    {
        /// <summary>
        /// Builds a payment form and returns it in a <see cref="RemotePost"/> object.
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        RemotePost GetPostForPaymentForm(int orderId);
    }
}
