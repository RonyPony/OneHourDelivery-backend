using Nop.Plugin.Misc.Alanube;
using Nop.Services.Plugins;
using Nop.Services.Localization;
using Nop.Services.Security;
using Nop.Web.Framework.Events;
using Nop.Web.Framework.Menu;
using Nop.Web.Framework.Mvc.Routing;

namespace Nop.Plugin.Misc.Alanube.Services;

/// <summary>
/// Adds the Alanube administration menu.
/// </summary>
public sealed class EventConsumer : BaseAdminMenuCreatedEventConsumer
{
    private readonly ILocalizationService _localizationService;
    private readonly IPermissionService _permissionService;
    private readonly INopUrlHelper _nopUrlHelper;

    public EventConsumer(
        IPluginManager<IPlugin> pluginManager,
        ILocalizationService localizationService,
        IPermissionService permissionService,
        INopUrlHelper nopUrlHelper) : base(pluginManager)
    {
        _localizationService = localizationService;
        _permissionService = permissionService;
        _nopUrlHelper = nopUrlHelper;
    }

    protected override async Task<bool> CheckAccessAsync()
    {
        return await _permissionService.AuthorizeAsync(StandardPermission.Configuration.MANAGE_PLUGINS);
    }

    protected override string PluginSystemName => "Misc.Alanube";

    protected override MenuItemInsertType InsertType => MenuItemInsertType.After;

    protected override string AfterMenuSystemName => "Configuration";

    protected override async Task<AdminMenuItem> GetAdminMenuItemAsync(IPlugin plugin)
    {
        var menu = new AdminMenuItem
        {
            Title = await _localizationService.GetResourceAsync("Plugins.Misc.Alanube.AdminMenu.Title"),
            Visible = true,
            SystemName = "Alanube-Main-Menu",
            IconClass = "fa-file-invoice"
        };

        menu.ChildNodes.Add(new AdminMenuItem
        {
            Title = await _localizationService.GetResourceAsync("Plugins.Misc.Alanube.AdminMenu.Companies"),
            Url = _nopUrlHelper.RouteUrl(AlanubeDefaults.CompaniesRouteName),
            Visible = true,
            SystemName = "Alanube-Companies-Menu",
            IconClass = "fa-building"
        });

        menu.ChildNodes.Add(new AdminMenuItem
        {
            Title = await _localizationService.GetResourceAsync("Plugins.Misc.Alanube.AdminMenu.Configuration"),
            Url = _nopUrlHelper.RouteUrl(AlanubeDefaults.ConfigurationRouteName),
            Visible = true,
            SystemName = "Alanube-Configuration-Menu",
            IconClass = "fa-cog"
        });

        menu.ChildNodes.Add(new AdminMenuItem
        {
            Title = await _localizationService.GetResourceAsync("Plugins.Misc.Alanube.AdminMenu.Offices"),
            Url = _nopUrlHelper.RouteUrl(AlanubeDefaults.OfficesRouteName),
            Visible = true,
            SystemName = "Alanube-Offices-Menu",
            IconClass = "fa-map-marker"
        });

        return menu;
    }
}
