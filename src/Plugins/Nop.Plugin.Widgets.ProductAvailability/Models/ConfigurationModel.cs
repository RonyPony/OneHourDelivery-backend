using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;
using System.Collections.Generic;

namespace Nop.Plugin.Widgets.ProductAvailability.Models
{
    /// <summary>
    /// Represents a plugin configuration for this plugin.
    /// </summary>
    public sealed class ConfigurationModel : BaseNopEntityModel
    {
        /// <summary>
        /// Indicates the route to make product's inventory requests.
        /// </summary>
        [NopResourceDisplayName("Plugins.Widgets.ProductAvailability.Config.ProductInventoryUrl")]
        public string ProductInventoryUrl { get; set; }

        /// <summary>
        /// Indicates the route to make stores/warehouses requests.
        /// </summary>
        [NopResourceDisplayName("Plugins.Widgets.ProductAvailability.Config.StoresUrl")]
        public string StoresUrl { get; set; }

        /// <summary>
        /// Indicates if all requests share the same token.
        /// </summary>
        [NopResourceDisplayName("Plugins.Widgets.ProductAvailability.Config.UseSameTokenForAllRequest")]
        public bool UseSameTokenForAllRequest { get; set; }

        /// <summary>
        /// Indicates the token that gives access to the product's inventory requests.
        /// </summary>
        [NopResourceDisplayName("Plugins.Widgets.ProductAvailability.Config.ProductInventoryUrlToken")]
        public string ProductInventoryUrlToken { get; set; }

        /// <summary>
        /// Indicates the token that gives access to the stores/warehouses requests.
        /// </summary>
        [NopResourceDisplayName("Plugins.Widgets.ProductAvailability.Config.StoresUrlToken")]
        public string StoresUrlToken { get; set; }

        /// <summary>
        /// Indicates the token that gives access to all requests.
        /// </summary>
        [NopResourceDisplayName("Plugins.Widgets.ProductAvailability.Config.Token")]
        public string Token { get; set; }

        /// <summary>
        /// Indicates the id of the warehouse from where the inventory will be requested.
        /// </summary>
        [NopResourceDisplayName("Plugins.Widgets.ProductAvailability.Config.InventoryWarehouseId")]
        public int InventoryWarehouseId { get; set; }

        /// <summary>
        /// Indicates the avaible warehouses.
        /// </summary>
        public List<SelectListItem> Warehouses { get; set; }

        /// <summary>
        /// Indicates the id of the product attribute to be updated in inventory sync.
        /// </summary>
        [NopResourceDisplayName("Plugins.Widgets.ProductAvailability.Config.ProductAttributeId")]
        public int ProductAttributeId { get; set; }

        /// <summary>
        /// Indicates the avaible product attributes.
        /// </summary>
        public List<SelectListItem> ProductAttributes { get; set; }

        /// <summary>
        /// Indicates the tries of inventory request's for each product when the request fails.
        /// </summary>
        [NopResourceDisplayName("Plugins.Widgets.ProductAvailability.Config.InventoryRequestTries")]
        public int InventoryRequestTries { get; set; }

        /// <summary>
        /// Indicates if the table with the inventory details by store should be displayed on product details view.
        /// </summary>
        [NopResourceDisplayName("Plugins.Widgets.ProductAvailability.Config.DisplayStoreAvailabilityOnProductDetailsPage")]
        public bool DisplayStoreAvailabilityOnProductDetailsPage { get; set; }

        /// <summary>
        /// Indicates if the product attribute values must be deleted when syncing products inventory.
        /// </summary>
        [NopResourceDisplayName("Plugins.Widgets.ProductAvailability.Config.DeleteExistingProductAttributeValuesOnInventorySunc")]
        public bool DeleteExistingProductAttributeValuesOnInventorySync { get; set; }

        /// <summary>
        /// Indicates the code that identifies the one sized products.
        /// </summary>
        [NopResourceDisplayName("Plugins.Widgets.ProductAvailability.Config.OneSizeProductsIdentifierCode")]
        public string OneSizeProductsIdentifierCode { get; set; }
    }
}
