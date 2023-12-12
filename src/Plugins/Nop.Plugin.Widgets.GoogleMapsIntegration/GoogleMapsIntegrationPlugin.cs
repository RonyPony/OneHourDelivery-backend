using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Nop.Core.Domain.Cms;
using Nop.Plugin.Widgets.GoogleMapsIntegration.Domains;
using Nop.Plugin.Widgets.GoogleMapsIntegration.Helpers;
using Nop.Services.Cms;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Plugins;
using System.Collections.Generic;
using System.Linq;

namespace Nop.Plugin.Widgets.GoogleMapsIntegration
{
    /// <summary>
    /// Represents the main file for Google Maps Integration Plugin.
    /// </summary>
    public sealed class GoogleMapsIntegrationPlugin : BasePlugin, IWidgetPlugin
    {
        #region Fields

        private readonly WidgetSettings _widgetSettings;
        private readonly ILanguageService _languageService;
        private readonly ILocalizationService _localizationService;
        private readonly ISettingService _settingService;
        private readonly IUrlHelperFactory _urlHelperFactory;
        private readonly IActionContextAccessor _actionContextAccessor;

        #endregion

        #region Ctor

        /// <summary>
        /// Initializes a new instance of <see cref="GoogleMapsIntegrationPlugin"/>.
        /// </summary>
        /// <param name="widgetSettings">An instance of <see cref="WidgetSettings"/>.</param>
        /// <param name="languageService">An implementation of <see cref="ILanguageService"/>.</param>
        /// <param name="localizationService">An implementation of <see cref="ILocalizationService"/>.</param>
        /// <param name="settingService">An implementation of <see cref="ISettingService"/>.</param>
        /// <param name="urlHelperFactory">An implementation of <see cref="IUrlHelperFactory"/>.</param>
        /// <param name="actionContextAccessor">An implementation of <see cref="IActionContextAccessor"/>.</param>
        public GoogleMapsIntegrationPlugin(
            WidgetSettings widgetSettings,
            ILanguageService languageService,
            ILocalizationService localizationService,
            ISettingService settingService,
            IUrlHelperFactory urlHelperFactory,
            IActionContextAccessor actionContextAccessor)
        {
            _widgetSettings = widgetSettings;
            _languageService = languageService;
            _localizationService = localizationService;
            _settingService = settingService;
            _urlHelperFactory = urlHelperFactory;
            _actionContextAccessor = actionContextAccessor;
        }

        #endregion

        #region Utilities

        private void InsertLanguagesResources()
        {
            var language = _languageService.GetAllLanguages().FirstOrDefault(x => x.LanguageCulture.Contains("es-"));

            if (language != null)
            {
                AddLocaleResources(GoogleMapsIntegrationLocaleResources.SpanishResources, language.LanguageCulture);
            }

            AddLocaleResources(GoogleMapsIntegrationLocaleResources.EnglishResources, "en-US");
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
        /// Determines whether to hide or not the plugin in the widget list.
        /// </summary>
        public bool HideInWidgetList => false;

        /// <summary>
        /// Gets a name of a view component for displaying widget.
        /// </summary>
        /// <param name="widgetZone">Name of the widget zone.</param>
        /// <returns>The name of the view component for this plugin.</returns>
        public string GetWidgetViewComponentName(string widgetZone) => string.Empty;

        /// <summary>
        /// Returns the widget zones for this plugin.
        /// </summary>
        /// <returns>An instance of <see cref="List{T}"/> where T the name of a widget zone used by this plugin.</returns>
        public IList<string> GetWidgetZones() => new List<string>();

        /// <summary>
        /// Gets the configuration page URL.
        /// </summary>
        public override string GetConfigurationPageUrl()
            => _urlHelperFactory.GetUrlHelper(_actionContextAccessor.ActionContext).RouteUrl(Defaults.ConfigurationRouteName);

        /// <summary>
        /// Installs Google Maps Integration plugin.
        /// </summary>
        public override void Install()
        {
            InsertLanguagesResources();

            if (!_widgetSettings.ActiveWidgetSystemNames.Contains(Defaults.SystemName))
            {
                _widgetSettings.ActiveWidgetSystemNames.Add(Defaults.SystemName);
                _settingService.SaveSetting(_widgetSettings);
            }

            base.Install();
        }

        /// <summary>
        /// Uninstalls Google Maps Integration plugin.
        /// </summary>
        public override void Uninstall()
        {
            if (_widgetSettings.ActiveWidgetSystemNames.Contains(Defaults.SystemName))
            {
                _widgetSettings.ActiveWidgetSystemNames.Remove(Defaults.SystemName);
                _settingService.SaveSetting(_widgetSettings);
            }

            _settingService.DeleteSetting<PluginConfigurationSettings>();
            _localizationService.DeletePluginLocaleResources(Defaults.ResourcesNamePrefix);

            base.Uninstall();
        }

        #endregion
    };
}
