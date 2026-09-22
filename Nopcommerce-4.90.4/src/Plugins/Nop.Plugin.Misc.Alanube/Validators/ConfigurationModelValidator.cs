using FluentValidation;
using Nop.Plugin.Misc.Alanube.Configuration;
using Nop.Plugin.Misc.Alanube.Models;
using Nop.Services.Localization;
using Nop.Web.Framework.Validators;

namespace Nop.Plugin.Misc.Alanube.Validators;

/// <summary>
/// Represents the Alanube configuration validator.
/// </summary>
public sealed class ConfigurationModelValidator : BaseNopValidator<ConfigurationModel>
{
    public ConfigurationModelValidator(ILocalizationService localizationService)
    {
        RuleFor(model => model.Environment)
            .IsInEnum()
            .WithMessageAwait(localizationService.GetResourceAsync("Plugins.Misc.Alanube.Fields.Environment.Invalid"));

        RuleFor(model => model.SandboxApiToken)
            .NotEmpty()
            .WithMessageAwait(localizationService.GetResourceAsync("Plugins.Misc.Alanube.Fields.SandboxApiToken.Required"))
            .When(model => model.Enabled && model.Environment == AlanubeEnvironment.Sandbox);

        RuleFor(model => model.ProductionApiToken)
            .NotEmpty()
            .WithMessageAwait(localizationService.GetResourceAsync("Plugins.Misc.Alanube.Fields.ProductionApiToken.Required"))
            .When(model => model.Enabled && model.Environment == AlanubeEnvironment.Production);

        RuleFor(model => model.InvoiceTrigger)
            .IsInEnum()
            .WithMessage("The invoice trigger is invalid.");

        RuleFor(model => model.CompanyId)
            .NotEmpty()
            .WithMessage("An Alanube company must be selected when the plugin is enabled.")
            .When(model => model.Enabled);

        RuleFor(model => model.CompanyId)
            .Length(26)
            .WithMessage("Alanube company ID must contain exactly 26 characters.")
            .When(model => model.Enabled);

        RuleFor(model => model.OfficeId)
            .NotEmpty()
            .WithMessage("An Alanube office must be selected when the plugin is enabled.")
            .When(model => model.Enabled);

        RuleFor(model => model.OfficeId)
            .Length(26)
            .WithMessage("Alanube office ID must contain exactly 26 characters.")
            .When(model => model.Enabled);

        RuleFor(model => model.WebhookSecret)
            .NotEmpty()
            .WithMessage("A webhook secret is required when webhooks are enabled.")
            .When(model => model.Enabled && model.EnableWebhook);

        RuleFor(model => model.BillingPoint).Matches("^[0-9]{3}$").WithMessage("Billing point must contain exactly three digits.");
        RuleFor(model => model.NextFiscalNumber).InclusiveBetween(1, 9_999_999_999).WithMessage("The next fiscal number must be between 1 and 9999999999.");
        RuleFor(model => model.IssueType).Must(value => value is "01" or "02" or "03" or "04").WithMessage("Issue type is invalid.");
        RuleFor(model => model.DocumentType).Must(value => value is "01" or "02" or "03" or "08" or "09").WithMessage("Document type is invalid for the invoices endpoint.");
        RuleFor(model => model.Nature).Must(value => value is "01" or "02" or "10" or "11" or "12" or "13" or "14" or "20" or "21").WithMessage("Nature is invalid.");
        RuleFor(model => model.OperationType).InclusiveBetween(1, 2).WithMessage("Operation type is invalid.");
        RuleFor(model => model.Destination).InclusiveBetween(1, 2).WithMessage("Destination is invalid.");
        RuleFor(model => model.ReceiverContainer).InclusiveBetween(1, 2).WithMessage("Receiver container is invalid.");
        RuleFor(model => model.CafeFormat).InclusiveBetween(1, 3).WithMessage("CAFE format is invalid.");
        RuleFor(model => model.CafeDelivery).InclusiveBetween(1, 3).WithMessage("CAFE delivery is invalid.");
        RuleFor(model => model.SaleType).InclusiveBetween(1, 4).WithMessage("Sale type is invalid.");
        RuleFor(model => model).Must(model => model.DocumentType != "03" || model.Destination == 2).WithMessage("Export invoices require destination 2 (foreign).");
        RuleFor(model => model).Must(model => model.DocumentType != "01" || model.Destination == 1).WithMessage("Internal invoices require destination 1 (Panama).");
    }
}
