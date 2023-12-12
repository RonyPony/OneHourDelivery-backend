using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Nop.Core.Domain.Cms;
using Nop.Core.Domain.Shipping;
using Nop.Core.Domain.Tasks;
using Nop.Plugin.Synchronizers.ASAP.Domains;
using Nop.Plugin.Synchronizers.ASAP.Models;
using Nop.Plugin.Synchronizers.ASAP.Resources;
using Nop.Services.Cms;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Plugins;
using Nop.Services.Shipping;
using Nop.Services.Shipping.Tracking;
using Nop.Services.Tasks;
using Nop.Web.Framework.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace Nop.Plugin.Synchronizers.ASAP
{
    /// <summary>
    /// Represents the main file for this plugin.
    /// </summary>
    public sealed class AsapDelivery : BasePlugin, IShippingRateComputationMethod, IWidgetPlugin
    {
        #region Fields

        private readonly WidgetSettings _widgetSettings;
        private readonly ISettingService _settingService;
        private readonly IActionContextAccessor _actionContextAccessor;
        private readonly IUrlHelperFactory _urlHelperFactory;
        private readonly ILanguageService _languageService;
        private readonly ILocalizationService _localizationService;
        private readonly IScheduleTaskService _scheduleTaskService;
        private readonly AsapShipmentTracker _asapShipmentTracker;

        #endregion

        #region Ctor

        /// <summary>
        /// Creates an instance of <see cref="AsapDelivery"/>
        /// </summary>
        /// <param name="urlHelperFactory">Represents an implementation of <see cref="IUrlHelperFactory"/>.</param>
        /// <param name="actionContextAccessor">Represents an implementation of <see cref="IActionContextAccessor"/>.</param>
        /// <param name="languageService">Represents an implementation of <see cref="ILanguageService"/>.</param>
        /// <param name="localizationService">Represents an implementation of <see cref="ILocalizationService"/>.</param>
        /// <param name="widgetSettings">Represents an instance of <see cref="WidgetSettings"/>.</param>
        /// <param name="settingService">Represents an implementation of <see cref="ISettingService"/>.</param>
        /// <param name="scheduleTaskService">Represents an implementation of <see cref="IScheduleTaskService"/>.</param>
        /// <param name="asapShipmentTracker">Represents an instance of <see cref="AsapShipmentTracker"/>.</param>
        public AsapDelivery(WidgetSettings widgetSettings, ISettingService settingService, IActionContextAccessor actionContextAccessor,
            IUrlHelperFactory urlHelperFactory, ILanguageService languageService, ILocalizationService localizationService,
            IScheduleTaskService scheduleTaskService, AsapShipmentTracker asapShipmentTracker)
        {
            _widgetSettings = widgetSettings;
            _settingService = settingService;
            _actionContextAccessor = actionContextAccessor;
            _urlHelperFactory = urlHelperFactory;
            _languageService = languageService;
            _localizationService = localizationService;
            _scheduleTaskService = scheduleTaskService;
            _asapShipmentTracker = asapShipmentTracker;
        }

        #endregion

        #region Methods

        /// <inheritdoc/>
        ShippingRateComputationMethodType IShippingRateComputationMethod.ShippingRateComputationMethodType => ShippingRateComputationMethodType.Offline;

        /// <inheritdoc/>
        IShipmentTracker IShippingRateComputationMethod.ShipmentTracker => _asapShipmentTracker;

        public bool HideInWidgetList => true;

        /// <summary>
        /// Gets the configuration page URL
        /// </summary>
        public override string GetConfigurationPageUrl() => _urlHelperFactory.GetUrlHelper(_actionContextAccessor.ActionContext).RouteUrl(AsapDeliveryDefaults.AsapConfigurationRoute);

        /// <summary>
        /// Installs Asap Delivery plugin
        /// </summary>
        public override void Install()
        {
            if (!_widgetSettings.ActiveWidgetSystemNames.Contains(AsapDeliveryDefaults.SystemName))
            {
                _widgetSettings.ActiveWidgetSystemNames.Add(AsapDeliveryDefaults.SystemName);
                _settingService.SaveSetting(_widgetSettings);
            }

            InsertCustomerScheduleTask();

            InsertLanguageLocaleResources();

            base.Install();
        }

        private void InsertCustomerScheduleTask()
        {
            var scheduleTask = new ScheduleTask
            {
                Name = AsapDeliveryDefaults.TaskName,
                Seconds = AsapDeliveryDefaults.TaskDuration,
                Type = AsapDeliveryDefaults.TaskType
            };

            _scheduleTaskService.InsertTask(scheduleTask);
        }

        private void InsertLanguageLocaleResources()
        {
            var language = _languageService.GetAllLanguages().FirstOrDefault(x => x.LanguageCulture.Contains("es"));

            if (language != null)
            {
                AddLocaleResources(AsapResources.SpanishResources, language.LanguageCulture);
            }

            AddLocaleResources(AsapResources.EnglishResources, "en-US");
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
        /// Uninstalls Asap Delivery plugin
        /// </summary>
        public override void Uninstall()
        {
            if (_widgetSettings.ActiveWidgetSystemNames.Contains(AsapDeliveryDefaults.SystemName))
            {
                _widgetSettings.ActiveWidgetSystemNames.Remove(AsapDeliveryDefaults.SystemName);
                _settingService.SaveSetting(_widgetSettings);
            }

            _localizationService.DeletePluginLocaleResources(AsapDeliveryDefaults.AsapConfigurationResources);

            ScheduleTask scheduleTask = _scheduleTaskService.GetTaskByType(AsapDeliveryDefaults.TaskType);
            _scheduleTaskService.DeleteTask(scheduleTask);

            base.Uninstall();
        }

        /// <inheritdoc/>
        GetShippingOptionResponse IShippingRateComputationMethod.GetShippingOptions(GetShippingOptionRequest getShippingOptionRequest)
        {
            var settings = _settingService.LoadSetting<AsapDeliveryConfigurationSettings>();

            return new GetShippingOptionResponse
            {
                ShippingOptions = new List<ShippingOption>
                {
                    new ShippingOption
                    {
                        Description = settings?.Description,
                        Name = AsapDeliveryDefaults.ShippingMethodName,
                        Rate =  settings?.Rate ?? 0M,
                        IsPickupInStore = false
                    }
                }
            };
        }

        /// <inheritdoc/>
        decimal? IShippingRateComputationMethod.GetFixedRate(GetShippingOptionRequest getShippingOptionRequest)
        {
            return null;
        }
        
        /// <summary>
        /// Returns the widget zones for this plugin.
        /// </summary>
        /// <returns>An implementation an implementation of <see cref="GetWidgetZones"/> </returns>
        public IList<string> GetWidgetZones()
        {

            return new List<string> { AdminWidgetZones.OrderShipmentDetailsButtons };
        }
        
        /// <summary>
        /// Gets a name of a view component for displaying widget
        /// </summary>
        /// <param name="widgetZone">Name of the widget zone</param>
        /// <returns>View component name</returns>
        public string GetWidgetViewComponentName(string widgetZone)
        {
            return "CustomAdminWidget";
        }

        #endregion
    }
}
