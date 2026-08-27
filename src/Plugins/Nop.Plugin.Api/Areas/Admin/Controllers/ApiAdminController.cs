using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Plugin.Api.Areas.Admin.Models;
using Nop.Plugin.Api.Domain;
using Nop.Plugin.Api.MappingExtensions;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Messages;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;

namespace Nop.Plugin.Api.Areas.Admin.Controllers;

[AuthorizeAdmin]
[Area(AreaNames.ADMIN)]
public class ApiAdminController : BasePluginController
{
    private readonly ICustomerActivityService _customerActivityService;
    private readonly ILocalizationService _localizationService;
    private readonly INotificationService _notificationService;
    private readonly ISettingService _settingService;
    private readonly IStoreContext _storeContext;

    public ApiAdminController(
        IStoreContext storeContext,
        ISettingService settingService,
        ICustomerActivityService customerActivityService,
        ILocalizationService localizationService,
        INotificationService notificationService)
    {
        _storeContext = storeContext;
        _settingService = settingService;
        _customerActivityService = customerActivityService;
        _localizationService = localizationService;
        _notificationService = notificationService;
    }

    [HttpGet]
    public async Task<IActionResult> Settings()
    {
        var storeScope = await _storeContext.GetActiveStoreScopeConfigurationAsync();
        var apiSettings = await _settingService.LoadSettingAsync<ApiSettings>(storeScope);
        var model = apiSettings.ToModel();

        model.ActiveStoreScopeConfiguration = storeScope;

        if (model.EnableApi_OverrideForStore || storeScope == 0)
            await _settingService.SaveSettingAsync(apiSettings, x => x.EnableApi, storeScope, false);

        await _settingService.ClearCacheAsync();

        return View("~/Plugins/Nop.Plugin.Api/Areas/Admin/Views/ApiAdmin/Settings.cshtml", model);
    }

    [HttpPost]
    public async Task<IActionResult> Settings(ConfigurationModel model)
    {
        var storeScope = await _storeContext.GetActiveStoreScopeConfigurationAsync();
        var settings = model.ToEntity();

        if (model.EnableApi_OverrideForStore || storeScope == 0)
            await _settingService.SaveSettingAsync(settings, x => x.EnableApi, storeScope, false);

        await _settingService.ClearCacheAsync();

        await _customerActivityService.InsertActivityAsync("EditApiSettings", "Edit Api Settings");

        await _notificationService.SuccessNotificationAsync(await _localizationService.GetResourceAsync("Admin.Plugins.Saved"));

        return View("~/Plugins/Nop.Plugin.Api/Areas/Admin/Views/ApiAdmin/Settings.cshtml", model);
    }
}
