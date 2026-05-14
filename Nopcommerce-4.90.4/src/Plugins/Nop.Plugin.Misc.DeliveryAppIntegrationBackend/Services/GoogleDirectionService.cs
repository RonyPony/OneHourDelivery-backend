using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Shipping;
using Nop.Data;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.contracts;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains.Enums;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Helpers;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models;
using Nop.Services.Catalog;
using Nop.Services.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Services
{
    /// <summary>
    /// Represents the services implementation to google direction services to consume directions.
    /// </summary>
    public class GoogleDirectionService : IGoogleDirectionService
    {
        #region Fields
        private readonly IRepository<OrderDeliveryStatusMapping> _orderDeliveryStatusMappingRepository;
        private readonly IOrderService _orderService;
        private readonly IRepository<Order> _orderRepository;
        private readonly IProductService _productService;
        private readonly IRepository<VendorWarehouseMapping> _vendorWarehouseMappingRepository;
        private readonly IRepository<Warehouse> _warehouseRepository;
        private readonly IRepository<AddressGeoCoordinatesMapping> _addressGeoCoordinateRepository;
        private readonly IRepository<GoogleDirectionMapping> _googleDirectionMappingRepository;
        private readonly PluginConfigurationSettings _pluginConfigurationSettings;

        #endregion

        #region Ctor
        /// <summary>
        /// Initializes a new instance of <see cref="GoogleDirectionService"/>.
        /// </summary>
        /// <param name="orderDeliveryStatusMappingRepository">An implementation of <see cref="IRepository{OrderDeliveryStatusMapping}"/>.</param>
        /// <param name="productService">An implementation of <see cref="IProductService"/>.</param>
        /// <param name="orderService">An implementation of <see cref="IOrderService"/>.</param>
        /// <param name="vendorWarehouseMappingRepository">An implementation of <see cref="IRepository{VendorWarehouseMapping}"/>.</param>
        /// <param name="warehouseRepository">An implementation of <see cref="IRepository{Warehouse}"/>.</param>
        /// <param name="addressGeoCoordinateRepository">An implementation of <see cref="IRepository{AddressGeoCoordinatesMapping}"/>.</param>
        /// <param name="orderRepository">An implementation of <see cref="IRepository{Order}"/>.</param>
        /// <param name="googleDirectionMappingRepository">An implementation of <see cref="IRepository{GoogleDirectionMapping}"/>.</param>
        /// <param name="pluginConfigurationSettings">An implementation of <see cref="PluginConfigurationSettings"/>.</param>
        public GoogleDirectionService
            (IRepository<OrderDeliveryStatusMapping> orderDeliveryStatusMappingRepository,
             IProductService productService,
             IOrderService orderService,
             IRepository<VendorWarehouseMapping> vendorWarehouseMappingRepository,
             IRepository<Warehouse> warehouseRepository,
             IRepository<AddressGeoCoordinatesMapping> addressGeoCoordinateRepository,
             IRepository<Order> orderRepository,
             IRepository<GoogleDirectionMapping> googleDirectionMappingRepository,
             PluginConfigurationSettings pluginConfigurationSettings)
        {
            _orderDeliveryStatusMappingRepository = orderDeliveryStatusMappingRepository;
            _productService = productService;
            _orderService = orderService;
            _orderRepository = orderRepository;
            _vendorWarehouseMappingRepository = vendorWarehouseMappingRepository;
            _warehouseRepository = warehouseRepository;
            _addressGeoCoordinateRepository = addressGeoCoordinateRepository;
            _googleDirectionMappingRepository = googleDirectionMappingRepository;
            _pluginConfigurationSettings = pluginConfigurationSettings;
        }
        #endregion

        ///<inheritdoc/>
        public IOperationResult<GoogleDirectionResourceModel> GetGoogleDirections(GoogleDirecctionRequest directionRequest)
        {
            var googleResourceCoordinates = new GoogleDirectionResourceModel();

            if (directionRequest is null)
                throw new ArgumentException("InvalidGoogleDirection");

            var order = _orderDeliveryStatusMappingRepository.Table
                .FirstOrDefault(mapping => mapping.OrderId == directionRequest.OrderId);

            if (order is null)
                throw new ArgumentException("OrderNotFound");

            if (order.DeliveryStatusId == (int)DeliveryStatus.OrderPreparationCompleted
                || order.DeliveryStatusId == (int)DeliveryStatus.AssignedToMessenger)
            {
                var storeCoordinate = FindStoreCoordinates(directionRequest);

                var result = GetGoogleResource(directionRequest, storeCoordinate);
                if (!result.Success) {
                    return BasicOperationResult<GoogleDirectionResourceModel>.Fail(result.Message, result.MessageDetail);
                }

                googleResourceCoordinates = result.Entity;
                InsertGoogleDirections(directionRequest, DestinationType.Commerce, googleResourceCoordinates);
            }
            else if (order.DeliveryStatusId == (int)DeliveryStatus.DeliveryInProgress)
            {
                var customerCoordinate = FindCustomerCoordinates(directionRequest);

                var result = GetGoogleResource(directionRequest, customerCoordinate);
                if (!result.Success)
                {
                    return BasicOperationResult<GoogleDirectionResourceModel>.Fail(result.Message, result.MessageDetail);
                }

                googleResourceCoordinates = result.Entity;
                InsertGoogleDirections(directionRequest, DestinationType.Client, googleResourceCoordinates);
            }

            return BasicOperationResult<GoogleDirectionResourceModel>.Ok( googleResourceCoordinates);
        }

        private Dictionary<int, decimal> FindStoreCoordinates(GoogleDirecctionRequest directionRequest)
        {
            IList<OrderItem> foundStoreOrderItems = _orderService.GetOrderItems(directionRequest.OrderId);

            if (foundStoreOrderItems.Count == 0)
                throw new ArgumentException("OrderWithoutItems");

            Product foundStoreProduct = _productService.GetProductById(foundStoreOrderItems.First().ProductId);

            if (foundStoreProduct is null)
                throw new ArgumentException("ProductNotFound");

            VendorWarehouseMapping foundStoreVendorWarehouse = _vendorWarehouseMappingRepository.Table
                .FirstOrDefault(vendor => vendor.VendorId == foundStoreProduct.VendorId);

            if (foundStoreVendorWarehouse is null)
                throw new ArgumentException("VendorNotAssociatedWithAnyWarehouse");

            Warehouse foundStoreWarehouse = _warehouseRepository.Table
                .FirstOrDefault(warehouse => warehouse.Id == foundStoreVendorWarehouse.WarehouseId);

            if (foundStoreWarehouse is null)
                throw new ArgumentException("WarehouseNotFound");

            AddressGeoCoordinatesMapping foundStoreAddress = _addressGeoCoordinateRepository.Table
                .FirstOrDefault(address => address.AddressId == foundStoreWarehouse.AddressId);
            Dictionary<int, decimal> storeCoordinates = new Dictionary<int, decimal>();
            storeCoordinates.Add((int)CoordinatesKey.LatitudeIndex, foundStoreAddress.Latitude);
            storeCoordinates.Add((int)CoordinatesKey.LongitudeIndex, foundStoreAddress.Longitude);
            return storeCoordinates;
        }

        private Dictionary<int, decimal> FindCustomerCoordinates(GoogleDirecctionRequest directionRequest)
        {
            var customerOrder = _orderRepository.Table
                .FirstOrDefault(ord => ord.Id == directionRequest.OrderId);

            if (customerOrder is null)
                throw new ArgumentException("OrderNotFound");

            AddressGeoCoordinatesMapping foundCustomerAddress = _addressGeoCoordinateRepository.Table
           .FirstOrDefault(delivery => delivery.AddressId == customerOrder.ShippingAddressId);

            if (foundCustomerAddress is null)
                throw new ArgumentException("ClientAddressGeoCoordinatesNotFound");

            Dictionary<int, decimal> customerCoordinates = new Dictionary<int, decimal>();
            customerCoordinates.Add((int)CoordinatesKey.LatitudeIndex, foundCustomerAddress.Latitude);
            customerCoordinates.Add((int)CoordinatesKey.LongitudeIndex, foundCustomerAddress.Longitude);
            return customerCoordinates;
        }

        private IOperationResult<GoogleDirectionResourceModel> GetGoogleResource(GoogleDirecctionRequest directionRequest, Dictionary<int, decimal> coordinates)
        {
            if (string.IsNullOrWhiteSpace(_pluginConfigurationSettings.ApiKey))
                return BasicOperationResult<GoogleDirectionResourceModel>.Fail("InvalidGoogleApiKey");

            using (var httpClient = new HttpClient())
            {
                string googleUrl = "https://maps.googleapis.com/maps/api/directions/json";
                UriBuilder uriBuilder = new UriBuilder(googleUrl);
                uriBuilder.Query = string.Format(
                    Defaults.GoogleDirectionQueryParamsTemplate,
                    directionRequest.Latitude,
                    directionRequest.Longitude,
                    coordinates[1],
                    coordinates[2],
                    _pluginConfigurationSettings.ApiKey,
                    "es-419");
                uriBuilder.Port = -1;
                Task<HttpResponseMessage> googleResponse = httpClient.GetAsync(uriBuilder.Uri);
                googleResponse.Wait();

                if (googleResponse.Result.IsSuccessStatusCode)
                {
                    string response = googleResponse.Result.Content.ReadAsStringAsync().Result;
                    GoogleDirectionResourceModel googleResource = JsonConvert.DeserializeObject<GoogleDirectionResourceModel>(response);

                    if ( googleResource == null || googleResource.routes.Count <= 0)
                        return BasicOperationResult<GoogleDirectionResourceModel>.Fail("InvalidGoogleRoutes");

                    return BasicOperationResult<GoogleDirectionResourceModel>.Ok(googleResource);
                }
                else
                {
                    string response = googleResponse.Result.Content.ReadAsStringAsync().Result;

                    var error = GoogleDirectionRouteError.FromJson(response);
                    return BasicOperationResult<GoogleDirectionResourceModel>.Fail("ErrorGettingRoutes", error.ErrorMessage);
                }
            }
        }

        public IOperationResult<decimal> GetDistanceBetweenPoints(decimal latitudePoint1, decimal longitudePoint1, decimal latitudePoint2, decimal longitudePoint2)
        {
            if (string.IsNullOrWhiteSpace(_pluginConfigurationSettings.ApiKey))
                return BasicOperationResult<decimal>.Fail("InvalidGoogleApiKey");

            DistanceRouteInfo googleResource;

            using (var httpClient = new HttpClient())
            {
                string googleUrl = $"https://maps.googleapis.com/maps/api/distancematrix/json?units=metric&origins={latitudePoint1},{longitudePoint1}&destinations={latitudePoint2},{longitudePoint2}&key={_pluginConfigurationSettings.ApiKey}";
                UriBuilder uriBuilder = new UriBuilder(googleUrl);

                Task<HttpResponseMessage> googleResponse = httpClient.GetAsync(uriBuilder.Uri);
                googleResponse.Wait();

                if (googleResponse.Result.IsSuccessStatusCode)
                {
                    string response = googleResponse.Result.Content.ReadAsStringAsync().Result;

                    googleResource = DistanceRouteInfo.FromJson(response);

                    if (googleResource == null || !googleResource.Rows.Any())
                        return BasicOperationResult<decimal>.Fail("CantAccessToDestinationRoute");

                    string status = googleResource.Rows[0].Elements[0].Status;

                    if (status.Equals("ZERO_RESULTS") || status.Equals("NOT_FOUND"))
                        return BasicOperationResult<decimal>.Fail("RouteCouldNotBeCalculated");

                    if (status.Equals("MAX_ROUTE_LENGTH_EXCEEDED"))
                        return BasicOperationResult<decimal>.Fail("RouteIsTooLong");

                    decimal distance = decimal.Parse(googleResource.Rows[0].Elements[0].Distance.Value.ToString()) / 1000.0m;

                    if (distance <= 0)
                        return BasicOperationResult<decimal>.Fail("ErrorCalculatingDistanceCantBeZero");

                    return BasicOperationResult<decimal>.Ok(Math.Round(distance));
                }
                else
                {
                    string response = googleResponse.Result.Content.ReadAsStringAsync().Result;

                    var error = DistanceRouteInfoError.FromJson(response); 

                    return BasicOperationResult<decimal>.Fail("ErrorCalculatingDistanceBetweenPoints", $"{error.ErrorMessage}. {googleResponse.Result.ReasonPhrase} - {googleResponse.Result.StatusCode}");
                }
            }
        }

        private void InsertGoogleDirections(GoogleDirecctionRequest directionRequest,
            DestinationType googleDestination, GoogleDirectionResourceModel googleResourceCoordinates)
        {
            GoogleDirectionMapping googleDirectionMappingsResult = _googleDirectionMappingRepository.Table
                  .FirstOrDefault(google => google.DestinationType == (int)googleDestination
                  && google.OrderId == directionRequest.OrderId);

            if (googleDirectionMappingsResult != null)
            {
                googleDirectionMappingsResult.GoogleResource = googleResourceCoordinates.routes[0]
                    .overview_polyline.points;
                googleDirectionMappingsResult.RequestNumber++;
                _googleDirectionMappingRepository.Update(googleDirectionMappingsResult);
            }
            else
            {
                const int requestCount = 1;

                _googleDirectionMappingRepository.Insert(new GoogleDirectionMapping
                {
                    CreateOnUtc = DateTime.UtcNow,
                    DestinationType = (int)googleDestination,
                    GoogleResource = googleResourceCoordinates.routes[0].overview_polyline.points,
                    OrderId = directionRequest.OrderId,
                    RequestNumber = requestCount
                });

            }
          
        }

        public string GetGoogleDirectionPolyline(OrderDeliveryStatusMapping orderDeliveryStatus)
        {
            string polyline = string.Empty;

            if (orderDeliveryStatus.DeliveryStatusId == (int)DeliveryStatus.OrderPreparationCompleted || 
                orderDeliveryStatus.DeliveryStatusId == (int) DeliveryStatus.AssignedToMessenger)
            {
                polyline = _googleDirectionMappingRepository.Table
                   .Where(google => google.OrderId == orderDeliveryStatus.OrderId &&
                   google.DestinationType == (int)DestinationType.Commerce)
                   .OrderByDescending(orderby => orderby.RequestNumber)
                   .Select(select => select.GoogleResource).FirstOrDefault();
            }
            else if (orderDeliveryStatus.DeliveryStatusId == (int)DeliveryStatus.DeliveryInProgress)
            {
                polyline = _googleDirectionMappingRepository.Table
                   .Where(google => google.OrderId == orderDeliveryStatus.OrderId &&
                   google.DestinationType == (int)DestinationType.Client)
                   .OrderByDescending(orderby => orderby.RequestNumber)
                   .Select(select => select.GoogleResource).FirstOrDefault();
            }
            return polyline;
        }


    }
}
