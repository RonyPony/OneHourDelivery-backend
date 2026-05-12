using Nop.Plugin.Api.DTO.Orders;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models
{
    /// <summary>
    /// Represents the Order distance model.
    /// </summary>
    public sealed class OrderDistanceModel
   {
        /// <summary>
        /// Indicates the order.
        /// </summary>
        public OrderDto Order { get; set; }

        /// <summary>
        /// Indicates the order distance.
        /// </summary>
        public double? Distance { get; set; }
   }
}
