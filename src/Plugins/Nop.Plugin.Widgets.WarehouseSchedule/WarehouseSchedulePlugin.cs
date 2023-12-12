using Nop.Core.Domain.Cms;
using Nop.Data;
using Nop.Plugin.Widgets.WarehouseSchedule.Domains;
using Nop.Services.Cms;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Plugins;
using Nop.Web.Framework.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace Nop.Plugin.Widgets.WarehouseSchedule
{
    /// <summary>
    /// WarehousePlugin Install Options
    /// </summary>
    public sealed class WarehouseSchedulePlugin : BasePlugin, IWidgetPlugin
    {
        #region Fields

        private readonly WidgetSettings _widgetSettings;
        private readonly ISettingService _settingService;
        private readonly IRepository<Day> _dayRepository;
        private readonly ILanguageService _languageService;
        private readonly ILocalizationService _localizationService;

        #endregion

        #region Properties

        ///<inheritdoc/>
        public bool HideInWidgetList => false;

        #endregion

        #region Ctor

        /// <summary>
        /// Initializes a new instance of ImportProductPlugin>.
        /// </summary>
        /// <param name="widgetSettings">An implementation of <see cref="WidgetSettings"/>.</param>
        /// <param name="settingService">An implementation of <see cref="ISettingService"/>.</param>
        /// <param name="dayRepository"></param>
        /// <param name="languageService"></param>
        /// <param name="localizationService"></param>
        public WarehouseSchedulePlugin(WidgetSettings widgetSettings,
            ISettingService settingService,
            IRepository<Day> dayRepository,
            ILanguageService languageService,
            ILocalizationService localizationService)
        {
            _widgetSettings = widgetSettings;
            _settingService = settingService;
            _dayRepository = dayRepository;
            _languageService = languageService;
            _localizationService = localizationService;
        }

        #endregion

        #region Utilities

        private void AddLocaleResources(Dictionary<string, string> resources, string languageLanguageCulture)
        {
            foreach (var resource in resources)
            {
                _localizationService.AddOrUpdatePluginLocaleResource(resource.Key, resource.Value,
                    languageLanguageCulture);
            }
        }

        private void InsertDays()
        {
            int maxDays = 7;
            string[] daysNames = {
                $"{WarehouseScheduleDefaults.LocaleResourcesPrefix}.Days.Sunday",
                $"{WarehouseScheduleDefaults.LocaleResourcesPrefix}.Days.Monday",
                $"{WarehouseScheduleDefaults.LocaleResourcesPrefix}.Days.Tuesday",
                $"{WarehouseScheduleDefaults.LocaleResourcesPrefix}.Days.Wednesday",
                $"{WarehouseScheduleDefaults.LocaleResourcesPrefix}.Days.Thursday",
                $"{WarehouseScheduleDefaults.LocaleResourcesPrefix}.Days.Friday",
                $"{WarehouseScheduleDefaults.LocaleResourcesPrefix}.Days.Saturday"
            };
            
            for (int dayId = 0; dayId < maxDays; dayId++)
            {
                _dayRepository.Insert(new Day {
                    Id = dayId,
                    Name = daysNames[dayId]
                });
            }
        }

        #endregion

        #region Methods

        ///<inheritdoc/>
        public override void Install()
        {
            // Add widget to active widgets
            if (!_widgetSettings.ActiveWidgetSystemNames.Contains(WarehouseScheduleDefaults.SystemName))
            {
                _widgetSettings.ActiveWidgetSystemNames.Add(WarehouseScheduleDefaults.SystemName);
                _settingService.SaveSetting(_widgetSettings);
            }

            // Add language resources
            var language = _languageService.GetAllLanguages().FirstOrDefault(x => x.LanguageCulture.Contains("es-"));

            if (language != null)
            {
                AddLocaleResources(LocaleResources.SpanishResources, language.LanguageCulture);
            }

            AddLocaleResources(LocaleResources.EnglishResources, "en-US");

            //Add days data
            InsertDays();

            base.Install();
        }

        ///<inheritdoc/>
        public override void Uninstall()
        {
            if (_widgetSettings.ActiveWidgetSystemNames.Contains(WarehouseScheduleDefaults.SystemName))
            {
                _widgetSettings.ActiveWidgetSystemNames.Remove(WarehouseScheduleDefaults.SystemName);
                _settingService.SaveSetting(_widgetSettings);
            }

            _localizationService.DeletePluginLocaleResources(WarehouseScheduleDefaults.LocaleResourcesPrefix);

            base.Uninstall();
        }

        ///<inheritdoc/>
        public IList<string> GetWidgetZones() => new List<string> { AdminWidgetZones.WarehouseDetailsBottom };

        ///<inheritdoc/>
        public string GetWidgetViewComponentName(string widgetZone) => "WarehouseSchedule";

        #endregion
    }
}
