using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Shipping;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models;
using Nop.Web.Areas.Admin.Models.Shipping;
using System;
using System.Collections.Generic;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Services
{
    /// <summary>
    /// A contract to represents the behaviour of getting vendor information for delivery Apps.
    /// </summary>
    public interface IVendorDeliveryAppService
    {
        /// <summary>
        /// Gets all vendor products by categories.
        /// </summary>
        /// <returns>An instance of <see cref="DeliveryAppVendor"/>DeliveryAppVendor model.</returns>
        DeliveryAppVendor GetVendorProductsGroupByCategories(int vendorId , int customerId);

        /// <summary>
        /// Prepare paged vendor warehouse list model.
        /// </summary>
        /// <param name="vendorId">The vendor id</param>
        /// <param name="searchModel">Warehouse search model</param>
        /// <returns>Warehouse list model</returns>
        WarehouseListModel PrepareVendorWarehouseListModel(int vendorId, WarehouseSearchModel searchModel);

        /// <summary>
        /// Gets all warehouses from a vendor.
        /// </summary>
        /// <param name="vendorId">The vendor id</param>
        /// <param name="name">Warehouse name</param>
        /// <returns>Warehouses</returns>
        IList<Warehouse> GetAllWarehousesFromVendor(int vendorId, string name = null);

        /// <summary>
        /// Indicates if a given customer is a delivery app vendor.
        /// </summary>
        /// <param name="customer">The customer to validate.</param>
        /// <returns>A boolean that indicates if the current user is a delivery app vendor.</returns>
        bool IsDeliveryAppVendor(Customer customer);

        /// <summary>
        /// Retrieves the warehosue id of the warehouse assigned to a vendor by the vendor id.
        /// </summary>
        /// <param name="vendorId">The vendor id.</param>
        /// <returns>A warehouse id. If not founded then <see cref="null"/>.</returns>
        int? GetVendorWarehouseIdByVendorId(int vendorId);

        /// <summary>
        /// Inserts or updates a vendor warehouse.
        /// </summary>
        /// <param name="vendorWarehouse">The vendor warehouse toinsert or update.</param>
        void InsertOrUpdateVendorWarehouseMapping(VendorWarehouseMapping vendorWarehouse);

        /// <summary>
        /// Returns all Address Id associated with a vendor
        /// </summary>
        /// <param name="latitud">latitud vendor</param>
        /// <param name="longitud">longitud vendor</param>
        /// <returns>An instance of <see cref="IList{StoreResponseModel}"/></returns>
        IList<StoreResponseModel> GetClosestStores(decimal latitud, decimal longitud);

        /// <summary>
        /// Gets all store vendor.
        /// </summary>
        /// <returns>An instance of <see cref="IList{StoreResponseModel}"/></returns>
        IList<StoreResponseModel> GetAllStores();

        /// <summary>
        /// Gets all store vendor by category.
        /// </summary>
        /// <returns>An instance of <see cref="IList{StoreResponseModel}"/></returns>
        IList<StoreResponseModel> GetAllStoresByCategory(string vendorCategory, decimal latitude, decimal longitude);

        /// <summary>
        /// Gets the value for adults limitated attribute for a vendor by the vendor id.
        /// </summary>
        /// <param name="vendorId">The vendor id.</param>
        /// <returns>A <see cref="bool"/> that indicates if the vendor is adults limitated.</returns>
        bool GetVendorAdultsLimitatedAttribute(int vendorId);

        /// <summary>
        /// Gets the value for rating attribute for a vendor by the vendor id.
        /// </summary>
        /// <param name="vendorId">The vendor id.</param>
        /// <returns>A <see cref="string"/> with the rating for the vendor.</returns>
        string GetRatingAttribute(int vendorId);

        /// <summary>
        /// Gets the value for estimated time attribute for a vendor by the vendor id.
        /// </summary>
        /// <param name="vendorId">The vendor id.</param>
        /// <returns>A <see cref="string"/> with the estimated time for the vendor.</returns>
        string GetEstimatedTimeAttribute(int vendorId);

        /// <summary>
        /// Gets the value for category attribute for a vendor by the vendor id.
        /// </summary>
        /// <param name="vendorId">The vendor id.</param>
        /// <returns>A <see cref="string"/> with the category for the vendor.</returns>
        string GetVendorCategoryAttribute(int vendorId);

        /// <summary>
        /// Get earnings earned by the salesperson by date.
        /// </summary>
        /// <param name="customerId">The customer id.</param>
        /// <param name="searchByDate">the order date.</param>
        /// <returns>A <see cref="ProfitVendorModel"/> profit vendor.</returns>
        ProfitVendorModel GetVendorProfit(int vendorId);

        /// <summary>
        /// Gets a list of new stores
        /// </summary>
        /// <param name="latitud">latitud vendor</param>
        /// <param name="longitud">longitud vendor</param>
        /// <returns></returns>
        List<StoreResponseModel> GetAllNewStore(decimal latitud, decimal longitud);

        /// <summary>
        /// Gets a set of store groupings, near stores, popular stores, and new stores
        /// </summary>
        /// <param name="latitud">latitud vendor</param>
        /// <param name="longitud">longitud vendor</param>
        /// <returns></returns>
        List<StoreResponseModel> GetAllPopularStore(decimal latitud, decimal longitud);

        /// <summary>
        /// Gets near stores
        /// </summary>
        /// <param name="latitud">latitud vendor</param>
        /// <param name="longitud">longitud vendor</param>
        /// <returns></returns>
        List<StoreResponseModel> GetAllNearStore(decimal latitud, decimal longitud);
        /// <summary>
        /// Gets all stores with promotions
        /// </summary>
        /// <param name="latitud">latitud vendor</param>
        /// <param name="longitud">longitud vendor</param>
        /// <returns></returns>
        List<StoreResponseModel> GetAllStoresWithPromotions(decimal latitud,decimal longitud);

        /// <summary>
        /// Retrieves the order payment percentage by vendor id.
        /// </summary>
        /// <param name="vendorId">The vendor id.</param>
        /// <returns>The order payment percentage if is set; otherwise returns zero.</returns>
        decimal GetOrderPaymentPercentageByVendorId(int vendorId);

        /// <summary>
        /// Retrives a list of vendors searching by products.
        /// </summary>
        /// <param name="searchText">text for the search </param>
        /// <returns>list of vendors found by search text.</returns>
        IList<StoreResponseModel> GetVendorsBySearchText(string searchText, decimal latitude, decimal longitude);

        /// <summary>
        /// Retrieves specific vendor info.
        /// </summary>
        /// <param name="vendorId">vendor's id</param>
        /// <returns>a vendor info model</returns>
        VendorInfo GetVendorInfo(int vendorId);
    }
}
