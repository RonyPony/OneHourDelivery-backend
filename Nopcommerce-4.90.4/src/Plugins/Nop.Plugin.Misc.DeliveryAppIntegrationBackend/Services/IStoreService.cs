using Nop.Core.Domain.Catalog;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models;
using System.Collections.Generic;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Services
{
    /// <summary>
    /// Represents a contract for <see cref="StoreService"/>
    /// </summary>
    public interface IStoreService
    {
        /// <summary>
        /// Returns all products associated with a vendor.
        /// </summary>
        /// <param name="vendorId">vendor Id</param>
        /// <returns>An instance of <see cref="GetStoreProductsByVendorId"/>.</returns>
        IList<ProductModel> GetStoreProductsByVendorId(int vendorId);

        /// <summary>
        /// Maps the values from <see cref="Product"/> to <see cref="ProductModel"/>
        /// </summary>
        /// <param name="products"></param>
        /// <returns>An instance of <see cref="BuildProductModelsFromProducts"/></returns>
        IList<ProductModel> BuildProductModelsFromProducts(IList<Product> products);
    }
}                         