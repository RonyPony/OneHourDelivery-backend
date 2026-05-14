using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nop.Core;
using Nop.Core.Domain.Cms;
using Nop.Plugin.Widgets.GoogleMapsIntegration.Domains;
using Nop.Plugin.Widgets.GoogleMapsIntegration.Helpers;
using Nop.Services.Cms;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Plugins;

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
        private readonly IWebHelper _webHelper;

        #endregion

        #region Ctor

        /// <summary>
        /// Initializes a new instance of <see cref="GoogleMapsIntegrationPlugin"/>.
        /// </summary>
        /// <param name="widgetSettings">An instance of <see cref="WidgetSettings"/>.</param>
        /// <param name="languageService">An implementation of <see cref="ILanguageService"/>.</param>
        /// <param name="localizationService">An implementation of <see cref="ILocalizationService"/>.</param>
        /// <param name="settingService">An implementation of <see cref="ISettingService"/>.</param>
        /// <param name="webHelper">An implementation of <see cref="IWebHelper"/>.</param>
        public GoogleMapsIntegrationPlugin(
            WidgetSettings widgetSettings,
            ILanguageService languageService,
            ILocalizationService localizationService,
            ISettingService settingService,
            IWebHelper webHelper)
        {
            _widgetSettings = widgetSettings;
            _languageService = languageService;
            _localizationService = localizationService;
            _settingService = settingService;
            _webHelper = webHelper;
        }

        #endregion

        #region Utilities

        private async Task InsertLanguagesResourcesAsync()
        {
            var language = (await _languageService.GetAllLanguagesAsync(true)).FirstOrDefault(x => x.LanguageCulture.Contains("es-"));

            if (language != null)
                await AddLocaleResourcesAsync(GoogleMapsIntegrationLocaleResources.SpanishResources, language.LanguageCulture);

            await AddLocaleResourcesAsync(GoogleMapsIntegrationLocaleResources.EnglishResources, "en-US");
        }

        private async Task AddLocaleResourcesAsync(Dictionary<string, string> resources, string languageLanguageCulture)
        {
            foreach (var resource in resources)
                await _localizationService.AddOrUpdateLocaleResourceAsync(resource.Key, resource.Value, languageLanguageCulture);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Determines whether to hide or not the plugin in the widget list.
        /// </summary>
        public bool HideInWidgetList => false;

        /// <summary>
        /// Gets a type of a view component for displaying widget.
        /// </summary>
        /// <param name="widgetZone">Name of the widget zone.</param>
        /// <returns>The type of the view component for this plugin.</returns>
        public Type GetWidgetViewComponent(string widgetZone) => null;

        /// <summary>
        /// Returns the widget zones for this plugin.
        /// </summary>
        /// <returns>An instance of <see cref="List{T}"/> where T the name of a widget zone used by this plugin.</returns>
        public Task<IList<string>> GetWidgetZonesAsync() => Task.FromResult<IList<string>>(new List<string>());

        /// <summary>
        /// Gets the configuration page URL.
        /// </summary>
        public override string GetConfigurationPageUrl()
            => $"{_webHelper.GetStoreLocation()}Admin/GoogleMapsIntegration/Configure";

        /// <summary>
        /// Installs Google Maps Integration plugin.
        /// </summary>
        public override async Task InstallAsync()
        {
            await InsertLanguagesResourcesAsync();

            if (!_widgetSettings.ActiveWidgetSystemNames.Contains(Defaults.SystemName))
            {
                _widgetSettings.ActiveWidgetSystemNames.Add(Defaults.SystemName);
                await _settingService.SaveSettingAsync(_widgetSettings);
            }

            await base.InstallAsync();
        }

        /// <summary>
        /// Uninstalls Google Maps Integration plugin.
        /// </summary>
        public override async Task UninstallAsync()
        {
            if (_widgetSettings.ActiveWidgetSystemNames.Contains(Defaults.SystemName))
            {
                _widgetSettings.ActiveWidgetSystemNames.Remove(Defaults.SystemName);
                await _settingService.SaveSettingAsync(_widgetSettings);
            }

            await _settingService.DeleteSettingAsync<PluginConfigurationSettings>();
            await _localizationService.DeleteLocaleResourcesAsync(Defaults.ResourcesNamePrefix);

            await base.UninstallAsync();
        }

        #endregion
    }
}
