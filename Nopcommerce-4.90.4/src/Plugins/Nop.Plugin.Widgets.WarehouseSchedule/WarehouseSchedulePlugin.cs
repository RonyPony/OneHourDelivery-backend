using Nop.Core.Domain.Cms;
using Nop.Data;
using Nop.Plugin.Widgets.WarehouseSchedule.Domains;
using Nop.Services.Cms;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Plugins;
using Nop.Web.Framework.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        private async Task AddLocaleResourcesAsync(Dictionary<string, string> resources, string languageCulture)
        {
            foreach (var resource in resources)
            {
                await _localizationService.AddOrUpdateLocaleResourceAsync(resource.Key, resource.Value,
                    languageCulture);
            }
        }

        private async Task InsertDaysAsync()
        {
            if ((await _dayRepository.GetAllAsync(query => query)).Any())
                return;

            const int maxDays = 7;
            string[] daysNames = {
                $"{WarehouseScheduleDefaults.LocaleResourcesPrefix}.Days.Sunday",
                $"{WarehouseScheduleDefaults.LocaleResourcesPrefix}.Days.Monday",
                $"{WarehouseScheduleDefaults.LocaleResourcesPrefix}.Days.Tuesday",
                $"{WarehouseScheduleDefaults.LocaleResourcesPrefix}.Days.Wednesday",
                $"{WarehouseScheduleDefaults.LocaleResourcesPrefix}.Days.Thursday",
                $"{WarehouseScheduleDefaults.LocaleResourcesPrefix}.Days.Friday",
                $"{WarehouseScheduleDefaults.LocaleResourcesPrefix}.Days.Saturday"
            };

            var days = new List<Day>();
            for (int dayId = 0; dayId < maxDays; dayId++)
            {
                days.Add(new Day {
                    Id = dayId,
                    Name = daysNames[dayId]
                });
            }

            await _dayRepository.InsertAsync(days);
        }

        #endregion

        #region Methods

        ///<inheritdoc/>
        public override async Task InstallAsync()
        {
            // Add widget to active widgets
            if (!_widgetSettings.ActiveWidgetSystemNames.Contains(WarehouseScheduleDefaults.SystemName))
            {
                _widgetSettings.ActiveWidgetSystemNames.Add(WarehouseScheduleDefaults.SystemName);
                await _settingService.SaveSettingAsync(_widgetSettings);
            }

            // Add language resources
            var language = (await _languageService.GetAllLanguagesAsync()).FirstOrDefault(x =>
                x.LanguageCulture.Contains("es-", StringComparison.InvariantCultureIgnoreCase));

            if (language != null)
            {
                await AddLocaleResourcesAsync(LocaleResources.SpanishResources, language.LanguageCulture);
            }

            await AddLocaleResourcesAsync(LocaleResources.EnglishResources, "en-US");

            //Add days data
            await InsertDaysAsync();

            await base.InstallAsync();
        }

        ///<inheritdoc/>
        public override async Task UninstallAsync()
        {
            if (_widgetSettings.ActiveWidgetSystemNames.Contains(WarehouseScheduleDefaults.SystemName))
            {
                _widgetSettings.ActiveWidgetSystemNames.Remove(WarehouseScheduleDefaults.SystemName);
                await _settingService.SaveSettingAsync(_widgetSettings);
            }

            await _localizationService.DeleteLocaleResourcesAsync(WarehouseScheduleDefaults.LocaleResourcesPrefix);

            await base.UninstallAsync();
        }

        ///<inheritdoc/>
        public Task<IList<string>> GetWidgetZonesAsync()
            => Task.FromResult<IList<string>>(new List<string> { AdminWidgetZones.WarehouseDetailsBottom });

        ///<inheritdoc/>
        public Type GetWidgetViewComponent(string widgetZone)
            => typeof(Components.WarehouseScheduleViewComponent);

        #endregion
    }
}
