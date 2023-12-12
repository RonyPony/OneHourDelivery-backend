namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models
{
    /// <summary>
    /// Represent the data to update an order status when store cancelled it
    /// </summary>
    public sealed class DeclinedOrderByStore
    {
        /// <summary>
        /// vendor id of the store
        /// </summary>
        public int VendorId { get; set; }
        /// <summary>
        /// message to declined the order
        /// </summary>
        public string Message { get; set; }
    }
}
