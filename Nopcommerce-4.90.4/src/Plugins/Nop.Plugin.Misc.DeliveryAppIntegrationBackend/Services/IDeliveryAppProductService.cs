using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models;
using System.Collections.Generic;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Services
{

    /// <summary>
    /// Represents a contract for a service that is going to provider product data for delivery app integration >
    /// </summary>
    public interface IDeliveryAppProductService
    {
        /// <summary>
        /// Gets the related products of a given product.
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        IEnumerable<DeliveryAppRelatedProduct> GetProductRelatedProducts(int productId);

        /// <summary>
        ///  returns the total tax of the products of an order
        /// </summary>
        /// <param name="getOrderTotalTaxRequest">Order's products info request</param>
        /// <returns>the total amount of  products taxes</returns>
        decimal GetTaxAmountOfOrderProducts(GetOrderTotalTaxRequest getOrderTotalTaxRequest);
    }
}
