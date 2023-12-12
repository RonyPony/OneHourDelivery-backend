using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Helpers;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Services;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Messages;
using Nop.Services.Security;
using Nop.Web.Areas.Admin.Controllers;
using Nop.Web.Areas.Admin.Factories;
using Nop.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using Nop.Web.Areas.Admin.Models.Shipping;
using System;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Areas.Admin.Controllers
{
    /// <summary>
    /// Represents a shipping controller for the app delivery backend.
    /// </summary>
    public sealed class DeliveryAppShippingController : BaseAdminController
    {
        #region Fields

        private readonly IWorkContext _workContext;
        private readonly IPermissionService _permissionService;
        private readonly IVendorDeliveryAppService _vendorDeliveryAppService;
        private readonly INotificationService _notificationService;
        private readonly ILocalizationService _localizationService;
        private readonly ILogger _logger;
        private readonly IShippingModelFactory _shippingModelFactory;

        #endregion

        #region Ctor

        /// <summary>
        /// Initializes a new instance of <see cref="DeliveryAppShippingController"/>.
        /// </summary>
        /// <param name="localizationService">An implementation of <see cref="ILocalizationService"/>.</param>
        /// <param name="notificationService">An implementation of <see cref="INotificationService"/>.</param>
        /// <param name="permissionService">An implementation of <see cref="IPermissionService"/>.</param>
        /// <param name="workContext">An implementation of <see cref="IWorkContext"/>.</param>
        /// <param name="vendorDeliveryAppService">An implementation of <see cref="IVendorDeliveryAppService"/>.</param>
        /// <param name="logger">An implementation of <see cref="ILogger"/>.</param>
        public DeliveryAppShippingController(
            ILocalizationService localizationService,
            INotificationService notificationService,
            IPermissionService permissionService,
            IWorkContext workContext,
            IVendorDeliveryAppService vendorDeliveryAppService,
            IShippingModelFactory shippingModelFactory,
            ILogger logger)
        {
            _workContext = workContext;
            _permissionService = permissionService;
            _vendorDeliveryAppService = vendorDeliveryAppService;
            _notificationService = notificationService;
            _localizationService = localizationService;
            _logger = logger;
            _shippingModelFactory = shippingModelFactory;

        }

        #endregion

        #region Methods

        #region Warehouses

        /// <summary>
        /// Retrieves a list of warehouses associated to a vendor.
        /// </summary>
        /// <param name="searchModel">An instance of <see cref="WarehouseSearchModel"/>.</param>
        /// <returns>An implementation of <see cref="IActionResult"/>.</returns>
        [HttpPost]
        public IActionResult Warehouses(WarehouseSearchModel searchModel)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageShippingSettings))
                return AccessDeniedDataTablesJson();

            WarehouseListModel model = _workContext.CurrentCustomer.VendorId.Equals(0)
                                     ? _shippingModelFactory.PrepareWarehouseListModel(searchModel)
                                     : _vendorDeliveryAppService.PrepareVendorWarehouseListModel(_workContext.CurrentCustomer.VendorId, searchModel);

            return Json(model);
        }

        /// <summary>
        /// Allows a vendor to associate itself with one of the warehouses he created.
        /// </summary>
        /// <param name="model">An instance of <see cref="VendorWarehouseMappingModel"/>.</param>
        /// <returns>An implementation of <see cref="IActionResult"/>.</returns>
        [HttpPost]
        public IActionResult SelectWarehouse(VendorWarehouseMappingModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var vendorWarehouse = model.ToEntity<VendorWarehouseMapping>();
                    _vendorDeliveryAppService.InsertOrUpdateVendorWarehouseMapping(vendorWarehouse);
                    _notificationService.SuccessNotification(_localizationService.GetResource($"{Defaults.LocaleResourcesPrefix}.VendorWarehouse.Warehouse.SelectedSuccessfully"));
                }
            }
            catch (Exception e)
            {
                _logger.Error($"An error occurred selecting warehouse: {e.Message}", e);
                _notificationService.ErrorNotification($"An error occurred selecting warehouse: {e.Message}");
            }

            return RedirectToAction("Warehouses", "Shipping");
        }

        /// <summary>
        /// Allows the admin to assing a warehosue to a vendor.
        /// </summary>
        /// <param name="model">An instance of <see cref="VendorWarehouseMappingModel"/>.</param>
        /// <returns>An implementation of <see cref="IActionResult"/>.</returns>
        [HttpPost]
        public IActionResult AssignWarehouseToVendor(VendorWarehouseMappingModel model)
        {
            try
            {
                var vendorWarehouse = model.ToEntity<VendorWarehouseMapping>();
                _vendorDeliveryAppService.InsertOrUpdateVendorWarehouseMapping(vendorWarehouse);

                return Json(new
                {
                    Success = true,
                    Message = _localizationService.GetResource($"{Defaults.LocaleResourcesPrefix}.VendorWarehouse.Warehouse.SelectedSuccessfully")
                });
            }
            catch (Exception e)
            {
                _logger.Error($"An error occurred selecting warehouse: {e.Message}", e);

                return Json(new
                {
                    Success = false,
                    Message = $"An error occurred selecting warehouse: {e.Message}"
                });
            }
        }

        #endregion

        #endregion
    }
}
