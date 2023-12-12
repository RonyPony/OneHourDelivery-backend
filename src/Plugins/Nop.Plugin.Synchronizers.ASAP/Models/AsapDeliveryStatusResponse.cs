namespace Nop.Plugin.Synchronizers.ASAP.Models
{
    /// <summary>
    /// Represent the response of ASAP service Api for get the last status by order.
    /// </summary>
    public sealed class AsapDeliveryStatusResponse
    {
        /// <summary>
        /// Represent the status of response.
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// Represent the status of a order in ASAP Service.
        /// </summary>
        public int Delivery_Status { get; set; }

        /// <summary>
        /// Represent the last date that review the status by order.
        /// </summary>
        public string Updated_At { get; set; }

        /// <summary>
        /// Represent the error message of response.
        /// </summary>
        public string Message { get; set; }
    }
}
