using LinqToDB.Mapping;
using Nop.Core;

namespace Nop.Plugin.Payments.Banrural.Models
{
    /// <summary>
    /// Represents a log entry from the Banrural transaction log.
    /// </summary>
    public sealed class BanruralTransactionLog : BaseEntity
    {
        /// <summary>
        /// Transaction reference number.
        /// </summary>
        [Nullable]
        public string ReferenceNumber { get; set; }

        /// <summary>
        /// Description of the transaction.
        /// </summary>
        [Nullable]
        public string Description { get; set; }

        /// <summary>
        /// UUID of the transaction.
        /// </summary>
        [Nullable]
        public string Uuid { get; set; }

        /// <summary>
        /// Currency in which the transaction was done.
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// Tax amount paid in the transaction.
        /// </summary>
        [Nullable]
        public decimal TaxAmount { get; set; }

        /// <summary>
        /// Amount paid in the transaction, does not include taxes.
        /// </summary>
        [Nullable]
        public decimal Amount { get; set; }

        /// <summary>
        /// Email of the customer which executed the transaction.
        /// </summary>
        public string CustomerEmail { get; set; }

        /// <summary>
        /// Order number.
        /// </summary>
        public string Order { get; set; }

        /// <summary>
        /// Id of the transaction.
        /// </summary>
        [Nullable]
        public string TransactionId { get; set; }

        /// <summary>
        /// Determines whether transaction was successful or not.
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Full exception message (if any).
        /// </summary>
        [Nullable]
        public string FullException { get; set; }
    }
}
