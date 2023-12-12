using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Nop.Core.Domain.Cms;
using Nop.Plugin.Synchronizers.WAPIOrders.Domains;
using Nop.Plugin.Synchronizers.WAPIOrders.Helpers;
using Nop.Services.Common;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Plugins;
using System.Collections.Generic;
using System.Linq;

namespace Nop.Plugin.Synchronizers.WAPIOrders
{
    /// <summary>
    /// Represents he main class for WAPI Orders Synchronizer.
    /// </summary>
    public sealed class WapiOrdersPlugin : BasePlugin, IMiscPlugin
    {
        #region Fields

        private readonly ILanguageService _languageService;
        private readonly ILocalizationService _localizationService;
        private readonly IUrlHelperFactory _urlHelperFactory;
        private readonly IActionContextAccessor _actionContextAccessor;
        private readonly WidgetSettings _widgetSettings;
        private readonly ISettingService _settingService;

        #endregion

        #region Ctor

        /// <summary>
        /// Initializes a new instance of <see cref="WapiOrdersPlugin"/>.
        /// </summary>
        /// <param name="languageService">An implementation of <see cref="ILanguageService"/>.</param>
        /// <param name="localizationService">An implementation of <see cref="ILocalizationService"/>.</param>
        /// <param name="urlHelperFactory">An implementation of <see cref="IUrlHelperFactory"/>.</param>
        /// <param name="actionContextAccessor">An implementation of <see cref="IActionContextAccessor"/>.</param>
        /// <param name="widgetSettings">An instance of <see cref="WidgetSettings"/>.</param>
        /// <param name="settingService">An implementation of <see cref="ISettingService"/>.</param>
        public WapiOrdersPlugin(
            ILanguageService languageService,
            ILocalizationService localizationService,
            IUrlHelperFactory urlHelperFactory,
            IActionContextAccessor actionContextAccessor,
            WidgetSettings widgetSettings,
            ISettingService settingService)
        {
            _languageService = languageService;
            _localizationService = localizationService;
            _urlHelperFactory = urlHelperFactory;
            _actionContextAccessor = actionContextAccessor;
            _widgetSettings = widgetSettings;
            _settingService = settingService;
        }

        #endregion

        #region Utilities

        private void ActivatePlugin()
        {
            if (!_widgetSettings.ActiveWidgetSystemNames.Contains(Defaults.SystemName))
            {
                _widgetSettings.ActiveWidgetSystemNames.Add(Defaults.SystemName);
                _settingService.SaveSetting(_widgetSettings);
            }
        }

        private void InsertLanguagesResources()
        {
            var language = _languageService.GetAllLanguages().FirstOrDefault(x => x.LanguageCulture.Contains("es-"));

            if (language != null)
            {
                AddLocaleResources(LocaleResources.SpanishResources, language.LanguageCulture);
            }

            AddLocaleResources(LocaleResources.EnglishResources, "en-US");
        }

        private void AddLocaleResources(Dictionary<string, string> resources, string languageLanguageCulture)
        {
            foreach (var resource in resources)
            {
                _localizationService.AddOrUpdatePluginLocaleResource(resource.Key, resource.Value, languageLanguageCulture);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the configuration page URL.
        /// </summary>
        /// <returns>An URL with an absolute path for <see cref="Defaults.ConfigurationRouteName"/>.</returns>
        public override string GetConfigurationPageUrl()
            => _urlHelperFactory.GetUrlHelper(_actionContextAccessor.ActionContext).RouteUrl(Defaults.ConfigurationRouteName);

        /// <summary>
        /// Installs WAPI Orders Synchronizer.
        /// </summary>
        public override void Install()
        {
            ActivatePlugin();
            InsertLanguagesResources();

            base.Install();
        }

        /// <summary>
        /// Uninstalls WAPI Orders Synchronizer.
        /// </summary>
        public override void Uninstall()
        {
            if (_widgetSettings.ActiveWidgetSystemNames.Contains(Defaults.SystemName))
            {
                _widgetSettings.ActiveWidgetSystemNames.Remove(Defaults.SystemName);
                _settingService.SaveSetting(_widgetSettings);
            }

            _settingService.DeleteSetting<ConfigurationSettings>();
            _localizationService.DeletePluginLocaleResources(Defaults.LocaleResorcesPrefix);

            base.Uninstall();
        }

        #endregion
    }
}
