using System.Collections.Generic;

namespace Nop.Plugin.Widgets.ProductAvailability.Models.Inventory
{
    /// <summary>
    /// Represents the ProductAvailability RequestResponseModel
    /// </summary>
    public sealed class InventoryRequestResponseModel
    {
        /// <summary>
        /// Validate access to the endpoint
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Product SKU
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Product Name
        /// </summary>
        public string Producto { get; set; }

        /// <summary>
        /// Product Sizes
        /// </summary>
        public IList<string> Tallas { get; set; }

        /// <summary>
        /// Bench Office of any Product
        /// </summary>
        public IList<StoreModel> Sucursales { get; set; }
    }
}