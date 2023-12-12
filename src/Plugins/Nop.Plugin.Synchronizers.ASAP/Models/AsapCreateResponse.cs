namespace Nop.Plugin.Synchronizers.ASAP.Models
{
    /// <summary>
    /// Represent the response of ASAP service Api for create a order.
    /// </summary>
    public sealed class AsapCreateResponse
    {
        /// <summary>
        /// Represent the status of response.
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// Represent the error message of response.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Represent the result of response.
        /// </summary>
        public AsapDelivery Result { get; set; }
    }
}
