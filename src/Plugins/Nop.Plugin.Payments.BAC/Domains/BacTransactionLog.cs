using LinqToDB.Mapping;
using Nop.Core;
using System;

namespace Nop.Plugin.Payments.BAC.Domains
{
    /// <summary>
    /// Entity used to store BAC transaction history/information on the database
    /// </summary>
    public class BacTransactionLog : BaseEntity
    {
        /// <summary>
        /// NopCommerce order id/number that corresponds to the BAC transaction
        /// </summary>
        public int? OrderId { get; set; }

        /// <summary>
        /// Transaction's reason code received from BAC after processing the request.
        /// </summary>
        [Nullable]
        public int ReasonCode { get; set; }
        
        /// <summary>
        /// Transaction's response code received from BAC after processing the request.
        /// </summary>
        [Nullable]
        public int ResponseCode { get; set; }

        /// <summary>
        /// Full error message.
        /// </summary>
        [Nullable]
        public string FullException { get; set; }

        /// <summary>
        /// Represents when the entry log was created.
        /// </summary>
        public DateTime DateLogged { get; set; }
    }
}
