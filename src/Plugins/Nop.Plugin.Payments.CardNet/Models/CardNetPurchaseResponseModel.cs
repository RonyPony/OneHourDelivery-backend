namespace Nop.Plugin.Payments.CardNet.Models
{
    /// <summary>
    /// Response model used in <see cref="CardNetPurchaseResult"/>
    /// </summary>
    public sealed class CardNetPurchaseResponseModel
    {
        /// <summary>
        /// CardNet purchase Id
        /// </summary>
        public int? PurchaseId { get; set; }

        /// <summary>
        /// NopCommerce invoice (order) number
        /// </summary>
        public string Order { get; set; }

        /// <summary>
        /// <see cref="CardNetTransactionModel"/>
        /// </summary>
        public CardNetTransactionModel Transaction { get; set; }

        /// <summary>
        /// Processed transaction amount
        /// </summary>
        public int? Amount { get; set; }

        /// <summary>
        /// Currency in which the transaction was processed
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// Response description
        /// </summary>
        public string Description { get; set; }
    }
}
