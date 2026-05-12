using Nop.Core.Domain.Common;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models;
using System.Collections.Generic;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Services
{
    /// <summary>
    /// Represents a contract for the services of this plugin.
    /// </summary>
    public interface ICustomerFavoriteMappingService
    {
        /// <summary>
        /// Register customer favorite vendor.
        /// </summary>
        /// <param name="customerId">customer's id</param>
        /// <param name="vendorId">vendor's id.</param>
        void MarkVendorAsFavoriteByCustomer(int customerId , int vendorId);

        /// <summary>
        /// Get customer's favorite vendors.
        /// </summary>
        /// <param name="customerId">customer's id</param>
        /// <returns>list of customer's favorite vendors</returns>
        IList<StoreResponseModel> GetCustomerFavoriteVendors(int customerId);

        /// <summary>
        /// assigned a specific address by the customer for shipping
        /// </summary>
        /// <param name="customerId">Customer's id</param>
        /// <param name="shippingAddressId">Customer's shipping address id</param>
        void GetCustomerSelectedShippingAddress(int customerId, int shippingAddressId);

    }
}
