using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Plugin.Widgets.WhatsAppChat.Models;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Media;
using Nop.Services.Messages;
using Nop.Services.Security;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;

namespace Nop.Plugin.Widgets.WhatsAppChat.Controllers
{

    /// <summary>
    /// Represent the main controller. It's used for configuring the WhatsApp Chat plug-in.
    /// </summary>
    [Area(AreaNames.Admin)]
    [AutoValidateAntiforgeryToken]
    public class WidgetsWhatsAppChatController : BasePluginController
    {
        private readonly ILocalizationService _localizationService;
        private readonly INotificationService _notificationService;
        private readonly IPermissionService _permissionService;
        private readonly ISettingService _settingService;
        private readonly IStoreContext _storeContext;


        /// <summary>
        /// Initializes a new instance of WidgetsWhatsAppChatController class.
        /// </summary>
        /// <param name="localizationService"><see cref="ILocalizationService"/></param>
        /// <param name="notificationService"><see cref="INotificationService"/></param>
        /// <param name="permissionService"><see cref="IPermissionService"/></param>
        /// <param name="settingService"><see cref="ISettingService"/></param>
        /// <param name="storeContext"><see cref="IStoreContext"/></param>
        public WidgetsWhatsAppChatController(ILocalizationService localizationService,
            INotificationService notificationService,
            IPermissionService permissionService,
            ISettingService settingService,
            IStoreContext storeContext)
        {
            _localizationService = localizationService;
            _notificationService = notificationService;
            _permissionService = permissionService;
            _settingService = settingService;
            _storeContext = storeContext;
        }

        /// <summary>
        /// Load Configuration Page in admin
        /// </summary>
        /// <returns><see cref="IActionResult"/></returns>
        public IActionResult Configure()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            //load settings for a chosen store scope
            int storeScope = _storeContext.ActiveStoreScopeConfiguration;
            WhatsAppChatSettings WhatsAppChatSettings = _settingService.LoadSetting<WhatsAppChatSettings>(storeScope);
            ConfigurationModel model = new ConfigurationModel
            {
                Phone = WhatsAppChatSettings.Phone,
                HeaderTitle = WhatsAppChatSettings.HeaderTitle,
                PopupMessage = WhatsAppChatSettings.PopupMessage,
                Message = WhatsAppChatSettings.Message,
                ActiveStoreScopeConfiguration = storeScope
            };

            if (storeScope > 0)
            {
                model.Phone_OverrideForStore = _settingService.SettingExists(WhatsAppChatSettings, x => x.Phone, storeScope);
                model.HeaderTitle_OverrideForStore = _settingService.SettingExists(WhatsAppChatSettings, x => x.HeaderTitle, storeScope);
                model.PopupMessage_OverrideForStore = _settingService.SettingExists(WhatsAppChatSettings, x => x.PopupMessage, storeScope);
                model.Message_OverrideForStore = _settingService.SettingExists(WhatsAppChatSettings, x => x.Message, storeScope);
            }

            return View("~/Plugins/Widgets.WhatsAppChat/Views/Configure.cshtml", model);
        }

        /// <summary>
        /// Save Configuration
        /// </summary>
        /// <param name="model"><see cref="ConfigurationModel"/></param>
        /// <returns>An implementation of <see cref="IActionResult"/>Configure method in this controller</returns>
        [HttpPost]
        public IActionResult Configure(ConfigurationModel model)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            //load settings for a chosen store scope
            int storeScope = _storeContext.ActiveStoreScopeConfiguration;
            WhatsAppChatSettings WhatsAppChatSettings = _settingService.LoadSetting<WhatsAppChatSettings>(storeScope);
            WhatsAppChatSettings.Phone = model.Phone;
            WhatsAppChatSettings.HeaderTitle = model.HeaderTitle;
            WhatsAppChatSettings.PopupMessage = model.PopupMessage;
            WhatsAppChatSettings.Message = model.Message;

            /* We do not clear cache after each setting update.
             * This behavior can increase performance because cached settings will not be cleared 
             * and loaded from database after each update */
            _settingService.SaveSettingOverridablePerStore(WhatsAppChatSettings, x => x.Phone, model.Phone_OverrideForStore, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(WhatsAppChatSettings, x => x.HeaderTitle, model.HeaderTitle_OverrideForStore, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(WhatsAppChatSettings, x => x.PopupMessage, model.PopupMessage_OverrideForStore, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(WhatsAppChatSettings, x => x.Message, model.Message_OverrideForStore, storeScope, false);

            //now clear settings cache
            _settingService.ClearCache();
            _notificationService.SuccessNotification(_localizationService.GetResource("Admin.Plugins.Saved"));
            return Configure();
        }
    }
}