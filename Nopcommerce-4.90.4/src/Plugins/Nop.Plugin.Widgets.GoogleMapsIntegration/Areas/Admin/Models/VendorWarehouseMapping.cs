using Nop.Core;

namespace Nop.Plugin.Widgets.GoogleMapsIntegration.Areas.Admin.Models
{
    /// <summary>
    /// Represents the association between a vendor and a warehouse.
    /// </summary>
    public partial class VendorWarehouseMapping : BaseEntity
    {
        /// <summary>
        /// Indicatest the vendor id.
        /// </summary>
        public int VendorId { get; set; }

        /// <summary>
        /// Indicates the warehouse id.
        /// </summary>
        public int WarehouseId { get; set; }
    }
}
