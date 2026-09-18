using FluentValidation;
using Nop.Plugin.Misc.Alanube.Models;
using Nop.Services.Localization;
using Nop.Web.Framework.Validators;

namespace Nop.Plugin.Misc.Alanube.Validators;

public sealed class CompanyEditModelValidator : BaseNopValidator<CompanyEditModel>
{
    public CompanyEditModelValidator(ILocalizationService localizationService)
    {
        RuleFor(model => model.Ruc)
            .NotEmpty()
            .MaximumLength(20)
            .WithMessageAwait(localizationService.GetResourceAsync("Plugins.Misc.Alanube.Companies.Fields.Ruc.Required"));

        RuleFor(model => model.TypeRuc)
            .InclusiveBetween(1, 2)
            .WithMessageAwait(localizationService.GetResourceAsync("Plugins.Misc.Alanube.Companies.Fields.TypeRuc.Invalid"));

        RuleFor(model => model.SignatureCertificate)
            .NotEmpty()
            .WithMessageAwait(localizationService.GetResourceAsync("Plugins.Misc.Alanube.Companies.Fields.Certificate.Required"))
            ;

        RuleFor(model => model.AuthenticationCertificate)
            .NotEmpty()
            .WithMessageAwait(localizationService.GetResourceAsync("Plugins.Misc.Alanube.Companies.Fields.Certificate.Required"))
            ;
    }
}
