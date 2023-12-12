using System.Text.Json.Serialization;

namespace Nop.Plugin.Payments.Banrural.Models
{
    /// <summary>
    /// Model received from Banrural Callback webhook.
    /// </summary>
    public sealed class BanruralCallbackResponseModel
    {
        /// <summary>
        /// Transaction reference number.
        /// </summary>
        public string Ref { get; set; }

        /// <summary>
        /// Description of the transaction.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// UUID of the transaction.
        /// </summary>
        public string Uuid { get; set; }

        /// <summary>
        /// Currency in which the transaction was done.
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// Tax amount paid in the transaction.
        /// </summary>
        public decimal tax_amount { get; set; }

        /// <summary>
        /// Amount paid in the transaction, does not include taxes.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Email of the customer which executed the transaction.
        /// </summary>
        public string customer_email { get; set; }

        /// <summary>
        /// Order number.
        /// </summary>
        public string Order { get; set; }

        /// <summary>
        /// Id of the transaction.
        /// </summary>
        public string transaction_id { get; set; }
    }
}
