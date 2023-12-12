using Nop.Core;
using Nop.Core.Configuration;

namespace Nop.Plugin.Widgets.ProductAvailability
{
    /// <summary>
    /// Represents the fields of ProductAvailability Settings
    /// </summary>
    public sealed class ProductAvailabilitySettings : BaseEntity, ISettings
    {
        /// <summary>
        /// Indicates the route to make product's inventory requests.
        /// </summary>
        public string ProductInventoryUrl { get; set; }

        /// <summary>
        /// Indicates the route to make stores/warehouses requests.
        /// </summary>
        public string StoresUrl { get; set; }

        /// <summary>
        /// Indicates if all requests share the same token.
        /// </summary>
        public bool UseSameTokenForAllRequest { get; set; }

        /// <summary>
        /// Indicates the token that gives access to the product's inventory requests.
        /// </summary>
        public string ProductInventoryUrlToken { get; set; }

        /// <summary>
        /// Indicates the token that gives access to the stores/warehouses requests.
        /// </summary>
        public string StoresUrlToken { get; set; }

        /// <summary>
        /// Indicates the token that gives access to all requests.
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Indicates the id of the warehouse from where the inventory will be requested.
        /// </summary>
        public int InventoryWarehouseId { get; set; }

        /// <summary>
        /// Indicates the id of the product attribute to be updated in inventory sync.
        /// </summary>
        public int ProductAttributeId { get; set; }

        /// <summary>
        /// Indicates the tries of inventory request's for each product when the request fails.
        /// </summary>
        public int InventoryRequestTries { get; set; }

        /// <summary>
        /// Indicates if the table with the inventory details by store must be displayed on product details view.
        /// </summary>
        public bool DisplayStoreAvailabilityOnProductDetailsPage { get; set; }

        /// <summary>
        /// Indicates if the product attribute values must be deleted when syncing products inventory.
        /// </summary>
        public bool DeleteExistingProductAttributeValuesOnInventorySync { get; set; }

        /// <summary>
        /// Indicates the code that identifies the one sized products.
        /// </summary>
        public string OneSizeProductsIdentifierCode { get; set; }
    }
}
