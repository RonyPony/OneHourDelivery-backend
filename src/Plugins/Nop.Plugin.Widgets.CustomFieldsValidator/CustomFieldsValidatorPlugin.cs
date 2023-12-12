using Nop.Plugin.Widgets.CustomFieldsValidator.Domains;
using Nop.Services.Cms;
using Nop.Services.Localization;
using Nop.Services.Plugins;
using System.Collections.Generic;
using System.Linq;
using Nop.Core.Domain.Cms;
using Nop.Plugin.Widgets.CustomFieldsValidator.Helpers;
using Nop.Services.Configuration;

namespace Nop.Plugin.Widgets.CustomFieldsValidator
{
    /// <summary>
    /// Main file for this plugin
    /// </summary>
    public sealed class CustomFieldsValidatorPlugin : BasePlugin, IWidgetPlugin
    {
        private readonly ILanguageService _languageService;
        private readonly ILocalizationService _localizationService;
        private readonly WidgetSettings _widgetSettings;
        private readonly ISettingService _settingService;

        /// <summary>
        /// Initializes a new instance of CustomFieldsValidatorPlugin class with the value indicated as parameters.
        /// </summary>
        /// <param name="languageService">An implementation of <see cref="ILanguageService"/>.</param>
        /// <param name="localizationService">An implementation of <see cref="ILocalizationService"/>.</param>
        /// <param name="widgetSettings">An instance of <see cref="WidgetSettings"/>.</param>
        /// <param name="settingService">An implementation of <see cref="ILocalizationService"/>.</param>
        public CustomFieldsValidatorPlugin(ILanguageService languageService,
            ILocalizationService localizationService,
            WidgetSettings widgetSettings,
            ISettingService settingService)
        {
            _localizationService = localizationService;
            _widgetSettings = widgetSettings;
            _settingService = settingService;
            _languageService = languageService;
        }

        /// <summary>
        /// Indicated weather the Widget should be hide or not in the widget list.
        /// </summary>
        public bool HideInWidgetList => false;

        /// <inheritdoc />
        public IList<string> GetWidgetZones() => new List<string>();

        /// <inheritdoc />
        public string GetWidgetViewComponentName(string widgetZone) => string.Empty;

        /// <inheritdoc />
        public override void Install()
        {
            var language = _languageService.GetAllLanguages().FirstOrDefault(x => x.LanguageCulture.Contains("es-"));

            if (language != null)
            {
                AddLocaleResources(CustomFieldsValidatorLocaleResources.SpanishResources, language.LanguageCulture);
            }

            AddLocaleResources(CustomFieldsValidatorLocaleResources.EnglishResources, "en-US");

            // Add widget to active widgets
            if (!_widgetSettings.ActiveWidgetSystemNames.Contains(DefaultInfo.SystemName))
            {
                _widgetSettings.ActiveWidgetSystemNames.Add(DefaultInfo.SystemName);
                _settingService.SaveSetting(_widgetSettings);
            }

            base.Install();
        }

        private void AddLocaleResources(Dictionary<string, string> resources, string languageLanguageCulture)
        {
            foreach (var resource in resources)
            {
                _localizationService.AddOrUpdatePluginLocaleResource(resource.Key, resource.Value, languageLanguageCulture);
            }
        }

        /// <inheritdoc />
        public override void Uninstall()
        {
            if (_widgetSettings.ActiveWidgetSystemNames.Contains(DefaultInfo.SystemName))
            {
                _widgetSettings.ActiveWidgetSystemNames.Remove(DefaultInfo.SystemName);
                _settingService.SaveSetting(_widgetSettings);
            }

            _localizationService.DeletePluginLocaleResources("Plugins.Widgets.CustomFieldsValidator");

            base.Uninstall();
        }
    }
}
