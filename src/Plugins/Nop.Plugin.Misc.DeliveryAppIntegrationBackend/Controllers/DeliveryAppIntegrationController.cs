using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domain;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Messages;
using Nop.Services.Security;
using Nop.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class DeliveryAppIntegrationController : BasePluginController
    {
        #region Fields

        private readonly DeliveryAppBackendConfigurationSettings _deliveryAppBackendConfigurationSettings;
        private readonly ILocalizationService _localizationService;
        private readonly INotificationService _notificationService;
        private readonly IPermissionService _permissionService;
        private readonly ISettingService _settingService;
        private readonly IStoreContext _storeContext;

        #endregion

        #region Ctor

        /// <summary>
        /// Initializes an instance of <see cref="DeliveryAppIntegrationController"/>
        /// </summary>
        /// <param name="deliveryAppBackendConfigurationSettings">An implementation of <see cref="DeliveryAppBackendConfigurationSettings"/>.</param>
        /// <param name="localizationService">An implementation of <see cref="ILocalizationService"/>.</param>
        /// <param name="notificationService">An implementation of <see cref="INotificationService"/>.</param>
        /// <param name="permissionService">An implementation of <see cref="IPermissionService"/>.</param>
        /// <param name="settingService">An implementation of <see cref="ISettingService"/>.</param>
        /// <param name="storeContext">An implementation of <see cref="IStoreContext"/>.</param>
        public DeliveryAppIntegrationController(
            DeliveryAppBackendConfigurationSettings deliveryAppBackendConfigurationSettings,
            ILocalizationService localizationService,
            INotificationService notificationService,
            IPermissionService permissionService,
            ISettingService settingService,
            IStoreContext storeContext)
        {
            _deliveryAppBackendConfigurationSettings = deliveryAppBackendConfigurationSettings;
            _localizationService = localizationService;
            _notificationService = notificationService;
            _permissionService = permissionService;
            _settingService = settingService;
            _storeContext = storeContext;
        }

        #endregion

        /// <summary>
        /// Action used to GET configuration page.
        /// </summary>
        [AuthorizeAdmin]
        [Area(AreaNames.Admin)]
        public IActionResult Configure()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManagePaymentMethods))
                return AccessDeniedView();

            //Configurable fields in the plugin configuration.
            var model = _deliveryAppBackendConfigurationSettings.ToModel<DeliveryAppBackendConfigurationModel>();

            return View("~/Plugins/Misc.DeliveryAppIntegrationBackend/Views/Configure.cshtml", model);
        }

        /// <summary>
        /// Action used to POST plugin settings.
        /// </summary>
        /// <param name="model">Instance of <see cref="DeliveryAppBackendConfigurationModel"/></param>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeAdmin]
        [Area(AreaNames.Admin)]
        public IActionResult Configure(DeliveryAppBackendConfigurationModel model)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManagePaymentMethods))
                return AccessDeniedView();

            if (!ModelState.IsValid)
                return Configure();

            var settings = model.ToEntity<DeliveryAppBackendConfigurationSettings>();

            _settingService.SaveSetting(settings, _storeContext.ActiveStoreScopeConfiguration);
            _notificationService.SuccessNotification(_localizationService.GetResource("Admin.Plugins.Saved"));

            return Configure();
        }
    }
}
