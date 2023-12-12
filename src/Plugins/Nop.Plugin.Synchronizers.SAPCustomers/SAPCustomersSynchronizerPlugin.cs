using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Nop.Core.Domain.Cms;
using Nop.Core.Domain.Tasks;
using Nop.Plugin.Synchronizers.SAPCustomers.Domains;
using Nop.Plugin.Synchronizers.SAPCustomers.Helpers;
using Nop.Services.Cms;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Plugins;
using Nop.Services.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace Nop.Plugin.Synchronizers.SAPCustomers
{
    /// <summary>
    /// Represents the main file for this plugin.
    /// </summary>
    public sealed class SapCustomersSynchronizerPlugin : BasePlugin, IWidgetPlugin
    {
        private readonly IUrlHelperFactory _urlHelperFactory;
        private readonly IActionContextAccessor _actionContextAccessor;
        private readonly ILanguageService _languageService;
        private readonly ILocalizationService _localizationService;
        private readonly WidgetSettings _widgetSettings;
        private readonly ISettingService _settingService;
        private readonly IScheduleTaskService _scheduleTaskService;

        /// <summary>
        /// Initializes a new instance of SAPCustomersSynchronizerPlugin with the values indicated as parameters.
        /// </summary>
        /// <param name="urlHelperFactory">Represents an implementation of <see cref="IUrlHelperFactory"/>.</param>
        /// <param name="actionContextAccessor">Represents an implementation of <see cref="IActionContextAccessor"/>.</param>
        /// <param name="languageService">Represents an implementation of <see cref="ILanguageService"/>.</param>
        /// <param name="localizationService">Represents an implementation of <see cref="ILocalizationService"/>.</param>
        /// <param name="widgetSettings">Represents an instance of <see cref="WidgetSettings"/>.</param>
        /// <param name="settingService">Represents an implementation of <see cref="ISettingService"/>.</param>
        /// <param name="scheduleTaskService">Represents an implementation of <see cref="IScheduleTaskService"/>.</param>
        public SapCustomersSynchronizerPlugin(
            IUrlHelperFactory urlHelperFactory,
            IActionContextAccessor actionContextAccessor,
            ILanguageService languageService,
            ILocalizationService localizationService,
            WidgetSettings widgetSettings,
            ISettingService settingService,
            IScheduleTaskService scheduleTaskService)
        {
            _urlHelperFactory = urlHelperFactory;
            _actionContextAccessor = actionContextAccessor;
            _languageService = languageService;
            _localizationService = localizationService;
            _widgetSettings = widgetSettings;
            _settingService = settingService;
            _scheduleTaskService = scheduleTaskService;
        }

        /// <summary>
        /// Determines whether to hide or not the plugin in the widget list.
        /// </summary>
        public bool HideInWidgetList { get; } = false;

        /// <summary>
        /// Returns the widget zones for this plugin.
        /// </summary>
        /// <returns>An empty list due to this plugin does not need widget zones.</returns>
        public IList<string> GetWidgetZones() => new List<string> { string.Empty };

        /// <summary>
        /// Gets a name of a view component for displaying widget
        /// </summary>
        /// <param name="widgetZone">Name of the widget zone</param>
        /// <returns>An empty string due to this plugin does not need a view component.</returns>
        public string GetWidgetViewComponentName(string widgetZone) => string.Empty;

        /// <summary>
        /// Gets the configuration page URL
        /// </summary>
        public override string GetConfigurationPageUrl() => _urlHelperFactory.GetUrlHelper(_actionContextAccessor.ActionContext).RouteUrl(DefaultsInfo.ConfigurationRouteName);

        /// <summary>
        /// Install SAP Customers Synchronizer plugin
        /// </summary>
        public override void Install()
        {
            InstallLanguagesResources();

            InsertCustomerScheduleTask();

            base.Install();
        }

        private void InstallLanguagesResources()
        {
            var language = _languageService.GetAllLanguages().FirstOrDefault(x => x.LanguageCulture.Contains("es-"));

            if (language != null)
            {
                AddLocaleResources(SapCustomersSynchronizerLocaleResources.SpanishResources, language.LanguageCulture);
            }

            AddLocaleResources(SapCustomersSynchronizerLocaleResources.EnglishResources, "en-US");
        }

        private void AddLocaleResources(Dictionary<string, string> resources, string languageLanguageCulture)
        {
            foreach (var resource in resources)
            {
                _localizationService.AddOrUpdatePluginLocaleResource(resource.Key, resource.Value, languageLanguageCulture);
            }
        }

        private void InsertCustomerScheduleTask()
        {
            var scheduleTask = new ScheduleTask
            {
                Name = DefaultsInfo.TaskName,
                Seconds = DefaultsInfo.TaskDuration,
                Type = DefaultsInfo.TaskType
            };

            _scheduleTaskService.InsertTask(scheduleTask);
        }

        /// <summary>
        /// Uninstall SAP Customers Synchronizer plugin
        /// </summary>
        public override void Uninstall()
        {
            if (_widgetSettings.ActiveWidgetSystemNames.Contains(DefaultsInfo.SystemName))
            {
                _widgetSettings.ActiveWidgetSystemNames.Remove(DefaultsInfo.SystemName);
                _settingService.SaveSetting(_widgetSettings);
            }

            _settingService.DeleteSetting<SapCustomersSynchronizerConfigurationSettings>();

            _localizationService.DeletePluginLocaleResources("Plugins.Synchronizers.SAPCustomers");

            ScheduleTask scheduleTask = _scheduleTaskService.GetTaskByType(DefaultsInfo.TaskType);
            _scheduleTaskService.DeleteTask(scheduleTask);

            base.Uninstall();
        }
    }
}
