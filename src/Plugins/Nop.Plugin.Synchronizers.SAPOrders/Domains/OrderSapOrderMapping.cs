using Nop.Core;

namespace Nop.Plugin.Synchronizers.SAPOrders.Domains
{
    /// <summary>
    /// Represents the model used for creating the mapping table between Order entity and SapOrder entity.
    /// </summary>
    public sealed class OrderSapOrderMapping : BaseEntity
    {
        /// <summary>
        /// Gets or sets the order identifier
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// Gets or sets the SAP order identifier
        /// </summary>
        public string SapOrderId { get; set; }
    }
}