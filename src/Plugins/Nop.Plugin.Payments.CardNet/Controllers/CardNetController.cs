using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Nop.Core;
using Nop.Core.Domain.Orders;
using Nop.Plugin.Payments.CardNet.Helpers;
using Nop.Plugin.Payments.CardNet.Models;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Messages;
using Nop.Services.Security;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;

namespace Nop.Plugin.Payments.CardNet.Controllers
{
    /// <summary>
    /// Controller that manages actions for CardNet plug-in
    /// </summary>
    public class CardNetController : BasePaymentController
    {
        #region Fields

        private readonly ILocalizationService _localizationService;
        private readonly IPermissionService _permissionService;
        private readonly IStoreContext _storeContext;
        private readonly ISettingService _settingService;
        private readonly ShoppingCartSettings _shoppingCartSettings;
        private readonly INotificationService _notificationService;
        private readonly IMemoryCache _memoryCache;

        #endregion

        #region Ctor

        public CardNetController(ILocalizationService localizationService, IPermissionService permissionService,
            IStoreContext storeContext, ISettingService settingService, ShoppingCartSettings shoppingCartSettings,
            INotificationService notificationService, IMemoryCache memoryCache)
        {
            _localizationService = localizationService;
            _permissionService = permissionService;
            _storeContext = storeContext;
            _settingService = settingService;
            _shoppingCartSettings = shoppingCartSettings;
            _notificationService = notificationService;
            _memoryCache = memoryCache;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Prepares <see cref="CardNetConfigurationModel"/> to send it to Configure.cshtml page
        /// </summary>
        [AuthorizeAdmin]
        [Area(AreaNames.Admin)]
        public IActionResult Configure()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManagePaymentMethods))
            {
                return AccessDeniedView();
            }

            var storeScope = _storeContext.ActiveStoreScopeConfiguration;
            var settings = _settingService.LoadSetting<CardNetSettings>(storeScope);

            var model = new CardNetConfigurationModel
            {
                Url = settings.Url,
                PwCheckoutScriptUrl = settings.PwCheckoutScriptUrl,
                PublicApiKey = settings.PublicApiKey,
                PrivateApiKey = settings.PrivateApiKey,
                CardNetImageUrl = settings.CardNetImageUrl
            };

            if (!_shoppingCartSettings.RoundPricesDuringCalculation)
            {
                var url = Url.Action("AllSettings", "Setting",
                    new { settingName = nameof(ShoppingCartSettings.RoundPricesDuringCalculation) });
                var warning =
                    string.Format(_localizationService.GetResource("Plugins.Payments.CardNet.RoundingWarning"),
                        url);
                _notificationService.WarningNotification(warning, false);
            }

            return View("~/Plugins/Payments.CardNet/Views/Configure.cshtml", model);
        }

        /// <summary>
        /// Receives and sets CardNet configuration
        /// </summary>
        /// <param name="model">CardNet configuration model</param>
        [HttpPost]
        [AuthorizeAdmin]
        [Area(AreaNames.Admin)]
        public IActionResult Configure(CardNetConfigurationModel model)
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
            var settings = _settingService.LoadSetting<CardNetSettings>(storeScope);

            settings.Url = model.Url;
            settings.PwCheckoutScriptUrl = model.PwCheckoutScriptUrl;
            settings.PublicApiKey = model.PublicApiKey;
            settings.PrivateApiKey = model.PrivateApiKey;
            settings.CardNetImageUrl = model.CardNetImageUrl;

            _settingService.SaveSettingOverridablePerStore(settings, setting => setting.Url, true, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(settings, setting => setting.PwCheckoutScriptUrl, true, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(settings, setting => setting.PublicApiKey, true, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(settings, setting => setting.PrivateApiKey, true, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(settings, setting => setting.CardNetImageUrl, true, storeScope, false);
            _settingService.ClearCache();

            _notificationService.SuccessNotification(_localizationService.GetResource("Admin.Plugins.Saved"));

            return Configure();
        }

        /// <summary>
        /// Sets CardNet's OneTimeToken as a <see cref="IMemoryCache"/> variable
        /// </summary>
        /// <param name="model">OneTimeToken received from CardNet's capture page</param>
        [HttpPost]
        public bool AddTokenToCache([FromBody] CardNetTokenModel model)
        {
            bool tokenIsNotEmpty = !string.IsNullOrEmpty(model.Token);

            if (tokenIsNotEmpty)
                _memoryCache.Set(CardNetDefaults.CacheTokenName, model.Token);

            return tokenIsNotEmpty;
        }

        #endregion
    }
}