using FluentValidation;
using Nop.Plugin.Synchronizers.WAPIOrders.Helpers;
using Nop.Plugin.Synchronizers.WAPIOrders.Models;
using Nop.Services.Localization;
using Nop.Web.Framework.Validators;

namespace Nop.Plugin.Synchronizers.WAPIOrders.Validators
{
    /// <summary>
    /// A validator fot <see cref="ConfigurationModel"/>.
    /// </summary>
    public sealed class ConfigurationModelValidator : BaseNopValidator<ConfigurationModel>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ConfigurationModelValidator"/>.
        /// </summary>
        /// <param name="_localizationService">An implementation of <see cref="ILocalizationService"/>.</param>
        public ConfigurationModelValidator(ILocalizationService _localizationService)
        {
            RuleFor(config => config.AuthKeyName)
                .NotEmpty()
                .WithMessage(_localizationService.GetResource($"{Defaults.LocaleResorcesPrefix}.Fields.AuthKeyName.Required"));
            RuleFor(config => config.AuthKeyValue)
                .NotEmpty()
                .WithMessage(_localizationService.GetResource($"{Defaults.LocaleResorcesPrefix}.Fields.AuthKeyValue.Required"));
            RuleFor(config => config.ApiPostUrl)
                .NotEmpty()
                .WithMessage(_localizationService.GetResource($"{Defaults.LocaleResorcesPrefix}.Fields.ApiPostUrl.Required"));
            RuleFor(config => config.DefaultStorePickupCode)
                .NotEmpty()
                .WithMessage(_localizationService.GetResource($"{Defaults.LocaleResorcesPrefix}.Fields.DefaultStorePickupCode.Required"));
        }
    }
}
