namespace Nop.Plugin.Payments.CardNet.Models
{
    /// <summary>
    /// Contains information about CardNet transaction status and approval code
    /// </summary>
    public sealed class CardNetTransactionModel
    {
        /// <summary>
        /// CardNet transaction Id
        /// </summary>
        public int? TransactionId { get; set; }

        /// <summary>
        /// CardNet transaction status Id
        /// </summary>
        public int? TransactionStatusId { get; set; }

        /// <summary>
        /// CardNet transaction status text
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// CardNet approval code
        /// </summary>
        public string ApprovalCode { get; set; }

    }
}
