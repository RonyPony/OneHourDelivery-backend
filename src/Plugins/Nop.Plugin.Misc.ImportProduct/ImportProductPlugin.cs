using LinqToDB.Common.Internal.Cache;
using Nop.Core;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Cms;
using Nop.Services.Common;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Plugins;
    
namespace Nop.Plugin.Misc.ImportProduct
{
    /// <summary>
    /// Main file for this plug-in
    /// </summary>
    public sealed class ImportProductPlugin : BasePlugin, IMiscPlugin
    {
        #region Fields
        private readonly WidgetSettings _widgetSettings;
        private readonly ISettingService _settingService;

        #endregion

        #region Ctor
        /// <summary>
        /// Initializes a new instance of ImportProductPlugin>.
        /// </summary>
        /// <param name="widgetSettings">An implementation of <see cref="WidgetSettings"/>.</param>
        /// <param name="settingService">An implementation of <see cref="ISettingService"/>.</param>
        public ImportProductPlugin(WidgetSettings widgetSettings, ISettingService settingService)
        {
            _widgetSettings = widgetSettings;
            _settingService = settingService;
        }
        #endregion
        /// <summary>
        /// Installs the plug-in
        /// </summary>
        public override void Install()
        {
            // Add widget to active widgets
            if (!_widgetSettings.ActiveWidgetSystemNames.Contains(ImportProductDefaults.SystemName))
            {
                var catalogSettings = _settingService.LoadSetting<CatalogSettings>();
                catalogSettings.ExportImportAllowDownloadImages = true;
                _widgetSettings.ActiveWidgetSystemNames.Add(ImportProductDefaults.SystemName);
                _settingService.SaveSetting(catalogSettings);
                _settingService.SaveSetting(_widgetSettings);
                _settingService.ClearCache();
            }
            base.Install();
        }

        /// <summary>
        /// Uninstalls the plug-in
        /// </summary>
        public override void Uninstall()
        {
            if (_widgetSettings.ActiveWidgetSystemNames.Contains(ImportProductDefaults.SystemName))
            {
                _widgetSettings.ActiveWidgetSystemNames.Remove(ImportProductDefaults.SystemName);
                _settingService.SaveSetting(_widgetSettings);
            }
            base.Uninstall();
        }
    }
}
