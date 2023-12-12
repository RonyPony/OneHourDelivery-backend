using Nop.Core;
using Nop.Core.Domain.Cms;
using Nop.Core.Domain.Localization;
using Nop.Plugin.Widgets.RegionsOnRegisterPage.Domains;
using Nop.Plugin.Widgets.RegionsOnRegisterPage.Helpers;
using Nop.Services.Cms;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Plugins;
using System.Collections.Generic;
using System.Linq;

namespace Nop.Plugin.Widgets.RegionsOnRegisterPage
{
    /// <summary>
    /// Represents the main file for this plug-in.
    /// </summary>
    public class RegionsOnRegisterPagePlugin : BasePlugin, IWidgetPlugin
    {
        #region Fields
        private readonly WidgetSettings _widgetSettings;
        private readonly ISettingService _settingService;
        private readonly ILanguageService _languageService;
        private readonly ILocalizationService _localizationService;

        #endregion

        #region Ctor

        /// <summary>
        /// Initializes a new instance of RegionsOnRegisterPagePlugin class.
        /// </summary>
        /// <param name="widgetSettings"><see cref="WidgetSettings"/></param>
        /// <param name="settingService"><see cref="ISettingService"/></param>
        /// <param name="localizationService"><see cref="ILocalizationService"/></param>
        /// <param name="languageService"><see cref="ILanguageService"/></param>
        /// 
        public RegionsOnRegisterPagePlugin(
            WidgetSettings widgetSettings,
            ISettingService settingService,
            ILocalizationService localizationService,
            ILanguageService languageService)
        {
            _widgetSettings = widgetSettings;
            _settingService = settingService;
            _localizationService = localizationService;
            _languageService = languageService;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a value indicating whether to hide this plug-in on the widget list page in the admin area
        /// </summary>
        public bool HideInWidgetList => false;

        #endregion

        #region Methods

        /// <summary>
        /// Installs the plug-in
        /// </summary>
        public override void Install()
        {
            // Add widget to active widgets
            if (!_widgetSettings.ActiveWidgetSystemNames.Contains(DefaultsInfo.SystemName))
            {
                _widgetSettings.ActiveWidgetSystemNames.Add(DefaultsInfo.SystemName);
                _settingService.SaveSetting(_widgetSettings);
            }

            Language language = _languageService.GetAllLanguages().FirstOrDefault(x => x.LanguageCulture.Contains("es-"));

            if (language != null)
            {
                AddLocaleResources(RegionsOnRegisterPageLocaleResources.SpanishResources, language.LanguageCulture);
            }

            AddLocaleResources(RegionsOnRegisterPageLocaleResources.EnglishResources, "en-US");

            base.Install();
        }

        /// <summary>
        /// Uninstall plugin
        /// </summary>
        public override void Uninstall()
        {
            if (_widgetSettings.ActiveWidgetSystemNames.Contains(DefaultsInfo.SystemName))
            {
                _widgetSettings.ActiveWidgetSystemNames.Remove(DefaultsInfo.SystemName);
                _settingService.SaveSetting(_widgetSettings);
            }

            _settingService.DeleteSetting<RegionsOnRegisterPageSettings>();

            _localizationService.DeletePluginLocaleResources("plugins.widgets.RegionsOnRegisterPage");
            base.Uninstall();
        }

        /// <summary>
        /// Adds locale language resources to database
        /// </summary>
        /// <param name="resources">Resources to add</param>
        /// <param name="languageLanguageCulture">Language culture of the resources to add</param>
        private void AddLocaleResources(Dictionary<string, string> resources, string languageLanguageCulture)
        {
            foreach (var resource in resources)
            {
                _localizationService.AddOrUpdatePluginLocaleResource(resource.Key, resource.Value, languageLanguageCulture);
            }
        }

        /// <summary>
        /// Gets a name of a view component for displaying widget.
        /// </summary>
        /// <param name="widgetZone">Name of the widget zone</param>
        /// <returns>View component name</returns>
        public string GetWidgetViewComponentName(string widgetZone) => DefaultsInfo.RegionsOnRegisterPagePluginComponentName;

        /// <summary>
        /// Gets the plugin's widget zones.
        /// </summary>
        public IList<string> GetWidgetZones() => new List<string> {DefaultsInfo.RegionWidgetZoneName};
      
        #endregion
    }
}