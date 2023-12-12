using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Shipping;
using Nop.Data;
using Nop.Plugin.Synchronizers.ASAP.Contracts;
using Nop.Plugin.Synchronizers.ASAP.Enums;
using Nop.Plugin.Synchronizers.ASAP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Plugin.Synchronizers.ASAP.Managers
{
    /// <summary>
    /// Provides the businness logic for interaction that interacts with shipmets.
    /// </summary>
    public sealed class ShippingManager
    {

        #region Fileds

        private readonly IRepository<Shipment> _shipmentRepository;
        private readonly IRepository<Order> _orderRepository;
        private readonly IAsapDeliveryService _asapDeliveryService;
        private readonly IRepository<OrderItem> _orderItemRepository;
        private readonly IRepository<ShipmentItem> _shipmentItemRepository;

        #endregion

        #region Ctor

        /// <summary>
        /// Creates an instance of <see cref="ShippingManager"/>
        /// </summary>
        /// <param name="shipmentRepository">An implementation of <see cref="IRepository{Shipment}"/></param>
        /// <param name="orderRepository">An implementation of <see cref="IRepository{Order}"/></param>
        /// <param name="asapDeliveryService">An implementation of <see cref="IAsapDeliveryService"/></param>
        /// <param name="orderItemRepository">Asap delivery service <see cref="IRepository{OrderItem}"/></param>
        /// <param name="shipmentItemRepository">Asap delivery service <see cref="IRepository{ShipmentItem}"/></param>
        public ShippingManager(IRepository<Shipment> shipmentRepository, IRepository<Order> orderRepository, IAsapDeliveryService asapDeliveryService,
                               IRepository<OrderItem> orderItemRepository, IRepository<ShipmentItem> shipmentItemRepository)
        {
            _shipmentRepository = shipmentRepository;
            _orderRepository = orderRepository;
            _asapDeliveryService = asapDeliveryService;
            _orderItemRepository = orderItemRepository;
            _shipmentItemRepository = shipmentItemRepository;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates shipment in database
        /// </summary>
        /// <param name="order">Order <see cref="Order"/></param>
        /// <param name="deliveryId">delivery id <see cref="string"/></param>
        /// <returns>Returns <see cref="Task"/> For this operation </returns>
        public async Task CreateShipping(Order order, string deliveryId)
        {
            AsapDeliveryStatus deliveryStatus = await _asapDeliveryService.GetDeliveryStatus(deliveryId);
            order.ShippingStatusId = GetShippingStatudByDeliveryStatusId(deliveryStatus.DeliveryStatus);
            CreateShipment(order.Id, deliveryId);
            CreateShipmentItems(order.Id);
            _orderRepository.Update(order);
        }

        private void CreateShipment(int orderId, string deliveryId)
        {
            List<OrderItem> orderItems = _orderItemRepository.Table.Where(orderItem => orderItem.OrderId == orderId).ToList();
            decimal? totalWeight = 0;

            foreach (OrderItem orderItem in orderItems)
            {
                if (orderItem.ItemWeight != null)
                {
                    totalWeight += orderItem.ItemWeight;
                }
            }

            Shipment shipment = new Shipment
            {
                OrderId = orderId,
                TrackingNumber = deliveryId,
                CreatedOnUtc = DateTime.Now,
                ShippedDateUtc = DateTime.Now,
                TotalWeight = totalWeight
            };

            _shipmentRepository.Insert(shipment);
        }

        private void CreateShipmentItems(int orderId)
        {
            List<OrderItem> orderItems = _orderItemRepository.Table.Where(orderItem => orderItem.OrderId == orderId).ToList();

            foreach (OrderItem orderItem in orderItems)
            {
                Shipment shipment = _shipmentRepository.Table.FirstOrDefault(shipment => shipment.OrderId == orderId);
                if (shipment == null)
                {
                    throw new Exception($"No se encontro ninguna orden de delivery para la orden {orderId}");
                }

                ShipmentItem shipmentItem = new ShipmentItem
                {
                    ShipmentId = shipment.Id,
                    OrderItemId = orderItem.Id,
                    Quantity = orderItem.Quantity
                };

                _shipmentItemRepository.Insert(shipmentItem);
            }
        }

        private int GetShippingStatudByDeliveryStatusId(int deliveryStatusId)
        {
            switch (deliveryStatusId)
            {
                case (int)DeliveryStatus.Delivered:
                    return (int)ShippingStatus.Delivered;
                case (int)DeliveryStatus.OnTheWay:
                    return (int)ShippingStatus.Shipped ;
                default:
                    return (int)ShippingStatus.NotYetShipped;
            }
        }

        /// <summary>
        /// Gets shipment by order id
        /// </summary>
        /// <param name="orderId">Order id<see cref="int"/></param>
        /// <returns>Returns <see cref="Shipment"/> For this operation </returns>
        public Shipment GetShipmentByOrderId(int orderId) => _shipmentRepository.Table.FirstOrDefault(shipment => shipment.OrderId == orderId);

        /// <summary>
        /// Updates shipment in database
        /// </summary>
        /// <param name="shipment">Order id<see cref="Shipment"/></param>
        /// <param name="order">Order id<see cref="Order"/></param>
        public async Task UpdateShipping(Shipment shipment, Order order)
        {
            AsapDeliveryStatus deliveryStatus = await _asapDeliveryService.GetDeliveryStatus(shipment.TrackingNumber);

            shipment = UpdateShipmentDatesByDeliveryStatus(shipment, deliveryStatus);
            order.ShippingStatusId = GetShippingStatudByDeliveryStatusId(deliveryStatus.DeliveryStatus);

            _orderRepository.Update(order);
            _shipmentRepository.Update(shipment);
        }

        private Shipment UpdateShipmentDatesByDeliveryStatus(Shipment shipment, AsapDeliveryStatus deliveryStatus)
        {
            bool result = true;
            switch (deliveryStatus.DeliveryStatus)
            {
                case (int)DeliveryStatus.Payment_failed:
                case (int)DeliveryStatus.Failed:
                case (int)DeliveryStatus.Returned:
                case (int)DeliveryStatus.Confirmed:
                case (int)DeliveryStatus.InProgress:
                case (int)DeliveryStatus.Canceled:
                    shipment.ShippedDateUtc = null;
                    shipment.DeliveryDateUtc = null;
                    break;
                case (int)DeliveryStatus.Delivered:
                    result = DateTime.TryParse(deliveryStatus.ShippedDate, out DateTime deliveryDate);
                    if (result) shipment.DeliveryDateUtc = deliveryDate;
                    else throw new ArgumentException($"An error has occurred while trying to parse ASAP delivery status date. Date received: {deliveryStatus.ShippedDate}");
                    break;
                case (int)DeliveryStatus.OnTheWay:
                    result = DateTime.TryParse(deliveryStatus.ShippedDate, out DateTime shippedDate);
                    if (result) shipment.ShippedDateUtc = shippedDate;
                    else throw new ArgumentException($"An error has occurred while trying to parse ASAP delivery status date. Date received: {deliveryStatus.ShippedDate}");
                    break;
                default:
                    break;
            }

            return shipment;
        }

        /// <summary>
        /// Gets delivery tracking link
        /// </summary>
        /// <param name="deliveryId">Order id<see cref="string"/></param>
        /// <returns>Returns a delivery tracking link</returns>
        public async Task<string> GetDeliveryTrackingLink(string deliveryId)
        {
            return await _asapDeliveryService.GetDeliveryTrackingLink(deliveryId);
        }

        #endregion
    }
}