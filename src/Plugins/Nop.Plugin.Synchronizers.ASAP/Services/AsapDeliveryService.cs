using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Directory;
using Nop.Core.Domain.Shipping;
using Nop.Plugin.Synchronizers.ASAP.Contracts;
using Nop.Plugin.Synchronizers.ASAP.Domains;
using Nop.Plugin.Synchronizers.ASAP.Enums;
using Nop.Plugin.Synchronizers.ASAP.Models;
using Nop.Plugin.Widgets.GoogleMapsIntegrationMultientrega.Domains;
using Nop.Plugin.Widgets.GoogleMapsIntegrationMultientrega.Services;
using Nop.Services.Common;
using Nop.Services.Configuration;
using Nop.Services.Directory;
using Nop.Services.Shipping;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Synchronizers.ASAP.Services
{
    /// <summary>
    /// Provides the functionality of interacting with the  Asap Delivery.
    /// </summary>
    public sealed class AsapDeliveryService : IAsapDeliveryService
    {
        #region Field

        private readonly AsapDeliveryConfigurationSettings _asapDeliveryConfigurationSettings;
        private readonly IAddressGeoCoordinatesService _addressGeoCoordinatesService;
        private readonly ISettingService _settingService;
        private readonly IShippingService _shippingService;
        private readonly ICountryService _countryService;
        private readonly IStateProvinceService _stateProvinceService;
        private readonly IAddressService _addressService;

        #endregion

        #region Ctor

        /// <summary>
        /// Creates an instance of a <see cref="AsapDeliveryService"/>
        /// </summary>
        /// <param name="asapDeliveryConfigurationSettings">ASAP configuration setting.</param>
        /// <param name="addressGeoCoordinatesService">An implementation of a <see cref="IAddressGeoCoordinatesService"/></param>
        /// <param name="settingService">An implementation of a <see cref="ISettingService"/></param>
        /// <param name="shippingService">An implementation of a <see cref="IShippingService"/></param>
        /// <param name="countryService">An implementation of a <see cref="ICountryService"/></param>
        /// <param name="stateProvinceService">An implementation of a <see cref="IStateProvinceService"/></param>
        /// <param name="addressService">An implementation of a <see cref="IAddressService"/></param>
        public AsapDeliveryService(
            AsapDeliveryConfigurationSettings asapDeliveryConfigurationSettings,
            IAddressGeoCoordinatesService addressGeoCoordinatesService,
            ISettingService settingService,
            IShippingService shippingService,
            ICountryService countryService,
            IStateProvinceService stateProvinceService,
            IAddressService addressService)
        {
            _asapDeliveryConfigurationSettings = asapDeliveryConfigurationSettings;
            _addressGeoCoordinatesService = addressGeoCoordinatesService;
            _settingService = settingService;
            _shippingService = shippingService;
            _countryService = countryService;
            _stateProvinceService = stateProvinceService;
            _addressService = addressService;
        }

        #endregion

        #region Methods

        /// <inheritdoc/>
        async Task<string> IAsapDeliveryService.GetDeliveryTrackingLink(string deliveryId)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("x-api-key", _asapDeliveryConfigurationSettings.ApiKey);

                HttpResponseMessage response = await client.GetAsync($"{_asapDeliveryConfigurationSettings.ServiceUrl}/order/tracking?user_token={_asapDeliveryConfigurationSettings.UserToken}&shared_secret={_asapDeliveryConfigurationSettings.SharedSecret}&delivery_id={deliveryId}");
                string responseContent = await response.Content.ReadAsStringAsync();

                AsapDeliveryTrackingLinkResponse asapResponse = JsonConvert.DeserializeObject<AsapDeliveryTrackingLinkResponse>(responseContent);

                if (!asapResponse.Status)
                {
                    throw new Exception(asapResponse.Message);
                }

                return asapResponse.Tracking_Link;
            }
        }

        /// <inheritdoc/>
        async Task<AsapDeliveryStatus> IAsapDeliveryService.GetDeliveryStatus(string deliveryId)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("x-api-key", _asapDeliveryConfigurationSettings.ApiKey);

                HttpResponseMessage response = await client.GetAsync($"{_asapDeliveryConfigurationSettings.ServiceUrl}/order/status?token={_asapDeliveryConfigurationSettings.UserToken}&delivery_id={deliveryId}&shared_secret={_asapDeliveryConfigurationSettings.SharedSecret}");
                string responseContent = await response.Content.ReadAsStringAsync();

                AsapDeliveryStatusResponse asapResponse = JsonConvert.DeserializeObject<AsapDeliveryStatusResponse>(responseContent);

                if (!asapResponse.Status)
                {
                    throw new Exception(asapResponse.Message);
                }

                return new AsapDeliveryStatus
                {
                    DeliveryStatus = asapResponse.Delivery_Status,
                    ShippedDate = asapResponse.Updated_At,
                };
            }
        }

        /// <inheritdoc/>
        async Task<string> IAsapDeliveryService.CreateOrder(Address shippingAddress)
        {
            AsapRequest asapRequest = BuildAsapRequest(shippingAddress);
            JsonSerializerSettings serializationSetting = BuildSerializationSettings();


            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("x-api-key", _asapDeliveryConfigurationSettings.ApiKey);

                string json = JsonConvert.SerializeObject(asapRequest, serializationSetting);
                var body = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync($"{_asapDeliveryConfigurationSettings.ServiceUrl}/order", body);
                string responseContent = await response.Content.ReadAsStringAsync();

                AsapCreateResponse asapResponse = JsonConvert.DeserializeObject<AsapCreateResponse>(responseContent);

                if (!asapResponse.Status || asapResponse.Result is null) throw new Exception(asapResponse.Message);

                return asapResponse.Result.Delivery_Id.ToString();
            }
        }

        private JsonSerializerSettings BuildSerializationSettings()
        {
            DefaultContractResolver contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            };

            return new JsonSerializerSettings
            {
                ContractResolver = contractResolver,
                Formatting = Formatting.Indented
            };
        }

        private AsapRequest BuildAsapRequest(Address shippingAddress)
        {
            AsapDeliveryConfigurationSettings settings = _settingService.LoadSetting<AsapDeliveryConfigurationSettings>();
            AddressGeoCoordinatesMapping coordinates = _addressGeoCoordinatesService.GetAddressGeoCoordinates(shippingAddress.Id);
            
            if (coordinates is null)
                throw new ArgumentException("Destination address coordinates were not found.");
            
            if (!coordinates.Latitude.HasValue || !coordinates.Longitude.HasValue)
                throw new InvalidOperationException("Shipping address geo-coordinates not found.");
            
            Warehouse warehouse = _shippingService.GetWarehouseById(settings.DefaultWarehouseId);
            
            if (warehouse is null)
                throw new ArgumentException("Default store warehouse was not found.");
            
            Address warehouseAddress = _addressService.GetAddressById(warehouse.AddressId);
            
            if (warehouseAddress is null)
                throw new ArgumentException("Address for the store default warehouse was not found.");
            
            AddressGeoCoordinatesMapping warehouseCoordinates = _addressGeoCoordinatesService.GetAddressGeoCoordinates(warehouse.AddressId);
            
            if (warehouseCoordinates is null)
                throw new ArgumentException("Store default warehouse coordinates were not found.");
            
            if (!warehouseCoordinates.Latitude.HasValue || !warehouseCoordinates.Longitude.HasValue)
                throw new InvalidOperationException("Default store geo'coordinates not found.");

            return new AsapRequest
            {
                User_token = _asapDeliveryConfigurationSettings.UserToken,
                Shared_secret = _asapDeliveryConfigurationSettings.SharedSecret,
                Type_id = (int)AsapRequestType.Shopping,
                Is_personal = (int)AsapRequestIsPersonal.Bussines,
                Is_oneway = (int)AsapRequestTripWay.OneWay,
                Source_address = warehouseAddress.Address1,
                Source_lat = warehouseCoordinates.Latitude.Value.ToString(),
                Source_long = warehouseCoordinates.Longitude.Value.ToString(),
                Special_inst = "",
                Desti_address = shippingAddress.Address1,
                Dest_special_inst = "",
                Desti_lat = coordinates.Latitude.Value.ToString(),
                Desti_long = coordinates.Longitude.Value.ToString(),
                Request_later_time = "",
                Request_later = (int)AsapRequestSchedule.Instant,
                Vehicle_type = "bike"
            };
        }

        #endregion
    }
}
