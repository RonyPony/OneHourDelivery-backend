using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Plugin.Synchronizers.SAPCustomers.Domains;
using Nop.Plugin.Synchronizers.SAPCustomers.Models;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Messages;
using Nop.Services.Security;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;

namespace Nop.Plugin.Synchronizers.SAPCustomers.Controllers
{
    /// <summary>
    /// Represents the controller for working with the SAP Customer Synchronizer.
    /// </summary>
    public sealed class SapCustomerSynchronizerController : BasePluginController
    {
        private readonly IPermissionService _permissionService;
        private readonly IStoreContext _storeContext;
        private readonly ISettingService _settingService;
        private readonly INotificationService _notificationService;
        private readonly ILocalizationService _localizationService;

        /// <summary>
        /// Initializes a new instance of SAPCustomerSynchronizerController class with the value indicted as parameters.
        /// </summary>
        /// <param name="permissionService">Represents an implementation of <see cref="IPermissionService"/>.</param>
        /// <param name="storeContext">Represents an implementation of <see cref="IStoreContext"/>.</param>
        /// <param name="settingService">Represents an implementation of <see cref="ISettingService"/>.</param>
        /// <param name="notificationService">Represents an implementation of <see cref="INotificationService"/>.</param>
        /// <param name="localizationService">Represents an implementation of <see cref="ILocalizationService"/>.</param>
        public SapCustomerSynchronizerController(
            IPermissionService permissionService,
            IStoreContext storeContext,
            ISettingService settingService,
            INotificationService notificationService,
            ILocalizationService localizationService)
        {
            _permissionService = permissionService;
            _storeContext = storeContext;
            _settingService = settingService;
            _notificationService = notificationService;
            _localizationService = localizationService;
        }

        /// <summary>
        /// Prepares a <see cref="SapCustomersSynchronizerConfigurationModel"/> object to send it to Configure.cshtml page and configure it for the plugin.
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

            var storeScope = _storeContext.ActiveStoreScopeConfiguration;
            var settings = _settingService.LoadSetting<SapCustomersSynchronizerConfigurationSettings>(storeScope);

            var model = new SapCustomersSynchronizerConfigurationModel
            {
                AuthenticationHeaderScheme = settings.AuthenticationHeaderScheme,
                AuthenticationHeaderParameter = settings.AuthenticationHeaderParameter,
                SapCustomerUrl = settings.SapCustomerUrl,
                SapCustomerRolesUrl = settings.SapCustomerRolesUrl,
                SapCustomerAddressUrl = settings.SapCustomerAddressUrl
            };

            return View("~/Plugins/Synchronizers.SAPCustomers/Views/Configure.cshtml", model);
        }

        /// <summary>
        /// Receives and sets a configuration desired for this plugin.
        /// </summary>
        /// <param name="model">Represents an instance <see cref="SapCustomersSynchronizerConfigurationModel"/></param>
        /// <returns>An <see cref="IActionResult"/> with the configuration's view.</returns>
        [HttpPost]
        [AuthorizeAdmin]
        [Area(AreaNames.Admin)]
        public IActionResult Configure(SapCustomersSynchronizerConfigurationModel model)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManagePaymentMethods))
            {
                return AccessDeniedView();
            }

            if (!ModelState.IsValid)
            {
                return Configure();
            }

            var storeScope = _storeContext.ActiveStoreScopeConfiguration;
            var settings = _settingService.LoadSetting<SapCustomersSynchronizerConfigurationSettings>(storeScope);

            settings.AuthenticationHeaderParameter = model.AuthenticationHeaderParameter;
            settings.AuthenticationHeaderScheme = model.AuthenticationHeaderScheme;
            settings.SapCustomerUrl = model.SapCustomerUrl;
            settings.SapCustomerRolesUrl = model.SapCustomerRolesUrl;
            settings.SapCustomerAddressUrl = model.SapCustomerAddressUrl;

            _settingService.SaveSettingOverridablePerStore(settings, setting => setting.AuthenticationHeaderParameter, true, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(settings, setting => setting.AuthenticationHeaderScheme, true, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(settings, setting => setting.SapCustomerUrl, true, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(settings, setting => setting.SapCustomerRolesUrl, true, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(settings, setting => setting.SapCustomerAddressUrl, true, storeScope, false);
            _settingService.ClearCache();

            _notificationService.SuccessNotification(_localizationService.GetResource("Admin.Plugins.Saved"));

            return Configure();
        }
    }
}
