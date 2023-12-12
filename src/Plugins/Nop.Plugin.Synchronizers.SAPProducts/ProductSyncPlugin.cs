using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Nop.Core.Domain.Tasks;
using Nop.Plugin.Synchronizers.SAPProducts.Domains;
using Nop.Plugin.Synchronizers.SAPProducts.Helpers;
using Nop.Services.Cms;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Plugins;
using Nop.Services.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace Nop.Plugin.Synchronizers.SAPProducts
{
    /// <summary>
    /// Represents the main file for the SAP Products Synchronizer plugin.
    /// </summary>
    public sealed class ProductSyncPlugin : BasePlugin, IWidgetPlugin
    {
        private readonly IScheduleTaskService _scheduleTaskService;
        private readonly ILocalizationService _localizationService;
        private readonly ISettingService _settingService;
        private readonly ILanguageService _languageService;
        private readonly IUrlHelperFactory _urlHelperFactory;
        private readonly IActionContextAccessor _actionContextAccessor;

        /// <summary>
        /// Gets a value indicating whether to hide this plugin on the widget list page in the admin area.
        /// </summary>
        public bool HideInWidgetList { get; } = false;

        /// <summary>
        /// Initializes a new instance of <see cref="ProductSyncPlugin"/>.
        /// </summary>
        /// <param name="scheduleTaskService">An implementation of <see cref="IScheduleTaskService"/>.</param>
        /// <param name="localizationService">An implementation of <see cref="ILocalizationService"/>.</param>
        /// <param name="settingService">An implementation of <see cref="ISettingService"/>.</param>
        /// <param name="languageService">An implementation of <see cref="ILanguageService"/>.</param>
        /// <param name="urlHelperFactory">An implementation of <see cref="IUrlHelperFactory"/>.</param>
        /// <param name="actionContextAccessor">An implementation of <see cref="IActionContextAccessor"/>.</param>
        public ProductSyncPlugin(IScheduleTaskService scheduleTaskService,
            ILocalizationService localizationService,
            ISettingService settingService,
            ILanguageService languageService,
            IUrlHelperFactory urlHelperFactory,
            IActionContextAccessor actionContextAccessor)
        {
            _scheduleTaskService = scheduleTaskService;
            _localizationService = localizationService;
            _settingService = settingService;
            _languageService = languageService;
            _urlHelperFactory = urlHelperFactory;
            _actionContextAccessor = actionContextAccessor;
        }

        /// <summary>
        /// Installs the plugin.
        /// </summary>
        public override void Install()
        {
            InsertProductScheduleTask();
            InsertLanguagesResources();

            base.Install();
        }

        private void InsertProductScheduleTask()
        {
            int minutesInAnHour = 60;
            int secondsInAMinute = 60;

            if (_scheduleTaskService.GetTaskByType(ProductSyncDefaults.SynchronizationTask) == null)
            {
                _scheduleTaskService.InsertTask(new ScheduleTask
                {
                    Enabled = false,
                    Seconds = ProductSyncDefaults.DefaultSynchronizationPeriod * minutesInAnHour * secondsInAMinute,
                    Name = ProductSyncDefaults.SynchronizationTaskName,
                    Type = ProductSyncDefaults.SynchronizationTask,
                });
            }
        }

        private void InsertLanguagesResources()
        {
            var language = _languageService.GetAllLanguages().FirstOrDefault(x => x.LanguageCulture.Contains("es-"));

            if (language != null)
            {
                AddLocaleResources(SapProductsSynchronizerLocaleResources.SpanishResources, language.LanguageCulture);
            }

            AddLocaleResources(SapProductsSynchronizerLocaleResources.EnglishResources, "en-US");
        }

        private void AddLocaleResources(Dictionary<string, string> resources, string languageLanguageCulture)
        {
            foreach (var resource in resources)
            {
                _localizationService.AddOrUpdatePluginLocaleResource(resource.Key, resource.Value, languageLanguageCulture);
            }
        }

        /// <summary>
        /// Uninstalls the plugin.
        /// </summary>
        public override void Uninstall()
        {
            //Uninstall schedule task
            var task = _scheduleTaskService.GetTaskByType(ProductSyncDefaults.SynchronizationTask);

            if (task != null)
                _scheduleTaskService.DeleteTask(task);

            //Remove language locales
            _localizationService.DeletePluginLocaleResources("Plugins.Synchronizers.SAPProducts");

            //settings
            _settingService.DeleteSetting<ProductSyncConfigurationSettings>();

            base.Uninstall();
        }

        /// <summary>
        /// Gets the configuration page URL.
        /// </summary>
        public override string GetConfigurationPageUrl()
            => _urlHelperFactory.GetUrlHelper(_actionContextAccessor.ActionContext).RouteUrl(ProductSyncDefaults.ConfigurationRouteName);

        /// <summary>
        /// Returns the widget zones for this plugin.
        /// </summary>
        /// <returns>An empty list since this plugin does not need widget zones.</returns>
        public IList<string> GetWidgetZones() => new List<string> { string.Empty };

        /// <summary>
        /// Gets the name of the view component for displaying widget.
        /// </summary>
        /// <param name="widgetZone">Name of the widget zone.</param>
        /// <returns>An empty string since this plugin does not need a view component.</returns>
        public string GetWidgetViewComponentName(string widgetZone) => string.Empty;
    }
}