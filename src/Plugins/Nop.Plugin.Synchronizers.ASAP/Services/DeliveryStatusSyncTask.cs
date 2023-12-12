using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Shipping;
using Nop.Plugin.Synchronizers.ASAP.Managers;
using Nop.Services.Logging;
using Nop.Services.Tasks;
using System;
using System.Collections.Generic;

namespace Nop.Plugin.Synchronizers.ASAP.Services
{
    /// <summary>
    /// Represents the task model class for synchronizing the delivery status.
    /// </summary>
    public sealed class DeliveryStatusSyncTask : IScheduleTask
    {
        #region Field

        private readonly ILogger _logger;
        private readonly ShippingManager _shippingManager;
        private readonly OrderManager _orderManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Creates an instance of <see cref="DeliveryStatusSyncTask"/>
        /// </summary>
        /// <param name="logger">Represents an implementation of <see cref="ILogger"/>.</param>
        /// <param name="shippingManager">Represents an instace of <see cref="ShippingManager"/>.</param>
        /// <param name="orderManager">Represents an instance of <see cref="OrderManager"/>.</param>
        public DeliveryStatusSyncTask(ILogger logger, ShippingManager shippingManager, OrderManager orderManager)
        {
            _logger = logger;
            _shippingManager = shippingManager;
            _orderManager = orderManager;
        }

        #endregion

        #region Method
        /// <inheritdoc/>
        void IScheduleTask.Execute()
        {
            try
            {
                IEnumerable<Order> orders = _orderManager.GetOrdersToUpdate();

                foreach (Order order in orders)
                {
                    Shipment shipment = _shippingManager.GetShipmentByOrderId(order.Id);
                    if (shipment == null) _logger.Error($"Error syncing delivery statuses. No shipment assigned to the order { order.Id}");
                    else UpdateShipping(shipment, order);
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"Error syncing delivery statuses. {ex.Message}" , ex);
            }
        }

        private void UpdateShipping(Shipment shipment, Order order)
        {
            try
            {
                _shippingManager.UpdateShipping(shipment, order).Wait();
            }
            catch (Exception ex)
            {
                _logger.Error($"Error syncing delivery statuses. {ex.Message}" , ex);
            }
        }

        #endregion
    }
}

