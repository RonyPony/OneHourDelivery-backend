namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models
{
    /// <summary>
    /// Represents a request to decline an order by a messenger.
    /// </summary>
    public sealed class DeclineOrderRequest
    {
        /// <summary>
        /// Indicates the decline reason.
        /// </summary>
        public string DeclineReason { get; set; }
    }
}
