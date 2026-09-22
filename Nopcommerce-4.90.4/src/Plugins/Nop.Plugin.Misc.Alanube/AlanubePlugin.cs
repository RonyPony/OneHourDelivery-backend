using Nop.Services.Common;
using Nop.Services.Localization;
using Nop.Services.Configuration;
using Nop.Services.Security;
using Nop.Services.Plugins;
using Nop.Web.Framework.Mvc.Routing;

namespace Nop.Plugin.Misc.Alanube;

/// <summary>
/// Represents the Alanube plugin.
/// </summary>
public sealed class AlanubePlugin : BasePlugin, IMiscPlugin
{
    private readonly ILocalizationService _localizationService;
    private readonly INopUrlHelper _nopUrlHelper;
    private readonly ISettingService _settingService;

    public AlanubePlugin(ILocalizationService localizationService, INopUrlHelper nopUrlHelper, ISettingService settingService)
    {
        _localizationService = localizationService;
        _nopUrlHelper = nopUrlHelper;
        _settingService = settingService;
    }

    public override string GetConfigurationPageUrl() => _nopUrlHelper.RouteUrl(AlanubeDefaults.ConfigurationRouteName);

    /// <summary>
    /// Installs the plugin.
    /// </summary>
    public override async Task InstallAsync()
    {
        await _settingService.SaveSettingAsync(new AlanubeSettings());
        await _localizationService.AddOrUpdateLocaleResourceAsync(new Dictionary<string, string>
        {
            ["Plugins.Misc.Alanube.Fields.Enabled"] = "Enabled",
            ["Plugins.Misc.Alanube.Fields.Environment"] = "Environment",
            ["Plugins.Misc.Alanube.Fields.Environment.Invalid"] = "Environment is invalid",
            ["Plugins.Misc.Alanube.Fields.SandboxApiToken"] = "Sandbox API token",
            ["Plugins.Misc.Alanube.Fields.SandboxApiToken.Required"] = "Sandbox API token is required when Sandbox is selected",
            ["Plugins.Misc.Alanube.Fields.ProductionApiToken"] = "Production API token",
            ["Plugins.Misc.Alanube.Fields.ProductionApiToken.Required"] = "Production API token is required when Production is selected",
            ["Plugins.Misc.Alanube.Fields.InvoiceTrigger"] = "Invoice trigger",
            ["Plugins.Misc.Alanube.Fields.EnableWebhook"] = "Enable document-status webhooks",
            ["Plugins.Misc.Alanube.Fields.WebhookSecret"] = "Webhook secret",
            ["Plugins.Misc.Alanube.Fields.BillingPoint"] = "Billing point",
            ["Plugins.Misc.Alanube.Fields.NextFiscalNumber"] = "Next fiscal number",
            ["Plugins.Misc.Alanube.Fields.IssueType"] = "Issue type",
            ["Plugins.Misc.Alanube.Fields.DocumentType"] = "Document type",
            ["Plugins.Misc.Alanube.Fields.Nature"] = "Nature",
            ["Plugins.Misc.Alanube.Fields.OperationType"] = "Operation type",
            ["Plugins.Misc.Alanube.Fields.Destination"] = "Destination",
            ["Plugins.Misc.Alanube.Fields.ReceiverContainer"] = "Receiver container",
            ["Plugins.Misc.Alanube.Fields.CafeFormat"] = "CAFE format",
            ["Plugins.Misc.Alanube.Fields.CafeDelivery"] = "CAFE delivery",
            ["Plugins.Misc.Alanube.Fields.SaleType"] = "Sale type",
            ["Plugins.Misc.Alanube.TestConnection"] = "Test connection",
            ["Plugins.Misc.Alanube.TestConnection.Result"] = "Connection test",
            ["Plugins.Misc.Alanube.TestConnection.Environment"] = "Environment",
            ["Plugins.Misc.Alanube.TestConnection.Status"] = "HTTP status",
            ["Plugins.Misc.Alanube.TestConnection.ErrorCode"] = "Alanube error code",
            ["Plugins.Misc.Alanube.TestConnection.Success"] = "Connection succeeded.",
            ["Plugins.Misc.Alanube.TestConnection.Failed"] = "Alanube rejected the connection test.",
            ["Plugins.Misc.Alanube.AdminMenu.Title"] = "Alanube",
            ["Plugins.Misc.Alanube.AdminMenu.Companies"] = "Companies",
            ["Plugins.Misc.Alanube.AdminMenu.Configuration"] = "Configuration",
            ["Plugins.Misc.Alanube.Companies.Empty"] = "No companies were found.",
            ["Plugins.Misc.Alanube.Companies.Id"] = "Alanube ID",
            ["Plugins.Misc.Alanube.Companies.Ruc"] = "RUC",
            ["Plugins.Misc.Alanube.Companies.TradeName"] = "Trade/company name",
            ["Plugins.Misc.Alanube.Companies.Status"] = "Status",
            ["Plugins.Misc.Alanube.Companies.Type"] = "Type",
            ["Plugins.Misc.Alanube.AdminMenu.Offices"] = "Offices",
            ["Plugins.Misc.Alanube.SelectCompany"] = "Select a company",
            ["Plugins.Misc.Alanube.SelectOffice"] = "Select an office",
            ["Plugins.Misc.Alanube.Offices.Company"] = "Company",
            ["Plugins.Misc.Alanube.Offices.SelectCompany"] = "Select a company"
            , ["Plugins.Misc.Alanube.Companies.Fields.Ruc"] = "RUC"
            , ["Plugins.Misc.Alanube.Companies.Fields.TypeRuc"] = "Taxpayer type"
            , ["Plugins.Misc.Alanube.Companies.Fields.Type"] = "Company type"
            , ["Plugins.Misc.Alanube.Companies.Fields.TradeName"] = "Trade name"
            , ["Plugins.Misc.Alanube.Companies.Fields.Qr"] = "Security QR"
            , ["Plugins.Misc.Alanube.Companies.Fields.SignatureCertificate"] = "Signature certificate (base64)"
            , ["Plugins.Misc.Alanube.Companies.Fields.AuthenticationCertificate"] = "Authentication certificate (base64)"
            , ["Plugins.Misc.Alanube.Companies.Fields.Ruc.Required"] = "RUC is required and must not exceed 20 characters"
            , ["Plugins.Misc.Alanube.Companies.Fields.TypeRuc.Invalid"] = "Taxpayer type must be 1 (natural) or 2 (legal)"
            , ["Plugins.Misc.Alanube.Companies.Fields.Certificate.Required"] = "Both DGI certificates are required"
        });
        await base.InstallAsync();
    }

    /// <summary>
    /// Uninstalls the plugin.
    /// </summary>
    public override async Task UninstallAsync()
    {
        await _settingService.DeleteSettingAsync<AlanubeSettings>();
        await _localizationService.DeleteLocaleResourcesAsync("Plugins.Misc.Alanube");
        await base.UninstallAsync();
    }
}
