using System.Collections.Generic;

namespace Nop.Plugin.Widgets.ProductAvailability.Models.Inventory
{
    /// <summary>
    /// Represents the ProductAvailability StoreModel
    /// </summary>
    public sealed class StoreModel
    {
        /// <summary>
        /// Store Name
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// Product Price
        /// </summary>
        public decimal Precio { get; set; }

        /// <summary>
        /// Product Availability
        /// </summary>
        public IList<string> Existencia { get; set; }

        /// <summary>
        /// Indicates the store number from DDP.
        /// </summary>
        public string NumTienda { get; set; }

        /// <summary>
        /// Indicates the cellar number for DDP.
        /// </summary>
        public string NumBodega { get; set; }
    }
}