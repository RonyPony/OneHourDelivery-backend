using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Synchronizers.SAPOrders.Models;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Messages;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;

namespace Nop.Plugin.Synchronizers.SAPOrders.Controllers
{
    /// <summary>
    /// Represents controller class for SapOrders controller
    /// </summary>
    public class SapOrdersSyncController : BasePluginController
    {
        #region Fields

        private readonly ISettingService _settingService;
        private readonly INotificationService _notificationService;
        private readonly ILocalizationService _localizationService;

        #endregion

        #region Ctor

        /// <summary>
        /// Represents the constructor method for this class
        /// </summary>
        /// <param name="settingService">Implementation of <see cref="ISettingService"/></param>
        /// <param name="notificationService">Implementation of <see cref="INotificationService"/></param>
        /// <param name="localizationService">Implementation of <see cref="ILocalizationService"/></param>
        public SapOrdersSyncController(ISettingService settingService,
            INotificationService notificationService, ILocalizationService localizationService)
        {
            _settingService = settingService;
            _notificationService = notificationService;
            _localizationService = localizationService;
        }

        #endregion

        #region Utilities

        private void PrepareModel(ConfigurationModel model)
        {
            var ordersSyncSettings = _settingService.LoadSetting<SapOrdersSyncSettings>();

            //whether plugin is configured
            if (string.IsNullOrWhiteSpace(ordersSyncSettings.ApiGetUrl) ||
                string.IsNullOrWhiteSpace(ordersSyncSettings.ApiPostUrl) ||
                string.IsNullOrWhiteSpace(ordersSyncSettings.AuthenticationScheme) ||
                string.IsNullOrWhiteSpace(ordersSyncSettings.ApiToken))
                return;

            //prepare common properties
            model.ApiPostUrl = ordersSyncSettings.ApiPostUrl;
            model.ApiGetUrl = ordersSyncSettings.ApiGetUrl;
            model.ApiAuthenticationScheme = ordersSyncSettings.AuthenticationScheme;
            model.ApiToken = ordersSyncSettings.ApiToken;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Prepares an instance of <see cref="ConfigurationModel"/> and returns configuration view for this plug-in
        /// </summary>
        /// <returns>The view to configure this plug-in</returns>
        [AuthorizeAdmin]
        [Area(AreaNames.Admin)]
        public IActionResult Configure()
        {
            var model = new ConfigurationModel();
            PrepareModel(model);

            return View("~/Plugins/Synchronizers.SAPOrders/Views/Configure.cshtml", model);
        }

        /// <summary>
        /// Saves configuration for this plug-in
        /// </summary>
        /// <param name="model">Represents an instance of <see cref="ConfigurationModel"/></param>
        /// <returns>The view to configure this plug-in, with the saved settings</returns>
        [AuthorizeAdmin]
        [Area(AreaNames.Admin)]
        [HttpPost, ActionName("Configure")]
        [FormValueRequired("save")]
        public IActionResult Configure(ConfigurationModel model)
        {
            if (!ModelState.IsValid)
                return Configure();

            var ordersSyncSettings = _settingService.LoadSetting<SapOrdersSyncSettings>();

            //set API key
            ordersSyncSettings.ApiPostUrl = model.ApiPostUrl;
            ordersSyncSettings.ApiGetUrl = model.ApiGetUrl;
            ordersSyncSettings.AuthenticationScheme = model.ApiAuthenticationScheme;
            ordersSyncSettings.ApiToken = model.ApiToken;

            _settingService.SaveSetting(ordersSyncSettings);
            _settingService.ClearCache();

            _notificationService.SuccessNotification(_localizationService.GetResource("Admin.Plugins.Saved"));

            return Configure();
        }

        #endregion
    }
}