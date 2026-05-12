using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.contracts;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Services
{
    /// <summary>
    /// Represents a contract for the delivery app shipping services.
    /// </summary>
    public interface IDeliveryAppShippingService
    {
        /// <summary>
        /// Retrieves a shipping method name by distance between a vendor warehouse address and a geo-coordinate.
        /// </summary>
        /// <param name="vendorId">The vendor id for the vendor assigned to the warehouse from where the delivery starts.</param>
        /// <param name="toLatitude">The destination latitude.</param>
        /// <param name="toLongitude">The destination longitude.</param>
        /// <returns>An <see cref="string"/> with the corresponding shipping method name.</returns>
        IOperationResult<DeliveryAppShippingInfo> GetShippingMethodNameByAddressesDistance(int vendorId, decimal toLatitude, decimal toLongitude);
    }
}
