namespace Nop.Plugin.Payments.CardNet.Models
{
    /// <summary>
    /// Error model used by <see cref="CardNetPurchaseResult"/>
    /// </summary>
    public sealed class CardNetErrorModel
    {
        /// <summary>
        /// CardNet error code
        /// </summary>
        public string ErrorCode { get; set; }

        /// <summary>
        /// CardNet error message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// CardNet error details
        /// </summary>
        public string Detail { get; set; }
    }
}
