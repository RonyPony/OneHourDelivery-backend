using Nop.Core;

namespace Nop.Plugin.Synchronizers.CommonERPTables.Domains
{
    /// <summary>
    /// Represents the entity used for mapping the clients between an ERP and nopCommerce
    /// </summary>
    public sealed class ErpCustomersNopCommerceCustomersMapping : BaseEntity
    {
        /// <summary>
        /// Represents the customer id at the nopCommerce
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Represents the customer id at the ERP
        /// </summary>
        public string ErpCustomerId { get; set; }
    }
}
