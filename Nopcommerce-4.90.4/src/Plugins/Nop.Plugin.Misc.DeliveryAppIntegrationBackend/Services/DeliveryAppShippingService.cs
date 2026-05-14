using Nop.Core.Domain.Shipping;
using Nop.Data;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.contracts;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Helpers;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models;
using Nop.Services.Configuration;
using Nop.Services.Shipping;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Services
{
    /// <summary>
    /// Represents a implementation for the delivery app shipping services.
    /// </summary>
    public sealed class DeliveryAppShippingService : IDeliveryAppShippingService
    {
        #region Fields

        private readonly IRepository<AddressGeoCoordinatesMapping> _addressGeoCoordinateRepository;
        private readonly IRepository<ShippingMethod> _shippingMethodRepository;
        private readonly IRepository<VendorWarehouseMapping> _vendorWarehouseMappingRepository;
        private readonly IShippingService _shippingService;
        private readonly ISettingService _settingService;
        private readonly IGoogleDirectionService _googleDirectionService;

        #endregion

        #region Ctor

        /// <summary>
        /// Initializes a new instance of <see cref="DeliveryAppShippingService"/>.
        /// </summary>
        /// <param name="deliveryAppAddressService">An implementation of <see cref="IDeliveryAppAddressService"/>.</param>
        /// <param name="addressGeoCoordinateRepository">An implementation of <see cref="IRepository{T}"/> where T is <see cref="AddressGeoCoordinatesMapping"/>.</param>
        /// <param name="shippingMethodRepository">An implementation of <see cref="IRepository{T}"/> where T is <see cref="ShippingMethod"/>.</param>
        /// <param name="vendorWarehouseMappingRepository">An implementation of <see cref="IRepository{T}"/> where T is <see cref="VendorWarehouseMapping"/>.</param>
        /// <param name="shippingService">An implementation of <see cref="IShippingService"/>.</param>
        /// <param name="settingService">An implementation of <see cref="ISettingService"/>.</param>
        /// <param name="googleDirectionService">An implementation of <see cref="IGoogleDirectionService"/>.</param>
        public DeliveryAppShippingService(

            IRepository<AddressGeoCoordinatesMapping> addressGeoCoordinateRepository,
            IRepository<ShippingMethod> shippingMethodRepository,
            IRepository<VendorWarehouseMapping> vendorWarehouseMappingRepository,
            IShippingService shippingService,
            ISettingService settingService,
            IGoogleDirectionService googleDirectionService)
        {
            _addressGeoCoordinateRepository = addressGeoCoordinateRepository;
            _shippingMethodRepository = shippingMethodRepository;
            _vendorWarehouseMappingRepository = vendorWarehouseMappingRepository;
            _shippingService = shippingService;
            _settingService = settingService;
            _googleDirectionService = googleDirectionService;
        }

        #endregion

        #region Utilities

        private DeliveryAppShippingInfo GetShippingMethodNameByDistanceInKm(decimal distanceInKm)
        {
            DeliveryAppShippingInfo result = new DeliveryAppShippingInfo();
            double distance = double.Parse(distanceInKm.ToString());

            int shippingMethodId = 0;
            IList<ShippingMethod> shippingMethods = _shippingMethodRepository.Table.ToList();
            Regex regex = new Regex("Ground: (([0-9]{1,2})km - ([0-9]{1,2})km)");

            foreach (ShippingMethod shippingMethod in shippingMethods)
            {
                MatchCollection matches = regex.Matches(shippingMethod.Name);
                if (matches.Count != 1 || !matches[0].Success || matches[0].Groups.Count != 4) continue;
                GroupCollection matchGroups = matches[0].Groups;
                bool startKmParse = double.TryParse(matchGroups[2].Value, out double startKm);
                bool endKmParse = double.TryParse(matchGroups[3].Value, out double endKm);
                if (!startKmParse || !endKmParse) continue;
                if (distance >= startKm && distance <= endKm)
                {
                    result.ShippingMethodName = shippingMethod.Name;

                    shippingMethodId = shippingMethod.Id;

                    break;
                }
            }

            var setting = _settingService.GetSetting(string.Format(Defaults.FixedByWeightByTotalRateSettingsKey, shippingMethodId));
            result.ShippingMethodCost = setting == null ? 0.0 : double.Parse(setting.Value);

            return result;
        }

        #endregion

        #region Methods

        ///<inheritdoc/>
        public IOperationResult<DeliveryAppShippingInfo> GetShippingMethodNameByAddressesDistance(int vendorId, decimal toLatitude, decimal toLongitude)
        {
            if (vendorId == 0) return BasicOperationResult<DeliveryAppShippingInfo>.Fail("VendorIdCantBeZero");
            if (toLatitude == 0 || toLongitude == 0) return BasicOperationResult<DeliveryAppShippingInfo>.Fail("DestinationAddressInvalid");

            VendorWarehouseMapping vendorWarehouse = _vendorWarehouseMappingRepository.Table.FirstOrDefault(mapping => mapping.VendorId == vendorId);

            if (vendorWarehouse is null) return BasicOperationResult<DeliveryAppShippingInfo>.Fail("FromDeliveryAddressNotDetermined");

            Warehouse warehouse = _shippingService.GetWarehouseById(vendorWarehouse.WarehouseId);

            if (warehouse is null || warehouse.AddressId == 0) return BasicOperationResult<DeliveryAppShippingInfo>.Fail("FromDeliveryAddressNotDetermined");

            AddressGeoCoordinatesMapping fromAddressGeoCoordinates = _addressGeoCoordinateRepository.Table
                .FirstOrDefault(mapping => mapping.AddressId == warehouse.AddressId);

            if (fromAddressGeoCoordinates is null) return BasicOperationResult<DeliveryAppShippingInfo>.Fail("FromDeliveryAddressNotDetermined");

            var distanteCalculationInKm = _googleDirectionService.GetDistanceBetweenPoints(toLatitude, toLongitude, fromAddressGeoCoordinates.Latitude, fromAddressGeoCoordinates.Longitude);

            if (distanteCalculationInKm.Success)
            {
                return BasicOperationResult<DeliveryAppShippingInfo>.Ok(GetShippingMethodNameByDistanceInKm(distanteCalculationInKm.Entity));
            }
            else
            {
                return BasicOperationResult<DeliveryAppShippingInfo>.Fail(distanteCalculationInKm.Message, distanteCalculationInKm.MessageDetail);
            }
        }

        #endregion
    }
}
