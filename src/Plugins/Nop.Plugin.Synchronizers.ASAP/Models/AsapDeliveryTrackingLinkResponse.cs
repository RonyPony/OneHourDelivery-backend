namespace Nop.Plugin.Synchronizers.ASAP.Models
{
    /// <summary>
    /// Represent the response of ASAP service Api for get the tracking link by order.
    /// </summary>
    public sealed class AsapDeliveryTrackingLinkResponse
    {
        /// <summary>
        /// Represent the status of response.
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// Represent the tracking link.
        /// </summary>
        public string Tracking_Link { get; set; }

        /// <summary>
        /// Represent the error message of response.
        /// </summary>
        public string Message { get; set; }
    }
}
