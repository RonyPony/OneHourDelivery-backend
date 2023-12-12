using FluentValidation;
using Nop.Plugin.Payments.Banrural.Models;
using Nop.Services.Localization;
using Nop.Web.Framework.Validators;

namespace Nop.Plugin.Payments.Banrural.Validators
{
    /// <summary>
    /// Responsible for validating the Banrural configuration form.
    /// </summary>
    public sealed class BanruralValidator : BaseNopValidator<BanruralConfigurationModel>
    {
        /// <summary>
        /// Initializes a new instance of BanruralValidator class.
        /// </summary>
        /// <param name="localizationService">An implementation of <see cref="ILocalizationService"/>
        public BanruralValidator(ILocalizationService localizationService)
        {
            RuleFor(model => model.KeyID)
                .NotEmpty()
                .WithMessage(localizationService.GetResource("Plugins.Payments.Banrural.IsRequired"));

            RuleFor(model => model.Url)
               .NotEmpty()
               .WithMessage(localizationService.GetResource("Plugins.Payments.Banrural.IsRequired"));
            RuleFor(model => model.Url)
                .MaximumLength(150)
                .WithMessage(localizationService.GetResource("Plugins.Payments.Banrural.UrlMaxLengthExceeded"));

            RuleFor(model => model.CancelUrl)
               .NotEmpty()
               .WithMessage(localizationService.GetResource("Plugins.Payments.Banrural.IsRequired"));
            RuleFor(model => model.CancelUrl)
                .MaximumLength(150)
                .WithMessage(localizationService.GetResource("Plugins.Payments.Banrural.UrlMaxLengthExceeded"));

            RuleFor(model => model.CompleteUrl)
               .NotEmpty()
               .WithMessage(localizationService.GetResource("Plugins.Payments.Banrural.IsRequired"));

            RuleFor(model => model.CallbackUrl)
               .NotEmpty()
               .WithMessage(localizationService.GetResource("Plugins.Payments.Banrural.IsRequired"));

            RuleFor(model => model.CompleteUrl)
                .MaximumLength(150)
                .WithMessage(localizationService.GetResource("Plugins.Payments.Banrural.UrlMaxLengthExceeded"));
        }        
    }
}