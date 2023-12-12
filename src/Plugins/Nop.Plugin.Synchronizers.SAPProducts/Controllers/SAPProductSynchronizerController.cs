using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Synchronizers.SAPProducts.Domains;
using Nop.Plugin.Synchronizers.SAPProducts.Models;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Messages;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;

namespace Nop.Plugin.Synchronizers.SAPProducts.Controllers
{
    /// <summary>
    /// Represents a controller for SAP product's synchronizer.
    /// </summary>
    [AutoValidateAntiforgeryToken]
    public class SAPProductSynchronizerController : BasePluginController
    {
        #region Fields

        private readonly ILocalizationService _localizationService;
        private readonly INotificationService _notificationService;
        private readonly ISettingService _settingService;

        #endregion

        #region Ctor

        /// <summary>
        /// Initializes a new instance of <see cref="SAPProductSynchronizerController"/>.
        /// </summary>
        /// <param name="localizationService">An implementation of <see cref="ILocalizationService"/>.</param>
        /// <param name="notificationService">An implementation of <see cref="INotificationService"/>.</param>
        /// <param name="settingService">An implementation of <see cref="ISettingService"/>.</param>
        public SAPProductSynchronizerController(ILocalizationService localizationService,
            INotificationService notificationService,
            ISettingService settingService)
        {
            _localizationService = localizationService;
            _notificationService = notificationService;
            _settingService = settingService;
        }

        #endregion

        #region Utilities

        /// <summary>
        /// Prepares the configuration model of the SAP products synchronizer.
        /// </summary>
        /// <param name="model">The configuration model to be prepared.</param>
        private void PrepareModel(ConfigurationModel model)
        {
            var sapProductSynchronizerSettings = _settingService.LoadSetting<ProductSyncConfigurationSettings>();

            //whether plugin is configured
            if (string.IsNullOrEmpty(sapProductSynchronizerSettings.SapProductUrl)
                || string.IsNullOrEmpty(sapProductSynchronizerSettings.AuthenticationHeaderParameter)
                || string.IsNullOrEmpty(sapProductSynchronizerSettings.AuthenticationHeaderScheme))
                return;

            if (sapProductSynchronizerSettings.SyncCategories && string.IsNullOrWhiteSpace(sapProductSynchronizerSettings.SapCategoryUrl))
                return;

            if (sapProductSynchronizerSettings.SyncManufacturers && string.IsNullOrWhiteSpace(sapProductSynchronizerSettings.SapManufacturerUrl))
                return;

            //prepare common properties
            model.SapProductUrl = sapProductSynchronizerSettings.SapProductUrl;
            model.AuthenticationHeaderScheme = sapProductSynchronizerSettings.AuthenticationHeaderScheme;
            model.AuthenticationHeaderParameter = sapProductSynchronizerSettings.AuthenticationHeaderParameter;
            model.SyncCategories = sapProductSynchronizerSettings.SyncCategories;
            model.SapCategoryUrl = sapProductSynchronizerSettings.SapCategoryUrl;
            model.SyncManufacturers = sapProductSynchronizerSettings.SyncManufacturers;
            model.SapManufacturerUrl = sapProductSynchronizerSettings.SapManufacturerUrl;
            model.AssignItemCodeFieldAsSku = sapProductSynchronizerSettings.AssignItemCodeFieldAsSku;
            model.UpdateProductInformation = sapProductSynchronizerSettings.UpdateProductInformation;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Prepares a <see cref="ConfigurationModel"/> object to send it to Configure.cshtml page and configure it for the plugin.
        /// </summary>
        /// <returns>An <see cref="IActionResult"/> with the configuration's view.</returns>
        [AuthorizeAdmin]
        [Area(AreaNames.Admin)]
        public IActionResult Configure()
        {
            var model = new ConfigurationModel();
            PrepareModel(model);

            return View("~/Plugins/Synchronizers.SAPProducts/Views/Configure.cshtml", model);
        }

        /// <summary>
        /// Receives and sets the configuration for this plugin.
        /// </summary>
        /// <param name="model">An instance <see cref="ConfigurationModel"/>.</param>
        /// <returns>An <see cref="IActionResult"/> with the configuration's view.</returns>
        [HttpPost]
        [AuthorizeAdmin]
        [Area(AreaNames.Admin)]
        public IActionResult Configure(ConfigurationModel model)
        {
            if (!ModelState.IsValid)
                return Configure();

            var productSyncSettings = _settingService.LoadSetting<ProductSyncConfigurationSettings>();

            productSyncSettings.SapProductUrl = model.SapProductUrl;
            productSyncSettings.AuthenticationHeaderScheme = model.AuthenticationHeaderScheme;
            productSyncSettings.AuthenticationHeaderParameter = model.AuthenticationHeaderParameter;
            productSyncSettings.SyncCategories = model.SyncCategories;
            productSyncSettings.SapCategoryUrl = model.SapCategoryUrl;
            productSyncSettings.SyncManufacturers = model.SyncManufacturers;
            productSyncSettings.SapManufacturerUrl = model.SapManufacturerUrl;
            productSyncSettings.AssignItemCodeFieldAsSku = model.AssignItemCodeFieldAsSku;
            productSyncSettings.UpdateProductInformation = model.UpdateProductInformation;

            _settingService.SaveSetting(productSyncSettings);
            _settingService.ClearCache();
            _notificationService.SuccessNotification(_localizationService.GetResource("Admin.Plugins.Saved"));

            return Configure();
        }

        #endregion
    }
}