using Nop.Core.Domain.Orders;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models;
using System.Collections.Generic;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Services
{
    /// <summary>
    /// Represents a contract for <see cref="OrderDeliveryStatusMapping"/> operations.
    /// </summary>
    public interface IOrderPaymentCollectionService
    {
        /// <summary>
        /// Marks an order payment collection status as collected.
        /// </summary>
        /// <param name="order">An instance of <see cref="Order"/>.</param>
        void MarkOrderPaymentAsCollected(Order order);

        /// <summary>
        /// Get orders payment collection by identifiers.
        /// </summary>
        /// <param name="orderIds">Order identifiers</param>
        /// <returns><see cref="ValidateOrdersForPaymentCollectionResult"/>.</returns>
        ValidateOrdersForPaymentCollectionResult GetValidOrdersForPymentCollectionByOrderIds(int[] orderIds);

        /// <summary>
        /// Inserts a new <see cref="OrderDeliveryStatusMapping"/>.
        /// </summary>
        /// <param name="driverId">The driver id.</param>
        /// <param name="order">An instance of <see cref="Order"/>.</param>
        void CreateOrderPaymentCollectionStatus(int driverId, Order order);

        /// <summary>
        /// Confirm if the order has been already paid to the delivery
        /// </summary>
        /// <param name="orderIds">Orders to consult</param>
        /// <returns></returns>
        ValidateOrdersForPaymentCollectionResult GetValidOrdersToPayDriver(int[] orderIds);

        /// <summary>
        /// Confirm if the order has been already paid to the vendor
        /// </summary>
        /// <param name="orderIds">Orders to consult</param>
        /// <returns></returns>
        ValidateOrdersForPaymentCollectionResult GetValidOrdersToPayVendor(int[] orderIds);

        /// <summary>
        /// Returns the orders pending to collect by driver customer id.
        /// </summary>
        /// <param name="driverCustomerId">The customer id of the driver.</param>
        /// <returns><see cref="IList{T}"/> where T is <see cref="OrderPaymentCollectionStatus"/></returns>
        IList<OrderPaymentCollectionStatus> GetOrdersPendingToCollectByDriverCustomerId(int driverCustomerId);
    }
}
