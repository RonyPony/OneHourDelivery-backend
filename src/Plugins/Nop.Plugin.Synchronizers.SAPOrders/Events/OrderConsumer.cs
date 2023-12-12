using Newtonsoft.Json;
using Nop.Core;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Directory;
using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Payments;
using Nop.Core.Events;
using Nop.Data;
using Nop.Plugin.Synchronizers.SAPCustomers.Domains;
using Nop.Plugin.Synchronizers.SAPOrders.Domains;
using Nop.Plugin.Synchronizers.SAPOrders.Helpers;
using Nop.Plugin.Synchronizers.SAPOrders.Models;
using Nop.Plugin.Synchronizers.SAPProducts.Domains;
using Nop.Services.Common;
using Nop.Services.Directory;
using Nop.Services.Events;
using Nop.Services.Logging;
using Nop.Services.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Synchronizers.SAPOrders.Events
{
    /// <summary>
    /// Represents a consumer for <see cref="EntityUpdatedEvent{T}"/> where T is <see cref="Order"/>
    /// </summary>
    public class OrderConsumer : IConsumer<EntityUpdatedEvent<Order>>
    {
        #region Fields

        private readonly ILogger _logger;
        private readonly IOrderService _orderService;
        private readonly ICountryService _countryService;
        private readonly IStateProvinceService _stateProvinceService;
        private readonly IAddressService _addressService;
        private readonly SapOrdersSyncSettings _sapOrdersSyncSettings;
        private readonly IRepository<ProductSapItemMapping> _productSapItemRepository;
        private readonly IRepository<CustomerSapCustomerMapping> _customerSapCustomerRepository;
        private readonly IRepository<OrderSapOrderMapping> _orderSapOrderRepository;

        #endregion

        #region Ctor

        /// <summary>
        /// The constructor method for this class.
        /// </summary>
        /// <param name="logger">Implementation of <see cref="ILogger"/></param>
        /// <param name="orderService">Implementation of <see cref="IOrderService"/></param>
        /// <param name="countryService">Implementation of <see cref="ICountryService"/></param>
        /// <param name="stateProvinceService">Implementation of <see cref="IStateProvinceService"/></param>
        /// <param name="addressService">Implementation of <see cref="IAddressService"/></param>
        /// <param name="sapOrdersSyncSettings">Implementation of <see cref="SapOrdersSyncSettings"/></param>
        /// <param name="productSapItemRepository">Implementation of <see cref="IRepository{TEntity}"/> where TEntity is <see cref="ProductSapItemMapping"/></param>
        /// <param name="customerSapCustomerRepository">Implementation of <see cref="IRepository{TEntity}"/> where TEntity is <see cref="CustomerSapCustomerMapping"/></param>
        /// <param name="orderSapOrderRepository">Implementation of <see cref="IRepository{TEntity}"/> where TEntity is <see cref="OrderSapOrderMapping"/></param>
        public OrderConsumer(ILogger logger, IOrderService orderService,
            ICountryService countryService, IStateProvinceService stateProvinceService, IAddressService addressService,
            SapOrdersSyncSettings sapOrdersSyncSettings, IRepository<ProductSapItemMapping> productSapItemRepository,
            IRepository<CustomerSapCustomerMapping> customerSapCustomerRepository, IRepository<OrderSapOrderMapping> orderSapOrderRepository)
        {
            _logger = logger;
            _orderService = orderService;
            _countryService = countryService;
            _stateProvinceService = stateProvinceService;
            _addressService = addressService;
            _sapOrdersSyncSettings = sapOrdersSyncSettings;
            _productSapItemRepository = productSapItemRepository;
            _customerSapCustomerRepository = customerSapCustomerRepository;
            _orderSapOrderRepository = orderSapOrderRepository;
        }

        #endregion

        #region Utilities

        private async Task<string> SendOrder(Order order)
        {
            OrderSapOrderMapping orderSapOrderMapping =
                _orderSapOrderRepository.Table.FirstOrDefault(sapOrderMapping => sapOrderMapping.OrderId == order.Id);

            // Must not send order to SAP if order is not Paid, or the order has SAP order code,
            // or order status is completed or cancelled.
            if (order.PaymentStatusId != (int)PaymentStatus.Paid || orderSapOrderMapping != null ||
                order.OrderStatusId == (int)OrderStatus.Cancelled || order.OrderStatusId == (int)OrderStatus.Complete)
            {
                return string.Empty;
            }

            try
            {
                using HttpClient client = new HttpClient
                {
                    DefaultRequestHeaders =
                    {
                        Authorization = new AuthenticationHeaderValue(_sapOrdersSyncSettings.AuthenticationScheme, _sapOrdersSyncSettings.ApiToken)
                    }
                };

                SapOrderRequestModel model = GetSapOrderRequestModelFromOrder(order);

                // When creating an order, order items aren't initially added to the order.
                // Thus, orders must not be sent to SAP until order items are added to the order.
                if (model.Rows.Count == 0)
                {
                    return string.Empty;
                }

                string json = JsonConvert.SerializeObject(model);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var result = await client.PostAsync(_sapOrdersSyncSettings.ApiPostUrl, content);

                if (!result.IsSuccessStatusCode)
                {
                    throw new Exception($"Status code: {result.StatusCode}.");
                }

                string jsonResponse = await result.Content.ReadAsStringAsync();
                var sapOrderResponse = JsonConvert.DeserializeObject<SapOrderResponseModel>(jsonResponse);

                return sapOrderResponse.Extra;
            }
            catch (Exception e)
            {
                _logger.Error($"Error sending order to SAP API. {e.Message},  Full exception: {e.StackTrace}, {e.InnerException?.Message}, {e.InnerException?.StackTrace}", e);
                throw new NopException($"Error sending order to SAP API. {e.Message}");
            }
        }

        private SapOrderRequestModel GetSapOrderRequestModelFromOrder(Order order)
        {
            if (order.ShippingAddressId == null)
            {
                return new SapOrderRequestModel
                {
                    CardCode = GetCardCodeByCustomerId(order.CustomerId),
                    NumAtCard = order.Id.ToString(),
                    PostingDate = order.CreatedOnUtc.ToSapDateTime(),
                    DeliveryDate = order.CreatedOnUtc.ToSapDateTime(),
                    DocumentDate = order.CreatedOnUtc.ToSapDateTime(),
                    PaymentTerms = 0,
                    ShipType = 1,
                    Remarks = string.Empty,
                    StreetS = string.Empty,
                    BlockS = string.Empty,
                    CityS = string.Empty,
                    ZipCodeS = string.Empty,
                    CountyS = string.Empty,
                    StateS = string.Empty,
                    CountryS = string.Empty,
                    Freight = decimal.Zero,
                    Rows = GetOrderRows(order.Id)
                };
            }

            Address shippingAddress = _addressService.GetAddressById(order.ShippingAddressId.Value);

            return new SapOrderRequestModel
            {
                CardCode = GetCardCodeByCustomerId(order.CustomerId),
                NumAtCard = order.Id.ToString(),
                PostingDate = order.CreatedOnUtc.ToSapDateTime(),
                DeliveryDate = order.CreatedOnUtc.ToSapDateTime(),
                DocumentDate = order.CreatedOnUtc.ToSapDateTime(),
                PaymentTerms = 0,
                ShipType = 1,
                Remarks = string.Empty,
                StreetS = shippingAddress.Address1,
                BlockS = shippingAddress.Address2,
                CityS = shippingAddress.City,
                ZipCodeS = shippingAddress.ZipPostalCode,
                CountyS = shippingAddress.County,
                StateS = GetShippingAddressStateName(shippingAddress.StateProvinceId.GetValueOrDefault()),
                CountryS = GetShippingAddressCountryName(shippingAddress.CountryId.GetValueOrDefault()),
                Freight = decimal.Zero,
                Rows = GetOrderRows(order.Id)
            };
        }

        private string GetCardCodeByCustomerId(int customerId)
        {
            CustomerSapCustomerMapping sapCustomerMapping = _customerSapCustomerRepository.Table
                .FirstOrDefault(sapCustMapping => sapCustMapping.CustomerId == customerId);

            return sapCustomerMapping == null
                ? SapOrdersSyncDefaults.NopCommerceCustomerCardCode
                : sapCustomerMapping.SapCustomerId;
        }

        private string GetShippingAddressStateName(int stateId)
        {
            StateProvince stateProvince = _stateProvinceService.GetStateProvinceById(stateId);
            return stateProvince.Abbreviation;
        }

        private string GetShippingAddressCountryName(int countryId)
        {
            Country country = _countryService.GetCountryById(countryId);
            return country.TwoLetterIsoCode;
        }

        private List<SapOrderProductModel> GetOrderRows(int orderId)
        {
            var sapOrderProducts = new List<SapOrderProductModel>();
            IList<OrderItem> orderItems = _orderService.GetOrderItems(orderId);

            foreach (var orderItem in orderItems)
            {
                sapOrderProducts.Add(new SapOrderProductModel
                {
                    ItemCode = GetItemProductCodeByProductId(orderItem.ProductId),
                    Quantity = orderItem.Quantity,
                    UnitPrice = orderItem.PriceExclTax,
                    TaxCode = ""
                });
            }

            return sapOrderProducts;
        }

        private string GetItemProductCodeByProductId(int productId)
        {
            ProductSapItemMapping sapProductMapping = _productSapItemRepository.Table
                .FirstOrDefault(productMapping => productMapping.ProductId == productId);

            return sapProductMapping == null ? string.Empty : sapProductMapping.SapItemCode;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Event consumer that will be fired every time an order is updated.
        /// </summary>
        /// <param name="eventMessage">Represents an implementation of <see cref="EntityUpdatedEvent{T}"/>, where T is an implementation of <see cref="Order"/></param>
        public void HandleEvent(EntityUpdatedEvent<Order> eventMessage)
        {
            Task<string> response = SendOrder(eventMessage.Entity);
            response.Wait();

            if (!string.IsNullOrEmpty(response.Result))
            {
                var orderSapOrderMapping = new OrderSapOrderMapping
                {
                    OrderId = eventMessage.Entity.Id,
                    SapOrderId = response.Result
                };

                _orderSapOrderRepository.Insert(orderSapOrderMapping);
            }
        }

        #endregion
    }
}
