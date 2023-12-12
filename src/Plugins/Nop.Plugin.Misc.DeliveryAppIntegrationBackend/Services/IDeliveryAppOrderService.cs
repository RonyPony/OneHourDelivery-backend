using Nop.Core.Domain.Customers;
using Nop.Plugin.Api.Delta;
using Nop.Plugin.Api.DTO.Orders;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.DTO;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models;
using System.Collections.Generic;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Services
{
    /// <summary>
    /// Represents a contract for the delivery app order services.
    /// </summary>
    public interface IDeliveryAppOrderService
    {
        /// <summary>
        /// Updates the corresponding order statuses to set the order as accepted by a messenger.
        /// </summary>
        /// <param name="orderId">The id of the order to update.</param>
        /// <param name="customerId">The id of the messenger taking the order.</param>
        void ChangeStatusToAcceptedByMessenger(int orderId, int customerId);

        /// <summary>
        /// Updates the corresponding order statuses to set the order as delivered by a messenger.
        /// </summary>
        /// <param name="orderId">The id of the order to update.</param>
        void ChangeStatusToDelivered(int orderId);

        /// <summary>
        /// Updates the corresponding order statuses to set the order as retrieved by a messenger.
        /// </summary>
        /// <param name="orderId">The id of the order to update.</param>
        void ChangeStatusToRetrievedByMessenger(int orderId);

        /// <summary>
        /// Updates the corresponding order statuses to set the order as ready for the messenger pickup.
        /// </summary>
        /// <param name="orderId">The id of the order to update.</param>
        void SetOrderReadyToPickup(int orderId);

        /// <summary>
        /// Updates the corresponding order statuses to set the order as declined by a messenger.
        /// </summary>
        /// <param name="orderId">The id of the order to update.</param>
        /// <param name="message">The reason why the messenger is rejecting the order.</param>
        /// <param name="customerId">The id of the messenger declining the order.</param>
        void RegisterOrderDeclinedByMessenger(int orderId, string message, int customerId);

        /// <summary>
        /// Retrieves a list of ready to pickup orders inside a radious of N km from a given geo-coordinate.
        /// </summary>
        /// <param name="latitude">The latitude of the geo-coordinate.</param>
        /// <param name="longitude">The longitude of the geo-coordinate.</param>
        /// <returns>An implementation of <see cref="IList{T}"/> where T is <see cref="OrderDto"/>.</returns>
        IList<OrderDto> GetOrdersReadyToPickup(int driverId, decimal latitude, decimal longitude);

        /// <summary>
        /// Retrieves a list of active orders for a messenger by it's customer id.
        /// </summary>
        /// <param name="customerId">The customer id for the messenger.</param>
        /// <returns>An implementation of <see cref="IList{T}"/> where T is <see cref="OrderDto"/>.</returns>
        IList<OrderDto> GetOrdersAssignedToMessenger(int customerId);

        /// <summary>
        /// Retrieves a list of delivered orders for a messenger by it's customer id.
        /// </summary>
        /// <param name="customerId">The customer id for the messenger.</param>
        /// <returns>An implementation of <see cref="IList{T}"/> where T is <see cref="OrderDto"/>.</returns>
        IList<OrderDto> GetOrdersDeliveredByMessenger(int customerId);

        /// <summary>
        /// Retrieves a list of pending to accept or decline orders for a store by vendor id.
        /// </summary>
        /// <param name="vendorId">The vendor id.</param>
        /// <returns>An implementation of <see cref="IList{T}"/> where T is <see cref="OrderDto"/>.</returns>
        IList<OrderDto> GetPendingOrdersByVendorId(int vendorId);

        /// <summary>
        /// Updates an order status to processing when a store accepts the order.
        /// </summary>
        /// <param name="orderId">The order id.</param>
        /// <param name="vendorId">The id of the vendor of the store making the request.</param>
        void SetOrderAcceptedByStore(int orderId, int vendorId);

        /// <summary>
        /// Retrieves a list of orders in progress for a store by it's vendor id.
        /// </summary>
        /// <param name="vendorId">The vendor id of the store.</param>
        /// <returns>An implementation of <see cref="IList{T}"/> where T is <see cref="OrderDto"/>.</returns>
        IList<OrderDto> GetOrdersInProgressByVendorId(int vendorId);

        /// <summary>
        /// Change the order status to cancelled or processed
        /// </summary>
        /// <param name="orderId">Order's id</param>
        /// <param name="vendorId">Vendor's id assigned to the store</param>
        void CancelOrder(int orderId , int vendorId , string message);

        /// <summary>
        /// Cancel the order.
        /// </summary>
        /// <param name="id">Order's id.</param>
        void DeleteOrder(int id);

        /// <summary>
        /// Retrieves a list of orders in progress for a store by it's vendor id.
        /// </summary>
        /// <param name="orderId">Order's id.</param>
        /// <returns>An implementation of <see cref="CoordinateRequest"/> where T is <see cref="T"/>.</returns>
         CoordinateRequest GetOrderCoordinates(int orderId);
        /// <summary>
        /// Retrieves a list of orders completed and cancelled  for a store by it's vendor id.
        /// </summary>
        /// <param name="vendorId">Vendor's id assigned to the store.</param>
        /// <returns>An implementation of <see cref="IList{T}"/> where T is <see cref="OrderDto"/>.</returns>
        IList<HistoricOrdersByVendorDto> GetHistoricOrdersByVendorId(int vendorId);

        /// <summary>
        /// Inserts a new order rating.
        /// </summary>
        /// <param name="orderId">The id of the rated order.</param>
        /// <param name="orderRating">The rating given by the customer.</param>
        void InsertOrderRating(int orderId, OrderRatingModel orderRating);

        /// <summary>
        /// Get the driver infor.
        /// </summary>
        /// <param name="orderId">The id of the order.</param>
        /// <returns>An implementation of <see cref="Customer"/> return a given driver <see cref="Customer"/>.</returns>
        Customer GetDriverInfoByOrderId(int orderId);

        /// <summary>
        /// Get driver location info for tracing.
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        DriverLocationInfoMapping GetDriverlocationInfo(int orderId);

        /// <summary>
        /// Create driver location info for tracing.
        /// </summary>
        /// <param name="driverRequest"></param>
        void DriverlocationCreateInfo(DriverLocationInfoRequest driverRequest);

        /// <summary>
        /// Retrieves the vendor id of the store related to an order.
        /// </summary>
        /// <param name="orderId">The id of the order.</param>
        /// <returns>The vendor id.</returns>
        int GetVendorIdByOrderId(int orderId);

        /// <summary>
        /// Updates the driver of an order.
        /// </summary>
        /// <param name="orderId">The order id.</param>
        /// <param name="driverId">The customer id of the driver.</param>
        void SaveOrderDriver(int orderId, int driverId);

        /// <summary>
        /// Update the specific order.
        /// </summary>
        /// <param name="orderDelta">Order's request</param>
        void UpdateOrder(Delta<OrderDto> orderDelta);

        /// <summary>
        ///  Update the paymentmethod of the specif order
        /// </summary>
        /// <param name="orderId">Order's id.</param>
        /// <param name="paymentMethod">New PaymentMethod</param>
        void ChangeOrderPaymentMethod(int orderId , string paymentMethod);

        /// <summary>
        /// Retrives de amount of the discount coupon.
        /// </summary>
        /// <param name="couponCode">Discount's coupon code.</param>
        /// <param name="customerId">Customer's id.</param>
        /// <returns></returns>
        CouponModel GetDiscountCouponToApplyOrder(string couponCode, int customerId);
    }
}
