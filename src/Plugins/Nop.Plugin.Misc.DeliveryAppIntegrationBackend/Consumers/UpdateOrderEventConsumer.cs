using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Payments;
using Nop.Core.Events;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domain;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Services;
using Nop.Services.Events;
using Nop.Services.Logging;
using Nop.Services.Messages;
using System;
using System.Reflection;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Consumers
{
    /// <summary>
    /// Represents a consumer for <see cref="EntityUpdatedEvent{T}"/> where T is an instance of <see cref="Order"/>.
    /// </summary>
    public sealed class UpdateOrderEventConsumer : IConsumer<EntityUpdatedEvent<Order>>
    {
        #region Fields

        private readonly DeliveryAppBackendConfigurationSettings _deliveryAppBackendConfigurationSettings;
        private readonly IDeliveryAppOrderService _deliveryAppOrderService;
        private readonly ILogger _logger;
        private readonly INotificationService _notificationService;
        private readonly IOrderPendingToClosePaymentService _orderPendingToClosePaymentService;
        private readonly IVendorDeliveryAppService _vendorDeliveryAppService;

        #endregion

        #region Ctor

        /// <summary>
        /// Creates an instance of <see cref="UpdateOrderEventConsumer"/>.
        /// </summary>
        /// <param name="deliveryAppBackendConfigurationSettings">An instance of <see cref="DeliveryAppBackendConfigurationSettings"/>.</param>
        /// <param name="deliveryAppOrderService">An implementation of <see cref="IDeliveryAppOrderService"/>.</param>
        /// <param name="logger">An implementation of <see cref="ILogger"/>.</param>
        /// <param name="notificationService">An implementation of <see cref="INotificationService"/>.</param>
        /// <param name="orderPendingToClosePaymentService">An implementation of <see cref="IOrderPendingToClosePaymentService"/>.</param>
        /// <param name="vendorDeliveryAppService">An implementation of <see cref="IVendorDeliveryAppService"/>.</param>
        public UpdateOrderEventConsumer(
            DeliveryAppBackendConfigurationSettings deliveryAppBackendConfigurationSettings,
            IDeliveryAppOrderService deliveryAppOrderService,
            ILogger logger,
            INotificationService notificationService,
            IOrderPendingToClosePaymentService orderPendingToClosePaymentService,
            IVendorDeliveryAppService vendorDeliveryAppService)
        {
            _deliveryAppBackendConfigurationSettings = deliveryAppBackendConfigurationSettings;
            _deliveryAppOrderService = deliveryAppOrderService;
            _logger = logger;
            _notificationService = notificationService;
            _orderPendingToClosePaymentService = orderPendingToClosePaymentService;
            _vendorDeliveryAppService = vendorDeliveryAppService;
        }

        #endregion

        #region Utilities

        private void InsertOrderPendingToClosePaymentRecord(Order order)
        {
            if (order.OrderStatus != OrderStatus.Complete || order.PaymentStatus != PaymentStatus.Paid)
                return;

            OrderPendingToClosePayment orderPendingToClosePayment = GetOrderPendingToClosePaymentFromOrder(order);

            orderPendingToClosePayment.OrderId = order.Id;
            orderPendingToClosePayment.VendorId = _deliveryAppOrderService.GetVendorIdByOrderId(order.Id);
            orderPendingToClosePayment.VendorPaymentStatus = PaymentStatus.Pending;
            orderPendingToClosePayment.DriverPaymentStatus = PaymentStatus.Pending;
            // Shipping profit
            orderPendingToClosePayment.ShippingTaxAdministrativePercentage = _deliveryAppBackendConfigurationSettings.AdministrativeProfit;
            orderPendingToClosePayment.ShippingTaxAdministrativeProfitAmount =
                order.OrderShippingInclTax * _deliveryAppBackendConfigurationSettings.AdministrativeProfit / 100M;
            orderPendingToClosePayment.ShippingTaxMessengerPercentage = _deliveryAppBackendConfigurationSettings.MessengerProfit;
            orderPendingToClosePayment.ShippingTaxMessengerProfitAmount =
                order.OrderShippingInclTax * _deliveryAppBackendConfigurationSettings.MessengerProfit / 100M;
            // Order payment profit
            decimal vendorOrderPaymentPercentage = _vendorDeliveryAppService.GetOrderPaymentPercentageByVendorId(orderPendingToClosePayment.VendorId);
            orderPendingToClosePayment.OrderTotalAdministrativePercentage = vendorOrderPaymentPercentage > 100M || vendorOrderPaymentPercentage < 0M ? 0M
                : vendorOrderPaymentPercentage;
            orderPendingToClosePayment.OrderTotalAdministrativeProfitAmount = order.OrderSubtotalExclTax * orderPendingToClosePayment.OrderTotalAdministrativePercentage / 100M;
            orderPendingToClosePayment.OrderTotalVendorPercentage = 100M - orderPendingToClosePayment.OrderTotalAdministrativePercentage;
            orderPendingToClosePayment.OrderTotalVendorProfitAmount = order.OrderSubtotalExclTax * orderPendingToClosePayment.OrderTotalVendorPercentage / 100M;

            _orderPendingToClosePaymentService.InsertOrder(orderPendingToClosePayment);
        }

        private OrderPendingToClosePayment GetOrderPendingToClosePaymentFromOrder(Order order)
        {
            var newOrderPendingToClosePayment = new OrderPendingToClosePayment();
            foreach (PropertyInfo propertyInfo in order.GetType().GetRuntimeProperties())
            {
                PropertyInfo prop = newOrderPendingToClosePayment.GetType().GetProperty(propertyInfo.Name, BindingFlags.Public | BindingFlags.Instance);
                if (null != prop && prop.CanWrite)
                    prop.SetValue(newOrderPendingToClosePayment, propertyInfo.GetValue(order), null);
            }

            return newOrderPendingToClosePayment;
        }

        #endregion

        #region Methods

        ///<inheritdoc/>
        public void HandleEvent(EntityUpdatedEvent<Order> eventMessage)
        {
            try
            {
                OrderPendingToClosePayment order = _orderPendingToClosePaymentService.GetOrderByCustomOrderNumber(eventMessage.Entity.CustomOrderNumber);

                if (order != null)
                {
                    order.OrderStatus = eventMessage.Entity.OrderStatus;
                    order.Deleted = eventMessage.Entity.Deleted;

                    _orderPendingToClosePaymentService.UpdateOrder(order);
                }
                else
                {
                    InsertOrderPendingToClosePaymentRecord(eventMessage.Entity);
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"Error updating orders pending to close payment. {ex.Message}", ex);
                _notificationService.WarningNotification($"Error updating orders pending to close payment. {ex.Message}");
            }
        }

        #endregion
    }
}