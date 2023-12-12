using System;
using LinqToDB.Mapping;
using Nop.Core;

namespace Nop.Plugin.Payments.CyberSource.Models
{
    /// <summary>
    /// Model used for logging entries to CyberSource transaction log
    /// </summary>
    public sealed class CyberSourceTransactionLog : BaseEntity
    {
        /// <summary>
        /// NopCommerce order id that corresponds to the CyberSource transaction
        /// </summary>
        public int? OrderId { get; set; }

        /// <summary>
        /// Id of the customer to which this order belongs
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Type of the card processed by CyberSource (Visa, MasterCard, etc.)
        /// </summary>
        [Nullable]
        public string CardType { get; set; }

        /// <summary>
        /// Transaction's amount processed by CyberSource
        /// </summary>
        [Nullable]
        public decimal? Amount { get; set; }

        /// <summary>
        /// Transaction's currency type processed by CyberSource
        /// </summary>
        [Nullable]
        public string Currency { get; set; }

        /// <summary>
        /// Id of the transaction processed by CyberSource
        /// </summary>
        [Nullable]
        public string TransactionId { get; set; }

        /// <summary>
        /// Uuid of the transaction processed by CyberSource
        /// </summary>
        [Nullable]
        public string TransactionUuid { get; set; }

        /// <summary>
        /// Reason code of the transaction processed by CyberSource
        /// </summary>
        [Nullable]
        public string ReasonCode { get; set; }

        /// <summary>
        /// Status of the transaction received from CyberSource
        /// </summary>
        [Nullable]
        public string Status { get; set; }

        /// <summary>
        /// Any message received from CyberSource
        /// </summary>
        [Nullable]
        public string Message { get; set; }

        /// <summary>
        /// Full error message
        /// </summary>
        [Nullable]
        public string FullException { get; set; }

        /// <summary>
        /// <see cref="DateTime"/> logged
        /// </summary>
        public DateTime DateLogged { get; set; }
    }
}
