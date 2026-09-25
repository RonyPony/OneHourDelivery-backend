namespace Nop.Plugin.Misc.Alanube;

/// <summary>
/// Represents default values for the Alanube plugin.
/// </summary>
public static class AlanubeDefaults
{
    public const string ConfigurationRouteName = "Plugin.Alanube.Configure";

    public const string CompaniesRouteName = "Plugin.Alanube.Companies";
    public const string OfficesRouteName = "Plugin.Alanube.Offices";
    public const string WebhookRouteName = "Plugin.Alanube.Webhook.Documents";
    public const string WebhookHeaderName = "X-Alanube-Webhook-Key";

    public const string SandboxApiBaseUrl = "https://sandbox-api.alanube.co/dom/v1/";

    public const string ProductionApiBaseUrl = "https://api.alanube.co/pan/v1/";
}
