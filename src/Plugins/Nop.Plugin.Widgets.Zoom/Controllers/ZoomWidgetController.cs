using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Plugin.Widgets.Zoom.Domains;
using Nop.Plugin.Widgets.Zoom.Domains.Enums;
using Nop.Plugin.Widgets.Zoom.Helpers;
using Nop.Plugin.Widgets.Zoom.Models;
using Nop.Services;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Messages;
using Nop.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;
using System;
using System.Collections.Generic;

namespace Nop.Plugin.Widgets.Zoom.Controllers
{
    /// <summary>
    /// Represents the main controller for Widget.Zoom plugin.
    /// </summary>
    public sealed class ZoomWidgetController : BasePluginController
    {
        #region Fields

        private readonly ILocalizationService _localizationService;
        private readonly INotificationService _notificationService;
        private readonly ISettingService _settingService;

        #endregion

        #region Ctor

        /// <summary>
        /// Initializes a new instance of <see cref="ZoomWidgetController"/>.
        /// </summary>
        /// <param name="localizationService">An implementation of <see cref="ILocalizationService"/>.</param>
        /// <param name="notificationService">An implementation of <see cref="INotificationService"/>.</param>
        /// <param name="settingService">An implementation of <see cref="ISettingService"/>.</param>
        public ZoomWidgetController(
            ILocalizationService localizationService,
            INotificationService notificationService,
            ISettingService settingService)
        {
            _localizationService = localizationService;
            _notificationService = notificationService;
            _settingService = settingService;
        }

        #endregion

        #region Utilities

        private void GetAvailableThumbsLocation(IList<SelectListItem> items)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            var availableLocationsItems = PictureThumbsLocation.Left.ToSelectList(false);
            foreach (var locationItem in availableLocationsItems)
            {
                items.Add(locationItem);
            }
        }

        #endregion

        #region Methods

        #region Configure

        /// <summary>
        /// Gets and prepares the required models and views for Plugin Configuration.
        /// </summary>
        /// <returns>An implementation of <see cref="IActionResult" />.</returns>
        [AuthorizeAdmin]
        [Area(AreaNames.Admin)]
        public IActionResult Configure()
        {
            var zoomSettings = _settingService.LoadSetting<ZoomPluginSettings>();
            var model = zoomSettings.ToModel<ZoomPluginConfigurationModel>();
            GetAvailableThumbsLocation(model.AvailableThumbsLocation);

            return View($"~/{ZoomDefaults.PluginOutputDir}/Views/Configure.cshtml", model);
        }

        /// <summary>
        /// Validates and inserts to the Configuration the given values.
        /// </summary>
        /// <param name="model">An instance of <see cref="ConfigurationModel"/></param>
        /// <returns>An implementation of <see cref="IActionResult" />.</returns>
        [HttpPost]
        [AuthorizeAdmin]
        [Area(AreaNames.Admin)]
        public IActionResult Configure(ZoomPluginConfigurationModel model)
        {
            if (!ModelState.IsValid)
                return Configure();

            var zoomSettings = model.ToEntity<ZoomPluginSettings>();
            _settingService.SaveSetting(zoomSettings);
            _settingService.ClearCache();
            _notificationService.SuccessNotification(_localizationService.GetResource("Admin.Plugins.Saved"));

            return Configure();
        }

        #endregion

        #endregion
    }
}
