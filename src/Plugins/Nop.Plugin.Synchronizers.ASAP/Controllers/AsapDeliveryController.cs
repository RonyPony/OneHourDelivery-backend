using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Core;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Shipping;
using Nop.Data;
using Nop.Plugin.Synchronizers.ASAP.Contracts;
using Nop.Plugin.Synchronizers.ASAP.Domains;
using Nop.Plugin.Synchronizers.ASAP.Models;
using Nop.Services.Common;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Messages;
using Nop.Services.Orders;
using Nop.Services.Security;
using Nop.Services.Shipping;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nop.Web.Areas.Admin.Infrastructure.Mapper.Extensions;

namespace Nop.Plugin.Synchronizers.ASAP.Controllers
{
    /// <summary>
    /// Represents the controller for working with the ASAP Synchronizer.
    /// </summary>
    public sealed class AsapDeliveryController : BasePluginController
    {
        #region Fields

        private readonly IPermissionService _permissionService;
        private readonly IStoreContext _storeContext;
        private readonly ISettingService _settingService;
        private readonly ILocalizationService _localizationService;
        private readonly INotificationService _notificationService;
        private readonly IRepository<Warehouse> _warehouseRepository;
        private readonly IAsapDeliveryService _asapDeliveryService;
        private readonly IOrderService _orderService;
        private readonly IAddressService _addressService;
        private readonly IShipmentService _shipmentService;
        private readonly ILogger _logger;

        #endregion

        #region Ctor

        /// <summary>
        /// Create an instance of <see cref="AsapDeliveryController"/>
        /// </summary>
        /// <param name="permissionService">Represents an implementation of <see cref="IPermissionService"/>.</param>
        /// <param name="storeContext">Represents an implementation of <see cref="IStoreContext"/>.</param>
        /// <param name="settingService">Represents an implementation of <see cref="ISettingService"/>.</param>
        /// <param name="localizationService">Represents an implementation of <see cref="ILocalizationService"/>.</param>
        /// <param name="notificationService">Represents an implementation of <see cref="INotificationService"/>.</param>
        /// <param name="warehouseRepository">Represents an implementation of <see cref="IRepository<Warehouse>"/>.</param>
        /// <param name="asapDeliveryService">Represents an implementation of <see cref="IAsapDeliveryService"/>.</param>
        /// <param name="orderService">Represents an implementation of <see cref="IOrderService"/>.</param>
        /// <param name="addressService">Represents an implementation of <see cref="IAddressService"/>.</param>
        /// <param name="shipmentService">Represents an implementation of <see cref="IShipmentService"/>.</param>
        /// <param name="logger">Represents an implementation of <see cref="ILogger"/>.</param>
        public AsapDeliveryController(
            IPermissionService permissionService,
            IStoreContext storeContext,
            ISettingService settingService,
            ILocalizationService localizationService,
            INotificationService notificationService,
            IRepository<Warehouse> warehouseRepository,
            IAsapDeliveryService asapDeliveryService,
            IOrderService orderService,
            IAddressService addressService,
            IShipmentService shipmentService,
            ILogger logger)
        {
            _permissionService = permissionService;
            _storeContext = storeContext;
            _settingService = settingService;
            _localizationService = localizationService;
            _notificationService = notificationService;
            _warehouseRepository = warehouseRepository;
            _asapDeliveryService = asapDeliveryService;
            _orderService = orderService;
            _addressService = addressService;
            _shipmentService = shipmentService;
            _logger = logger;
        }

        #endregion

        #region Utilities

        private List<SelectListItem> GetSelectListItemFromWarehouses(IList<Warehouse> warehouses)
        {
            var selectListItem = new List<SelectListItem>
            {
                new SelectListItem { Text = "Select warehouse", Value = "0" }
            };

            foreach (Warehouse warehouse in warehouses)
            {
                selectListItem.Add(new SelectListItem { Text = warehouse.Name, Value = warehouse.Id.ToString() });
            }

            return selectListItem;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Prepares a <see cref="DeliveryAsapConfigurationModel"/> object to send it to Configure.cshtml page and configure it for the plugin.
        /// </summary>
        /// <returns>An <see cref="IActionResult"/> with the configuration's view.</returns>
        [AuthorizeAdmin]
        [Area(AreaNames.Admin)]
        public IActionResult Configure()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManagePaymentMethods))
            {
                return AccessDeniedView();
            }

            int storeScope = _storeContext.ActiveStoreScopeConfiguration;
            AsapDeliveryConfigurationSettings settings = _settingService.LoadSetting<AsapDeliveryConfigurationSettings>(storeScope);

            var model = settings.ToModel<DeliveryAsapConfigurationModel>();

            model.Warehouses = GetSelectListItemFromWarehouses(_warehouseRepository.Table.ToList());

            return View("~/Plugins/Synchronizers.ASAP/Views/Configure.cshtml", model);
        }

        /// <summary>
        /// Receives and sets a configuration desired for this plugin.
        /// </summary>
        /// <param name="model">Represents an instance <see cref="DeliveryAsapConfigurationModel"/></param>
        /// <returns>An <see cref="IActionResult"/> with the configuration's view.</returns>
        [HttpPost]
        [AuthorizeAdmin]
        [Area(AreaNames.Admin)]
        public IActionResult Configure(DeliveryAsapConfigurationModel model)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManagePaymentMethods))
            {
                return AccessDeniedView();
            }

            if (!ModelState.IsValid)
            {
                return Configure();
            }
            
            var settings = model.ToEntity<AsapDeliveryConfigurationSettings>();

            _settingService.SaveSetting(settings);

            _settingService.ClearCache();

            if (IsValidModel(settings))
            {
                _notificationService.SuccessNotification(_localizationService.GetResource("Admin.Plugins.Saved"));
            }
            else
            {
                _notificationService.ErrorNotification(_localizationService.GetResource($"{AsapDeliveryDefaults.AsapConfigurationResources}.Fields.Required"));
            }

            return Configure();
        }

        private bool IsValidModel(AsapDeliveryConfigurationSettings settings)
        {
            //whether plugin is configured
            if (string.IsNullOrWhiteSpace(settings.ApiKey) ||
                string.IsNullOrWhiteSpace(settings.ServiceUrl) ||
                string.IsNullOrWhiteSpace(settings.SharedSecret) ||
                string.IsNullOrWhiteSpace(settings.UserToken) ||
                string.IsNullOrWhiteSpace(settings.Email) ||
                settings.DefaultWarehouseId < 0)
                return false;

            return true;
        }

        /// <summary>
        /// Receives and sets a shipment desired for DeliveryASAP.
        /// </summary>
        /// <param name="orderId">An implementation of <see cref="Order"/></param>
        /// <param name="shipmentId">An implementation of <see cref="Shipment"/></param>
        /// <returns>An implementation of <see cref="GetShipmentData"/></returns>
        [HttpGet]
        [AuthorizeAdmin]
        [Area(AreaNames.Admin)]
        public async Task<IActionResult> GetShipmentData(int orderId, int shipmentId)
        {
            Order order = _orderService.GetOrderById(orderId);
            Address shippingAddress = _addressService.GetAddressById(order.ShippingAddressId.Value);

            try
            {
                string createOrder = await _asapDeliveryService.CreateOrder(shippingAddress);

                if (!string.IsNullOrEmpty(createOrder) && !string.IsNullOrWhiteSpace(createOrder))
                {
                    Shipment shipment = _shipmentService.GetShipmentById(shipmentId);

                    shipment.TrackingNumber = createOrder;

                    _shipmentService.UpdateShipment(shipment);
                    _notificationService.SuccessNotification(_localizationService.GetResource($"{AsapDeliveryDefaults.AsapConfigurationResources}.Fields.RequestSuccess"));
                }
                else
                {
                    throw new Exception(_localizationService.GetResource($"{AsapDeliveryDefaults.AsapConfigurationResources}.Fields.ThrowException"));
                }
            }
            catch (Exception e)
            {
                _logger.Error($"An error has occured while processing shipment by Delivery ASAP. Error message: {e.Message}", e);
                _notificationService.ErrorNotification(e.Message);
            }

            return RedirectToAction("ShipmentDetails", "Order", new { area = AreaNames.Admin, id = shipmentId });
        }

        #endregion
    }
}