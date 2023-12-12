using Nop.Core.Domain.Cms;
using Nop.Plugin.Synchronizers.CommonERPTables.Helpers;
using Nop.Services.Cms;
using Nop.Services.Configuration;
using Nop.Services.Plugins;
using System.Collections.Generic;

namespace Nop.Plugin.Synchronizers.CommonERPTables
{
    /// <summary>
    /// Represents the main file for this plugin.
    /// </summary>
    public sealed class CommonErpTablesPlugin : BasePlugin, IWidgetPlugin
    {
        private readonly WidgetSettings _widgetSettings;
        private readonly ISettingService _settingService;

        /// <summary>
        /// Initializes a new instance of CommonErpTablesPlugin with the values indicated as parameters.
        /// </summary>
        /// <param name="widgetSettings">Represents an instance of <see cref="WidgetSettings"/>.</param>
        /// <param name="settingService">Represents an implementation of <see cref="ISettingService"/>.</param>
        public CommonErpTablesPlugin(WidgetSettings widgetSettings, ISettingService settingService)
        {
            _widgetSettings = widgetSettings;
            _settingService = settingService;
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
        /// Uninstall SAP Customers Synchronizer plugin
        /// </summary>
        public override void Uninstall()
        {
            if (_widgetSettings.ActiveWidgetSystemNames.Contains(DefaultsInfo.SystemName))
            {
                _widgetSettings.ActiveWidgetSystemNames.Remove(DefaultsInfo.SystemName);
                _settingService.SaveSetting(_widgetSettings);
            }

            base.Uninstall();
        }
    }
}
