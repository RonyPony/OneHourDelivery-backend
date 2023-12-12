using Newtonsoft.Json;
using Nop.Core;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Directory;
using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Payments;
using Nop.Core.Domain.Shipping;
using Nop.Core.Events;
using Nop.Data;
using Nop.Plugin.Payments.CyberSource.Models;
using Nop.Plugin.Pickup.PickupInStore.Domain;
using Nop.Plugin.Synchronizers.WAPIOrders.Domains;
using Nop.Plugin.Synchronizers.WAPIOrders.Helpers;
using Nop.Plugin.Synchronizers.WAPIOrders.Models;
using Nop.Plugin.Synchronizers.WAPIOrders.Models.RegisterSale;
using Nop.Plugin.Synchronizers.WAPIOrders.Models.RegisterSale.Request;
using Nop.Services.Catalog;
using Nop.Services.Common;
using Nop.Services.Customers;
using Nop.Services.Directory;
using Nop.Services.Events;
using Nop.Services.Logging;
using Nop.Services.Orders;
using Nop.Services.Shipping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Synchronizers.WAPIOrders.Consumers
{
    /// <summary>
    /// Represents a consumer for <see cref="EntityUpdatedEvent{T}"/> where T is an instance of <see cref="Order"/>.
    /// </summary>
    public partial class UpdateOrderEventConsumer : IConsumer<EntityUpdatedEvent<Order>>
    {
        #region Fields

        private readonly IRepository<OrderTransactionCodeMapping> _orderTransactionCodeMappingRepository;
        private readonly ConfigurationSettings _configurationSettings;
        private readonly ICustomerService _customerService;
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;
        private readonly ILogger _logger;
        private readonly IEventPublisher _eventPublisher;
        private readonly IRepository<CyberSourceTransactionLog> _cyberSourceTransactionLogRepository;
        private readonly IAddressService _addressService;
        private readonly IRepository<StorePickupPoint> _storePickupPointRepository;
        private readonly IRepository<Address> _addressRepository;
        private readonly IShipmentService _shipmentService;
        private readonly ICountryService _countryService;
        private readonly IStateProvinceService _stateProvinceService;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<ProductAttributeCombination> _productAttributeCombinationRepository;

        #endregion

        #region Ctor

        /// <summary>
        /// Initializes a new instance of <see cref="UpdateOrderEventConsumer"/>.
        /// </summary>
        /// <param name="orderTransactionCodeMappingRepository">An implementation of <see cref="IRepository{T}"/> where T is <see cref="OrderTransactionCodeMapping"/>.</param>
        /// <param name="configurationSettings">An instance of <see cref="ConfigurationSettings"/>.</param>
        /// <param name="customerService">An implementation of <see cref="ICustomerService"/>.</param>
        /// <param name="orderService">An implementation of <see cref="IOrderService"/>.</param>
        /// <param name="productService">An implementation of <see cref="IProductService"/>.</param>
        /// <param name="logger">An implementation of <see cref="ILogger"/>.</param>
        /// <param name="eventPublisher">An implementation of <see cref="IEventPublisher"/>.</param>
        /// <param name="cyberSourceTransactionLogRepository">An implementation of <see cref="IRepository{T}"/> where T is <see cref="CyberSourceTransactionLog"/>.</param>
        /// <param name="addressService">An implementation of <see cref="IAddressService"/>.</param>
        /// <param name="storePickupPointRepository">An implementation of <see cref="IRepository{T}"/> where T is <see cref="StorePickupPoint"/>.</param>
        /// <param name="addressRepository">An implementation of <see cref="IRepository{T}"/> where T is <see cref="Address"/>.</param>
        /// <param name="shipmentService">An implementation of <see cref="IShipmentService"/>.</param>
        /// <param name="countryService">An implementation of <see cref="ICountryService"/>.</param>
        /// <param name="stateProvinceService">An implementation of <see cref="IStateProvinceService"/>.</param>
        /// <param name="orderRepository">An implementation of <see cref="IRepository{T}"/> where T is <see cref="Order"/>.</param>
        /// <param name="productAttributeCombinationRepository">An implementation of <see cref="IRepository{T}"/> where T is <see cref="ProductAttributeCombination"/>.</param>
        public UpdateOrderEventConsumer(
            IRepository<OrderTransactionCodeMapping> orderTransactionCodeMappingRepository,
            ConfigurationSettings configurationSettings,
            ICustomerService customerService,
            IOrderService orderService,
            IProductService productService,
            ILogger logger,
            IEventPublisher eventPublisher,
            IRepository<CyberSourceTransactionLog> cyberSourceTransactionLogRepository,
            IAddressService addressService,
            IRepository<StorePickupPoint> storePickupPointRepository,
            IRepository<Address> addressRepository,
            IShipmentService shipmentService,
            ICountryService countryService,
            IStateProvinceService stateProvinceService,
            IRepository<Order> orderRepository,
            IRepository<ProductAttributeCombination> productAttributeCombinationRepository)
        {
            _orderTransactionCodeMappingRepository = orderTransactionCodeMappingRepository;
            _configurationSettings = configurationSettings;
            _customerService = customerService;
            _orderService = orderService;
            _productService = productService;
            _logger = logger;
            _eventPublisher = eventPublisher;
            _cyberSourceTransactionLogRepository = cyberSourceTransactionLogRepository;
            _addressService = addressService;
            _storePickupPointRepository = storePickupPointRepository;
            _addressRepository = addressRepository;
            _shipmentService = shipmentService;
            _countryService = countryService;
            _stateProvinceService = stateProvinceService;
            _orderRepository = orderRepository;
            _productAttributeCombinationRepository = productAttributeCombinationRepository;
        }

        #endregion

        #region Utilities

        private RegisterSaleRequest GetRegisterSaleRequest(Order order)
        {
            return new RegisterSaleRequest
            {
                Version = $"NopCommerce V{NopVersion.CurrentVersion}",
                ECommerce = "Polux eCommerce",
                OrderNumber = GenerateOrderNumberByOrderId(order.Id),
                ORequest = GenerateORequest(order)
            };
        }

        private string GenerateOrderNumberByOrderId(int orderId)
        {
            int orderNumberLength = 8;
            string orderNumber = orderId.ToString();

            while (orderNumber.Length < orderNumberLength)
            {
                orderNumber = orderNumber.Insert(0, "0");
            }

            return orderNumber;
        }

        private ORequest GenerateORequest(Order order)
        {
            Customer customer = _customerService.GetCustomerById(order.CustomerId);
            IList<Item> orderRequestItems = GetORequestItems(_orderService.GetOrderItems(order.Id), order);

            var totalItemDiscounts = orderRequestItems.Select(x => x.ItemDiscount).Sum();
            order.OrderDiscount = order.OrderSubTotalDiscountExclTax + totalItemDiscounts;

            return new ORequest
            {
                Cip = customer.Id.ToString(),
                TotalPayment = order.OrderTotal,
                TotalDiscount = order.OrderDiscount,
                TotalTax = order.OrderTax,
                QtyPayment = 1.00m, // FIXED UNTIL STATED OTHERWISE
                QtyItemLine = orderRequestItems.Count,
                MerchandisePickup = GetMerchandisePickup(order),
                Items = orderRequestItems,
                Payments = GetORequestPayments(order.Id)
            };
        }

        private IList<Item> GetORequestItems(IList<OrderItem> orderItems, Order order)
        {
            int itemDescriptionMaxLength = 40;
            var oRequestItems = new List<Item>();

            foreach (OrderItem orderItem in orderItems)
            {
                Product product = _productService.GetProductById(orderItem.ProductId);

                if (product.ShortDescription is null) product.ShortDescription = "";

                if (product != null)
                {
                    decimal taxAmount = orderItem.UnitPriceInclTax - orderItem.UnitPriceExclTax;

                    oRequestItems.Add(new Item
                    {
                        ItemLine = orderItems.IndexOf(orderItem) + 1,
                        ItemCode = GetOrderItemSkuFromProductAttributeCombination(orderItem, product.Sku),
                        ItemDescription = product.ShortDescription.Length > itemDescriptionMaxLength ?
                            product.ShortDescription.Substring(0, itemDescriptionMaxLength) : product.ShortDescription,
                        ExtOriginalPrice = decimal.Round(orderItem.UnitPriceExclTax, 2, MidpointRounding.AwayFromZero),
                        ExtSellPrice = decimal.Round(orderItem.UnitPriceExclTax, 2, MidpointRounding.AwayFromZero),
                        Qty = orderItem.Quantity,
                        TaxCod = taxAmount == decimal.Zero ? 0 : 1, // 0 IF NONE, 1 FOR 7% TAX.... THIS NEEDS TO BE CHANGED WHEN TAX CATEGORY INTEGRATION IS DONE.
                        TaxAmt = decimal.Round(taxAmount, 2, MidpointRounding.AwayFromZero),
                        ItemDiscount = decimal.Round(orderItem.DiscountAmountExclTax, 2, MidpointRounding.AwayFromZero)
                    });
                }
            }
            Item deliveryItem = GetDeliveryItem(order, oRequestItems.Last().ItemLine);
            if (deliveryItem != null) oRequestItems.Add(deliveryItem);

            return oRequestItems;
        }

        private string GetOrderItemSkuFromProductAttributeCombination(OrderItem orderItem, string productSku)
        {
            ProductAttributeCombination productAttributeCombination = _productAttributeCombinationRepository.Table
                .FirstOrDefault(combination => combination.ProductId == orderItem.ProductId
                       && combination.AttributesXml == orderItem.AttributesXml);

            return !string.IsNullOrWhiteSpace(orderItem.AttributesXml) ? productAttributeCombination?.Sku ?? productSku : productSku;
        }

        private Item GetDeliveryItem(Order order, int lastItemLineNumber)
        {
            if (!order.PickupInStore)
            {
                decimal shippingTaxAmount = order.OrderShippingInclTax - order.OrderShippingExclTax;
                return new Item
                {
                    ItemLine = lastItemLineNumber + 1,
                    ItemCode = Defaults.DeliveryItemCodeByShippingMethodName[order.ShippingMethod],
                    ItemDescription = order.ShippingMethod,
                    ExtOriginalPrice = order.OrderShippingExclTax,
                    ExtSellPrice = order.OrderShippingExclTax,
                    Qty = 1,
                    TaxCod = shippingTaxAmount > 0M ? 1 : 0, // 0 IF NONE, 1 FOR 7% TAX.... THIS NEEDS TO BE CHANGED WHEN TAX CATEGORY INTEGRATION IS DONE.
                    TaxAmt = shippingTaxAmount,
                    ItemDiscount = 0.00M
                };
            }

            return null;
        }

        private MerchandisePickup GetMerchandisePickup(Order order)
        {
            return new MerchandisePickup
            {
                TypePickup = Defaults.PickupTypesDictionary[order.PickupInStore || !order.ShippingAddressId.HasValue],
                StorePickup = GetStorePickupCode(order),
                Delivery = order.PickupInStore || !order.ShippingAddressId.HasValue ? null : GetDelivery(order)
            };
        }

        private string GetStorePickupCode(Order order)
        {
            if (order.PickupInStore)
            {
                string pickupPointName = order.ShippingMethod.Replace("Pickup at ", "");
                Address orderPickupAddress = _addressService.GetAddressById(order.PickupAddressId.Value);

                IList<int> pickupPointPossibleAddressesIds = _addressRepository.Table
                    .Where(address => address.Id != orderPickupAddress.Id
                        && address.CountryId == orderPickupAddress.CountryId
                        && address.StateProvinceId == orderPickupAddress.StateProvinceId
                        && address.City == orderPickupAddress.City
                        && address.ZipPostalCode == orderPickupAddress.ZipPostalCode
                        && address.Address1 == orderPickupAddress.Address1)
                    .Select(address => address.Id)
                    .ToList();

                IList<StorePickupPoint> storePickupPoints = _storePickupPointRepository.Table
                    .Where(pickupPoint => pickupPoint.Name == pickupPointName
                        && pickupPointPossibleAddressesIds.Contains(pickupPoint.AddressId))
                    .ToList();

                if (storePickupPoints.Count != 1)
                    return _configurationSettings.DefaultStorePickupCode;

                return storePickupPoints.First().Description;
            }
            else
            {
                return _configurationSettings.DefaultStorePickupCode;
            }
        }

        private Delivery GetDelivery(Order order)
        {
            Address shippingAddress = _addressService.GetAddressById(order.ShippingAddressId.Value);
            Shipment shipment = _shipmentService.GetShipmentsByOrderId(order.Id).FirstOrDefault();

            return new Delivery
            {
                TypeDelivery = Defaults.DeliveryTypesDictionary[order.PickupInStore],
                TrackingID = shipment?.TrackingNumber,
                ThirdPartyCode = order.PickupInStore ? string.Empty : order.ShippingMethod,
                WhoReceives = $"{shippingAddress.FirstName} {shippingAddress.LastName}",
                Telephone = shippingAddress.PhoneNumber,
                Direction = GetDirectionFromAddress(shippingAddress),
                SpecialInstruction = string.Empty // FIXED UNTIL STATED OTHERWISE
            };
        }

        private string GetDirectionFromAddress(Address address)
        {
            string direction = "";
            if (!string.IsNullOrEmpty(address.Address1))
                direction += $"{address.Address1}, ";
            if (!string.IsNullOrEmpty(address.Address2))
                direction += $"{address.Address2}, ";
            if (!string.IsNullOrEmpty(address.City))
                direction += $"{address.City} ";
            if (!string.IsNullOrEmpty(address.ZipPostalCode))
                direction += $"{address.ZipPostalCode}, ";
            if (address.StateProvinceId.HasValue && _stateProvinceService.GetStateProvinceById(address.StateProvinceId.Value) is StateProvince stateProvince)
                direction += $"{stateProvince.Name}, ";
            if (address.CountryId.HasValue && _countryService.GetCountryById(address.CountryId.Value) is Country country)
                direction += $"{country.Name}";

            return direction;
        }

        private IList<Payment> GetORequestPayments(int orderId)
        {
            CyberSourceTransactionLog cyberSourceTransactionLog = _cyberSourceTransactionLogRepository.Table
                .Where(log => log.OrderId == orderId).FirstOrDefault();

            if (cyberSourceTransactionLog is null)
                throw new ArgumentException("Order payment transaction was not found.");

            return new List<Payment>
            {
                new Payment
                {
                    PLine = 1,
                    PCode = "1",
                    PDescription = cyberSourceTransactionLog.CardType,
                    PAmt = cyberSourceTransactionLog.Amount.Value,
                    AuthCod = cyberSourceTransactionLog.TransactionId
                }
            };
        }

        #endregion

        #region Methods

        /// <summary>
        /// Event consumer that will be fired every time an order is updated.
        /// </summary>
        /// <param name="eventMessage">Represents an implementation of <see cref="EntityUpdatedEvent{T}"/>, where T is an implementation of <see cref="Order"/>.</param>
        public void HandleEvent(EntityUpdatedEvent<Order> eventMessage)
        {
            Task<string> response = RegisterSale(eventMessage.Entity);
            const int threeMinutesInMilliseconds = 180000;
            response.Wait(threeMinutesInMilliseconds);

            if (!string.IsNullOrWhiteSpace(response.Result))
            {
                var orderTransactionCode = new OrderTransactionCodeMapping
                {
                    OrderId = eventMessage.Entity.Id,
                    TransactionCode = response.Result
                };

                _orderTransactionCodeMappingRepository.Insert(orderTransactionCode);
                _eventPublisher.EntityInserted(orderTransactionCode);

                RegisterOrderNote($"The order has been registered via WAPI. WAPI transaction code: {orderTransactionCode.TransactionCode}",
                    eventMessage.Entity.Id);
            }

        }

        private async Task<string> RegisterSale(Order order)
        {
            OrderTransactionCodeMapping orderTransactionCode = _orderTransactionCodeMappingRepository.Table
                .FirstOrDefault(mapping => mapping.OrderId == order.Id);

            // Must not send order to WAPI if order is not Paid, or the order has WAPI transaction code,
            // or order status is completed or cancelled.
            if (orderTransactionCode != null
                || order.PaymentStatusId != (int)PaymentStatus.Paid
                || order.OrderStatusId == (int)OrderStatus.Cancelled)
                return string.Empty;

            try
            {
                using var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Defaults.AuthorizationScheme);
                client.DefaultRequestHeaders.Add(_configurationSettings.AuthKeyName, _configurationSettings.AuthKeyValue);

                RegisterSaleRequest request = GetRegisterSaleRequest(order);

                // When creating an order, order items aren't initially added to the order.
                // Thus, orders must not be sent to SAP until order items are added to the order.
                if (request.ORequest == null || request.ORequest.Items.Count == 0)
                    return string.Empty;

                string json = JsonConvert.SerializeObject(request);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                _logger.Information($"sending Json to WAPI: {json}");
                var result = await client.PostAsync(_configurationSettings.ApiPostUrl, content);

                if (!result.IsSuccessStatusCode)
                    throw new ArgumentException($"Status code: {result.StatusCode}.");

                string jsonResponse = await result.Content.ReadAsStringAsync();
                var response = JsonConvert.DeserializeObject<RegisterSaleResponse>(jsonResponse);

                if (response.CodRes != 0 || response.OResponse is null)
                    throw new ArgumentException($"WAPI response code: {response.CodRes}. WAPI response message: {response.MsgRes}");

                return response.OResponse.TransactionCode;
            }
            catch (Exception ex)
            {
                order.OrderStatusId = (int)OrderStatus.Pending;
                _orderRepository.Update(order);

                RegisterOrderNote($"Error sending order to WAPI. {ex.Message}", order.Id);

                _logger.Error($"Error sending order to WAPI. {ex.Message}.  Full exception: {ex.StackTrace}, {ex.InnerException?.Message}, {ex.InnerException?.StackTrace}", ex);

                throw new NopException(ex.Message);
            }
        }

        private void RegisterOrderNote(string note, int orderId)
        {
            var orderNote = new OrderNote
            {
                OrderId = orderId,
                Note = note,
                DisplayToCustomer = false,
                CreatedOnUtc = DateTime.UtcNow
            };

            _orderService.InsertOrderNote(orderNote);
            _eventPublisher.EntityInserted(orderNote);
        }

        #endregion
    }
}
