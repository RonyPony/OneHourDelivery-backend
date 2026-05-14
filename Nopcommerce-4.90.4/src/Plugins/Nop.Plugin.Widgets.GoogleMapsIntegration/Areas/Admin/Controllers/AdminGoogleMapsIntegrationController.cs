using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Shipping;
using Nop.Data;
using Nop.Plugin.Widgets.GoogleMapsIntegration.Areas.Admin.Models;
using Nop.Plugin.Widgets.GoogleMapsIntegration.Domains;
using Nop.Plugin.Widgets.GoogleMapsIntegration.Services;
using Nop.Services.Common;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Messages;
using Nop.Services.Security;
using Nop.Services.Shipping;
using Nop.Web.Areas.Admin.Controllers;
using Nop.Web.Areas.Admin.Factories;
using Nop.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using Nop.Web.Extensions;
using Nop.Web.Framework;
using Nop.Web.Framework.Mvc.Filters;

namespace Nop.Plugin.Widgets.GoogleMapsIntegration.Controllers
{
    /// <summary>
    /// Represents the admin area main controller for Google Maps Integration plugin.
    /// </summary>
    public class AdminGoogleMapsIntegrationController : BaseAdminController
    {
        #region Fields

        private readonly ILocalizationService _localizationService;
        private readonly INotificationService _notificationService;
        private readonly IAddressService _addressService;
        private readonly ICustomerActivityService _customerActivityService;
        private readonly IPermissionService _permissionService;
        private readonly IShippingModelFactory _shippingModelFactory;
        private readonly ISettingService _settingService;
        private readonly IWarehouseService _warehouseService;
        private readonly IAddressGeoCoordinatesService _addressGeoCoordinatesService;
        private readonly IRepository<WarehouseUserCreatorMapping> _warehouseUserCreatorMappingRepository;
        private readonly IRepository<VendorWarehouseMapping> _vendorWarehouseMappingRepository;

        #endregion

        #region Ctor

        /// <summary>
        /// Initializes a new instance of <see cref="AdminGoogleMapsIntegrationController"/>.
        /// </summary>
        public AdminGoogleMapsIntegrationController(
            ILocalizationService localizationService,
            INotificationService notificationService,
            IAddressService addressService,
            ICustomerActivityService customerActivityService,
            IPermissionService permissionService,
            IShippingModelFactory shippingModelFactory,
            ISettingService settingService,
            IWarehouseService warehouseService,
            IAddressGeoCoordinatesService addressGeoCoordinatesService,
            IRepository<WarehouseUserCreatorMapping> warehouseUserCreatorMappingRepository,
            IRepository<VendorWarehouseMapping> vendorWarehouseMappingRepository)
        {
            _localizationService = localizationService;
            _notificationService = notificationService;
            _addressService = addressService;
            _customerActivityService = customerActivityService;
            _permissionService = permissionService;
            _shippingModelFactory = shippingModelFactory;
            _settingService = settingService;
            _warehouseService = warehouseService;
            _addressGeoCoordinatesService = addressGeoCoordinatesService;
            _warehouseUserCreatorMappingRepository = warehouseUserCreatorMappingRepository;
            _vendorWarehouseMappingRepository = vendorWarehouseMappingRepository;
        }

        #endregion

        #region Methods

        #region Warehouse Address

        /// <summary>
        /// Gets and sets the required models for adding new warehouse including address geo coordinates.
        /// </summary>
        /// <returns>An implementation of <see cref="IActionResult"/>.</returns>
        public virtual async Task<IActionResult> CreateWarehouse()
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermission.Configuration.MANAGE_SHIPPING_SETTINGS))
                return AccessDeniedView();

            var model = new CustomWarehouseEditModel
            {
                PluginConfigurationSettings = await _settingService.LoadSettingAsync<PluginConfigurationSettings>(),
                AddressGeoCoordinatesMapping = new AddressGeoCoordinatesMapping
                {
                    Latitude = 0.0000000m,
                    Longitude = 0.0000000m
                }
            };

            await _shippingModelFactory.PrepareWarehouseModelAsync(model.WarehouseModel, null);

            return View(model);
        }

        /// <summary>
        /// Inserts a new warehouse with address and its geo coordinates.
        /// </summary>
        /// <param name="model">An instance of <see cref="CustomWarehouseEditModel"/>.</param>
        /// <param name="continueEditing">A boolean indicating if continue editing.</param>
        /// <returns>An implementation of <see cref="IActionResult"/>.</returns>
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public virtual async Task<IActionResult> CreateWarehouse(CustomWarehouseEditModel model, bool continueEditing)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermission.Configuration.MANAGE_SHIPPING_SETTINGS))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                var address = model.WarehouseModel.Address.ToEntity<Address>();
                address.CreatedOnUtc = DateTime.UtcNow;
                await _addressService.InsertAddressAsync(address);
                await _addressGeoCoordinatesService.InsertAddressGeoCoordinatesAsync(model.AddressGeoCoordinatesMapping, address.Id);

                var warehouse = model.WarehouseModel.ToEntity<Warehouse>();
                warehouse.AddressId = address.Id;

                await _warehouseService.InsertWarehouseAsync(warehouse);

                await _customerActivityService.InsertActivityAsync("AddNewWarehouse",
                    string.Format(await _localizationService.GetResourceAsync("ActivityLog.AddNewWarehouse"), warehouse.Id), warehouse);

                _notificationService.SuccessNotification(await _localizationService.GetResourceAsync("Admin.Configuration.Shipping.Warehouses.Added"));

                return continueEditing ? RedirectToAction("EditWarehouse", new { id = warehouse.Id }) : RedirectToAction("Warehouses", "Shipping");
            }

            model.PluginConfigurationSettings = await _settingService.LoadSettingAsync<PluginConfigurationSettings>();
            await _shippingModelFactory.PrepareWarehouseModelAsync(model.WarehouseModel, null, true);

            return View(model);
        }

        /// <summary>
        /// Gets and sets the required models to edit a warehouse with its address and geo coordinates.
        /// </summary>
        /// <param name="id">The id of the warehouse to edit.</param>
        /// <returns>An implementation of <see cref="IActionResult"/>.</returns>
        public virtual async Task<IActionResult> EditWarehouse(int id)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermission.Configuration.MANAGE_SHIPPING_SETTINGS))
                return AccessDeniedView();

            var warehouse = await _warehouseService.GetWarehouseByIdAsync(id);
            if (warehouse == null)
                return RedirectToAction("Warehouses", "Shipping");

            var model = new CustomWarehouseEditModel
            {
                PluginConfigurationSettings = await _settingService.LoadSettingAsync<PluginConfigurationSettings>(),
                AddressGeoCoordinatesMapping = await _addressGeoCoordinatesService.GetAddressGeoCoordinatesAsync(warehouse.AddressId),
                WarehouseModel = await _shippingModelFactory.PrepareWarehouseModelAsync(null, warehouse)
            };

            if (model.AddressGeoCoordinatesMapping is null)
            {
                model.AddressGeoCoordinatesMapping = new AddressGeoCoordinatesMapping
                {
                    AddressId = warehouse.AddressId,
                    Latitude = 0.0000000m,
                    Longitude = 0.0000000m
                };
            }

            return View(model);
        }

        /// <summary>
        /// Updates a warehouse with its address and geo coordinates.
        /// </summary>
        /// <param name="model">An instance of <see cref="CustomWarehouseEditModel"/>.</param>
        /// <param name="continueEditing">A boolean indicating if continue editing.</param>
        /// <returns>An implementation of <see cref="IActionResult"/>.</returns>
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public virtual async Task<IActionResult> EditWarehouse(CustomWarehouseEditModel model, bool continueEditing)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermission.Configuration.MANAGE_SHIPPING_SETTINGS))
                return AccessDeniedView();

            var warehouse = await _warehouseService.GetWarehouseByIdAsync(model.WarehouseModel.Id);
            if (warehouse == null)
                return RedirectToAction("Warehouses", "Shipping");

            if (ModelState.IsValid)
            {
                var address = await _addressService.GetAddressByIdAsync(warehouse.AddressId) ??
                    new Address
                    {
                        CreatedOnUtc = DateTime.UtcNow
                    };

                address = model.WarehouseModel.Address.ToEntity(address);
                if (address.Id > 0)
                    await _addressService.UpdateAddressAsync(address);
                else
                    await _addressService.InsertAddressAsync(address);

                await _addressGeoCoordinatesService.InsertAddressGeoCoordinatesAsync(model.AddressGeoCoordinatesMapping, address.Id);

                warehouse = model.WarehouseModel.ToEntity(warehouse);
                warehouse.AddressId = address.Id;

                await _warehouseService.UpdateWarehouseAsync(warehouse);

                await _customerActivityService.InsertActivityAsync("EditWarehouse",
                    string.Format(await _localizationService.GetResourceAsync("ActivityLog.EditWarehouse"), warehouse.Id), warehouse);

                _notificationService.SuccessNotification(await _localizationService.GetResourceAsync("Admin.Configuration.Shipping.Warehouses.Updated"));

                return continueEditing ? RedirectToAction("EditWarehouse", new { id = warehouse.Id }) : RedirectToAction("Warehouses", "Shipping");
            }

            model.PluginConfigurationSettings = await _settingService.LoadSettingAsync<PluginConfigurationSettings>();
            model.WarehouseModel = await _shippingModelFactory.PrepareWarehouseModelAsync(model.WarehouseModel, warehouse, true);

            return View(model);
        }

        /// <summary>
        /// Deletes a warehouse and its geo coordinates.
        /// </summary>
        /// <param name="id">The id of the warehouse to delete.</param>
        /// <returns>An implementation of <see cref="IActionResult"/>.</returns>
        //[HttpPost]
        //public virtual async Task<IActionResult> DeleteWarehouse(int id)
        //{
        //    if (!await _permissionService.AuthorizeAsync(StandardPermission.Configuration.MANAGE_SHIPPING_SETTINGS))
        //        return AccessDeniedView();

        //    var warehouse = await _warehouseService.GetWarehouseByIdAsync(id);
        //    if (warehouse == null)
        //        return RedirectToAction("Warehouses", "Shipping");

        //    await _addressGeoCoordinatesService.RemoveAddressGeoCoordinatesAsync(warehouse.AddressId);
        //    await _warehouseService.DeleteWarehouseAsync(warehouse);

        //    await _customerActivityService.InsertActivityAsync("DeleteWarehouse",
        //        string.Format(await _localizationService.GetResourceAsync("ActivityLog.DeleteWarehouse"), warehouse.Id), warehouse);

        //    _notificationService.SuccessNotification(await _localizationService.GetResourceAsync("Admin.Configuration.Shipping.warehouses.Deleted"));

        //    return RedirectToAction("Warehouses", "Shipping");
        //}

        [HttpPost]
        public virtual async Task<IActionResult> DeleteWarehouse(int id)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermission.Configuration.MANAGE_SHIPPING_SETTINGS))
                return AccessDeniedView();

            var warehouse = await _warehouseService.GetWarehouseByIdAsync(id);

            if (warehouse == null)
                return RedirectToAction("Warehouses", "Shipping");

            var warehouseUserCreatorMappings = _warehouseUserCreatorMappingRepository.Table
                .Where(mapping => mapping.WarehouseId == id)
                .ToList();

            foreach (var mapping in warehouseUserCreatorMappings)
                await _warehouseUserCreatorMappingRepository.DeleteAsync(mapping);

            var vendorWarehouseMappings = _vendorWarehouseMappingRepository.Table
                .Where(mapping => mapping.WarehouseId == id)
                .ToList();

            foreach (var mapping in vendorWarehouseMappings)
                await _vendorWarehouseMappingRepository.DeleteAsync(mapping);

            await _addressGeoCoordinatesService.RemoveAddressGeoCoordinatesAsync(warehouse.AddressId);

            await _warehouseService.DeleteWarehouseAsync(warehouse);

            await _customerActivityService.InsertActivityAsync(
                "DeleteWarehouse",
                string.Format(await _localizationService.GetResourceAsync("ActivityLog.DeleteWarehouse"), warehouse.Id),
                warehouse);

            _notificationService.SuccessNotification(
                await _localizationService.GetResourceAsync("Admin.Configuration.Shipping.Warehouses.Deleted"));

            return RedirectToAction("Warehouses", "Shipping");
        }

        #endregion

        #endregion
    }
}
