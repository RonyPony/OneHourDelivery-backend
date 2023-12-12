using Nop.Core;

namespace Nop.Plugin.Widgets.ProductAvailability.Domains
{
    /// <summary>
    /// Represents the relation between nopCommerce warehouses and pickup points with DDP's physical stores.
    /// </summary>
    public sealed class WarehousePickupPointMapping : BaseEntity
    {
        /// <summary>
        /// Indicates the nopCommerce warehouse id.
        /// </summary>
        public int WarehouseId { get; set; }

        /// <summary>
        /// Indicates the nopCommerce pickup point id.
        /// </summary>
        public int PickupPointId { get; set; }

        /// <summary>
        /// Indicates the physical store cellar number.
        /// </summary>
        public string NumBodega { get; set; }

        /// <summary>
        /// Indicates the physical store number.
        /// </summary>
        public string NumTienda { get; set; }
    }
}
