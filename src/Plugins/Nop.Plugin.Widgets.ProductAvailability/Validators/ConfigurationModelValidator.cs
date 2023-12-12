using FluentValidation;
using Nop.Plugin.Widgets.ProductAvailability.Models;
using Nop.Services.Localization;
using Nop.Web.Framework.Validators;

namespace Nop.Plugin.Widgets.ProductAvailability.Validators
{
    /// <summary>
    /// Represents a validator for <see cref="ConfigurationModel"/>.
    /// </summary>
    public sealed class ConfigurationModelValidator : BaseNopValidator<ConfigurationModel>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ConfigurationModelValidator"/>.
        /// </summary>
        /// <param name="_localizationService">An implementation of <see cref="ILocalizationService"/>.</param>
        public ConfigurationModelValidator(ILocalizationService _localizationService)
        {
            RuleFor(config => config.ProductInventoryUrl)
                .NotEmpty()
                .WithMessage(_localizationService.GetResource($"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Config.ProductInventoryUrl.Required"));
            RuleFor(config => config.StoresUrl)
                .NotEmpty()
                .WithMessage(_localizationService.GetResource($"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Config.StoresUrl.Required"));


            RuleFor(config => config.ProductInventoryUrlToken)
                .NotEmpty()
                .When(config => !config.UseSameTokenForAllRequest)
                .WithMessage(_localizationService.GetResource($"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Config.ProductInventoryUrlToken.Required"));
            RuleFor(config => config.StoresUrlToken)
                .NotEmpty()
                .When(config => !config.UseSameTokenForAllRequest)
                .WithMessage(_localizationService.GetResource($"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Config.StoresUrlToken.Required"));

            RuleFor(config => config.Token)
                .NotEmpty()
                .When(config => config.UseSameTokenForAllRequest)
                .WithMessage(_localizationService.GetResource($"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Config.Token.Required"));

            RuleFor(config => config.ProductAttributeId)
                .GreaterThan(0)
                .WithMessage(_localizationService.GetResource($"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Config.ProductAttributeId.Required"));

            RuleFor(config => config.InventoryRequestTries)
                .GreaterThan(0)
                .WithMessage(_localizationService.GetResource($"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Config.InventoryRequestTries.Required"));
        }
    }
}
