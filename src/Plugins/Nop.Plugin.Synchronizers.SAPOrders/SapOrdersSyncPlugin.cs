using Nop.Core;
using Nop.Core.Domain.Cms;
using Nop.Core.Domain.Tasks;
using Nop.Plugin.Synchronizers.SAPOrders.Domains;
using Nop.Services.Common;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Plugins;
using Nop.Services.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace Nop.Plugin.Synchronizers.SAPOrders
{
    /// <summary>
    /// Represents this plug-in's main class
    /// </summary>
    public class SapOrdersSyncPlugin : BasePlugin, IMiscPlugin
    {
        #region Fields

        private readonly IWebHelper _webHelper;
        private readonly IScheduleTaskService _scheduleTaskService;
        private readonly WidgetSettings _widgetSettings;
        private readonly ISettingService _settingService;
        private readonly ILanguageService _languageService;
        private readonly ILocalizationService _localizationService;

        #endregion

        #region Ctor

        /// <summary>
        /// Represents the constructor method for this class
        /// </summary>
        /// <param name="webHelper">Implementation of <see cref="IWebHelper"/></param>
        /// <param name="scheduleTaskService">Implementation of <see cref="IScheduleTaskService"/></param>
        /// <param name="widgetSettings">Implementation of <see cref="WidgetSettings"/></param>
        /// <param name="settingService">Implementation of <see cref="ISettingService"/></param>
        /// <param name="languageService">Implementation of <see cref="ILanguageService"/></param>
        /// <param name="localizationService">Implementation of <see cref="ILocalizationService"/></param>
        public SapOrdersSyncPlugin(IWebHelper webHelper, IScheduleTaskService scheduleTaskService,
            WidgetSettings widgetSettings, ISettingService settingService, ILanguageService languageService,
            ILocalizationService localizationService)
        {
            _webHelper = webHelper;
            _scheduleTaskService = scheduleTaskService;
            _widgetSettings = widgetSettings;
            _settingService = settingService;
            _languageService = languageService;
            _localizationService = localizationService;
        }

        #endregion

        #region Utilities

        private void InsertOrdersSyncScheduleTask()
        {
            var scheduleTask = new ScheduleTask
            {
                Name = SapOrdersSyncDefaults.TaskName,
                Type = SapOrdersSyncDefaults.TaskType,
                Seconds = SapOrdersSyncDefaults.TaskDuration
            };

            _scheduleTaskService.InsertTask(scheduleTask);
        }

        private void DeleteOrdersSyncScheduleTask()
        {
            ScheduleTask scheduleTask = _scheduleTaskService.GetTaskByType(SapOrdersSyncDefaults.TaskType);

            _scheduleTaskService.DeleteTask(scheduleTask);
        }

        private void InsertLanguageLocaleResources()
        {
            var language = _languageService.GetAllLanguages().FirstOrDefault(x => x.LanguageCulture.Contains("es-"));

            if (language != null)
            {
                AddLocaleResources(SapOrdersSyncResources.SpanishResources, language.LanguageCulture);
            }

            AddLocaleResources(SapOrdersSyncResources.EnglishResources, "en-US");
        }

        private void AddLocaleResources(Dictionary<string, string> resources, string languageLanguageCulture)
        {
            foreach (var resource in resources)
            {
                _localizationService.AddOrUpdatePluginLocaleResource(resource.Key, resource.Value,
                    languageLanguageCulture);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Installs SAP Orders Synchronizer plugin
        /// </summary>
        public override void Install()
        {
            if (!_widgetSettings.ActiveWidgetSystemNames.Contains(SapOrdersSyncDefaults.SystemName))
            {
                _widgetSettings.ActiveWidgetSystemNames.Add(SapOrdersSyncDefaults.SystemName);
                _settingService.SaveSetting(_widgetSettings);
            }

            InsertLanguageLocaleResources();

            InsertOrdersSyncScheduleTask();

            base.Install();
        }

        /// <summary>
        /// Uninstalls SAP Orders Synchronizer plugin
        /// </summary>
        public override void Uninstall()
        {
            if (_widgetSettings.ActiveWidgetSystemNames.Contains(SapOrdersSyncDefaults.SystemName))
            {
                _widgetSettings.ActiveWidgetSystemNames.Remove(SapOrdersSyncDefaults.SystemName);
                _settingService.SaveSetting(_widgetSettings);
            }

            _settingService.DeleteSetting<SapOrdersSyncSettings>();

            _localizationService.DeletePluginLocaleResources(SapOrdersSyncDefaults.LocaleResourcesPrefix);

            DeleteOrdersSyncScheduleTask();

            base.Uninstall();
        }

        /// <summary>
        /// Gets the configuration page URL
        /// </summary>
        public override string GetConfigurationPageUrl()
        {
            return $"{_webHelper.GetStoreLocation()}Admin/SapOrdersSync/Configure";
        }

        #endregion
    }
}
