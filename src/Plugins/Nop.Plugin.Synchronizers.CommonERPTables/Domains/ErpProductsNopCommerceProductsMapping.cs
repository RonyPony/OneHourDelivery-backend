using Nop.Core;

namespace Nop.Plugin.Synchronizers.CommonERPTables.Domains
{
    /// <summary>
    /// Represents the entity used for mapping the products between an ERP and nopCommerce
    /// </summary>
    public sealed class ErpProductsNopCommerceProductsMapping : BaseEntity
    {
        /// <summary>
        /// Represents the product id at the nopCommerce
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Represents the product id at the ERP
        /// </summary>
        public string ErpProductId { get; set; }
    }
}
