using System.Collections.Generic;
using System.Threading.Tasks;
using Nop.Core;
using Nop.Core.Domain.Customers;
using Nop.Plugin.Api.Domain;
using Nop.Services.Configuration;
using Nop.Services.Customers;
using Nop.Services.Localization;
using Nop.Services.Plugins;
using Nop.Web.Framework.Menu;

namespace Nop.Plugin.Api.Infrastructure;

public class ApiPlugin : BasePlugin, IAdminMenuPlugin
{
    private readonly ICustomerService _customerService;
    private readonly ILocalizationService _localizationService;
    private readonly ISettingService _settingService;
    private readonly IWebHelper _webHelper;

    public ApiPlugin(
        ISettingService settingService,
        ICustomerService customerService,
        ILocalizationService localizationService,
        IWebHelper webHelper)
    {
        _settingService = settingService;
        _customerService = customerService;
        _localizationService = localizationService;
        _webHelper = webHelper;
    }

    public override async Task InstallAsync()
    {
        await _localizationService.AddOrUpdateLocaleResourceAsync(new Dictionary<string, string>
        {
            ["Plugins.Api"] = "Api plugin",
            ["Plugins.Api.Admin.Menu.ManageClients"] = "Manage Api Clients",
            ["Plugins.Api.Admin.Configure"] = "Configure Web Api",
            ["Plugins.Api.Admin.GeneralSettings"] = "General Settings",
            ["Plugins.Api.Admin.EnableApi"] = "Enable Api",
            ["Plugins.Api.Admin.EnableApi.Hint"] = "By checking this settings you can Enable/Disable the Web Api",
            ["Plugins.Api.Admin.Menu.Title"] = "API",
            ["Plugins.Api.Admin.Menu.Settings.Title"] = "Settings",
            ["Plugins.Api.Admin.Page.Settings.Title"] = "Api Settings",
            ["Plugins.Api.Admin.Settings.GeneralSettingsTitle"] = "General Settings",
            ["Plugins.Api.Admin.Edit"] = "Edit",
            ["Api.Categories.Fields.Id.Invalid"] = "Id is invalid",
            ["Api.InvalidPropertyType"] = "Invalid Property Type",
            ["Api.InvalidType"] = "Invalid {0} type",
            ["Api.InvalidRequest"] = "Invalid request",
            ["Api.InvalidRootProperty"] = "Invalid root property",
            ["Api.NoJsonProvided"] = "No Json provided",
            ["Api.InvalidJsonFormat"] = "Json format is invalid",
            ["Api.Category.InvalidImageAttachmentFormat"] = "Invalid image attachment base64 format",
            ["Api.Category.InvalidImageSrc"] = "Invalid image source",
            ["Api.Category.InvalidImageSrcType"] = "You have provided an invalid image source/attachment "
        });

        await _settingService.SaveSettingAsync(new ApiSettings());

        var apiRole = await _customerService.GetCustomerRoleBySystemNameAsync(Constants.Roles.ApiRoleSystemName);

        if (apiRole == null)
        {
            apiRole = new CustomerRole
            {
                Name = Constants.Roles.ApiRoleName,
                Active = true,
                SystemName = Constants.Roles.ApiRoleSystemName
            };

            await _customerService.InsertCustomerRoleAsync(apiRole);
        }
        else if (!apiRole.Active)
        {
            apiRole.Active = true;
            await _customerService.UpdateCustomerRoleAsync(apiRole);
        }

        await base.InstallAsync();
    }

    public override async Task UninstallAsync()
    {
        await _localizationService.DeleteLocaleResourcesAsync("Plugins.Api");

        var apiRole = await _customerService.GetCustomerRoleBySystemNameAsync(Constants.Roles.ApiRoleSystemName);
        if (apiRole != null)
        {
            apiRole.Active = false;
            await _customerService.UpdateCustomerRoleAsync(apiRole);
        }

        await base.UninstallAsync();
    }

    public void ManageSiteMap(SiteMapNode rootNode)
        => ManageSiteMapAsync(rootNode).GetAwaiter().GetResult();

    public async Task ManageSiteMapAsync(SiteMapNode rootNode)
    {
        var pluginMenuName = await _localizationService.GetResourceAsync("Plugins.Api.Admin.Menu.Title");
        var settingsMenuName = await _localizationService.GetResourceAsync("Plugins.Api.Admin.Menu.Settings.Title");

        const string adminUrlPart = "Admin/";

        var pluginMainMenu = new SiteMapNode
        {
            Title = string.IsNullOrWhiteSpace(pluginMenuName) ? "API" : pluginMenuName,
            Visible = true,
            SystemName = "Api-Main-Menu",
            IconClass = "fa-genderless"
        };

        pluginMainMenu.ChildNodes.Add(new SiteMapNode
        {
            Title = string.IsNullOrWhiteSpace(settingsMenuName) ? "Settings" : settingsMenuName,
            Url = _webHelper.GetStoreLocation() + adminUrlPart + "ApiAdmin/Settings",
            Visible = true,
            SystemName = "Api-Settings-Menu",
            IconClass = "fa-genderless"
        });

        rootNode.ChildNodes.Add(pluginMainMenu);
    }
}
