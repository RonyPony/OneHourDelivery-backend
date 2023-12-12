using Nop.Core;
using Nop.Core.Domain.Cms;
using Nop.Core.Domain.Localization;
using Nop.Core.Domain.Tasks;
using Nop.Core.Domain.Topics;
using Nop.Plugin.Widgets.ProductAvailability.Domains;
using Nop.Services.Cms;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Plugins;
using Nop.Services.Seo;
using Nop.Services.Tasks;
using Nop.Services.Topics;
using Nop.Web.Framework.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace Nop.Plugin.Widgets.ProductAvailability
{
    /// <summary>
    /// Main file for this plug-in
    /// </summary>
    public sealed class ProductAvailabilityPlugin : BasePlugin, IWidgetPlugin
    {
        #region Fields

        private readonly WidgetSettings _widgetSettings;
        private readonly ISettingService _settingService;
        private readonly ILocalizationService _localizationService;
        private readonly IWebHelper _webHelper;
        private readonly ILanguageService _languageService;
        private readonly ITopicService _topicService;
        private readonly IUrlRecordService _urlRecordService;
        private readonly ILocalizedEntityService _localizedEntityService;
        private readonly IScheduleTaskService _scheduleTaskService;

        #endregion

        #region Ctor

        /// <summary>
        /// Initializes a new instance of ProductAvailabilityPlugin
        /// </summary>
        /// <param name="widgetSettings">An implementation of <see cref="WidgetSettings"/></param>
        /// <param name="settingService">An implementation of <see cref="ISettingService"/></param>
        /// <param name="localizationService">An implementation of <see cref="ILocalizationService"/></param>
        /// <param name="webHelper">An implementation of <see cref="IWebHelper"/></param>
        /// <param name="languageService">An implementation of <see cref="ILanguageService"/></param>
        /// <param name="topicService">An implementation of <see cref="ITopicService"/></param>
        /// <param name="urlRecordService">An implementation of <see cref="IUrlRecordService"/></param>
        /// <param name="localizedEntityService">An implementation of <see cref="ILocalizedEntityService"/></param>
        /// <param name="scheduleTaskService">An implementation of <see cref="IScheduleTaskService"/></param>
        public ProductAvailabilityPlugin(WidgetSettings widgetSettings,
            ISettingService settingService,
            ILocalizationService localizationService,
            IWebHelper webHelper,
            ILanguageService languageService,
            ITopicService topicService,
            IUrlRecordService urlRecordService,
            ILocalizedEntityService localizedEntityService,
            IScheduleTaskService scheduleTaskService)
        {
            _widgetSettings = widgetSettings;
            _settingService = settingService;
            _localizationService = localizationService;
            _webHelper = webHelper;
            _languageService = languageService;
            _topicService = topicService;
            _urlRecordService = urlRecordService;
            _localizedEntityService = localizedEntityService;
            _scheduleTaskService = scheduleTaskService;
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

        private void InsertInventorySearchTopicPage()
        {
            int defaultLanguageId = 1;

            var topic = new Topic
            {
                Title = _localizationService.GetResource($"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Topic.InventorySearch.Title", defaultLanguageId),
                DisplayOrder = 0,
                IncludeInFooterColumn3 = true,
                Published = true,
                IncludeInSitemap = true,
                SystemName = ProductAvailabilityDefault.InventorySearchTopicPageSystemName
            };

            _topicService.InsertTopic(topic);

            // Insert search engine optimazer
            _urlRecordService.SaveSlug(topic, ProductAvailabilityDefault.InventorySearchTopicPageSeName, 0);
            Language language = _languageService.GetAllLanguages().FirstOrDefault(x => x.LanguageCulture.Contains("es-"));

            if (language != null)
            {
                _urlRecordService.SaveSlug(topic, ProductAvailabilityDefault.InventorySearchTopicPageSeName, language.Id);

                // Insert localized topic page title
                _localizedEntityService.SaveLocalizedValue(topic,
                    x => x.Title,
                    _localizationService.GetResource($"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Topic.InventorySearch.Title", language.Id),
                    language.Id);
            }
        }

        private void DeleteInventorySearchTopicPage()
        {
            var topic = _topicService.GetTopicBySystemName(ProductAvailabilityDefault.InventorySearchTopicPageSystemName);

            if (topic == null)
                return;

            _topicService.DeleteTopic(topic);
        }

        private void InsertInventoryScheduleTask()
        {
            if (_scheduleTaskService.GetTaskByType(ProductAvailabilityDefault.ScheduleTaskType) != null)
                return;

            _scheduleTaskService.InsertTask(new ScheduleTask
            {
                Enabled = true,
                Seconds = ProductAvailabilityDefault.SecondsIn12Hours,
                Name = ProductAvailabilityDefault.ScheduleTaskName,
                Type = ProductAvailabilityDefault.ScheduleTaskType,
            });
        }

        private void DeleteInventoryScheduleTask()
        {
            if (_scheduleTaskService.GetTaskByType(ProductAvailabilityDefault.ScheduleTaskType) is ScheduleTask task)
                _scheduleTaskService.DeleteTask(task);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets Configuration Page Url
        /// </summary>
        /// <returns></returns>
        public override string GetConfigurationPageUrl()
        {
            return $"{_webHelper.GetStoreLocation()}Admin/ProductAvailability/Configure";
        }

        /// <summary>
        /// Installs the plug-in
        /// </summary>
        public override void Install()
        {
            // Add widget to active widgets
            if (!_widgetSettings.ActiveWidgetSystemNames.Contains(ProductAvailabilityDefault.SystemName))
            {
                _widgetSettings.ActiveWidgetSystemNames.Add(ProductAvailabilityDefault.SystemName);
                _settingService.SaveSetting(_widgetSettings);
            }

            // Insert locales
            var language = _languageService.GetAllLanguages().FirstOrDefault(x => x.LanguageCulture.Contains("es-"));
            if (language != null)
                AddLocaleResources(LocaleResources.SpanishResources, language.LanguageCulture);
            AddLocaleResources(LocaleResources.EnglishResources, "en-US");

            // Insert topic page for inventory search
            InsertInventorySearchTopicPage();

            // Insert schedule task for inventory sync
            InsertInventoryScheduleTask();

            base.Install();
        }

        /// <summary>
        /// Uninstalls the plug-in
        /// </summary>
        public override void Uninstall()
        {
            // Delete settings
            if (_widgetSettings.ActiveWidgetSystemNames.Contains(ProductAvailabilityDefault.SystemName))
            {
                _widgetSettings.ActiveWidgetSystemNames.Remove(ProductAvailabilityDefault.SystemName);
                _settingService.SaveSetting(_widgetSettings);
            }

            _settingService.DeleteSetting<ProductAvailabilitySettings>();

            // Delete locales
            _localizationService.DeletePluginLocaleResources(ProductAvailabilityDefault.LocaleResourcesPrefix);

            // Delete topic page for inventory search
            DeleteInventorySearchTopicPage();

            // Delete schedule task for inventory sync
            DeleteInventoryScheduleTask();

            base.Uninstall();
        }

        /// <summary>
        /// Returns a boolean indicating if this plugin will be hidden or not in the widget list of nopCommerce.
        /// </summary>
        public bool HideInWidgetList => true;

        /// <summary>
        /// Gets widget zones where this widget should be rendered.
        /// </summary>
        /// <returns>An implementation of <see cref="IList{T}"/> where T is <see cref="string"/>.</returns>
        public IList<string> GetWidgetZones()
            => new List<string> { PublicWidgetZones.ProductDetailsEssentialBottom };

        /// <summary>
        /// Gets a name of a view component for displaying widget.
        /// </summary>
        /// <param name="widgetZone">Name of the widget zone.</param>
        /// <returns>View component name.</returns>
        public string GetWidgetViewComponentName(string widgetZone) => "ProductAvailability";

        #endregion
    }
}
