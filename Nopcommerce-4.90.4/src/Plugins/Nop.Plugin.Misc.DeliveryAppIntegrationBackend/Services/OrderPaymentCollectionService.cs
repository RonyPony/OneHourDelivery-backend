using Nop.Core;
using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Payments;
using Nop.Data;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains.Enums;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Helpers;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models;
using Nop.Services.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Services
{
    /// <summary>
    /// Represents an implementation for <see cref="IOrderPaymentCollectionService"/>.
    /// </summary>
    public sealed class OrderPaymentCollectionService : IOrderPaymentCollectionService
    {
        #region Fields

        private readonly ILocalizationService _localizationService;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<OrderDeliveryStatusMapping> _orderDeliveryStatusRepository;
        private readonly IRepository<OrderPaymentCollectionStatus> _orderPaymentCollectionRepository;
        private readonly IRepository<OrderPendingToClosePayment> _orderPendintToClosePayment;
        private readonly IOrderPaymentMethodService _orderPaymentMethodService;
        private readonly IWorkContext _workContext;
        private readonly IOrderPendingToClosePaymentService _orderPendingToPayService;

        #endregion

        #region Ctor

        /// <summary>
        /// Initializes a new instance of <see cref="OrderPaymentCollectionService"/>.
        /// </summary>
        /// <param name="localizationService">An implementation of <see cref="ILocalizationService"/>.</param>
        /// <param name="orderRepository">An implementation of <see cref="IRepository{Order}"/>.</param>
        /// <param name="orderDeliveryStatusRepository">An implementation of <see cref="IRepository{OrderDeliveryStatusMapping}"/>.</param>
        /// <param name="orderPaymentCollectionRepository">An implementation of <see cref="IRepository{OrderPaymentCollectionStatus}"/>.</param>
        /// <param name="orderPaymentMethodService">An implementation of <see cref="IOrderPaymentMethodService"/>.</param>
        /// <param name="workContext">An implementation of <see cref="IWorkContext"/>.</param>
        public OrderPaymentCollectionService(
            ILocalizationService localizationService,
            IRepository<Order> orderRepository,
            IRepository<OrderDeliveryStatusMapping> orderDeliveryStatusRepository,
            IRepository<OrderPaymentCollectionStatus> orderPaymentCollectionRepository,
            IOrderPaymentMethodService orderPaymentMethodService,
            IOrderPendingToClosePaymentService orderPendingToPayService,
            IRepository<OrderPendingToClosePayment> orderPendintToClosePayment,
            IWorkContext workContext)
        {
            _localizationService = localizationService;
            _orderRepository = orderRepository;
            _orderDeliveryStatusRepository = orderDeliveryStatusRepository;
            _orderPaymentCollectionRepository = orderPaymentCollectionRepository;
            _orderPaymentMethodService = orderPaymentMethodService;
            _workContext = workContext;
            _orderPendingToPayService = orderPendingToPayService;
            _orderPendintToClosePayment = orderPendintToClosePayment;
        }

        #endregion

        #region Utilities

        private IList<Order> ValidateOrdersForPaymentCollection(int[] orderIds, StringBuilder stringBuilder, ValidateOrdersForPaymentCollectionResult result)
        {
            var builder = new StringBuilder();
            var ordersQuery = from o in _orderRepository.Table
                              where orderIds.Contains(o.Id)
                              select o;
            var orders = ordersQuery.ToList();

            if (orderIds.Length != orders.Count)
            {
                builder.Clear();
                foreach (int id in orderIds.Where(id => orders.All(order => order.Id != id)).ToList())
                {
                    builder.Append($"{id},");
                }
                stringBuilder.Append(string.Format(_localizationService.GetResource($"{Defaults.LocaleResourcesPrefix}.PaymentCollection.SelectedOrdersNotFound"), builder.ToString()));

                result.Success = false;
            }

            // Validate all orders payment method is cash.
            if (orders.Any(o => !o.CheckoutAttributeDescription.Contains(Defaults.CashPaymentCheckoutAttributeName)))
            {
                builder.Clear();
                foreach (Order order in orders)
                {
                    if (!_orderPaymentMethodService.GetOrderPaymentMethodName(order).Equals(Defaults.CashPaymentCheckoutAttributeName))
                    {
                        builder.Append($"{order.Id},");
                        result.Success = false;
                    }
                }
                stringBuilder.Append(string.Format(_localizationService.GetResource($"{Defaults.LocaleResourcesPrefix}.PaymentCollection.SelectedOrdersPaymentMethod"), builder.ToString()));
            }

            return orders;
        }

        private IList<OrderPendingToClosePayment> ValidateDriverAsPaid(int[] orderIds, StringBuilder stringBuilder, ValidateOrdersForPaymentCollectionResult result)
        {
            var builder = new StringBuilder();
            var ordersQuery = from o in _orderPendintToClosePayment.Table
                              where orderIds.Contains(o.Id)
                              select o;
            var orders = ordersQuery.ToList();

            if (orderIds.Length != orders.Count)
            {
                builder.Clear();
                foreach (int id in orderIds.Where(id => orders.All(order => order.Id != id)).ToList())
                {
                    builder.Append($"{id},");
                }
                stringBuilder.Append(string.Format(_localizationService.GetResource($"{Defaults.LocaleResourcesPrefix}.PaymentCollection.SelectedOrdersNotFound"), builder.ToString()));

                result.Success = false;
            }

            builder.Clear();
            foreach (var order in orders)
            {
                if (order.DriverPaymentStatusId == (int)PaymentStatus.Paid)
                {
                    builder.Append($"{order.OrderId},");
                    result.Success = false;
                }
            }
            stringBuilder.Append(string.Format(_localizationService.GetResource($"{Defaults.LocaleResourcesPrefix}.PaymentCollection.SelectedOrdersToValidateDriverPayment"), builder.ToString()));

            return orders;
        }

        private IList<OrderPendingToClosePayment> ValidateVendorAsPaid(int[] orderIds, StringBuilder stringBuilder, ValidateOrdersForPaymentCollectionResult result)
        {
            var builder = new StringBuilder();
            var ordersQuery = from o in _orderPendintToClosePayment.Table
                              where orderIds.Contains(o.Id)
                              select o;
            var orders = ordersQuery.ToList();

            if (orderIds.Length != orders.Count)
            {
                builder.Clear();
                foreach (int id in orderIds.Where(id => orders.All(order => order.Id != id)).ToList())
                {
                    builder.Append($"{id},");
                }
                stringBuilder.Append(string.Format(_localizationService.GetResource($"{Defaults.LocaleResourcesPrefix}.PaymentCollection.SelectedOrdersNotFound"), builder.ToString()));

                result.Success = false;
            }

            builder.Clear();
            foreach (var order in orders)
            {
                if (order.VendorPaymentStatusId == (int)PaymentStatus.Paid)
                {
                    builder.Append($"{order.OrderId},");
                    result.Success = false;
                }
            }
            stringBuilder.Append(string.Format(_localizationService.GetResource($"{Defaults.LocaleResourcesPrefix}.PaymentCollection.SelectedOrdersToValidateVendorPayment"), builder.ToString()));

            return orders;
        }

        private void ValidateOrderDeliveryStatusForPaymentCollection(int[] orderIds, StringBuilder stringBuilder, ValidateOrdersForPaymentCollectionResult result)
        {
            var builder = new StringBuilder();
            var ordersDeliveryStatusQuery = from o in _orderDeliveryStatusRepository.Table
                                            where orderIds.Contains(o.OrderId)
                                            select o;
            var ordersDeliveryStatus = ordersDeliveryStatusQuery.ToList();

            if (orderIds.Length != ordersDeliveryStatus.Count)
            {
                builder.Clear();
                foreach (int id in orderIds.Where(id => ordersDeliveryStatus.All(order => order.Id != id)).ToList())
                {
                    builder.Append($"{id},");
                }
                stringBuilder.Append(string.Format(_localizationService.GetResource($"{Defaults.LocaleResourcesPrefix}.PaymentCollection.DeliveryStatusNotFoundForSelectedOrders"), builder.ToString()));

                result.Success = false;
            }

            // Validate all orders are delivered.
            if (ordersDeliveryStatus.Any(orderDelivery => orderDelivery.DeliveryStatusId != (int)DeliveryStatus.Delivered))
            {
                builder.Clear();
                foreach (OrderDeliveryStatusMapping orderDelivery
                    in ordersDeliveryStatus.Where(orderDelivery => orderDelivery.DeliveryStatusId != (int)DeliveryStatus.Delivered).ToList())
                {
                    builder.Append($"{orderDelivery.OrderId},");
                }
                stringBuilder.Append(string.Format(_localizationService.GetResource($"{Defaults.LocaleResourcesPrefix}.PaymentCollection.SelectedOrdersNotDelivered"), builder.ToString()));

                result.Success = false;
            }

            // Validate all orders have the same driver.
            int driverId = ordersDeliveryStatus.FirstOrDefault(orderDelivery => orderDelivery.CustomerId != null)?.CustomerId ?? 0;
            if (driverId == 0 || ordersDeliveryStatus.Any(orderDelivery => orderDelivery.CustomerId != driverId))
            {
                stringBuilder.Append(_localizationService.GetResource($"{Defaults.LocaleResourcesPrefix}.PaymentCollection.SelectedOrdersMustHaveSameDriver"));
                result.Success = false;
            }
        }

        private void ValidateOrderCollectionStatusForPaymentCollection(int[] orderIds, StringBuilder stringBuilder, ValidateOrdersForPaymentCollectionResult result)
        {
            var builder = new StringBuilder();
            var ordersCollectionStatusQuery = from o in _orderPaymentCollectionRepository.Table
                                              where orderIds.Contains(o.OrderId)
                                              select o;
            var ordersCollectionStatus = ordersCollectionStatusQuery.ToList();

            if (orderIds.Length != ordersCollectionStatus.Count)
            {
                builder.Clear();
                foreach (int id in orderIds.Where(id => ordersCollectionStatus.All(order => order.OrderId != id)).ToList())
                {
                    builder.Append($"{id},");
                }
                stringBuilder.Append(string.Format(_localizationService.GetResource($"{Defaults.LocaleResourcesPrefix}.PaymentCollection.PaymentCollectionNotFoundForSelectedOrders"), builder.ToString()));

                result.Success = false;
            }

            // Validate if orders are collected.
            if (ordersCollectionStatus.Any(orderCollection => orderCollection.PaymentCollectionStatus == PaymentCollectionStatus.Collected))
            {
                builder.Clear();
                foreach (OrderPaymentCollectionStatus orderCollection
                    in ordersCollectionStatus.Where(orderDelivery => orderDelivery.PaymentCollectionStatus == PaymentCollectionStatus.Collected).ToList())
                {
                    builder.Append($"{orderCollection.OrderId},");
                }
                stringBuilder.Append(string.Format(_localizationService.GetResource($"{Defaults.LocaleResourcesPrefix}.PaymentCollection.SelectedOrdersAlreadyCollected"), builder.ToString()));

                result.Success = false;
            }

            // Validate if orders apply for collection.
            if (ordersCollectionStatus.Any(orderCollection => orderCollection.PaymentCollectionStatus == PaymentCollectionStatus.DoesNotApply))
            {
                builder.Clear();
                foreach (OrderPaymentCollectionStatus orderCollection
                    in ordersCollectionStatus.Where(orderDelivery => orderDelivery.PaymentCollectionStatus == PaymentCollectionStatus.DoesNotApply).ToList())
                {
                    builder.Append($"{orderCollection.OrderId},");
                }
                stringBuilder.Append(string.Format(_localizationService.GetResource($"{Defaults.LocaleResourcesPrefix}.PaymentCollection.CollectionNotRequiredForSelectedOrders"), builder.ToString()));

                result.Success = false;
            }
        }

        private PaymentCollectionStatus GetPaymentCollectionStatusByPaymentMethod(Order order)
            => _orderPaymentMethodService.GetOrderPaymentMethodName(order).Equals(Defaults.CashPaymentCheckoutAttributeName) ?
                PaymentCollectionStatus.NotCollected : PaymentCollectionStatus.DoesNotApply;

        #endregion

        #region Methods

        ///<inheritdoc/>
        public void MarkOrderPaymentAsCollected(Order order)
        {
            var orderPaymentCollection = _orderPaymentCollectionRepository.Table.FirstOrDefault(mapping => mapping.OrderId == order.Id);

            if (orderPaymentCollection is null)
                throw new ArgumentNullException(nameof(order), string.Format(_localizationService.GetResource($"{Defaults.LocaleResourcesPrefix}.PaymentCollection.PaymentCollectionNotFound"), order.Id));

            orderPaymentCollection.PaymentCollectionStatus = PaymentCollectionStatus.Collected;
            orderPaymentCollection.CollectedOnUtc = DateTime.UtcNow;
            orderPaymentCollection.CollectedByCustomerId = _workContext.GetCurrentCustomerAsync().GetAwaiter().GetResult().Id;

            _orderPaymentCollectionRepository.Update(orderPaymentCollection);
        }

        ///<inheritdoc/>
        public ValidateOrdersForPaymentCollectionResult GetValidOrdersForPymentCollectionByOrderIds(int[] orderIds)
        {
            var stringBuilder = new StringBuilder();
            var result = new ValidateOrdersForPaymentCollectionResult { Success = true, Orders = new List<Order>() };

            if (orderIds == null || orderIds.Length == 0)
            {
                stringBuilder.Append(_localizationService.GetResource($"{Defaults.LocaleResourcesPrefix}.PaymentCollection.SelectAnOrderError"));
                result.Success = false;
            }
            

            var orders = ValidateOrdersForPaymentCollection(orderIds, stringBuilder, result);
            ValidateOrderDeliveryStatusForPaymentCollection(orderIds, stringBuilder, result);
            ValidateOrderCollectionStatusForPaymentCollection(orderIds, stringBuilder, result);

            if (result.Success)
            {
                result.Orders = orders;
            }
            else
            {
                result.Message = stringBuilder.ToString();
            }

            return result;
        }

        ///<inheritdoc/>
        public ValidateOrdersForPaymentCollectionResult GetValidOrdersToPayDriver(int[] orderIds)
        {
            var stringBuilder = new StringBuilder();
            var result = new ValidateOrdersForPaymentCollectionResult { Success = true, OrdersPendingToClose = new List<OrderPendingToClosePayment>() };

            if (orderIds == null || orderIds.Length == 0)
            {
                stringBuilder.Append(_localizationService.GetResource($"{Defaults.LocaleResourcesPrefix}.PaymentCollection.SelectAnOrderError"));
                result.Success = false;
            }

            var orders = ValidateDriverAsPaid(orderIds, stringBuilder, result);

            if (result.Success)
            {
                result.OrdersPendingToClose = orders;
            }
            else
            {
                result.Message = stringBuilder.ToString();
            }

            return result;
        }

        ///<inheritdoc/>
        public ValidateOrdersForPaymentCollectionResult GetValidOrdersToPayVendor(int[] orderIds)
        {
            var stringBuilder = new StringBuilder();
            var result = new ValidateOrdersForPaymentCollectionResult { Success = true, OrdersPendingToClose = new List<OrderPendingToClosePayment>() };

            if (orderIds == null || orderIds.Length == 0)
            {
                stringBuilder.Append(_localizationService.GetResource($"{Defaults.LocaleResourcesPrefix}.PaymentCollection.SelectAnOrderError"));
                result.Success = false;
            }

            var orders = ValidateVendorAsPaid(orderIds, stringBuilder, result);

            if (result.Success)
            {
                result.OrdersPendingToClose = orders;
            }
            else
            {
                result.Message = stringBuilder.ToString();
            }

            return result;
        }

        ///<inheritdoc/>
        public void CreateOrderPaymentCollectionStatus(int driverId, Order order)
        {
            var orderCollectionStatus = _orderPaymentCollectionRepository.Table.FirstOrDefault(opc => opc.OrderId == order.Id);

            if (orderCollectionStatus != null)
            {
                throw new ArgumentException(string.Format(_localizationService.GetResource($"{Defaults.LocaleResourcesPrefix}.PaymentCollection.PaymentCollectionAlreadyRegistered"), order.Id));
            }

            _orderPaymentCollectionRepository.Insert(new OrderPaymentCollectionStatus
            {
                OrderId = order.Id,
                CustomerId = driverId,
                OrderTotal = order.OrderTotal,
                PaymentCollectionStatus = GetPaymentCollectionStatusByPaymentMethod(order),
                CreatedOnUtc = DateTime.UtcNow,
                CollectedByCustomerId = null,
                CollectedOnUtc = null
            });
        }

        ///<inheritdoc/>
        public IList<OrderPaymentCollectionStatus> GetOrdersPendingToCollectByDriverCustomerId(int driverCustomerId)
        {
            if (driverCustomerId == 0)
                throw new ArgumentException("DriverCustomerIdInvalid");

            return _orderPaymentCollectionRepository.Table.Where(opc => opc.CustomerId == driverCustomerId
                && opc.PaymentCollectionStatusId == (int)PaymentCollectionStatus.NotCollected).ToList();
        }

        #endregion
    }
}
