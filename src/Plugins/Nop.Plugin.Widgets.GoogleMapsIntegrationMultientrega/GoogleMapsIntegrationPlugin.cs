using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Nop.Core.Domain.Cms;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Shipping;
using Nop.Core.Domain.Tasks;
using Nop.Plugin.Widgets.GoogleMapsIntegrationMultientrega.Domains;
using Nop.Plugin.Widgets.GoogleMapsIntegrationMultientrega.Helpers;
using Nop.Plugin.Widgets.GoogleMapsIntegrationMultientrega.Services;
using Nop.Services.Cms;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Plugins;
using Nop.Services.Shipping;
using Nop.Services.Shipping.Tracking;
using Nop.Services.Tasks;
using Nop.Web.Framework.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nop.Plugin.Widgets.GoogleMapsIntegrationMultientrega
{
    /// <summary>
    /// Represents the main file for Google Maps Integration Plugin.
    /// </summary>
    public sealed class GoogleMapsIntegrationPlugin : BasePlugin, IWidgetPlugin, IShippingRateComputationMethod
    {
        #region Fields

        private readonly WidgetSettings _widgetSettings;
        private readonly ILanguageService _languageService;
        private readonly ILocalizationService _localizationService;
        private readonly ISettingService _settingService;
        private readonly IUrlHelperFactory _urlHelperFactory;
        private readonly IActionContextAccessor _actionContextAccessor;
        private readonly IScheduleTaskService _scheduleTaskService;
        private readonly IShippingService _shippingService;
        private readonly IMultientregaAddressService _multientregaAddressService;
        private readonly ILogger _logger;

        #endregion

        #region Ctor

        /// <summary>
        /// Initializes a new instance of <see cref="GoogleMapsIntegrationPlugin"/>.
        /// </summary>
        /// <param name="widgetSettings">An instance of <see cref="WidgetSettings"/>.</param>
        /// <param name="languageService">An implementation of <see cref="ILanguageService"/>.</param>
        /// <param name="localizationService">An implementation of <see cref="ILocalizationService"/>.</param>
        /// <param name="settingService">An implementation of <see cref="ISettingService"/>.</param>
        /// <param name="urlHelperFactory">An implementation of <see cref="IUrlHelperFactory"/>.</param>
        /// <param name="actionContextAccessor">An implementation of <see cref="IActionContextAccessor"/>.</param>
        /// <param name="scheduleTaskService">An implementation of <see cref="IScheduleTaskService"/>.</param>
        /// <param name="shippingService">An implementation of <see cref="IShippingService"/>.</param>
        /// <param name="multientregaAddressService">An implementation of <see cref="IMultientregaAddressService"/>.</param>
        /// <param name="logger">An implementation of <see cref="ILogger"/>.</param>
        public GoogleMapsIntegrationPlugin(
            WidgetSettings widgetSettings,
            ILanguageService languageService,
            ILocalizationService localizationService,
            ISettingService settingService,
            IUrlHelperFactory urlHelperFactory,
            IActionContextAccessor actionContextAccessor,
            IScheduleTaskService scheduleTaskService,
            IShippingService shippingService,
            IMultientregaAddressService multientregaAddressService,
            ILogger logger)
        {
            _widgetSettings = widgetSettings;
            _languageService = languageService;
            _localizationService = localizationService;
            _settingService = settingService;
            _urlHelperFactory = urlHelperFactory;
            _actionContextAccessor = actionContextAccessor;
            _scheduleTaskService = scheduleTaskService;
            _shippingService = shippingService;
            _multientregaAddressService = multientregaAddressService;
            _logger = logger;
        }

        #endregion

        #region Utilities

        private void InsertLanguagesResources()
        {
            var language = _languageService.GetAllLanguages().FirstOrDefault(x => x.LanguageCulture.Contains("es-"));

            if (language != null)
            {
                AddLocaleResources(GoogleMapsIntegrationLocaleResources.SpanishResources, language.LanguageCulture);
            }

            AddLocaleResources(GoogleMapsIntegrationLocaleResources.EnglishResources, "en-US");
        }

        private void AddLocaleResources(Dictionary<string, string> resources, string languageLanguageCulture)
        {
            foreach (var resource in resources)
            {
                _localizationService.AddOrUpdatePluginLocaleResource(resource.Key, resource.Value, languageLanguageCulture);
            }
        }
        private void InsertMultientregasStructureScheduleTask()
        {
            if (_scheduleTaskService.GetTaskByType(Defaults.ScheduleTaskType) != null)
                return;

            _scheduleTaskService.InsertTask(new ScheduleTask
            {
                Enabled = true,
                Seconds = Defaults.SecondsInAWeek,
                Name = Defaults.ScheduleTaskName,
                Type = Defaults.ScheduleTaskType,
            });
        }

        private void DeleteMultientregasStructureScheduleTask()
        {
            if (_scheduleTaskService.GetTaskByType(Defaults.ScheduleTaskType) is ScheduleTask task)
                _scheduleTaskService.DeleteTask(task);
        }

        private void CustomizeAddressSettings()
        {
            var addressSettings = _settingService.LoadSetting<AddressSettings>();
            var customerSettings = _settingService.LoadSetting<CustomerSettings>();

            if (addressSettings != null)
            {
                addressSettings.StateProvinceEnabled = false;
                addressSettings.CityEnabled = false;
                addressSettings.CityRequired = false;
                addressSettings.ZipPostalCodeEnabled = false;
                addressSettings.ZipPostalCodeRequired = false;
                addressSettings.FaxEnabled = false;
                addressSettings.FaxRequired = false;
            }

            _settingService.SaveSetting(addressSettings);

            if (customerSettings != null)
            {
                customerSettings.StateProvinceEnabled = false;
                customerSettings.CityEnabled = false;
                customerSettings.CityRequired = false;
                customerSettings.ZipPostalCodeEnabled = false;
                customerSettings.ZipPostalCodeRequired = false;
                customerSettings.FaxEnabled = false;
                customerSettings.FaxRequired = false;
            }

            _settingService.SaveSetting(customerSettings);
        }

        private void ResetAddressSettings()
        {
            var addressSettings = _settingService.LoadSetting<AddressSettings>();
            var customerSettings = _settingService.LoadSetting<CustomerSettings>();

            if (addressSettings != null)
            {
                addressSettings.StateProvinceEnabled = true;
                addressSettings.CityEnabled = true;
                addressSettings.CityRequired = true;
                addressSettings.ZipPostalCodeEnabled = true;
                addressSettings.ZipPostalCodeRequired = true;
                addressSettings.FaxEnabled = true;
                addressSettings.FaxRequired = true;
            }

            _settingService.SaveSetting(addressSettings);

            if (customerSettings != null)
            {
                customerSettings.StateProvinceEnabled = true;
                customerSettings.CityEnabled = true;
                customerSettings.CityRequired = true;
                customerSettings.ZipPostalCodeEnabled = true;
                customerSettings.ZipPostalCodeRequired = true;
                customerSettings.FaxEnabled = true;
                customerSettings.FaxRequired = true;
            }

            _settingService.SaveSetting(customerSettings);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Determines whether to hide or not the plugin in the widget list.
        /// </summary>
        public bool HideInWidgetList => false;

        ///<inheritdoc/>
        public ShippingRateComputationMethodType ShippingRateComputationMethodType => ShippingRateComputationMethodType.Offline;

        ///<inheritdoc/>
        public IShipmentTracker ShipmentTracker => null;

        /// <summary>
        /// Gets a name of a view component for displaying widget.
        /// </summary>
        /// <param name="widgetZone">Name of the widget zone.</param>
        /// <returns>The name of the view component for this plugin.</returns>
        public string GetWidgetViewComponentName(string widgetZone)
            => Defaults.WidgetZonesViewComponentNamesDictionary[widgetZone];

        /// <summary>
        /// Returns the widget zones for this plugin.
        /// </summary>
        /// <returns>An instance of <see cref="List{T}"/> where T the name of a widget zone used by this plugin.</returns>
        public IList<string> GetWidgetZones() => new List<string>
        {
            AdminWidgetZones.OrderAddressDetailsBottom,
            PublicWidgetZones.CheckoutBillingAddressMiddle,
            PublicWidgetZones.CheckoutShippingAddressMiddle
        };

        /// <summary>
        /// Gets the configuration page URL.
        /// </summary>
        public override string GetConfigurationPageUrl()
            => _urlHelperFactory.GetUrlHelper(_actionContextAccessor.ActionContext).RouteUrl(Defaults.ConfigurationRouteName);

        /// <summary>
        /// Installs Google Maps Integration plugin.
        /// </summary>
        public override void Install()
        {
            InsertLanguagesResources();
            InsertMultientregasStructureScheduleTask();
            CustomizeAddressSettings();
            if (!_widgetSettings.ActiveWidgetSystemNames.Contains(Defaults.SystemName))
            {
                _widgetSettings.ActiveWidgetSystemNames.Add(Defaults.SystemName);
                _settingService.SaveSetting(_widgetSettings);
            }

            base.Install();
        }

        /// <summary>
        /// Uninstalls Google Maps Integration plugin.
        /// </summary>
        public override void Uninstall()
        {
            DeleteMultientregasStructureScheduleTask();
            ResetAddressSettings();
            if (_widgetSettings.ActiveWidgetSystemNames.Contains(Defaults.SystemName))
            {
                _widgetSettings.ActiveWidgetSystemNames.Remove(Defaults.SystemName);
                _settingService.SaveSetting(_widgetSettings);
            }

            _settingService.DeleteSetting<PluginConfigurationSettings>();
            _localizationService.DeletePluginLocaleResources(Defaults.ResourcesNamePrefix);

            base.Uninstall();
        }

        ///<inheritdoc/>
        public GetShippingOptionResponse GetShippingOptions(GetShippingOptionRequest getShippingOptionRequest)
        {
            if (getShippingOptionRequest == null)
                throw new ArgumentNullException(nameof(getShippingOptionRequest));

            var response = new GetShippingOptionResponse();

            if (getShippingOptionRequest.Items == null || !getShippingOptionRequest.Items.Any())
            {
                response.AddError("No shipment items");
                return response;
            }

            if (getShippingOptionRequest.ShippingAddress == null)
            {
                response.AddError("Shipping address is not set");
                return response;
            }

            try
            {
                var pluginSettings = _settingService.LoadSetting<PluginConfigurationSettings>();
                response.ShippingOptions = new List<ShippingOption>
                {
                    new ShippingOption
                    {
                        Name = Defaults.ShippingMethodName,
                        ShippingRateComputationMethodSystemName = Defaults.SystemName,
                        Description = pluginSettings.ShippingOptionDescription,
                        IsPickupInStore = false,
                        Rate = _multientregaAddressService.GetTaxRateByAddressId(getShippingOptionRequest.ShippingAddress.Id)
                    }
                };
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, e);
                response.AddError(e.Message);
                return response;
            }

            return response;
        }

        ///<inheritdoc/>
        public decimal? GetFixedRate(GetShippingOptionRequest getShippingOptionRequest) => null;

        #endregion
    }
}
