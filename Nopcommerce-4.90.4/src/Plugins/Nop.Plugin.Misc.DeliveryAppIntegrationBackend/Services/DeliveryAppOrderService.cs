using Nop.Core;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Discounts;
using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Payments;
using Nop.Core.Domain.Shipping;
using Nop.Core.Domain.Vendors;
using Nop.Data;
using Nop.Plugin.Api.Delta;
using Nop.Plugin.Api.DTO.Orders;
using Nop.Plugin.Api.DTO.Products;
using Nop.Plugin.Api.Helpers;
using Nop.Plugin.Api.Services;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domain;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains.Enums;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.DTO;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Helpers;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models;
using Nop.Services.Catalog;
using Nop.Services.Common;
using Nop.Services.Customers;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Orders;
using Nop.Services.Shipping;
using Nop.Services.Vendors;
using NUglify.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using static Nop.Plugin.Api.Infrastructure.Constants;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Services
{
    /// <summary>
    /// Represents a implementation for the delivery app order services.
    /// </summary>
    public sealed class DeliveryAppOrderService : IDeliveryAppOrderService
    {
        #region Fields

        private readonly ICustomerService _customerService;
        private readonly IDeliveryAppAddressService _deliveryAppAddressService;
        private readonly IDTOHelper _dtoHelper;
        private readonly IOrderApiService _orderApiService;
        private readonly IOrderService _orderService;
        private readonly IShipmentService _shipmentService;
        private readonly IRepository<AddressGeoCoordinatesMapping> _addressGeoCoordinateRepository;
        private readonly IRepository<MessengerDeclinedOrderMapping> _messengerDeclinedOrderMappingRepository;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<OrderDeliveryStatusMapping> _orderDeliveryStatusMappingRepository;
        private readonly IRepository<OrderItem> _orderItemRepository;
        private readonly IRepository<OrderRatingMapping> _orderRatingRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<VendorWarehouseMapping> _vendorWarehouseMappingRepository;
        private readonly IRepository<Warehouse> _warehouseRepository;
        private readonly IProductService _productService;
        private readonly IRepository<DriverLocationInfoMapping> _driverLocationInfoMappingRepository;
        private readonly INotificationCenterService _notificationCenterService;
        private readonly INotificationRequestBuilder _notificationRequestBuilder;
        private readonly IOrderProcessingService _orderProcessingService;
        private readonly ICustomerActivityService _customerActivityService;
        private readonly ILocalizationService _localizationService;
        private readonly IShippingService _shippingService;
        private readonly IGenericAttributeService _genericAttributeService;
        private readonly IStoreContext _storeContext;
        private readonly ICheckoutAttributeParser _checkoutAttributeParser;
        private readonly IRepository<CheckoutAttributeValue> _checkoutAttributeValueRepository;
        private readonly ICheckoutAttributeFormatter _checkoutAttributeFormatter;
        private readonly IOrderPaymentCollectionService _orderPaymentCollectionService;
        private readonly DeliveryAppBackendConfigurationSettings _deliveryAppBackendConfigurationSettings;
        private readonly IOrderPaymentMethodService _orderPaymentMethodService;
        private readonly IRepository<CustomerPendingReviewMapping> _customerReviewMappingRepository;
        private readonly IVendorService _vendorService;
        private readonly IRepository<Discount> _discountRepository;
        private readonly IRepository<CustomerDiscountMapping> _customerDiscountMappingRepository;
        private readonly IRepository<ProductAttributeMapping> _productAttributeRepository;
        private readonly IProductAttributeService _productAttributeService;

        #endregion

        #region Ctor

        /// <summary>
        /// Initializes a new instance of <see cref="DeliveryAppOrderService"/>.
        /// </summary>
        /// <param name="customerService">An implementation of <see cref="ICustomerService"/>.</param>
        /// <param name="deliveryAppAddressService">An implementation of <see cref="IDeliveryAppAddressService"/>.</param>
        /// <param name="dtoHelper">An implementation of <see cref="IDTOHelper"/>.</param>
        /// <param name="orderApiService">An implementation of <see cref="IOrderApiService"/>.</param>
        /// <param name="orderService">An implementation of <see cref="IOrderService"/>.</param>
        /// <param name="shipmentService">An implementation of <see cref="IShipmentService"/>.</param>
        /// <param name="addressGeoCoordinateRepository">An implementation of <see cref="IRepository{T}"/> where T is <see cref="AddressGeoCoordinatesMapping"/>.</param>
        /// <param name="messengerDeclinedOrderMappingRepository">An implementation of <see cref="IRepository{T}"/> where T is <see cref="MessengerDeclinedOrderMapping"/>.</param>
        /// <param name="orderRepository">An implementation of <see cref="IRepository{T}"/> where T is <see cref="Order"/>.</param>
        /// <param name="orderDeliveryStatusMappingRepository">An implementation of <see cref="IRepository{T}"/> where T is <see cref="OrderDeliveryStatusMapping"/>.</param>
        /// <param name="orderItemRepository">An implementation of <see cref="IRepository{T}"/> where T is <see cref="OrderItem"/>.</param>
        /// <param name="orderRatingRepository">An implementation of <see cref="IRepository{T}"/> where T is <see cref="OrderRatingMapping"/>.</param>
        /// <param name="productRepository">An implementation of <see cref="IRepository{T}"/> where T is <see cref="Product"/>.</param>
        /// <param name="vendorWarehouseMappingRepository">An implementation of <see cref="IRepository{T}"/> where T is <see cref="VendorWarehouseMapping"/>.</param>
        /// <param name="warehouseRepository">An implementation of <see cref="IRepository{T}"/> where T is <see cref="Warehouse"/>.</param>
        /// <param name="productService">An implementation of <see cref="IRepository{T}"/> where T is <see cref="Product"/>.</param>
        /// <param name="orderProcessingService">An implementation of <see cref="IOrderProcessingService"/>.</param>
        /// <param name="customerActivityService">An implementation of <see cref="ICustomerActivityService"/>.</param>
        /// <param name="localizationService">An implementation of <see cref="ILocalizationService"/>.</param>
        /// <param name="shippingService">An implementation of <see cref="IShippingService"/>.</param>
        /// <param name="genericAttributeService">An implementation of <see cref="IGenericAttributeService"/>.</param>
        /// <param name="storeContext">An implementation of <see cref="IStoreContext"/>.</param>
        /// <param name="checkoutAttributeParser">An implementation of <see cref="ICheckoutAttributeParser"/>.</param>
        /// <param name="checkoutAttributeValueRepository">An implementation of <see cref="IRepository{CheckoutAttributeValue}"/>.</param>
        /// <param name="checkoutAttributeFormatter">An implementation of <see cref="ICheckoutAttributeFormatter"/>.</param>
        /// <param name="orderPaymentCollectionService">An implementation of <see cref="IOrderPaymentCollectionService"/>.</param>
        /// <param name="deliveryAppBackendConfigurationSettings">An instance of <see cref="DeliveryAppBackendConfigurationSettings"/>.</param>
        /// <param name="orderPaymentMethodService">An implementation of <see cref="IOrderPaymentMethodService"/>.</param>
        /// <param name="driverLocationInfoMappingRepository">An implementation of <see cref="IRepository{T}"/> where T is <see cref="DriverLocationInfoMapping"/>.</param>
        /// <param name="notificationCenterService">An implementation of <see cref="INotificationCenterService"/>.</param>
        /// <param name="notificationRequestBuilder">An implementation of <see cref="INotificationRequestBuilder"/>.</param>
        /// <param name="customerReviewMappingRepository">An implementation of <see cref="IRepository{CustomerPendingReviewMapping}"/>.</param>
        /// <param name="vendorService">An implementation of <see cref="IVendorService"/>.</param>
        /// <param name="customerDiscountMappingRepository">An implementation of <see cref="IRepository{CustomerDiscountMapping}"/>.</param>
        /// <param name="productAttributeRepository"> An implementation of <see cref="IRepository{ProductAttributeMapping}"/></param>
        /// <param name="productAttributeService">An implementation of <see cref="IProductAttributeService"/></param>
        public DeliveryAppOrderService(
            ICustomerService customerService,
            IDeliveryAppAddressService deliveryAppAddressService,
            IDTOHelper dtoHelper,
            IOrderApiService orderApiService,
            IOrderService orderService,
            IShipmentService shipmentService,
            IRepository<AddressGeoCoordinatesMapping> addressGeoCoordinateRepository,
            IRepository<MessengerDeclinedOrderMapping> messengerDeclinedOrderMappingRepository,
            IRepository<Order> orderRepository,
            IRepository<OrderDeliveryStatusMapping> orderDeliveryStatusMappingRepository,
            IRepository<OrderItem> orderItemRepository,
            IRepository<OrderRatingMapping> orderRatingRepository,
            IRepository<Product> productRepository,
            IRepository<VendorWarehouseMapping> vendorWarehouseMappingRepository,
            IRepository<Warehouse> warehouseRepository,
            IProductService productService,
            IRepository<DriverLocationInfoMapping> driverLocationInfoMappingRepository,
            INotificationCenterService notificationCenterService,
            INotificationRequestBuilder notificationRequestBuilder,
            IOrderProcessingService orderProcessingService,
            ICustomerActivityService customerActivityService,
            ILocalizationService localizationService,
            IShippingService shippingService,
            IGenericAttributeService genericAttributeService,
            IStoreContext storeContext,
            ICheckoutAttributeParser checkoutAttributeParser,
            IRepository<CheckoutAttributeValue> checkoutAttributeValueRepository,
            ICheckoutAttributeFormatter checkoutAttributeFormatter,
            IOrderPaymentCollectionService orderPaymentCollectionService,
            DeliveryAppBackendConfigurationSettings deliveryAppBackendConfigurationSettings,
            IOrderPaymentMethodService orderPaymentMethodService,
            IRepository<CustomerPendingReviewMapping> customerReviewMappingRepository,
            IVendorService vendorService,
            IRepository<Discount> discountRepository,
            IRepository<CustomerDiscountMapping> customerDiscountMappingRepository,
            IRepository<ProductAttributeMapping> productAttributeRepository,
            IProductAttributeService productAttributeService)
        {
            _customerService = customerService;
            _deliveryAppAddressService = deliveryAppAddressService;
            _dtoHelper = dtoHelper;
            _orderApiService = orderApiService;
            _orderService = orderService;
            _shipmentService = shipmentService;
            _addressGeoCoordinateRepository = addressGeoCoordinateRepository;
            _messengerDeclinedOrderMappingRepository = messengerDeclinedOrderMappingRepository;
            _orderRepository = orderRepository;
            _orderDeliveryStatusMappingRepository = orderDeliveryStatusMappingRepository;
            _orderItemRepository = orderItemRepository;
            _orderRatingRepository = orderRatingRepository;
            _productRepository = productRepository;
            _vendorWarehouseMappingRepository = vendorWarehouseMappingRepository;
            _warehouseRepository = warehouseRepository;
            _productService = productService;
            _driverLocationInfoMappingRepository = driverLocationInfoMappingRepository;
            _notificationCenterService = notificationCenterService;
            _notificationRequestBuilder = notificationRequestBuilder;
            _orderProcessingService = orderProcessingService;
            _customerActivityService = customerActivityService;
            _localizationService = localizationService;
            _shippingService = shippingService;
            _genericAttributeService = genericAttributeService;
            _storeContext = storeContext;
            _checkoutAttributeParser = checkoutAttributeParser;
            _checkoutAttributeValueRepository = checkoutAttributeValueRepository;
            _checkoutAttributeFormatter = checkoutAttributeFormatter;
            _orderPaymentCollectionService = orderPaymentCollectionService;
            _deliveryAppBackendConfigurationSettings = deliveryAppBackendConfigurationSettings;
            _orderPaymentMethodService = orderPaymentMethodService;
            _customerReviewMappingRepository = customerReviewMappingRepository;
            _vendorService = vendorService;
            _discountRepository = discountRepository;
            _customerDiscountMappingRepository = customerDiscountMappingRepository;
            _productAttributeRepository = productAttributeRepository;
            _productAttributeService = productAttributeService;
        }

        #endregion

        #region Utilities

        private void InsertOrderShipment(int orderId)
        {
            var shipment = new Shipment
            {
                OrderId = orderId,
                CreatedOnUtc = DateTime.UtcNow
            };

            _shipmentService.InsertShipment(shipment);
            InsertShipmentItems(orderId, shipment.Id);
        }

        private void InsertShipmentItems(int orderId, int shipmentId)
        {
            IList<OrderItem> orderItems = _orderService.GetOrderItems(orderId);

            foreach (OrderItem item in orderItems)
            {
                _shipmentService.InsertShipmentItem(new ShipmentItem
                {
                    ShipmentId = shipmentId,
                    OrderItemId = item.Id,
                    Quantity = item.Quantity,
                    WarehouseId = 0
                });
            }
        }

        private IList<OrderDto> FilterOrdersByLocation(IList<OrderDto> orders, decimal latitude, decimal longitude)
        {
            var orderShippingAddressIds = orders.Select(order => order.ShippingAddress?.Id);
            var shippingAddressGeoCoordinates = new List<AddressGeoCoordinatesMapping>();

            foreach (int id in orderShippingAddressIds)
            {
                var geoCoordinates = _addressGeoCoordinateRepository.Table.FirstOrDefault(coordinate => coordinate.AddressId == id);
                if (geoCoordinates != null) shippingAddressGeoCoordinates.Add(geoCoordinates);
            }

            var filteredOrders = new List<OrderDistanceModel>();

            foreach (AddressGeoCoordinatesMapping coordinate in shippingAddressGeoCoordinates)
            {
                double distance = _deliveryAppAddressService.GetDistanceOnMeters(coordinate, latitude, longitude);
                if (distance <= 30000D)
                {
                    OrderDto order = orders.FirstOrDefault(order => order.ShippingAddress.Id == coordinate.AddressId);
                    filteredOrders.Add(new OrderDistanceModel { Distance = distance, Order = order });
                }
            }

            return filteredOrders.OrderBy(order => order.Distance)
                                 .ThenBy(order => order.Order.CreatedOnUtc)
                                 .Select(order => order.Order).ToList();
        }

        private bool IsDeliveryAppMessenger(Customer customer)
        {
            IList<CustomerRole> customerRoles = _customerService.GetCustomerRoles(customer, true);
            return customerRoles.Any(role => role.SystemName == "Mensajero");
        }

        private bool EvaluateIfOrderPertainVendor(int orderId, int vendorId)
        {
            IList<int> orderItemsProductsIds = _orderItemRepository.Table
                .Where(orderItem => orderItem.OrderId == orderId)
                .Select(orderItem => orderItem.ProductId)
                .ToList();
            IList<int> productsVendorIds = _productRepository.Table
                .Where(product => orderItemsProductsIds.Contains(product.Id) && !product.Deleted)
                .Select(product => product.VendorId)
                .ToList();
            return productsVendorIds.All(id => id == vendorId);
        }

        private bool ValidateDriverDoesNotExceedsMaxCashAmount(int driverCustomerId, decimal orderTotal)
        {
            var pendingToCollectOrders = _orderPaymentCollectionService.GetOrdersPendingToCollectByDriverCustomerId(driverCustomerId);
            if (pendingToCollectOrders != null && pendingToCollectOrders.Any() && _deliveryAppBackendConfigurationSettings.MaxMoneyAmountDriverCanCarry != 0)
            {
                if (pendingToCollectOrders.Sum(opc => opc.OrderTotal) + orderTotal > _deliveryAppBackendConfigurationSettings.MaxMoneyAmountDriverCanCarry)
                {
                    return false;
                }
            }
            return true;
        }

        private void SetProductAttributeMappings(IList<OrderDto> orders)
        {
            foreach (var order in orders)
            {
                foreach (var item in order.OrderItems)
                {
                    IEnumerable<int> attributeIds = item.Attributes.Select(attribute => attribute.Id);

                    IQueryable<ProductAttributeMapping> attributesMappings = _productAttributeRepository.Table.Where(x => attributeIds.Contains(x.Id));

                    IList<ProductAttributeMappingDto> attributesMappingsdto = new List<ProductAttributeMappingDto>();

                    foreach (var attributesMapping in attributesMappings)
                    {
                        ProductAttribute attribute = _productAttributeService.GetProductAttributeById(attributesMapping.ProductAttributeId);
                        List<ProductAttributeValue> attributeValues = _productAttributeService.GetProductAttributeValues(attributesMapping.Id).ToList();

                        attributesMappingsdto.Add(new ProductAttributeMappingDto
                        {
                            ProductAttributeId = attributesMapping.ProductAttributeId,
                            Id = attributesMapping.Id,
                            AttributeControlTypeId = attributesMapping.AttributeControlTypeId,
                            AttributeControlType = attributesMapping.AttributeControlType.ToString(),
                            DefaultValue = attributesMapping.DefaultValue,
                            ProductAttributeName = attribute.Name,
                            ProductAttributeValues = attributeValues.Select(value => new ProductAttributeValueDto
                            {
                                Id = value.Id,
                                AssociatedProductId = value.AssociatedProductId,
                                AttributeValueType = value.AttributeValueType.ToString(),
                                AttributeValueTypeId = value.AttributeValueTypeId,
                                ColorSquaresRgb = value.ColorSquaresRgb,
                                Cost = value.Cost,
                                DisplayOrder = value.DisplayOrder,
                                IsPreSelected = value.IsPreSelected,
                                Name = value.Name,
                                PictureId = value.PictureId,
                                PriceAdjustment = value.PriceAdjustment,
                                Quantity = value.Quantity,
                                WeightAdjustment = value.WeightAdjustment,
                            }).ToList(),
                        });
                    }

                    item.Product.ProductAttributeMappings = attributesMappingsdto.ToList();
                }
            }
        }

        #endregion

        #region Methods

        ///<inheritdoc/>
        public void ChangeStatusToAcceptedByMessenger(int orderId, int customerId)
        {
            if (customerId == 0) throw new ArgumentException("InvalidMessengerId");
            Customer customer = _customerService.GetCustomerById(customerId);
            if (customer is null) throw new ArgumentException("DriverNotFound");
            OrderDeliveryStatusMapping orderDeliveryStatus = _orderDeliveryStatusMappingRepository.Table
                .FirstOrDefault(mapping => mapping.OrderId == orderId);
            if (orderDeliveryStatus is null) throw new ArgumentException("OrderDeliveryStatusCouldNotBeFound");
            if (orderDeliveryStatus.CustomerId != null)
                throw new ArgumentException("OrderAlreadyAcceptedForAnotherMessenger");

            Order order = _orderService.GetOrderById(orderId);

            if (order is null)
                throw new ArgumentException("OrderNotFound");

            if (_orderPaymentMethodService.GetOrderPaymentMethodName(order).Equals(Defaults.CashPaymentCheckoutAttributeName))
            {
                if (!ValidateDriverDoesNotExceedsMaxCashAmount(customerId, order.OrderTotal))
                    throw new InvalidOperationException("MessengerCashAmountIsTooHigh");

                _orderPaymentCollectionService.CreateOrderPaymentCollectionStatus(customerId, order);
            }

            orderDeliveryStatus.CustomerId = customerId;
            orderDeliveryStatus.DeliveryStatusId = (int)DeliveryStatus.AssignedToMessenger;
            orderDeliveryStatus.AcceptedByMessengerDate = DateTime.UtcNow;

            _orderDeliveryStatusMappingRepository.Update(orderDeliveryStatus);


            _notificationCenterService.SendNotification(_notificationRequestBuilder.Build(NotificationTemplateType.DriverClientOrderAccepted, orderId));
            _notificationCenterService.SendNotification(_notificationRequestBuilder.Build(NotificationTemplateType.DriverCommerceOrderAccepted, orderId));

            var driverRequest = new DriverLocationInfoRequest
            {
                OrderId = orderId,
                CanContactDriver = false,
            };

            _notificationCenterService.SendDriverCoordinateTrackingUpdate(driverRequest);
        }

        ///<inheritdoc/>
        public void ChangeStatusToDelivered(int orderId)
        {
            Order foundOrder = _orderService.GetOrderById(orderId);
            if (foundOrder is null) throw new ArgumentException("OrderNotFound");
            IList<Shipment> orderShipments = _shipmentService.GetShipmentsByOrderId(orderId);
            if (orderShipments.Count == 0) throw new ArgumentException("OrderShipmentCouldNotBeFound");
            OrderDeliveryStatusMapping orderDeliveryStatus = _orderDeliveryStatusMappingRepository.Table
                .FirstOrDefault(mapping => mapping.OrderId == orderId);
            if (orderDeliveryStatus is null) throw new ArgumentException("OrderDeliveryStatusCouldNotBeFound");

            IList<OrderItem> foundStoreOrderItems = _orderService.GetOrderItems(orderId);

            if (foundStoreOrderItems.Count == 0) throw new ArgumentException("OrderWithoutItems");

            Product foundStoreProduct = _productService.GetProductById(foundStoreOrderItems.First().ProductId);

            Vendor foundVendor = _vendorService.GetVendorById(foundStoreProduct.VendorId);

            if (foundVendor is null)
                throw new ArgumentException("VendorNotFound");

            foundOrder.OrderStatusId = (int)OrderStatus.Complete;
            foundOrder.ShippingStatusId = (int)ShippingStatus.Delivered;
            _orderService.UpdateOrder(foundOrder);

            foreach (Shipment shipment in orderShipments)
            {
                shipment.DeliveryDateUtc = DateTime.UtcNow;
                _shipmentService.UpdateShipment(shipment);
            }

            bool isCashOrKeycard = foundOrder.CheckoutAttributeDescription.Contains(Defaults.CashPaymentCheckoutAttributeName) ||
                                foundOrder.CheckoutAttributeDescription.Contains(Defaults.ClaveCardPaymentCheckoutAttributeName);

            if (isCashOrKeycard)
            {
                foundOrder.PaymentStatus = PaymentStatus.Paid;
                _orderService.UpdateOrder(foundOrder);
            }

            orderDeliveryStatus.DeliveryStatusId = (int)DeliveryStatus.Delivered;
            orderDeliveryStatus.DeliveredDate = DateTime.UtcNow;
            _orderDeliveryStatusMappingRepository.Update(orderDeliveryStatus);

            _customerReviewMappingRepository.Insert(new CustomerPendingReviewMapping
            {
                CustomerId = foundOrder.CustomerId,
                OrderId = foundOrder.Id,
                VendorId = foundVendor.Id,
                PendingReviewStatus = PendingReviewStatus.PendingToRate,
                CreatedOnUtc = DateTime.UtcNow
            });

            _notificationCenterService.SendNotification(_notificationRequestBuilder.Build(NotificationTemplateType.DriverClientOrderDelivered, orderId));
            _notificationCenterService.SendNotification(_notificationRequestBuilder.Build(NotificationTemplateType.DriverCommerceOrderDelivered, orderId));

            _notificationCenterService.SendOrderStatusTrackingUpdate(orderId);
        }

        ///<inheritdoc/>
        public void ChangeStatusToRetrievedByMessenger(int orderId)
        {
            Order foundOrder = _orderService.GetOrderById(orderId);
            if (foundOrder is null) throw new ArgumentException("OrderNotFound");
            IList<Shipment> orderShipments = _shipmentService.GetShipmentsByOrderId(orderId);
            if (orderShipments.Count == 0) throw new ArgumentException("OrderShipmentCouldNotBeFound");
            OrderDeliveryStatusMapping orderDeliveryStatus = _orderDeliveryStatusMappingRepository.Table
                .FirstOrDefault(mapping => mapping.OrderId == orderId);
            if (orderDeliveryStatus is null) throw new ArgumentException("OrderDeliveryStatusCouldNotBeFound");

            foundOrder.ShippingStatusId = (int)ShippingStatus.Shipped;
            _orderService.UpdateOrder(foundOrder);

            foreach (Shipment shipment in orderShipments)
            {
                shipment.ShippedDateUtc = DateTime.UtcNow;
                _shipmentService.UpdateShipment(shipment);
            }

            orderDeliveryStatus.DeliveryStatusId = (int)DeliveryStatus.DeliveryInProgress;
            orderDeliveryStatus.DeliveryInProgressDate = DateTime.UtcNow;
            _orderDeliveryStatusMappingRepository.Update(orderDeliveryStatus);

            _notificationCenterService.SendNotification(_notificationRequestBuilder.Build(NotificationTemplateType.DriverClientOrderRetreived, orderId));
        }

        ///<inheritdoc/>
        public void SetOrderReadyToPickup(int orderId)
        {
            Order foundOrder = _orderService.GetOrderById(orderId);
            if (foundOrder is null) throw new ArgumentException("OrderNotFound");

            OrderDeliveryStatusMapping orderDeliveryStatus = _orderDeliveryStatusMappingRepository.Table
                                                            .FirstOrDefault(mapping => mapping.OrderId == orderId);

            if (orderDeliveryStatus is null)
                throw new ArgumentException("OrderDeliveryStatusCouldNotBeFound");

            if (orderDeliveryStatus?.CustomerId is null)
                throw new ArgumentException("DoesNotHaveDriverAssignedToTheOrder");

            if (orderDeliveryStatus.DeliveryStatusId >= (int)DeliveryStatus.OrderPreparationCompleted)
                throw new ArgumentException("OrderAlreadyMarkedAsReadyToPickup");

            if (new[] { ShippingStatus.Delivered, ShippingStatus.ShippingNotRequired }.Contains(foundOrder.ShippingStatus))
                throw new ArgumentException("OrderShippedOrShippingNotRequired");

            foundOrder.OrderStatusId = (int)OrderStatus.Processing;
            foundOrder.ShippingStatusId = (int)ShippingStatus.NotYetShipped;

            _orderService.UpdateOrder(foundOrder);

            IList<Shipment> orderShipments = _shipmentService.GetShipmentsByOrderId(orderId);

            if (orderShipments.Count == 0) InsertOrderShipment(orderId);

            orderDeliveryStatus.DeliveryStatusId = (int)DeliveryStatus.OrderPreparationCompleted;
            _orderDeliveryStatusMappingRepository.Update(orderDeliveryStatus);

            _notificationCenterService.SendNotification(_notificationRequestBuilder.Build(NotificationTemplateType.CommerceDriverOrderCompleted, orderId));
            _notificationCenterService.SendNotification(_notificationRequestBuilder.Build(NotificationTemplateType.CommerceClientOrderCompleted, orderId));
        }

        ///<inheritdoc/>
        public void RegisterOrderDeclinedByMessenger(int orderId, string message, int customerId)
        {
            if (customerId == 0) throw new ArgumentException("InvalidMessengerId");
            Customer customer = _customerService.GetCustomerById(customerId);
            if (customer is null) throw new ArgumentException("DriverNotFound");
            OrderDeliveryStatusMapping orderDeliveryStatus = _orderDeliveryStatusMappingRepository.Table
                .FirstOrDefault(mapping => mapping.OrderId == orderId);
            if (orderDeliveryStatus is null) throw new ArgumentException("OrderDeliveryStatusCouldNotBeFound");
            MessengerDeclinedOrderMapping messengerDeclinedOrder = _messengerDeclinedOrderMappingRepository.Table
                .FirstOrDefault(mapping => mapping.OrderId == orderId && mapping.CustomerId == customer.Id);
            if (messengerDeclinedOrder != null) throw new ArgumentException("OrderAlreadyDeclinedByMessenger");

            orderDeliveryStatus.DeliveryStatusId = (int)DeliveryStatus.AwaitingForMessenger;
            orderDeliveryStatus.AwaitingForMessengerDate = DateTime.UtcNow;
            _orderDeliveryStatusMappingRepository.Update(orderDeliveryStatus);

            _orderService.InsertOrderNote(new OrderNote
            {
                OrderId = orderId,
                CreatedOnUtc = DateTime.UtcNow,
                DisplayToCustomer = false,
                Note = $"{customer.Email} rejected by delivery the order. Driver message: {message}"
            });

            _messengerDeclinedOrderMappingRepository.Insert(new MessengerDeclinedOrderMapping
            {
                CustomerId = customer.Id,
                OrderId = orderId,
                DeclinedDate = DateTime.UtcNow,
                DeclinedMessage = message
            });

            _notificationCenterService.SendNotification(_notificationRequestBuilder.Build(NotificationTemplateType.DriverCommerceOrderCancelled, orderId));
        }

        ///<inheritdoc/>
        public IList<OrderDto> GetOrdersReadyToPickup(int driverId, decimal latitude, decimal longitude)
        {
            List<int> declinedOrdersByMessenger = _messengerDeclinedOrderMappingRepository.Table
                                            .Where(x => x.CustomerId.Equals(driverId))
                                            .Select(x => x.OrderId).ToList();

            if (latitude == decimal.Zero || longitude == decimal.Zero) throw new ArgumentException("GeoCoordinatesCantBeZero");
            IList<int> readyToPickupOrderIds = _orderDeliveryStatusMappingRepository.Table
                .Where(mapping => mapping.DeliveryStatusId == (int)DeliveryStatus.AwaitingForMessenger
                        && !declinedOrdersByMessenger.Contains(mapping.OrderId))
                .Select(mapping => mapping.OrderId)
                .ToList();
            IList<OrderDto> orders = readyToPickupOrderIds.Count <= 0 ? new List<OrderDto>() :
                _orderApiService.GetOrders(ids: readyToPickupOrderIds).Select(x => _dtoHelper.PrepareOrderDTO(x)).ToList();

            return FilterOrdersByLocation(orders, latitude, longitude);
        }

        ///<inheritdoc/>
        public IList<OrderDto> GetOrdersAssignedToMessenger(int customerId)
        {
            if (customerId == 0) throw new ArgumentException("InvalidMessengerId");
            Customer customer = _customerService.GetCustomerById(customerId);
            if (customer is null) throw new ArgumentException("DriverNotFound");
            if (!IsDeliveryAppMessenger(customer)) throw new ArgumentException("MissingPermissions");
            IList<int> ordersAssignedToMessengerIds = _orderDeliveryStatusMappingRepository.Table
                .Where(mapping => mapping.CustomerId == customer.Id
                                  && mapping.DeliveryStatusId != (int)DeliveryStatus.AwaitingForMessenger
                                  && mapping.DeliveryStatusId != (int)DeliveryStatus.Delivered)
                .Select(mapping => mapping.OrderId)
                .ToList();

            return ordersAssignedToMessengerIds.Count <= 0 ? new List<OrderDto>() :
                _orderApiService.GetOrders(ids: ordersAssignedToMessengerIds).Select(x => _dtoHelper.PrepareOrderDTO(x)).ToList();
        }

        ///<inheritdoc/>
        public IList<OrderDto> GetOrdersDeliveredByMessenger(int customerId)
        {
            if (customerId == 0) throw new ArgumentException("InvalidMessengerId");
            Customer customer = _customerService.GetCustomerById(customerId);
            if (customer is null) throw new ArgumentException("DriverNotFound");
            if (!IsDeliveryAppMessenger(customer)) throw new ArgumentException("MissingPermissions");
            IList<int> ordersAssignedToMessengerIds = _orderDeliveryStatusMappingRepository.Table
                .Where(mapping => mapping.CustomerId == customer.Id
                                  && mapping.DeliveryStatusId == (int)DeliveryStatus.Delivered)
                .OrderByDescending(mapping => mapping.OrderId).Take(Configurations.DefaultLimit)
                .Select(mapping => mapping.OrderId)
                .ToList();

            return ordersAssignedToMessengerIds.Count <= 0 ? new List<OrderDto>() :
                _orderApiService.GetOrders(ids: ordersAssignedToMessengerIds).Select(x => _dtoHelper.PrepareOrderDTO(x)).ToList();
        }

        ///<inheritdoc/>
        public IList<OrderDto> GetPendingOrdersByVendorId(int vendorId)
        {
            if (vendorId == 0) throw new ArgumentException("VendorIdCantBeZero");
            IList<int> vendorProductIds = _productRepository.Table
                .Where(product => product.VendorId == vendorId && !product.Deleted)
                .Select(product => product.Id)
                .ToList();
            IList<int> orderItemsOrderIds = _orderItemRepository.Table
                .Where(orderItem => vendorProductIds.Contains(orderItem.ProductId))
                .Select(orderItem => orderItem.OrderId)
                .ToList();

            List<OrderDto> ordersThatWillRecievePaymentAtCustomerHouse = _orderApiService.GetOrders(ids: orderItemsOrderIds, status: OrderStatus.Pending)
                                .Where(x => (x.CheckoutAttributeDescription.Contains(Defaults.CashPaymentCheckoutAttributeName)
                                       || x.CheckoutAttributeDescription.Contains(Defaults.ClaveCardPaymentCheckoutAttributeName))

                                       )
                                .Select(x => _dtoHelper.PrepareOrderDTO(x))
                                .ToList();

            List<OrderDto> creditCardOrders = _orderApiService.GetOrders(ids: orderItemsOrderIds, status: OrderStatus.Pending, paymentStatus: PaymentStatus.Paid)
                    .Where(x => x.CheckoutAttributeDescription.Contains(Defaults.CreditCardPaymentCheckoutAttributeName))
                    .Select(x => _dtoHelper.PrepareOrderDTO(x))
                    .ToList();

            List<OrderDto> ordersFound = orderItemsOrderIds.Count <= 0 ? new List<OrderDto>() :
                                                   ordersThatWillRecievePaymentAtCustomerHouse.Union(creditCardOrders).ToList();

            SetProductAttributeMappings(ordersFound);

            return ordersFound;
        }

        ///<inheritdoc/>
        public void SetOrderAcceptedByStore(int orderId, int vendorId)
        {
            if (vendorId == 0) throw new ArgumentException("VendorIdCantBeZero");
            Order foundOrder = _orderService.GetOrderById(orderId);
            if (foundOrder is null) throw new ArgumentException("OrderNotFound");

            if (!EvaluateIfOrderPertainVendor(orderId, vendorId)) throw new ArgumentException("NotAuthorized");
            foundOrder.OrderStatusId = (int)OrderStatus.Processing;
            _orderService.UpdateOrder(foundOrder);

            if (_orderDeliveryStatusMappingRepository.Table.FirstOrDefault(mapping => mapping.OrderId == orderId) != null)
                throw new ArgumentException("OrderAlreadyHasDeliveryStatus");

            _orderDeliveryStatusMappingRepository.Insert(new OrderDeliveryStatusMapping
            {
                OrderId = orderId,
                DeliveryStatusId = (int)DeliveryStatus.AwaitingForMessenger,
                AwaitingForMessengerDate = DateTime.UtcNow
            });

            _notificationCenterService.SendNotification(_notificationRequestBuilder.Build(NotificationTemplateType.CommerceClientOrderAccepted, orderId));
        }

        ///<inheritdoc/>
        public IList<OrderDto> GetOrdersInProgressByVendorId(int vendorId)
        {
            if (vendorId == 0) throw new ArgumentException("VendorIdCantBeZero");

            IList<int> vendorProductIds = _productRepository.Table
                .Where(product => product.VendorId == vendorId && !product.Deleted)
                .Select(product => product.Id)
                .ToList();

            IList<int> orderItemsOrderIds = _orderItemRepository.Table
                .Where(orderItem => vendorProductIds.Contains(orderItem.ProductId))
                .Select(orderItem => orderItem.OrderId)
                .ToList();

            IList<OrderDto> ordersFound = orderItemsOrderIds.Count <= 0 ? new List<OrderDto>() :
                                                        _orderApiService
                                                        .GetOrders(ids: orderItemsOrderIds, status: OrderStatus.Processing)
                                                        .Select(x => _dtoHelper.PrepareOrderDTO(x))
                                                        .OrderBy(x => x.CreatedOnUtc).ToList();

            SetProductAttributeMappings(ordersFound);

            return ordersFound;
        }

        ///<inheritdoc/>
        public void InsertOrderRating(int orderId, OrderRatingModel orderRating)
        {
            if (orderId == 0) throw new ArgumentException("OrderIdCantBeZero");

            if (orderRating is null) throw new ArgumentException("InvalidRating");

            Order foundOrder = _orderService.GetOrderById(orderId);

            if (foundOrder is null) throw new ArgumentException("OrderNotFound");

            OrderRatingMapping orderRatingMapping = _orderRatingRepository.Table.FirstOrDefault(mapping => mapping.OrderId == orderId);

            if (orderRatingMapping != null) throw new ArgumentException("OrderAlreadyRated");

            _orderRatingRepository.Insert(new OrderRatingMapping
            {
                OrderId = orderId,
                Rate = orderRating.Rate,
                Comment = orderRating.Comment,
                CreatedOnUtc = DateTime.UtcNow
            });

            _orderService.InsertOrderNote(new OrderNote
            {
                OrderId = orderId,
                DisplayToCustomer = true,
                CreatedOnUtc = DateTime.UtcNow,
                Note = $"Customer rated the order with: {orderRating.Rate}"
            });
        }

        ///<inheritdoc/>
        public void CancelOrder(int orderId, int vendorId, string message)
        {
            if (orderId == 0) throw new ArgumentException("OrderIdCantBeZero");
            if (vendorId == 0) throw new ArgumentException("VendorIdCantBeZero");

            if (string.IsNullOrEmpty(message) || string.IsNullOrWhiteSpace(message))
                throw new ArgumentException("CancellationMessageIsInvalid");

            var order = _orderApiService.GetOrderById(orderId);
            if (order is null) throw new ArgumentException("OrderNotFound");

            if (!EvaluateIfOrderPertainVendor(orderId, vendorId))
                throw new ArgumentException("OrderDoesNotBelongToVendor");


            order.OrderStatus = OrderStatus.Cancelled;
            _orderService.UpdateOrder(order);

            OrderDeliveryStatusMapping orderDeliveryStatusMapping = _orderDeliveryStatusMappingRepository.Table.
                FirstOrDefault(mapping => mapping.OrderId == orderId);

            if (orderDeliveryStatusMapping != null)
            {
                if (orderDeliveryStatusMapping.DeliveryStatusId != (int)DeliveryStatus.AwaitingForMessenger)
                    throw new ArgumentException("OrderInProgress");

                orderDeliveryStatusMapping.DeliveryStatusId = (int)DeliveryStatus.DeclinedByStore;
                orderDeliveryStatusMapping.DeclinedByStoreDate = DateTime.UtcNow;
                orderDeliveryStatusMapping.MessageToDeclinedOrder = message;
                _orderDeliveryStatusMappingRepository.Update(orderDeliveryStatusMapping);
            }
            else
            {
                _orderDeliveryStatusMappingRepository.Insert(new OrderDeliveryStatusMapping
                {
                    OrderId = orderId,
                    DeliveryStatusId = (int)DeliveryStatus.DeclinedByStore,
                    DeclinedByStoreDate = DateTime.UtcNow,
                    MessageToDeclinedOrder = message,
                });
            }

            _orderService.InsertOrderNote(new OrderNote
            {
                OrderId = orderId,
                CreatedOnUtc = DateTime.UtcNow,
                DisplayToCustomer = false,
                Note = $"The vendor/store cancelled the order. Message: {message}"
            });

            _notificationCenterService.SendNotification(_notificationRequestBuilder.Build(NotificationTemplateType.CommerceClientOrderCancelled, orderId));
            _notificationCenterService.SendOrderStatusTrackingUpdate(orderId);
        }

        ///<inheritdoc/>
        public IList<HistoricOrdersByVendorDto> GetHistoricOrdersByVendorId(int vendorId)
        {
            if (vendorId == 0)
                throw new ArgumentException("VendorIdCantBeZero");

            var vendor = _vendorService.GetVendorById(vendorId);

            if (vendor is null)
                throw new ArgumentException("VendorNotFound");

            return (from o in _orderRepository.Table
                    join oi in _orderItemRepository.Table on o.Id equals oi.OrderId
                    join p in _productRepository.Table on oi.ProductId equals p.Id
                    where p.VendorId == vendorId &&
                    (o.OrderStatusId == (int)OrderStatus.Complete ||
                    o.OrderStatusId == (int)OrderStatus.Cancelled) &&
                    !o.Deleted
                    select o)
                          .Select(order => new HistoricOrdersByVendorDto
                          {
                              Id = order.Id,
                              VendorName = vendor.Name,
                              OrderStatus = order.OrderStatusId,
                              OrderItemsLength = GetOrderItemCount(order.Id),
                              OrderSubtotalInclTax = order.OrderSubtotalInclTax,
                              DeliveryDateUtc = GetShipmentsByOrderId(order.Id)
                          })
                          .DistinctBy(x => x.Id)
                          .OrderByDescending(x => x.DeliveryDateUtc)
                          .Take(Configurations.DefaultLimit)
                          .ToList();
        }

        public DateTime? GetShipmentsByOrderId(int orderId)
        {
            var shipment = _shipmentService.GetShipmentsByOrderId(orderId)
                .OrderByDescending(x => x.CreatedOnUtc)
                .FirstOrDefault();

            return shipment?.DeliveryDateUtc;
        }

        public int GetOrderItemCount(int orderId)
        {
            return _orderService.GetOrderItems(orderId).Count;
        }

        ///<inheritdoc/>
        public CoordinateRequest GetOrderCoordinates(int orderId)
        {
            if (orderId == 0) throw new ArgumentException("OrderIdCantBeZero");

            IList<OrderItem> foundStoreOrderItems = _orderService.GetOrderItems(orderId);

            if (foundStoreOrderItems.Count == 0) throw new ArgumentException("OrderWithoutItems");

            Product foundStoreProduct = _productService.GetProductById(foundStoreOrderItems.First().ProductId);

            if (foundStoreProduct is null) throw new ArgumentException("ProductNotFound");

            VendorWarehouseMapping foundStoreVendorWarehouse = _vendorWarehouseMappingRepository.Table.FirstOrDefault(vendor => vendor.VendorId == foundStoreProduct.VendorId);

            if (foundStoreVendorWarehouse is null) throw new ArgumentException("VendorNotAssociatedWithAnyWarehouse");

            Warehouse foundStoreWarehouse = _warehouseRepository.Table.FirstOrDefault(warehouse => warehouse.Id == foundStoreVendorWarehouse.WarehouseId);

            if (foundStoreWarehouse is null) throw new ArgumentException("WarehouseNotFound");

            AddressGeoCoordinatesMapping foundStoreAddress = _addressGeoCoordinateRepository.Table
                .FirstOrDefault(address => address.AddressId == foundStoreWarehouse.AddressId);

            if (foundStoreAddress is null) throw new ArgumentException("VendorAddressGeoCoordinatesNotFound");

            Order foundDeliveryOrder = _orderRepository.GetById(orderId);

            if (foundDeliveryOrder is null) throw new ArgumentException("OrderNotFound");

            AddressGeoCoordinatesMapping foundDeliveryAddress = _addressGeoCoordinateRepository.Table
                .FirstOrDefault(delivery => delivery.AddressId == foundDeliveryOrder.ShippingAddressId);

            if (foundDeliveryOrder is null) throw new ArgumentException("ClientAddressGeoCoordinatesNotFound");

            var coordinateRequest = new CoordinateRequest
            {
                StoreLatitude = foundStoreAddress.Latitude,
                StoreLongitude = foundStoreAddress.Longitude,
                DeliveryOrderLatitude = foundDeliveryAddress.Latitude,
                DeliveryOrderLongitude = foundDeliveryAddress.Longitude
            };

            return coordinateRequest;
        }

        ///<inheritdoc/>
        public Customer GetDriverInfoByOrderId(int orderId)
        {
            if (orderId == 0) throw new ArgumentException("OrderIdCantBeZero");

            var order = _orderDeliveryStatusMappingRepository.Table.FirstOrDefault(o => o.OrderId == orderId);
            if (order is null) throw new ArgumentException("OrderNotFound");

            if (order.DeliveryStatusId == (int)DeliveryStatus.DeclinedByStore ||
                order.DeliveryStatusId == (int)DeliveryStatus.AwaitingForMessenger
                || order.DeliveryStatusId == (int)DeliveryStatus.OrderPreparationCompleted)
                throw new ArgumentException("DoesNotHaveDriverAssignedToTheOrder");

            var customer = _customerService.GetCustomerById(order.CustomerId.Value);
            return customer;
        }

        ///<inheritdoc/>
        public DriverLocationInfoMapping GetDriverlocationInfo(int orderId)
        {
            var driverLocationInfoMapping = new DriverLocationInfoMapping();

            if (orderId == 0)
                throw new ArgumentException("OrderIdCantBeZero");

            var orderDelivery = _orderDeliveryStatusMappingRepository.Table
                .FirstOrDefault(order => order.OrderId == orderId);

            if (orderDelivery is null)
                throw new ArgumentException("OrderNotFound");

            if (orderDelivery.DeliveryStatusId == (int)DeliveryStatus.OrderPreparationCompleted)
            {
                driverLocationInfoMapping = _driverLocationInfoMappingRepository.Table
                    .Where(location => location.OrderId == orderId &&
                   location.DestinationType == (int)DestinationType.Commerce)
                    .OrderByDescending(location => location.CreatedOnUtc).FirstOrDefault();
            }
            else if (orderDelivery.DeliveryStatusId == (int)DeliveryStatus.DeliveryInProgress)
            {
                driverLocationInfoMapping = _driverLocationInfoMappingRepository.Table
                    .Where(location => location.OrderId == orderId &&
                   location.DestinationType == (int)DestinationType.Client)
                    .OrderByDescending(location => location.CreatedOnUtc).FirstOrDefault();
            }

            return driverLocationInfoMapping;
        }

        ///<inheritdoc/>
        public void DriverlocationCreateInfo(DriverLocationInfoRequest driverRequest)
        {
            var driverLocationInfoMapping = new DriverLocationInfoMapping();

            if (driverRequest is null)
                throw new ArgumentException("InvalidDriverLocationRequest");

            var orderDelivery = _orderDeliveryStatusMappingRepository.Table
                .FirstOrDefault(order => order.OrderId == driverRequest.OrderId);

            if (orderDelivery is null)
                throw new ArgumentException("OrderNotFound");

            if (orderDelivery.DeliveryStatusId == (int)DeliveryStatus.OrderPreparationCompleted ||
                orderDelivery.DeliveryStatusId == (int)DeliveryStatus.AssignedToMessenger)
            {
                driverLocationInfoMapping.DeliveryStatus = (int)DeliveryStatus.OrderPreparationCompleted;
                driverLocationInfoMapping.DestinationType = (int)DestinationType.Commerce;

                _notificationCenterService.SendDriverCoordinateTrackingUpdate(driverRequest);

            }
            else if (orderDelivery.DeliveryStatusId == (int)DeliveryStatus.DeliveryInProgress)
            {
                driverLocationInfoMapping.DeliveryStatus = (int)DeliveryStatus.DeliveryInProgress;
                driverLocationInfoMapping.DestinationType = (int)DestinationType.Client;
                _notificationCenterService.SendDriverCoordinateTrackingUpdate(driverRequest);
            }

            driverLocationInfoMapping.CreatedOnUtc = DateTime.UtcNow;
            driverLocationInfoMapping.OrderId = driverRequest.OrderId;
            driverLocationInfoMapping.Latitude = driverRequest.Latitude;
            driverLocationInfoMapping.Longitude = driverRequest.Longitude;
            _driverLocationInfoMappingRepository.Insert(driverLocationInfoMapping);
        }

        ///<inheritdoc/>
        public int GetVendorIdByOrderId(int orderId)
        {
            IList<int> orderItemsProductsIds = _orderItemRepository.Table
                .Where(orderItem => orderItem.OrderId == orderId)
                .Select(orderItem => orderItem.ProductId)
                .ToList();
            IList<int> productsVendorIds = _productRepository.Table
                .Where(product => orderItemsProductsIds.Contains(product.Id))
                .Select(product => product.VendorId)
                .ToList();

            if (productsVendorIds.Count==0)
            {
                return 0;
            }
            if (productsVendorIds.Any(id => id != productsVendorIds[0]))
                throw new ArgumentException("VendorCouldNotBeDetermined");

            return productsVendorIds[0];
        }

        ///<inheritdoc/>
        public void SaveOrderDriver(int orderId, int driverId)
        {
            OrderDeliveryStatusMapping orderDelivery = _orderDeliveryStatusMappingRepository.Table.FirstOrDefault(mapping => mapping.OrderId == orderId);
            if (orderDelivery is null) throw new ArgumentException("Order delivery not found.");
            orderDelivery.CustomerId = driverId;
            _orderDeliveryStatusMappingRepository.Update(orderDelivery);
        }

        ///<inheritdoc/>
        public void DeleteOrder(int id)
        {
            if (id <= 0)
                throw new ArgumentException("InvalidOrderId");

            var orderToDelete = _orderApiService.GetOrderById(id);

            if (orderToDelete == null)
                throw new ArgumentException("OrderNotFound");

            _orderProcessingService.DeleteOrder(orderToDelete);

            _customerActivityService.InsertActivity("DeleteOrder",
                _localizationService.GetResource("ActivityLog.DeleteOrder"), orderToDelete);

        }

        ///<inheritdoc/>
        public void UpdateOrder(Delta<OrderDto> orderDelta)
        {
            if (orderDelta is null)
                throw new ArgumentException("InvalidOrderRequest");

            var currentOrder = _orderApiService.GetOrderById(orderDelta.Dto.Id);

            if (currentOrder == null)
                throw new ArgumentException("OrderNotFound");

            var customer = _customerService.GetCustomerById(currentOrder.CustomerId);

            var shippingRequired = _orderService.GetOrderItems(currentOrder.Id)
                .Any(item => !_productService
                .GetProductById(item.Id).IsFreeShipping);

            if (shippingRequired)
            {
                var isValid = true;

                if (!string.IsNullOrEmpty(orderDelta.Dto.ShippingRateComputationMethodSystemName) ||
                    !string.IsNullOrEmpty(orderDelta.Dto.ShippingMethod))
                {
                    var storeId = orderDelta.Dto.StoreId ?? _storeContext.GetCurrentStore().Id;

                    isValid &= SetShippingOption(orderDelta.Dto.ShippingRateComputationMethodSystemName ?? currentOrder.ShippingRateComputationMethodSystemName,
                                                 orderDelta.Dto.ShippingMethod,
                                                 storeId,
                                                 customer, BuildShoppingCartItemsFromOrderItems(_orderService.GetOrderItems(currentOrder.Id).ToList(), customer.Id, storeId));
                }

                if (isValid)
                {
                    currentOrder.ShippingMethod = orderDelta.Dto.ShippingMethod;
                }
                else
                {
                    throw new ArgumentException("ShippingIsNotValid");
                }
            }

            orderDelta.Merge(currentOrder);

            customer.BillingAddressId = currentOrder.BillingAddressId = orderDelta.Dto.BillingAddress.Id;
            customer.ShippingAddressId = currentOrder.ShippingAddressId = orderDelta.Dto.ShippingAddress.Id;


            ChangeOrderPaymentMethod(currentOrder.Id, orderDelta.Dto.PaymentMethodCheckoutAttribute);


            _orderService.UpdateOrder(currentOrder);

            _customerActivityService
                .InsertActivity("UpdateOrder", _localizationService.GetResource("ActivityLog.UpdateOrder"), currentOrder);


            var ordersRootObject = new OrdersRootObject();

            var placedOrderDto = _dtoHelper.PrepareOrderDTO(currentOrder);
            placedOrderDto.ShippingMethod = orderDelta.Dto.ShippingMethod;

            ordersRootObject.Orders.Add(placedOrderDto);
        }

        ///<inheritdoc/>
        private bool SetShippingOption(string shippingRateComputationMethodSystemName,
            string shippingOptionName,
            int storeId,
            Customer customer,
            List<ShoppingCartItem> shoppingCartItems)
        {
            var isValid = true;

            if (string.IsNullOrEmpty(shippingRateComputationMethodSystemName))
            {
                throw new ArgumentException("PleaseProvideShippingRateComputationMethodSystemName");
            }
            else if (string.IsNullOrEmpty(shippingOptionName))
            {
                throw new ArgumentException("PleaseProvideShippingOptionName");
            }
            else
            {
                var shippingOptionResponse = _shippingService.GetShippingOptions(shoppingCartItems, _customerService.GetCustomerShippingAddress(customer), customer,
                                                                                 shippingRateComputationMethodSystemName, storeId);

                if (shippingOptionResponse.Success)
                {
                    var shippingOptions = shippingOptionResponse.ShippingOptions.ToList();

                    var shippingOption = shippingOptions
                        .Find(so => !string.IsNullOrEmpty(so.Name) && so.Name.Equals(shippingOptionName, StringComparison.InvariantCultureIgnoreCase));

                    _genericAttributeService.SaveAttribute(customer,
                                                           NopCustomerDefaults.SelectedShippingOptionAttribute,
                                                      shippingOption, storeId);
                }
                else
                {
                    isValid = false;

                    foreach (var errorMessage in shippingOptionResponse.Errors)
                    {
                        throw new ArgumentException(errorMessage);
                    }
                }
            }

            return isValid;
        }

        private List<ShoppingCartItem> BuildShoppingCartItemsFromOrderItems(List<OrderItem> orderItems, int customerId, int storeId)
        {
            var shoppingCartItems = new List<ShoppingCartItem>();

            foreach (var orderItem in orderItems)
            {
                shoppingCartItems.Add(new ShoppingCartItem
                {
                    ProductId = orderItem.ProductId,
                    CustomerId = customerId,
                    Quantity = orderItem.Quantity,
                    RentalStartDateUtc = orderItem.RentalStartDateUtc,
                    RentalEndDateUtc = orderItem.RentalEndDateUtc,
                    StoreId = storeId,
                    ShoppingCartType = ShoppingCartType.ShoppingCart
                });
            }

            return shoppingCartItems;
        }

        ///<inheritdoc/>
        public void ChangeOrderPaymentMethod(int orderId, string paymentMethod)
        {
            Order foundOrder = _orderService.GetOrderById(orderId);

            if (foundOrder is null)
                throw new ArgumentException("OrderNotFound");

            IList<CheckoutAttribute> checkoutAttributes = _checkoutAttributeParser
               .ParseCheckoutAttributes(foundOrder.CheckoutAttributesXml);

            CheckoutAttribute checkoutAttribute = checkoutAttributes
                .FirstOrDefault(attribute => attribute.Name.Equals("MetodoPago"));

            if (checkoutAttribute is null)
                throw new ArgumentException("CheckoutAttributeNotFound");

            if (string.IsNullOrWhiteSpace(paymentMethod))
            {
                throw new ArgumentException("PaymentMethodCanBeNullOrEmpty");
            }

            if (!Defaults.PaymentMethodCheckoutAttribute.Options.Contains(paymentMethod))
            {
                throw new ArgumentException("InvalidPaymentMethod");
            }


            string checkoutAttributeValue = _checkoutAttributeValueRepository.Table
            .FirstOrDefault(value => value.Name.Equals(paymentMethod)).Id.ToString();

            string orderCheckoutAttributeXml = _checkoutAttributeParser
                .RemoveCheckoutAttribute(foundOrder.CheckoutAttributesXml, checkoutAttribute);

            const int paymentOptionPosition = 2;

            foundOrder.PaymentMethodSystemName = paymentMethod
                .Equals(Defaults.PaymentMethodCheckoutAttribute.Options[paymentOptionPosition])
                ? "Payments.CyberSource" : "Payments.CheckMoneyOrder";

            string newCheckOutAttribute = _checkoutAttributeParser
            .AddCheckoutAttribute(orderCheckoutAttributeXml, checkoutAttribute, checkoutAttributeValue);

            foundOrder.CheckoutAttributesXml = newCheckOutAttribute;

            Customer foundCustomer = _customerService.GetCustomerById(foundOrder.CustomerId);

            if (foundCustomer is null)
                throw new ArgumentException("CustomerNotFound");

            foundOrder.CheckoutAttributeDescription = _checkoutAttributeFormatter
                .FormatAttributes(newCheckOutAttribute, foundCustomer);
            _orderService.UpdateOrder(foundOrder);
        }

        ///<inheritdoc/>
        public CouponModel GetDiscountCouponToApplyOrder(string couponCode, int customerId)
        {
            var coupon = new CouponModel();

            if (string.IsNullOrWhiteSpace(couponCode))
                throw new ArgumentException("InvalidCouponCode");

            Discount foundCoupon = _discountRepository.Table
                .FirstOrDefault(discount => discount.CouponCode.Equals(couponCode));

            if (foundCoupon is null)
                throw new ArgumentException("CouponNotFound");

            if (DateTime.UtcNow > foundCoupon.EndDateUtc)
                throw new ArgumentException("CouponIsExpired");

            bool couponIsAssignedToOtherCustomer = _customerDiscountMappingRepository.Table.Any(x => x.DiscountId == foundCoupon.Id && x.CustomerId != customerId);

            if (couponIsAssignedToOtherCustomer)
            {
                throw new ArgumentException("CouponIsAssignedToOtherCustomer");
            }

            coupon.DiscountAmount = foundCoupon.UsePercentage ? foundCoupon.DiscountPercentage : foundCoupon.DiscountAmount;
            coupon.UsePercentage = foundCoupon.UsePercentage;

            return coupon;
        }

        #endregion
    }
}

