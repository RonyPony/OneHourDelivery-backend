using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Nop.Plugin.Misc.Alanube.Api;
using Nop.Plugin.Misc.Alanube.Api.Companies;
using Nop.Plugin.Misc.Alanube.Api.Offices;
using Nop.Plugin.Misc.Alanube.Domain;
using Nop.Plugin.Misc.Alanube.Configuration;
using Nop.Plugin.Misc.Alanube.Models;
using Nop.Plugin.Misc.Alanube.Services;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Messages;
using Nop.Services.Security;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;

namespace Nop.Plugin.Misc.Alanube.Controllers;

[Area(AreaNames.ADMIN)]
[AuthorizeAdmin]
[AutoValidateAntiforgeryToken]
public sealed class AlanubeAdminController : BasePluginController
{
    private readonly AlanubeSettings _settings;
    private readonly ILocalizationService _localizationService;
    private readonly ISettingService _settingService;
    private readonly INotificationService _notificationService;
    private readonly IAlanubeClient _alanubeClient;
    private readonly IAlanubeCompanyClient _alanubeCompanyClient;
    private readonly IAlanubeOfficeClient _alanubeOfficeClient;
    private readonly IAlanubeCatalogService _alanubeCatalogService;
    private readonly IAlanubeProductMappingService _productMappingService;

    public AlanubeAdminController(
        AlanubeSettings settings,
        ILocalizationService localizationService,
        ISettingService settingService,
        INotificationService notificationService,
        IAlanubeClient alanubeClient,
        IAlanubeCompanyClient alanubeCompanyClient,
        IAlanubeOfficeClient alanubeOfficeClient,
        IAlanubeCatalogService alanubeCatalogService,
        IAlanubeProductMappingService productMappingService)
    {
        _settings = settings;
        _localizationService = localizationService;
        _settingService = settingService;
        _notificationService = notificationService;
        _alanubeClient = alanubeClient;
        _alanubeCompanyClient = alanubeCompanyClient;
        _alanubeOfficeClient = alanubeOfficeClient;
        _alanubeCatalogService = alanubeCatalogService;
        _productMappingService = productMappingService;
    }

    [HttpGet]
    [CheckPermission(StandardPermission.Configuration.MANAGE_PLUGINS)]
    public async Task<IActionResult> ProductMapping(int productId)
    {
        var mapping = await _productMappingService.GetByProductIdAsync(productId);
        var model = new ProductMappingModel { ProductId = productId, GoodsServiceCode = mapping?.GoodsServiceCode, MeasurementUnitCode = mapping?.MeasurementUnitCode, TaxCode = mapping?.TaxCode, DescriptionOverride = mapping?.DescriptionOverride };
        await PopulateCatalogOptionsAsync(model);
        return View("~/Plugins/Misc.Alanube/Views/ProductMapping.cshtml", model);
    }

    [HttpPost]
    [CheckPermission(StandardPermission.Configuration.MANAGE_PLUGINS)]
    public async Task<IActionResult> ProductMapping(ProductMappingModel model)
    {
        try { await _productMappingService.SaveAsync(new AlanubeProductMapping { ProductId = model.ProductId, GoodsServiceCode = model.GoodsServiceCode, MeasurementUnitCode = model.MeasurementUnitCode, TaxCode = model.TaxCode, DescriptionOverride = model.DescriptionOverride }); }
        catch (ArgumentException exception) { ModelState.AddModelError(string.Empty, exception.Message); }
        if (!ModelState.IsValid) { await PopulateCatalogOptionsAsync(model); return View("~/Plugins/Misc.Alanube/Views/ProductMapping.cshtml", model); }
        _notificationService.SuccessNotification(await _localizationService.GetResourceAsync("Admin.Common.Updated"));
        return RedirectToRoute("Plugin.Alanube.ProductMapping", new { productId = model.ProductId });
    }

    private async Task PopulateCatalogOptionsAsync(ProductMappingModel model)
    {
        model.GoodsServices = (await _alanubeCatalogService.SearchAsync(AlanubeCatalogType.GoodsAndServices)).Select(x => new CatalogOptionModel { Code = x.ExternalCode, Description = x.Description }).ToList();
        model.MeasurementUnits = (await _alanubeCatalogService.SearchAsync(AlanubeCatalogType.MeasurementUnit)).Select(x => new CatalogOptionModel { Code = x.ExternalCode, Description = x.Description }).ToList();
    }

    [HttpGet]
    [CheckPermission(StandardPermission.Configuration.MANAGE_PLUGINS)]
    public async Task<IActionResult> DgiCatalogs()
    {
        var model = new DgiCatalogsModel();
        foreach (var type in Enum.GetValues<AlanubeCatalogType>())
        {
            var entries = await _alanubeCatalogService.SearchAsync(type);
            model.Catalogs.Add(new DgiCatalogSummaryModel { CatalogType = type, Count = entries.Count, LastSynchronizationUtc = entries.Count == 0 ? null : entries.Max(x => x.LastSyncedOnUtc) });
        }
        var synchronizationDates = model.Catalogs.Where(x => x.LastSynchronizationUtc.HasValue).Select(x => x.LastSynchronizationUtc.Value).ToList();
        model.LastSynchronizationUtc = synchronizationDates.Count == 0 ? null : synchronizationDates.Max();
        return View("~/Plugins/Misc.Alanube/Views/DgiCatalogs.cshtml", model);
    }

    [HttpPost]
    [CheckPermission(StandardPermission.Configuration.MANAGE_PLUGINS)]
    public async Task<IActionResult> SyncDgiCatalogs()
    {
        var result = await _alanubeCatalogService.SyncCatalogsAsync(HttpContext.RequestAborted);
        var failed = result.Items.Count(x => !x.Succeeded);
        _notificationService.SuccessNotification($"DGI catalogs synchronized: {result.Items.Sum(x => x.RecordsProcessed)} records. {failed} catalog(s) failed.");
        return RedirectToRoute("Plugin.Alanube.DgiCatalogs");
    }

    [HttpGet]
    [CheckPermission(StandardPermission.Configuration.MANAGE_PLUGINS)]
    public async Task<IActionResult> Configure()
    {
        var model = new ConfigurationModel
        {
            Enabled = _settings.Enabled,
            Environment = _settings.Environment,
            CompanyId = _settings.CompanyId,
            OfficeId = _settings.OfficeId,
            SandboxApiToken = _settings.SandboxApiToken,
            ProductionApiToken = _settings.ProductionApiToken
        };
        if (HasConfiguredApiToken())
        {
            try
            {
                var companies = await _alanubeCompanyClient.GetCompaniesAsync(cancellationToken: HttpContext.RequestAborted);
                if (companies.IsSuccessStatusCode)
                    model.Companies = companies.Body?.ToList() ?? new List<CompanyResponseDto>();

                if (!string.IsNullOrWhiteSpace(model.CompanyId))
                {
                    var offices = await _alanubeOfficeClient.GetOfficesAsync(model.CompanyId, HttpContext.RequestAborted);
                    if (offices.IsSuccessStatusCode)
                        model.Offices = offices.Body?.ToList() ?? new List<OfficeResponseDto>();
                }
            }
            catch (AlanubeApiException)
            {
                // Configure must remain available while credentials or Alanube are unavailable.
            }
        }
        return View("~/Plugins/Misc.Alanube/Views/Configure.cshtml", model);
    }

    private bool HasConfiguredApiToken() => _settings.Environment switch
    {
        AlanubeEnvironment.Sandbox => !string.IsNullOrWhiteSpace(_settings.SandboxApiToken),
        AlanubeEnvironment.Production => !string.IsNullOrWhiteSpace(_settings.ProductionApiToken),
        _ => false
    };

    [HttpGet]
    [CheckPermission(StandardPermission.Configuration.MANAGE_PLUGINS)]
    public async Task<IActionResult> Companies()
    {
        var model = new CompaniesModel();

        try
        {
            var response = await _alanubeCompanyClient.GetCompaniesAsync(cancellationToken: HttpContext.RequestAborted);
            if (response.IsSuccessStatusCode)
                model.Companies = response.Body?.ToList() ?? new List<CompanyResponseDto>();
            else
                model.ErrorMessage = Sanitize(response.Error?.Message) ?? $"Alanube returned HTTP status {response.StatusCode}.";
        }
        catch (AlanubeApiException exception)
        {
            model.ErrorMessage = Sanitize(exception.Error?.Message) ?? Sanitize(exception.Message) ?? "The companies could not be loaded.";
        }

        return View("~/Plugins/Misc.Alanube/Views/Companies.cshtml", model);
    }

    [HttpGet]
    [CheckPermission(StandardPermission.Configuration.MANAGE_PLUGINS)]
    public IActionResult CreateCompany() => View("~/Plugins/Misc.Alanube/Views/CompanyEdit.cshtml", new CompanyEditModel());

    [HttpPost]
    [CheckPermission(StandardPermission.Configuration.MANAGE_PLUGINS)]
    public async Task<IActionResult> CreateCompany(CompanyEditModel model)
    {
        if (!ModelState.IsValid)
            return View("~/Plugins/Misc.Alanube/Views/CompanyEdit.cshtml", model);

        var response = await _alanubeCompanyClient.CreateCompanyAsync(new CreateCompanyRequestDto
        {
            Ruc = model.Ruc, TypeRuc = model.TypeRuc, Type = model.Type, TradeName = model.TradeName, Qr = model.Qr,
            Certificates = new CompanyCertificatesRequestDto
            {
                Signature = new CompanyCertificateRequestDto { Content = model.SignatureCertificate },
                Authentication = new CompanyCertificateRequestDto { Content = model.AuthenticationCertificate }
            }
        }, HttpContext.RequestAborted);

        if (!response.IsSuccessStatusCode)
        {
            AddApiError(response);
            return View("~/Plugins/Misc.Alanube/Views/CompanyEdit.cshtml", model);
        }

        _notificationService.SuccessNotification(await _localizationService.GetResourceAsync("Admin.Common.Added"));
        return RedirectToRoute(AlanubeDefaults.CompaniesRouteName);
    }

    [HttpGet]
    [CheckPermission(StandardPermission.Configuration.MANAGE_PLUGINS)]
    public async Task<IActionResult> EditCompany(string id)
    {
        var response = await _alanubeCompanyClient.GetCompanyAsync(id, HttpContext.RequestAborted);
        if (!response.IsSuccessStatusCode || response.Body is null)
        {
            _notificationService.ErrorNotification(Sanitize(response.Error?.Message) ?? $"Alanube returned HTTP status {response.StatusCode}.");
            return RedirectToRoute(AlanubeDefaults.CompaniesRouteName);
        }

        return View("~/Plugins/Misc.Alanube/Views/CompanyEdit.cshtml", ToModel(response.Body));
    }

    [HttpPost]
    [CheckPermission(StandardPermission.Configuration.MANAGE_PLUGINS)]
    public async Task<IActionResult> EditCompany(CompanyEditModel model)
    {
        if (!ModelState.IsValid)
            return View("~/Plugins/Misc.Alanube/Views/CompanyEdit.cshtml", model);

        var response = await _alanubeCompanyClient.UpdateCompanyAsync(model.Id, new UpdateCompanyRequestDto
        {
            TradeName = model.TradeName, Qr = model.Qr,
            Certificates = new CompanyCertificatesRequestDto
            {
                Signature = new CompanyCertificateRequestDto { Content = model.SignatureCertificate },
                Authentication = new CompanyCertificateRequestDto { Content = model.AuthenticationCertificate }
            }
        }, HttpContext.RequestAborted);

        if (!response.IsSuccessStatusCode)
        {
            AddApiError(response);
            return View("~/Plugins/Misc.Alanube/Views/CompanyEdit.cshtml", model);
        }

        _notificationService.SuccessNotification(await _localizationService.GetResourceAsync("Admin.Common.Updated"));
        return RedirectToRoute(AlanubeDefaults.CompaniesRouteName);
    }

    private static CompanyEditModel ToModel(CompanyResponseDto company) => new()
    {
        Id = company.Id, Ruc = company.Ruc, TypeRuc = company.TypeRuc ?? 2,
        Type = company.Type ?? AlanubeCompanyType.Associated, TradeName = company.TradeName
    };

    private void AddApiError<T>(AlanubeApiResponse<T> response)
    {
        var message = Sanitize(response.Error?.Message) ?? $"Alanube returned HTTP status {response.StatusCode}.";
        if (!string.IsNullOrWhiteSpace(response.Error?.Code))
            message = $"{Sanitize(response.Error.Code)}: {message}";
        ModelState.AddModelError(string.Empty, message);
        foreach (var item in response.Error?.ValidationErrors ?? [])
            ModelState.AddModelError(item.Field ?? string.Empty, Sanitize(item.Message) ?? message);
    }

    [HttpPost]
    [CheckPermission(StandardPermission.Configuration.MANAGE_PLUGINS)]
    public async Task<IActionResult> Configure(ConfigurationModel model)
    {
        if (!ModelState.IsValid)
            return View("~/Plugins/Misc.Alanube/Views/Configure.cshtml", model);

        _settings.Enabled = model.Enabled;
        _settings.Environment = model.Environment;
        _settings.CompanyId = model.CompanyId;
        _settings.OfficeId = await IsOfficeValidAsync(model.CompanyId, model.OfficeId) ? model.OfficeId : string.Empty;

        if (!string.IsNullOrWhiteSpace(model.SandboxApiToken))
            _settings.SandboxApiToken = model.SandboxApiToken;

        if (!string.IsNullOrWhiteSpace(model.ProductionApiToken))
            _settings.ProductionApiToken = model.ProductionApiToken;

        await _settingService.SaveSettingAsync(_settings);
        _notificationService.SuccessNotification(await _localizationService.GetResourceAsync("Admin.Plugins.Saved"));

        return RedirectToRoute(AlanubeDefaults.ConfigurationRouteName);
    }

    private async Task<bool> IsOfficeValidAsync(string companyId, string officeId)
    {
        if (string.IsNullOrWhiteSpace(companyId) || string.IsNullOrWhiteSpace(officeId))
            return false;
        var response = await _alanubeOfficeClient.GetOfficesAsync(companyId, HttpContext.RequestAborted);
        return response.IsSuccessStatusCode && response.Body?.Any(x => x.Id == officeId) == true;
    }

    [HttpGet]
    [CheckPermission(StandardPermission.Configuration.MANAGE_PLUGINS)]
    public async Task<IActionResult> Offices(string companyId)
    {
        var model = new OfficesModel { CompanyId = companyId };
        var companies = await _alanubeCompanyClient.GetCompaniesAsync(cancellationToken: HttpContext.RequestAborted);
        model.Companies = companies.Body?.ToList() ?? new List<CompanyResponseDto>();
        if (!string.IsNullOrWhiteSpace(companyId))
        {
            var response = await _alanubeOfficeClient.GetOfficesAsync(companyId, HttpContext.RequestAborted);
            if (response.IsSuccessStatusCode)
                model.Offices = response.Body?.ToList() ?? new List<OfficeResponseDto>();
            else
                model.ErrorMessage = Sanitize(response.Error?.Message) ?? $"Alanube returned HTTP status {response.StatusCode}.";
        }
        return View("~/Plugins/Misc.Alanube/Views/Offices.cshtml", model);
    }

    [HttpGet]
    [CheckPermission(StandardPermission.Configuration.MANAGE_PLUGINS)]
    public async Task<IActionResult> CreateOffice(string companyId) => View("~/Plugins/Misc.Alanube/Views/OfficeEdit.cshtml", new OfficeEditModel { CompanyId = companyId });

    [HttpPost]
    [CheckPermission(StandardPermission.Configuration.MANAGE_PLUGINS)]
    public async Task<IActionResult> CreateOffice(OfficeEditModel model)
    {
        if (!ModelState.IsValid) return View("~/Plugins/Misc.Alanube/Views/OfficeEdit.cshtml", model);
        var response = await _alanubeOfficeClient.CreateOfficeAsync(model.CompanyId, ToRequest(model), HttpContext.RequestAborted);
        if (!response.IsSuccessStatusCode) { AddApiError(response); return View("~/Plugins/Misc.Alanube/Views/OfficeEdit.cshtml", model); }
        _notificationService.SuccessNotification(await _localizationService.GetResourceAsync("Admin.Common.Added"));
        return RedirectToRoute(AlanubeDefaults.OfficesRouteName, new { companyId = model.CompanyId });
    }

    [HttpGet]
    [CheckPermission(StandardPermission.Configuration.MANAGE_PLUGINS)]
    public async Task<IActionResult> EditOffice(string companyId, string id)
    {
        var response = await _alanubeOfficeClient.GetOfficesAsync(companyId, HttpContext.RequestAborted);
        var office = response.Body?.FirstOrDefault(x => x.Id == id);
        if (office is null) return RedirectToRoute(AlanubeDefaults.OfficesRouteName, new { companyId });
        return View("~/Plugins/Misc.Alanube/Views/OfficeEdit.cshtml", new OfficeEditModel { Id = office.Id, CompanyId = companyId, Type = office.Type, Email = office.Email, Code = office.Code, Coordinates = office.Coordinates, Address = office.Address, Telephone = office.Telephone, Location = office.Location });
    }

    [HttpPost]
    [CheckPermission(StandardPermission.Configuration.MANAGE_PLUGINS)]
    public async Task<IActionResult> EditOffice(OfficeEditModel model)
    {
        if (!ModelState.IsValid) return View("~/Plugins/Misc.Alanube/Views/OfficeEdit.cshtml", model);
        var response = await _alanubeOfficeClient.UpdateOfficeAsync(model.CompanyId, model.Id, ToRequest(model), HttpContext.RequestAborted);
        if (!response.IsSuccessStatusCode) { AddApiError(response); return View("~/Plugins/Misc.Alanube/Views/OfficeEdit.cshtml", model); }
        _notificationService.SuccessNotification(await _localizationService.GetResourceAsync("Admin.Common.Updated"));
        return RedirectToRoute(AlanubeDefaults.OfficesRouteName, new { companyId = model.CompanyId });
    }

    private static OfficeRequestDto ToRequest(OfficeEditModel model) => new() { Type = model.Type, Email = model.Email, Code = model.Code, Coordinates = model.Coordinates, Address = model.Address, Telephone = model.Telephone, Location = model.Location };

    [HttpPost]
    [ActionName("Configure")]
    [FormValueRequired("test-connection")]
    [CheckPermission(StandardPermission.Configuration.MANAGE_PLUGINS)]
    public async Task<IActionResult> TestConnection(ConfigurationModel model)
    {
        model.TestConnectionEnvironment = _settings.Environment.ToString();

        try
        {
            var response = await HttpClientTestAsync();
            model.TestConnectionSucceeded = response.IsSuccessStatusCode;
            model.TestConnectionStatusCode = response.StatusCode;
            model.TestConnectionErrorCode = Sanitize(response.Error?.Code);
            model.TestConnectionMessage = response.IsSuccessStatusCode
                ? await _localizationService.GetResourceAsync("Plugins.Misc.Alanube.TestConnection.Success")
                : Sanitize(response.Error?.Message) ?? await _localizationService.GetResourceAsync("Plugins.Misc.Alanube.TestConnection.Failed");
        }
        catch (AlanubeApiException exception)
        {
            model.TestConnectionSucceeded = false;
            model.TestConnectionStatusCode = exception.StatusCode;
            model.TestConnectionErrorCode = Sanitize(exception.Error?.Code);
            model.TestConnectionMessage = Sanitize(exception.Error?.Message) ??
                Sanitize(exception.Message) ??
                await _localizationService.GetResourceAsync("Plugins.Misc.Alanube.TestConnection.Failed");
        }

        return View("~/Plugins/Misc.Alanube/Views/Configure.cshtml", model);
    }

    private Task<AlanubeApiResponse<JsonElement>> HttpClientTestAsync()
    {
        return _alanubeClient.GetAsync<JsonElement>("companies", cancellationToken: HttpContext.RequestAborted);
    }

    private static string Sanitize(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return null;

        var sanitized = value.Replace("\r", " ").Replace("\n", " ").Trim();
        return sanitized.Length <= 500 ? sanitized : sanitized[..500];
    }
}
