using Microsoft.AspNetCore.Mvc;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Shipping;
using Nop.Plugin.Widgets.GoogleMapsIntegrationMultientrega.Areas.Admin.Models;
using Nop.Plugin.Widgets.GoogleMapsIntegrationMultientrega.Domains;
using Nop.Plugin.Widgets.GoogleMapsIntegrationMultientrega.Services;
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
using Nop.Web.Framework.Mvc.Filters;
using System;

namespace Nop.Plugin.Widgets.GoogleMapsIntegrationMultientrega.Controllers
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
        private readonly IShippingService _shippingService;
        private readonly ISettingService _settingService;
        private readonly IAddressGeoCoordinatesService _addressGeoCoordinatesService;

        #endregion

        #region Ctor

        /// <summary>
        /// Initializes a new instance of <see cref="AdminGoogleMapsIntegrationController"/>.
        /// </summary>
        /// <param name="localizationService">An implementation of <see cref="ILocalizationService"/>.</param>
        /// <param name="notificationService">An implementation of <see cref="INotificationService"/>.</param>
        /// <param name="addressService">An implementation of <see cref="IAddressService"/>.</param>
        /// <param name="addressGeoCoordinatesService">An implementation of <see cref="IAddressGeoCoordinatesService"/>.</param>
        /// <param name="customerActivityService">An implementation of <see cref="ICustomerActivityService"/>.</param>
        /// <param name="permissionService">An implementation of <see cref="IPermissionService"/>.</param>
        /// <param name="settingService">An implementation of <see cref="ISettingService"/>.</param>
        /// <param name="shippingModelFactory">An implementation of <see cref="IShippingModelFactory"/>.</param>
        /// <param name="shippingService">An implementation of <see cref="IShippingService"/>.</param>
        public AdminGoogleMapsIntegrationController(
            ILocalizationService localizationService,
            INotificationService notificationService,
            IAddressService addressService,
            ICustomerActivityService customerActivityService,
            IPermissionService permissionService,
            IShippingModelFactory shippingModelFactory,
            IShippingService shippingService,
            ISettingService settingService,
            IAddressGeoCoordinatesService addressGeoCoordinatesService)
        {
            _localizationService = localizationService;
            _notificationService = notificationService;
            _addressService = addressService;
            _customerActivityService = customerActivityService;
            _permissionService = permissionService;
            _shippingModelFactory = shippingModelFactory;
            _shippingService = shippingService;
            _settingService = settingService;
            _addressGeoCoordinatesService = addressGeoCoordinatesService;
        }

        #endregion

        #region Methods

        #region Warehouse Address

        /// <summary>
        /// Gets and sets the required models for adding new warehouse including address geo coordinates.
        /// </summary>
        /// <returns>An implementation of <see cref="IActionResult"/>.</returns>
        public virtual IActionResult CreateWarehouse()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageShippingSettings))
                return AccessDeniedView();

            //prepare model
            var model = new CustomWarehouseEditModel
            {
                PluginConfigurationSettings = _settingService.LoadSetting<PluginConfigurationSettings>(),
                AddressGeoCoordinatesMapping = new AddressGeoCoordinatesMapping
                {
                    Latitude = 0.0000000m,
                    Longitude = 0.0000000m,
                    ProvinceId = "",
                    DistrictId = "",
                    TownshipId = "",
                    NeighborhoodId = ""
                }
            };

            _shippingModelFactory.PrepareWarehouseModel(model.WarehouseModel, null);

            return View(model);
        }

        /// <summary>
        /// Inserts a new warehouse with address and its geo coordinates.
        /// </summary>
        /// <param name="model">An instance of <see cref="CustomWarehouseEditModel"/>.</param>
        /// <param name="continueEditing">A boolean indicating if continue editing.</param>
        /// <returns>An implementation of <see cref="IActionResult"/>.</returns>
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public virtual IActionResult CreateWarehouse(CustomWarehouseEditModel model, bool continueEditing)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageShippingSettings))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                var address = model.WarehouseModel.Address.ToEntity<Address>();
                address.CreatedOnUtc = DateTime.UtcNow;
                _addressService.InsertAddress(address);
                _addressGeoCoordinatesService.InsertAddressGeoCoordinates(model.AddressGeoCoordinatesMapping, address.Id);

                //fill entity from model
                var warehouse = model.WarehouseModel.ToEntity<Warehouse>();
                warehouse.AddressId = address.Id;

                _shippingService.InsertWarehouse(warehouse);

                //activity log
                _customerActivityService.InsertActivity("AddNewWarehouse",
                    string.Format(_localizationService.GetResource("ActivityLog.AddNewWarehouse"), warehouse.Id), warehouse);

                _notificationService.SuccessNotification(_localizationService.GetResource("Admin.Configuration.Shipping.Warehouses.Added"));

                return continueEditing ? RedirectToAction("EditWarehouse", new { id = warehouse.Id }) : RedirectToAction("Warehouses", "Shipping");
            }

            //prepare model
            var pluginConfigSettings = _settingService.LoadSetting<PluginConfigurationSettings>();
            model.PluginConfigurationSettings = pluginConfigSettings;
            _shippingModelFactory.PrepareWarehouseModel(model.WarehouseModel, null, true);

            //if we got this far, something failed, redisplay form
            return View(model);
        }

        /// <summary>
        /// Gets and sets the required models to edit a warehouse with its address and geo coordinates.
        /// </summary>
        /// <param name="id">The id of the warehouse to edit.</param>
        /// <returns>An implementation of <see cref="IActionResult"/>.</returns>
        public virtual IActionResult EditWarehouse(int id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageShippingSettings))
                return AccessDeniedView();

            //try to get a warehouse with the specified id
            var warehouse = _shippingService.GetWarehouseById(id);
            if (warehouse == null)
                return RedirectToAction("Warehouses", "Shipping");

            //prepare model
            var model = new CustomWarehouseEditModel
            {
                PluginConfigurationSettings = _settingService.LoadSetting<PluginConfigurationSettings>(),
                AddressGeoCoordinatesMapping = _addressGeoCoordinatesService.GetAddressGeoCoordinates(warehouse.AddressId),
                WarehouseModel = _shippingModelFactory.PrepareWarehouseModel(null, warehouse)
            };

            if (model.AddressGeoCoordinatesMapping is null)
            {
                model.AddressGeoCoordinatesMapping = new AddressGeoCoordinatesMapping
                {
                    AddressId = warehouse.AddressId,
                    Latitude = 0.0000000m,
                    Longitude = 0.0000000m,
                    ProvinceId = "",
                    DistrictId = "",
                    TownshipId = "",
                    NeighborhoodId = ""
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
        public virtual IActionResult EditWarehouse(CustomWarehouseEditModel model, bool continueEditing)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageShippingSettings))
                return AccessDeniedView();

            //try to get a warehouse with the specified id
            var warehouse = _shippingService.GetWarehouseById(model.WarehouseModel.Id);
            if (warehouse == null)
                return RedirectToAction("Warehouses", "Shipping");

            if (ModelState.IsValid)
            {
                var address = _addressService.GetAddressById(warehouse.AddressId) ??
                    new Address
                    {
                        CreatedOnUtc = DateTime.UtcNow
                    };
                address = model.WarehouseModel.Address.ToEntity(address);
                if (address.Id > 0)
                    _addressService.UpdateAddress(address);
                else
                    _addressService.InsertAddress(address);

                _addressGeoCoordinatesService.InsertAddressGeoCoordinates(model.AddressGeoCoordinatesMapping, address.Id);

                //fill entity from model
                warehouse = model.WarehouseModel.ToEntity(warehouse);

                warehouse.AddressId = address.Id;

                _shippingService.UpdateWarehouse(warehouse);

                //activity log
                _customerActivityService.InsertActivity("EditWarehouse",
                    string.Format(_localizationService.GetResource("ActivityLog.EditWarehouse"), warehouse.Id), warehouse);

                _notificationService.SuccessNotification(_localizationService.GetResource("Admin.Configuration.Shipping.Warehouses.Updated"));

                return continueEditing ? RedirectToAction("EditWarehouse", new { id = warehouse.Id }) : RedirectToAction("Warehouses", "Shipping");
            }

            //prepare model
            var pluginConfigSettings = _settingService.LoadSetting<PluginConfigurationSettings>();
            model.PluginConfigurationSettings = pluginConfigSettings;
            model.WarehouseModel = _shippingModelFactory.PrepareWarehouseModel(model.WarehouseModel, warehouse, true);

            //if we got this far, something failed, redisplay form
            return View(model);
        }

        /// <summary>
        /// Deletes a warehouse and its geo coordinates.
        /// </summary>
        /// <param name="addressId">The id of the address to delete.</param>
        /// <returns>An implementation of <see cref="IActionResult"/>.</returns>
        [HttpPost]
        public virtual IActionResult DeleteWarehouse(int id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageShippingSettings))
                return AccessDeniedView();

            //try to get a warehouse with the specified id
            var warehouse = _shippingService.GetWarehouseById(id);
            if (warehouse == null)
                return RedirectToAction("Warehouses");

            _addressGeoCoordinatesService.RemoveAddressGeoCoordinates(warehouse.AddressId);
            _shippingService.DeleteWarehouse(warehouse);

            //activity log
            _customerActivityService.InsertActivity("DeleteWarehouse",
                string.Format(_localizationService.GetResource("ActivityLog.DeleteWarehouse"), warehouse.Id), warehouse);

            _notificationService.SuccessNotification(_localizationService.GetResource("Admin.Configuration.Shipping.warehouses.Deleted"));

            return RedirectToAction("Warehouses", "Shipping");
        }

        #endregion

        #endregion
    }
}
