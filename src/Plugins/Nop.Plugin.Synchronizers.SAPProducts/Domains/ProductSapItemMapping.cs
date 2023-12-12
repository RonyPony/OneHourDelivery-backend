using Nop.Core;

namespace Nop.Plugin.Synchronizers.SAPProducts.Domains
{
    /// <summary>
    /// Represents the relation between a product and a SAP item.
    /// </summary>
    public sealed class ProductSapItemMapping : BaseEntity
    {
        /// <summary>
        /// Indicates the product id.
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Indicates the SAP item code.
        /// </summary>
        public string SapItemCode { get; set; }
    }
}
