using Nop.Core;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains
{
    /// <summary>
    /// Represents the relation between a warehouse and it's creator.
    /// </summary>
    public partial class WarehouseUserCreatorMapping : BaseEntity
    {
        /// <summary>
        /// Indicates the warehouse id.
        /// </summary>
        public int WarehouseId { get; set; }

        /// <summary>
        /// Indicates the customer id of the customer that created the warehouse.
        /// </summary>
        public int CustomerId { get; set; }
    }
}
