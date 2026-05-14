using Newtonsoft.Json;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Media;
using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Shipping;
using Nop.Core.Domain.Vendors;
using Nop.Data;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domain;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains.Enums;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models;
using Nop.Services.Catalog;
using Nop.Services.Common;
using Nop.Services.Customers;
using Nop.Services.Logging;
using Nop.Services.Media;
using Nop.Services.Orders;
using Nop.Services.Shipping;
using Nop.Services.Vendors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Services
{
    ///<inheritdoc/>
    public class NotificationCenterService : INotificationCenterService
    {
        #region Fields

        private readonly IAddressService _addressService;
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;
        private readonly IVendorService _vendorService;
        private readonly IRepository<Product> _productRespository;
        private readonly IRepository<OrderDeliveryStatusMapping> _orderDeliveryStatusMappingRepository;
        private readonly IRepository<Customer> _customerRepository;
        private readonly ICustomerService _customerService;
        private readonly IGenericAttributeService _genericAttributeService;
        private readonly IPictureService _pictureService;
        private readonly MediaSettings _mediaSettings;
        private readonly IAddressGeoCoordinatesService _addressGeoCoordinatesService;
        private readonly IGoogleDirectionService _googleDirectionService;
        private readonly DeliveryAppBackendConfigurationSettings _deliveryAppBackendConfigurationSettings;
        private readonly ILogger _logger;
        #endregion

        #region Ctor
        /// <summary>
        /// Initializes a new instance of <see cref="NotificationCenterService"/>.
        /// </summary>
        /// <param name="addressService">An implementation of <see cref="IAddressService"/>.</param>
        /// <param name="orderService">An implementation of <see cref="IOrderService"/>.</param>
        /// <param name="productService">An implementation of <see cref="IProductService"/>.</param>
        /// <param name="vendorService">An implementation of <see cref="IVendorService"/>.</param>
        /// <param name="productRepository">An implementation of <see cref="IRepository{Product}"/>.</param>
        /// <param name="orderDeliveryStatusMappingRepository">An implementation of <see cref="IRepository{OrderDeliveryStatusMapping}"/>.</param>
        /// <param name="googleDirectionService">An implementation of <see cref="IGoogleDirectionService"/>.</param>
        /// <param name="mediaSettings">An implementation of <see cref="MediaSettings"/>.</param>
        /// <param name="pictureService">An implementation of <see cref="IPictureService"/>.</param>
        /// <param name="customerService">An implementation of <see cref="ICustomerService"/>.</param>
        /// <param name="genericAttributeService">An implementation of <see cref="IGenericAttributeService"/>.</param>
        /// <param name="addressGeoCoordinatesService">An implementation of <see cref="IAddressGeoCoordinatesService"/>.</param>
        /// <param name="deliveryAppBackendConfigurationSettings">An instance of <see cref="DeliveryAppBackendConfigurationSettings"/>.</param>
        /// <param name="logger">An instance of <see cref="ILogger"/>.</param>
        /// <param name="customerRepository">An instance of <see cref="IRepository{Customer}"/>.</param>
        public NotificationCenterService(
            IAddressService addressService,
            IOrderService orderService,
            IProductService productService,
            IVendorService vendorService,
            IRepository<Product> productRepository,
            IRepository<OrderDeliveryStatusMapping> orderDeliveryStatusMappingRepository,
            ICustomerService customerService,
            IGenericAttributeService genericAttributeService,
            IPictureService pictureService,
            MediaSettings mediaSettings,
            IAddressGeoCoordinatesService addressGeoCoordinatesService,
            IGoogleDirectionService googleDirectionService,
            DeliveryAppBackendConfigurationSettings deliveryAppBackendConfigurationSettings,
            ILogger logger,
            IRepository<Customer> customerRepository)
        {
            _addressService = addressService;
            _orderService = orderService;
            _productService = productService;
            _vendorService = vendorService;
            _productRespository = productRepository;
            _orderDeliveryStatusMappingRepository = orderDeliveryStatusMappingRepository;
            _customerService = customerService;
            _genericAttributeService = genericAttributeService;
            _pictureService = pictureService;
            _mediaSettings = mediaSettings;
            _addressGeoCoordinatesService = addressGeoCoordinatesService;
            _googleDirectionService = googleDirectionService;
            _deliveryAppBackendConfigurationSettings = deliveryAppBackendConfigurationSettings;
            _logger = logger;
            _customerRepository = customerRepository;
        }
        #endregion

        ///<inheritdoc/>
        public void SendNotification(NotificationRequest notificationRequest)
        {
            Task<HttpResponseMessage> result = null;

            try
            {
                if (notificationRequest is null)
                    throw new ArgumentException("InvalidNotificationRequest");

                Order order = _orderService.GetOrderById(notificationRequest.OrderId);

                if (order is null)
                    throw new ArgumentException("OrderNotFound");

                using HttpClient _httpClient = new HttpClient();
                string notificationUrl = string.Format(
                    _deliveryAppBackendConfigurationSettings.NotificationCenterUrl,
                    notificationRequest.TemplateType,
                    notificationRequest.CustomerId,
                    notificationRequest.AppPackageName);

                if (!notificationRequest.Payload.ContainsKey("orderId"))
                    notificationRequest.Payload.Add("orderId", notificationRequest.OrderId.ToString());

                StringContent bodyContent = new StringContent(JsonConvert.SerializeObject(notificationRequest.Payload), Encoding.UTF8, "application/json");
                result = _httpClient.PostAsync(notificationUrl, bodyContent);

                if (!result.Result.IsSuccessStatusCode)
                {
                    throw new ArgumentException($"ErrorTryingToSendNotification. {result.Result}");
                }

                AddOrderNote(order, $"Request to send notification of type \"{notificationRequest.TemplateType.Value}\" to the customer id {notificationRequest.CustomerId} was sent successfully,");
            }
            catch (Exception e)
            {
                _logger.Error(e.ToString() + "\n\n" + $"Request body: \n{JsonConvert.SerializeObject(notificationRequest)}" + "\n\n" + $"Response: {JsonConvert.SerializeObject(result == null ? default : result.Result)}", e);
            }
        }

        ///<inheritdoc/>
        public void SendDriverCoordinateTrackingUpdate(DriverLocationInfoRequest driverRequest)
        {
            try
            {
                if (driverRequest is null)
                    throw new ArgumentException("InvalidNotificationRequest");

                Order order = _orderService.GetOrderById(driverRequest.OrderId);

                if (order is null)
                    throw new ArgumentException("OrderNotFound");

                OrderDeliveryStatusMapping orderMapping = _orderDeliveryStatusMappingRepository
                    .Table.FirstOrDefault(statusMapping => statusMapping.OrderId == driverRequest.OrderId);

                if (orderMapping is null)
                    throw new ArgumentException("OrderDeliveryNotFound");

                if (orderMapping.CustomerId is null)
                    throw new ArgumentException("ThereIsNoDriverAssignedToTheOrder");

                Customer driver = _customerService.GetCustomerById(orderMapping.CustomerId.Value);

                if (driver is null)
                    throw new ArgumentException("DriverNotFound");

                string PictureUrl = _pictureService.GetPictureUrl(_genericAttributeService
                    .GetAttribute<int>(driver, NopCustomerDefaults.AvatarPictureIdAttribute),
                    _mediaSettings.AvatarPictureSize, false);

                if (driver.ShippingAddressId is null)
                    throw new ArgumentException("DriverShippingAddresIdNotFound");

                Address addressInfo = _addressService.GetAddressById(driver.ShippingAddressId.Value);

                if (addressInfo is null)
                    throw new ArgumentException("AddressNotFound");

                var orderItems = _orderService.GetOrderItems(driverRequest.OrderId);

                if (orderItems is null || !orderItems.Any())
                    throw new ArgumentException("OrderWithoutItems");

                var product = _productService.GetProductById(orderItems.First().ProductId);

                if (product is null)
                    throw new ArgumentException("ProductNotFound");

                bool containsCoordinates = driverRequest.Latitude != null && driverRequest.Longitude != null;

                string googlePolyline = "";
                string estimatedTime = "";
                string destinationPlace = "";

                if (containsCoordinates)
                {
                    googlePolyline = _googleDirectionService.GetGoogleDirectionPolyline(orderMapping);

                    estimatedTime = _googleDirectionService.GetGoogleDirections(new GoogleDirecctionRequest
                    {
                        OrderId = driverRequest.OrderId,
                        Latitude = driverRequest.Latitude,
                        Longitude = driverRequest.Longitude

                    }).Entity.routes[0].legs[0].duration.text ?? "N/A";

                    string customerDestinationMessage = "TowardsYourLocation";

                    destinationPlace = orderMapping.DeliveryStatusId ==
                       (int)DeliveryStatus.OrderPreparationCompleted ?
                       GetVendorFromOrder(orderMapping.OrderId).Name : customerDestinationMessage;
                }

                using var _httpClient = new HttpClient();
                string notificationUrl = string.Format(_deliveryAppBackendConfigurationSettings.NotificationDriverTrackingUrl, driverRequest.OrderId, order.CustomerId);

                var bodyContent = new StringContent(JsonConvert.SerializeObject(new
                {
                    coordinates = containsCoordinates ? new
                    {
                        latitude = driverRequest?.Latitude,
                        longitude = driverRequest?.Longitude
                    } : null,
                    shipmentStatus = order.ShippingStatusId,
                    orderStatus = orderMapping.DeliveryStatusId,
                    polyline = googlePolyline,
                    estimatedTime = estimatedTime,
                    destinationPlace = destinationPlace,
                    driver = new
                    {
                        id = driver?.Id ?? null,
                        driverName = string.Concat(addressInfo.FirstName, " ", addressInfo.LastName),
                        driverImageUrl = PictureUrl,
                        driverPhoneNumber = addressInfo?.PhoneNumber ?? "",
                        canBeContactedByCustomer = driverRequest.CanContactDriver
                    }

                }), Encoding.UTF8, "application/json");
                Task<HttpResponseMessage> result = _httpClient.PostAsync(notificationUrl, bodyContent);
                result.Wait();

                if (!result.Result.IsSuccessStatusCode)
                {
                    throw new ArgumentException($"ErrorTryingToSendNotification. {result.Result}");
                }

            }
            catch (Exception e)
            {
                _logger.Error(e.ToString(), e);
            }
        }

        ///<inheritdoc/>
        public void SendOrderStatusTrackingUpdate(int orderId)
        {
            try
            {
                var order = _orderService.GetOrderById(orderId);

                if (order is null)
                    throw new ArgumentException("OrderNotFound");

                OrderDeliveryStatusMapping orderMapping = _orderDeliveryStatusMappingRepository
                    .Table.FirstOrDefault(statusMapping => statusMapping.OrderId == order.Id);

                if (orderMapping is null)
                    throw new ArgumentException("OrderDeliveryNotFound");

                using var _httpClient = new HttpClient();
                string notificationUrl = string.Format(_deliveryAppBackendConfigurationSettings.NotificationDriverTrackingUrl, order.Id, order.CustomerId);

                var bodyContent = new StringContent(JsonConvert.SerializeObject(new
                {
                    shipmentStatus = order.ShippingStatusId,
                    orderStatus = orderMapping.DeliveryStatusId
                }), Encoding.UTF8, "application/json");
                Task<HttpResponseMessage> result = _httpClient.PostAsync(notificationUrl, bodyContent);
                result.Wait();

                if (!result.Result.IsSuccessStatusCode)
                {
                    throw new ArgumentException($"ErrorTryingToSendNotification. {result.Result}");
                }

            }
            catch (Exception e)
            {
                _logger.Error(e.ToString(), e);
            }
        }

        #region Private methods
        private Vendor GetVendorFromOrder(int orderId)
        {
            IList<OrderItem> orderItems = _orderService.GetOrderItems(orderId);
            Product product = _productService.GetProductById(orderItems.First().ProductId);

            return _vendorService.GetVendorById(product.VendorId);
        }

        private void AddOrderNote(Order order, string note)
        {
            _orderService.InsertOrderNote(new OrderNote
            {
                OrderId = order.Id,
                Note = note,
                DisplayToCustomer = false,
                CreatedOnUtc = DateTime.UtcNow
            });
        }
        #endregion
    }
}