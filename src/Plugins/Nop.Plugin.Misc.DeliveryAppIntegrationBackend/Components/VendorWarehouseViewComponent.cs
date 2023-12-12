using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Factories;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Helpers;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Services;
using Nop.Services.Logging;
using Nop.Services.Messages;
using Nop.Web.Areas.Admin.Models.Vendors;
using Nop.Web.Framework.Components;
using Nop.Web.Framework.Infrastructure;
using System;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Components
{
    /// <summary>
    /// Represents a view component for vendor warehouse association.
    /// </summary>
    [ViewComponent(Name = "VendorWarehouse")]
    public class VendorWarehouseViewComponent : NopViewComponent
    {
        #region Fields

        private readonly ILogger _logger;
        private readonly INotificationService _notificationService;
        private readonly IVendorDeliveryAppService _vendorDeliveryAppService;
        private readonly IVendorWarehouseMappingModelFactory _vendorWarehouseMappingModelFactory;
        private readonly IWorkContext _workContext;

        #endregion

        #region Ctor

        /// <summary>
        /// Initializes a new instance of <see cref="VendorWarehouseViewComponent"/>.
        /// </summary>
        /// <param name="logger">An implementation of <see cref="ILogger"/>.</param>
        /// <param name="notificationService">An implementation of <see cref="INotificationService"/>.</param>
        /// <param name="vendorDeliveryAppService">An implementation of <see cref="IVendorDeliveryAppService"/>.</param>
        /// <param name="vendorWarehouseMappingModelFactory">An implementation of <see cref="IVendorWarehouseMappingModelFactory"/>.</param>
        /// <param name="workContext">An implementation of <see cref="IWorkContext"/>.</param>
        public VendorWarehouseViewComponent(
            ILogger logger,
            INotificationService notificationService,
            IVendorDeliveryAppService vendorDeliveryAppService,
            IVendorWarehouseMappingModelFactory vendorWarehouseMappingModelFactory,
            IWorkContext workContext)
        {
            _logger = logger;
            _notificationService = notificationService;
            _vendorDeliveryAppService = vendorDeliveryAppService;
            _vendorWarehouseMappingModelFactory = vendorWarehouseMappingModelFactory;
            _workContext = workContext;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Invokes the view component.
        /// </summary>
        /// <param name="widgetZone">Indicates the widget zone invoking the view component.</param>
        /// <param name="additionalData">Indicates the data send to the view.</param>
        /// <returns>An implementation of <see cref="IViewComponentResult"/>.</returns>
        public IViewComponentResult Invoke(string widgetZone, object additionalData)
        {
            try
            {
                if (widgetZone.Equals(AdminWidgetZones.WarehouseListButtons) && _vendorDeliveryAppService.IsDeliveryAppVendor(_workContext.CurrentCustomer))
                {
                    VendorWarehouseMappingModel model = _vendorWarehouseMappingModelFactory.PrepareVendorWarehouseMappingModel(_workContext.CurrentCustomer.VendorId);
                    return View($"/{Defaults.OutputDir}/Views/VendorWarehouse.Vendor.cshtml", model);
                }

                if (widgetZone.Equals(AdminWidgetZones.VendorDetailsBlock) && additionalData is VendorModel vendorModel)
                {
                    VendorWarehouseMappingModel model = _vendorWarehouseMappingModelFactory.PrepareVendorWarehouseMappingModel(vendorModel.Id);
                    return View($"/{Defaults.OutputDir}/Views/VendorWarehouse.Admin.cshtml", model);
                }
            }
            catch (Exception e)
            {
                _logger.Error($"An error occurred while processing vendor warehouse. {e.Message}", e);
                _notificationService.WarningNotification(e.Message);
            }

            return Content(string.Empty);
        }

        #endregion
    }
}
