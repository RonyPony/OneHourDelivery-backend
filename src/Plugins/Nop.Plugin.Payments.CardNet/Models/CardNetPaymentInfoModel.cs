namespace Nop.Plugin.Payments.CardNet.Models
{
    /// <summary>
    /// Model used by PaymentInfo.cshtml
    /// </summary>
    public sealed class CardNetPaymentInfoModel
    {
        /// <summary>
        /// Formatted order total
        /// </summary>
        public string OrderTotal { get; set; }
    }
}
