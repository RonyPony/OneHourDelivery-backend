using Nop.Core;

namespace Nop.Plugin.Synchronizers.WAPIOrders.Domains
{
    /// <summary>
    /// Represents the relation between an order from NopCommerce and the transaction code of the same order but when registered via WAPI services.
    /// </summary>
    public sealed class OrderTransactionCodeMapping : BaseEntity
    {
        /// <summary>
        /// Indicates the NopCommerce order id.
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// Indicates the WAPI transaction code.
        /// </summary>
        public string TransactionCode { get; set; }
    }
}
