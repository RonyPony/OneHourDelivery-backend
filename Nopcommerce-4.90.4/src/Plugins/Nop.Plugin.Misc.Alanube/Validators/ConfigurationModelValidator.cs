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
    }
}
