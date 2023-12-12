using Nop.Core;

namespace Nop.Plugin.Synchronizers.SAPProducts.Domains
{
    /// <summary>
    /// Represents the relation between a manufacturer and a SAP manufacturer.
    /// </summary>
    public sealed class ManufacturerSapManufacturerMapping : BaseEntity
    {
        /// <summary>
        /// Indicates the manufacturer id.
        /// </summary>
        public int ManufacturerId { get; set; }

        /// <summary>
        /// Indicates the SAP manufacturer code.
        /// </summary>
        public int SapManufacturerCode { get; set; }
    }
}
