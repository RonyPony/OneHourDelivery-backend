using System;
using System.Collections.Generic;
using System.Linq;
using Nop.Core;
using Nop.Core.Infrastructure;
using Nop.Plugin.Widgets.WhatsAppChat.Models;
using Nop.Services.Cms;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Media;
using Nop.Services.Plugins;
using Nop.Web.Framework.Infrastructure;

namespace Nop.Plugin.Widgets.WhatsAppChat
{
    /// <summary>
    /// Main file for this plug-in
    /// </summary>
    public class WhatsAppChatPlugin : BasePlugin, IWidgetPlugin
    {
        private readonly ILocalizationService _localizationService;
        private readonly ISettingService _settingService;
        private readonly IWebHelper _webHelper;
        private readonly ILanguageService _languageService;

        /// <summary>
        /// Initializes a new instance of WhatsAppChatPlugin class.
        /// </summary>
        /// <param name="localizationService"><see cref="ILocalizationService"/></param>
        /// <param name="settingService"><see cref="ISettingService"/></param>
        /// <param name="webHelper"><see cref="IWebHelper"/></param>
        /// <param name="languageService"><see cref="ILanguageService"/></param>
        public WhatsAppChatPlugin(ILocalizationService localizationService,
            ISettingService settingService,
            IWebHelper webHelper,
            ILanguageService languageService)
        {
            _localizationService = localizationService;
            _settingService = settingService;
            _webHelper = webHelper;
            _languageService = languageService;
        }

        /// <summary>
        /// Gets widget zones where this widget should be rendered
        /// </summary>
        /// <returns>Widget zones</returns>
        public IList<string> GetWidgetZones()
        {
            return new List<string> { PublicWidgetZones.ContentAfter };
        }

        /// <summary>
        /// Gets a configuration page URL
        /// </summary>
        public override string GetConfigurationPageUrl()
        {
            return _webHelper.GetStoreLocation() + "Admin/WidgetsWhatsAppChat/Configure";
        }

        /// <summary>
        /// Gets a name of a view component for displaying widget
        /// </summary>
        /// <param name="widgetZone">Name of the widget zone</param>
        /// <returns>View component name</returns>
        public string GetWidgetViewComponentName(string widgetZone)
        {
            return "WidgetsWhatsAppChat";
        }

        /// <summary>
        /// Install plugin
        /// </summary>
        public override void Install()
        {
            // Add language resources
            var language = _languageService.GetAllLanguages().FirstOrDefault(x => x.LanguageCulture.Contains("es-"));

            if (language != null)
            {
                AddLocaleResources(WhatsAppChatLocaleResources.SpanishResources, language.LanguageCulture);
            }

            AddLocaleResources(WhatsAppChatLocaleResources.EnglishResources, "en-US");

            base.Install();
        }

        private void AddLocaleResources(Dictionary<string, string> resources, string languageLanguageCulture)
        {
            foreach (var resource in resources)
            {
                _localizationService.AddOrUpdatePluginLocaleResource(resource.Key, resource.Value,
                    languageLanguageCulture);
            }
        }

        /// <summary>
        /// Uninstall plugin
        /// </summary>
        public override void Uninstall()
        {
            //settings
            _settingService.DeleteSetting<WhatsAppChatSettings>();

            //locales
            _localizationService.DeletePluginLocaleResources("Plugins.Widgets.WhatsAppChat");

            base.Uninstall();
        }

        /// <summary>
        /// Gets a value indicating whether to hide this plugin on the widget list page in the admin area
        /// </summary>
        public bool HideInWidgetList => false;
    }
}