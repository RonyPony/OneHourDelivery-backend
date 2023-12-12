using Nop.Core;

namespace Nop.Plugin.Synchronizers.CommonERPTables.Domains
{
    /// <summary>
    /// Represents the entity used for mapping the orders between an ERP and nopCommerce
    /// </summary>
    public sealed class ErpOrdersNopCommerceOrdersMapping : BaseEntity
    {
        /// <summary>
        /// Represents the order id at the nopCommerce
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// Represents the order id at the ERP
        /// </summary>
        public string ErpOrderId { get; set; }
    }
}
