using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Widgets.GoogleMapsIntegrationMultientrega.Helpers;
using Nop.Plugin.Widgets.GoogleMapsIntegrationMultientrega.Models;
using Nop.Plugin.Widgets.GoogleMapsIntegrationMultientrega.Services;
using Nop.Services.Logging;
using Nop.Services.Messages;
using Nop.Web.Areas.Admin.Models.Orders;
using Nop.Web.Framework.Components;
using System;

namespace Nop.Plugin.Widgets.GoogleMapsIntegrationMultientrega.Component
{
    /// <summary>
    /// Represents a view component for order address edit geo-coordinates and address multientrega's structure.
    /// </summary>
    [ViewComponent(Name = "UpdateOrderAddressLatLng")]
    public sealed class UpdateOrderAddressLatLngViewComponent : NopViewComponent
    {
        private readonly IAddressGeoCoordinatesService _addressGeoCoordinatesService;
        private readonly ILogger _logger;
        private readonly IMultientregaAddressService _multientregaAddressService;
        private readonly INotificationService _notificationService;

        /// <summary>
        /// Initializes a new instance of <see cref="UpdateOrderAddressLatLngViewComponent"/>.
        /// </summary>
        /// <param name="addressGeoCoordinatesService">An implementation of <see cref="IAddressGeoCoordinatesService"/>.</param>
        /// <param name="logger">An implementation of <see cref="ILogger"/>.</param>
        /// <param name="multientregaAddressService">An implementation of <see cref="IMultientregaAddressService"/>.</param>
        /// <param name="notificationService">An implementation of <see cref="INotificationService"/>.</param>
        public UpdateOrderAddressLatLngViewComponent(
            IAddressGeoCoordinatesService addressGeoCoordinatesService,
            ILogger logger,
            IMultientregaAddressService multientregaAddressService,
            INotificationService notificationService)
        {
            _addressGeoCoordinatesService = addressGeoCoordinatesService;
            _logger = logger;
            _multientregaAddressService = multientregaAddressService;
            _notificationService = notificationService;
        }

        /// <summary>
        /// Invokes the view component.
        /// </summary>
        /// <param name="widgetZone">Name of the widget zone invoking the view component.</param>
        /// <param name="additionalData">Data send through the widget zone.</param>
        /// <returns>An implementation of <see cref="IViewComponentResult"/>.</returns>
        public IViewComponentResult Invoke(string widgetZone, OrderAddressModel additionalData)
        {
            try
            {
                var model = new AddressCoordinatesMultientregaStructureModel
                {
                    MultientregaAddressStructure = _multientregaAddressService.GetMultientregaAddressStructureByAddressId(additionalData.Address.Id),
                    AddressGeoCoordinates = _addressGeoCoordinatesService.GetAddressGeoCoordinates(additionalData.Address.Id)
                };

                if (model.AddressGeoCoordinates is null || model.MultientregaAddressStructure is null) return Content(string.Empty);

                return View($"~/{Defaults.PluginOutputDir}/Views/UpdateOrderAddressLatLng.cshtml", model);
            }
            catch (Exception e)
            {
                _logger.Error($"An error occurred loading address additional data: {e.Message}", e);
                _notificationService.ErrorNotification($"An error occurred loading address additional data: {e.Message}");
                return Content(string.Empty);
            }
        }
    }
}
