using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Shipping;
using Nop.Plugin.Widgets.ProductAvailability.Domains;
using Nop.Plugin.Widgets.ProductAvailability.Models;
using Nop.Plugin.Widgets.ProductAvailability.Models.Inventory;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nop.Plugin.Widgets.ProductAvailability.Services
{
    /// <summary>
    /// Represents a contract for the services of this plugin.
    /// </summary>
    public interface IProductAvailabilityService
    {
        /// <summary>
        /// Get the inventory information of a product by it's SKU.
        /// </summary>
        /// <param name="sku">The SKU of the product.</param>
        /// <param name="filterEmptySizesFromInventory">Optional value that indicates if the empty sizes must be deleted from the response. Default value is <see cref="true"/>.</param>
        /// <returns>An instance of <see cref="InventoryRequestResponseModel"/>.</returns>
        Task<InventoryRequestResponseModel> GetProductInventoryBySku(string sku, bool filterEmptySizesFromInventory = true);

        /// <summary>
        /// Retrieves a boolean indicating if the plugin is correctly configured.
        /// </summary>
        /// <returns>A boolean.</returns>
        bool PluginIsConfigured();

        /// <summary>
        /// Gets a <see cref="WarehousePickupPointMapping"/> by the picup point id.
        /// </summary>
        /// <param name="id">The pickup point id.</param>
        /// <returns>An instance of <see cref="WarehousePickupPointMapping"/> or <see cref="null"/> if not found.</returns>
        WarehousePickupPointMapping GetWarehousePickupPointMappingByPickupPointId(int id);

        /// <summary>
        /// Gets a <see cref="WarehousePickupPointMapping"/> by the warehouse id.
        /// </summary>
        /// <param name="id">The warehouse id.</param>
        /// <returns>An instance of <see cref="WarehousePickupPointMapping"/> or <see cref="null"/> if not found.</returns>
        WarehousePickupPointMapping GetWarehousePickupPointMappingByWarehouseId(int id);

        /// <summary>
        /// Gets a <see cref="WarehousePickupPointMapping"/> by the store number.
        /// </summary>
        /// <param name="storeNumber">The store number.</param>
        /// <returns>An instance of <see cref="WarehousePickupPointMapping"/> or <see cref="null"/> if not found.</returns>
        WarehousePickupPointMapping GetWarehousePickupPointMappingByStoreNumber(string storeNumber);

        /// <summary>
        /// Verifies the availability of the products before placing an order.
        /// </summary>
        /// <param name="pickupPointCellarNumber">The id number for the selected pickup point option.</param>
        /// <param name="cartItems">The items about to be checked out.</param>
        /// <returns>An instance of <see cref="Task{TResult}"/> where TResult is <see cref="CheckoutAvailabilityVerificationResult"/>.</returns>
        Task<CheckoutAvailabilityVerificationResult> CheckShoppingCartItemsAvailabilityOnSelectedPickupPointBeforeContinuingCheckout(string pickupPointCellarNumber, IList<ShoppingCartItem> cartItems);

        /// <summary>
        /// Retrieves the cellar number for an given pickup point by it's id.
        /// </summary>
        /// <param name="id">The id of the pickup point.</param>
        /// <returns>An <see cref="string"/> containing the cellar number.</returns>
        string GetCellarNumberByPickupPointId(string id);

        /// <summary>
        /// Retrieves the cellar number of the configured inventory warehouse.
        /// </summary>
        /// <returns>An <see cref="string"/> containing the cellar number.</returns>
        string GetConfiguredWarehouseCellarNumber();

        /// <summary>
        /// Indicates if a product attribute named 'Size' exists.
        /// </summary>
        /// <returns>A <see cref="bool"/> indicating whether the attribute exists or not.</returns>
        bool SizeProductAttributeExists();

        /// <summary>
        /// Inserts or updates the size product attribute combinations for a product.
        /// </summary>
        /// <param name="product">The product. An instance of <see cref="Product"/>.</param>
        /// <param name="inventoryRequestResponse">The response from the inventory web service for the product.</param>
        void InsertOrUpdateProductAttributeCombination(Product product, InventoryRequestResponseModel inventoryRequestResponse);

        /// <summary>
        /// Deletes all product attribute combinations by product id.
        /// </summary>
        /// <param name="productId">The product id.</param>
        void DeleteAllProductAttributeCombinationsByProductId(int productId);

        /// <summary>
        /// Deletes all product attribute values by product id.
        /// </summary>
        /// <param name="productId">The product id.</param>
        void DeleteAllProductAttributeValuesByProductId(int productId);

        /// <summary>
        /// Inserts or updates the inventory for an one size product.
        /// </summary>
        /// <param name="product">The product. An instance of <see cref="Product"/>.</param>
        /// <param name="inventoryRequestResponse">The response from the inventory web service for the product.</param>
        void UpdateOneSizeProductInventory(Product product, InventoryRequestResponseModel inventoryRequestResponse);
    }
}
