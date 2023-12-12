namespace Nop.Plugin.Synchronizers.ASAP.Models
{
    /// <summary>
    /// Represent the entity of ASAP service Api for get the last status by order.
    /// </summary>
    public sealed class AsapDeliveryStatus
    {
        /// <summary>
        /// Represent the status of a order in ASAP Service.
        /// </summary>
        public int DeliveryStatus { get; set; }

        /// <summary>
        /// Represent the date that is created a order.
        /// </summary>
        public string ShippedDate { get; set; }
    }
}
