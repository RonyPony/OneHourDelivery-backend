using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Nop.Plugin.Widgets.Zoom.Domains;
using Nop.Plugin.Widgets.Zoom.Helpers;
using Nop.Services.Cms;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Plugins;
using Nop.Web.Framework.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace Nop.Plugin.Widgets.Zoom
{
    /// <summary>
    /// Represents the main class for Widget.Zoom plugin.
    /// </summary>
    public class ZoomPlugin : BasePlugin, IWidgetPlugin
    {
        #region Fields

        private readonly IActionContextAccessor _actionContextAccessor;
        private readonly ILanguageService _languageService;
        private readonly ILocalizationService _localizationService;
        private readonly ISettingService _settingService;
        private readonly IUrlHelperFactory _urlHelperFactory;

        #endregion

        #region Ctor

        /// <summary>
        /// Initializes a new instance of <see cref="ZoomPlugin"/>.
        /// </summary>
        /// <param name="actionContextAccessor">An implementation of <see cref="IActionContextAccessor"/>.</param>
        /// <param name="languageService">An implementation of <see cref="ILanguageService"/>.</param>
        /// <param name="localizationService">An implementation of <see cref="ILocalizationService"/>.</param>
        /// <param name="settingService">An implementation of <see cref="ISettingService"/>.</param>
        /// <param name="urlHelperFactory">An implementation of <see cref="IUrlHelperFactory"/>.</param>
        public ZoomPlugin(
            IActionContextAccessor actionContextAccessor,
            ILanguageService languageService,
            ILocalizationService localizationService,
            ISettingService settingService,
            IUrlHelperFactory urlHelperFactory)
        {
            _actionContextAccessor = actionContextAccessor;
            _languageService = languageService;
            _localizationService = localizationService;
            _settingService = settingService;
            _urlHelperFactory = urlHelperFactory;
        }

        #endregion

        #region Utilities

        private void InsertLanguagesResources()
        {
            var language = _languageService.GetAllLanguages().FirstOrDefault(x => x.LanguageCulture.Contains("es-"));

            if (language != null)
            {
                AddLocaleResources(ZoomPluginLocaleResources.SpanishResources, language.LanguageCulture);
            }

            AddLocaleResources(ZoomPluginLocaleResources.EnglishResources, "en-US");
        }

        private void AddLocaleResources(Dictionary<string, string> resources, string languageLanguageCulture)
        {
            foreach (var resource in resources)
            {
                _localizationService.AddOrUpdatePluginLocaleResource(resource.Key, resource.Value, languageLanguageCulture);
            }
        }

        #endregion

        /// <summary>
        /// Gets a value indicating whether to hide this plugin on the widget list page in the admin area
        /// </summary>
        public bool HideInWidgetList => false;

        /// <summary>
        /// Gets a name of a view component for displaying widget
        /// </summary>
        /// <param name="widgetZone">Name of the widget zone</param>
        /// <returns>View component name</returns>
        public string GetWidgetViewComponentName(string widgetZone)
            => "ProductDetailsAfterPictures";

        /// <summary>
        /// Gets widget zones where this widget should be rendered
        /// </summary>
        /// <returns>Widget zones</returns>
        public IList<string> GetWidgetZones()
            => new List<string> { PublicWidgetZones.ProductDetailsAfterPictures };

        /// <summary>
        /// Gets the configuration page URL.
        /// </summary>
        public override string GetConfigurationPageUrl()
            => _urlHelperFactory.GetUrlHelper(_actionContextAccessor.ActionContext).RouteUrl(ZoomDefaults.ConfigurationRouteName);

        /// <summary>
        /// Install the plugin
        /// </summary>
        public override void Install()
        {
            InsertLanguagesResources();

            base.Install();
        }

        /// <summary>
        /// Uninstall the plugin
        /// </summary>
        public override void Uninstall()
        {
            _settingService.DeleteSetting<ZoomPluginSettings>();
            _localizationService.DeletePluginLocaleResources(ZoomDefaults.ResourcesNamePrefix);

            base.Uninstall();
        }
    }
}
