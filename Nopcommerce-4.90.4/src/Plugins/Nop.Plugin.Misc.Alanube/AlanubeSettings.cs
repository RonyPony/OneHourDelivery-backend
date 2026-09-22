using Nop.Core.Configuration;
using Nop.Plugin.Misc.Alanube.Configuration;

namespace Nop.Plugin.Misc.Alanube;

/// <summary>
/// Represents the base settings for the Alanube plugin.
/// </summary>
public class AlanubeSettings : ISettings
{
    /// <summary>
    /// Gets or sets a value indicating whether the plugin is enabled.
    /// </summary>
    public bool Enabled { get; set; }

    /// <summary>
    /// Gets or sets the Alanube API environment.
    /// </summary>
    public AlanubeEnvironment Environment { get; set; } = AlanubeEnvironment.Sandbox;

    /// <summary>
    /// Gets or sets the Alanube sandbox API token.
    /// </summary>
    public string SandboxApiToken { get; set; }

    /// <summary>
    /// Gets or sets the Alanube production API token.
    /// </summary>
    public string ProductionApiToken { get; set; }

    /// <summary>
    /// Gets or sets the Alanube company identifier.
    /// </summary>
    public string CompanyId { get; set; }

    /// <summary>
    /// Gets or sets the Alanube office identifier.
    /// </summary>
    public string OfficeId { get; set; }

    /// <summary>
    /// Gets or sets the invoice processing trigger.
    /// </summary>
    public InvoiceTrigger InvoiceTrigger { get; set; } = InvoiceTrigger.Manual;

    /// <summary>
    /// Gets or sets a value indicating whether the invoice webhook is enabled.
    /// </summary>
    public bool EnableWebhook { get; set; }

    /// <summary>
    /// Gets or sets the secret used to validate Alanube webhook requests.
    /// </summary>
    public string WebhookSecret { get; set; }

    public string BillingPoint { get; set; } = "001";
    public long NextFiscalNumber { get; set; } = 1;
    public string IssueType { get; set; } = "01";
    public string DocumentType { get; set; } = "01";
    public string Nature { get; set; } = "01";
    public int OperationType { get; set; } = 1;
    public int Destination { get; set; } = 1;
    public int ReceiverContainer { get; set; } = 1;
    public int CafeFormat { get; set; } = 3;
    public int CafeDelivery { get; set; } = 3;
    public int SaleType { get; set; } = 1;
}
