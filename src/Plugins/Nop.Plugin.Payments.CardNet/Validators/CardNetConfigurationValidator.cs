using FluentValidation;
using Nop.Plugin.Payments.CardNet.Models;
using Nop.Services.Localization;
using Nop.Web.Framework.Validators;

namespace Nop.Plugin.Payments.CardNet.Validators
{
    /// <summary>
    /// Validator for <see cref="CardNetConfigurationModel"/>
    /// </summary>
    public class CardNetConfigurationValidator : BaseNopValidator<CardNetConfigurationModel>
    {
        public CardNetConfigurationValidator(ILocalizationService localizationService)
        {
            RuleFor(model => model.Url)
                .NotEmpty()
                .WithMessage(localizationService.GetResource("Plugins.Payments.CardNet.Fields.Url.ErrorMessage"));

            RuleFor(model => model.PwCheckoutScriptUrl)
                .NotEmpty()
                .WithMessage(localizationService.GetResource("Plugins.Payments.CardNet.Fields.PwCheckoutScriptUrl.ErrorMessage"));

            RuleFor(model => model.PublicApiKey)
                .NotEmpty()
                .WithMessage(localizationService.GetResource("Plugins.Payments.CardNet.Fields.PublicApiKey.ErrorMessage"));

            RuleFor(model => model.PrivateApiKey)
                .NotEmpty()
                .WithMessage(localizationService.GetResource("Plugins.Payments.CardNet.Fields.PrivateApiKey.ErrorMessage"));
        }
    }
}
