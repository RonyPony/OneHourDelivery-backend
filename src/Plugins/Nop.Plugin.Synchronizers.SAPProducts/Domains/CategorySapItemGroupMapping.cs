using Nop.Core;

namespace Nop.Plugin.Synchronizers.SAPProducts.Domains
{
    /// <summary>
    /// Represents the relation between a category and a SAP item group.
    /// </summary>
    public sealed class CategorySapItemGroupMapping : BaseEntity
    {
        /// <summary>
        /// Indicates the category id.
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// Indicates the item group number.
        /// </summary>
        public int ItemGroupNumber { get; set; }
    }
}
