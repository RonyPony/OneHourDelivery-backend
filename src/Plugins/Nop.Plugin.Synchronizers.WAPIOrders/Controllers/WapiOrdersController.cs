using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Synchronizers.WAPIOrders.Helpers;
using Nop.Plugin.Synchronizers.WAPIOrders.Models;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Messages;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;

namespace Nop.Plugin.Synchronizers.WAPIOrders.Controllers
{
    /// <summary>
    /// Represents a controller for WAPI orders synchronizer.
    /// </summary>
    [AutoValidateAntiforgeryToken]
    public partial class WapiOrdersController : BasePluginController
    {
        #region Fields

        private readonly ISettingService _settingService;
        private readonly INotificationService _notificationService;
        private readonly ILocalizationService _localizationService;

        #endregion

        #region Ctor

        /// <summary>
        /// Initializes a new instance of <see cref="WapiOrdersController"/>.
        /// </summary>
        /// <param name="settingService">An implementation of <see cref="ISettingService"/>.</param>
        /// <param name="notificationService">An implementation of <see cref="INotificationService"/>.</param>
        /// <param name="localizationService">An implementation of <see cref="ILocalizationService"/>.</param>
        public WapiOrdersController(
            ISettingService settingService,
            INotificationService notificationService,
            ILocalizationService localizationService)
        {
            _settingService = settingService;
            _notificationService = notificationService;
            _localizationService = localizationService;
        }

        #endregion

        #region Utilities

        private void PrepareModel(ConfigurationModel model)
        {
            var pluginConfigSettings = _settingService.LoadSetting<ConfigurationSettings>();

            //whether plugin is configured
            if (string.IsNullOrWhiteSpace(pluginConfigSettings.AuthKeyName)
                || string.IsNullOrWhiteSpace(pluginConfigSettings.AuthKeyValue)
                || string.IsNullOrWhiteSpace(pluginConfigSettings.ApiPostUrl)
                || string.IsNullOrWhiteSpace(pluginConfigSettings.DefaultStorePickupCode))
                return;

            //prepare common properties
            model.AuthKeyName = pluginConfigSettings.AuthKeyName;
            model.AuthKeyValue = pluginConfigSettings.AuthKeyValue;
            model.ApiPostUrl = pluginConfigSettings.ApiPostUrl;
            model.DefaultStorePickupCode = pluginConfigSettings.DefaultStorePickupCode;
        }

        #endregion

        #region Methods

        #region Configure

        /// <summary>
        /// Gets and sets the configuration for this plugin.
        /// </summary>
        /// <returns>An implementation of <see cref="IActionResult"/> with the configuration's view.</returns>
        [AuthorizeAdmin]
        [Area(AreaNames.Admin)]
        public IActionResult Configure()
        {
            var model = new ConfigurationModel();
            PrepareModel(model);

            return View($"~/{Defaults.PluginOutputDir}/Views/Configure.cshtml", model);
        }

        /// <summary>
        /// Receives and sets the configuration for this plugin.
        /// </summary>
        /// <param name="model">An instance <see cref="ConfigurationModel"/>.</param>
        /// <returns>An implementation of <see cref="IActionResult"/> with the configuration's view.</returns>
        [HttpPost]
        [AuthorizeAdmin]
        [Area(AreaNames.Admin)]
        public IActionResult Configure(ConfigurationModel model)
        {
            if (!ModelState.IsValid)
                return Configure();

            var pluginConfigSettings = _settingService.LoadSetting<ConfigurationSettings>();

            pluginConfigSettings.AuthKeyName = model.AuthKeyName;
            pluginConfigSettings.AuthKeyValue = model.AuthKeyValue;
            pluginConfigSettings.ApiPostUrl = model.ApiPostUrl;
            pluginConfigSettings.DefaultStorePickupCode = model.DefaultStorePickupCode;

            _settingService.SaveSetting(pluginConfigSettings);
            _settingService.ClearCache();
            _notificationService.SuccessNotification(_localizationService.GetResource("Admin.Plugins.Saved"));

            return Configure();
        }

        #endregion

        #endregion
    }
}
