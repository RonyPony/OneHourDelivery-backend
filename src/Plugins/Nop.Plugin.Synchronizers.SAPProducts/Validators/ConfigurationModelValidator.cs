using FluentValidation;
using Nop.Plugin.Synchronizers.SAPProducts.Models;
using Nop.Services.Localization;
using Nop.Web.Framework.Validators;

namespace Nop.Plugin.Synchronizers.SAPProducts.Validators
{
    /// <summary>
    /// Represents an <see cref="ConfigurationModel"/> validator.
    /// </summary>
    public class ConfigurationModelValidator : BaseNopValidator<ConfigurationModel>
    {
        /// <summary>
        /// Initializes a newinstance of <see cref="ConfigurationModelValidator"/>.
        /// </summary>
        /// <param name="localizationService">An implementation of <see cref="ILocalizationService"/>.</param>
        public ConfigurationModelValidator(ILocalizationService localizationService)
        {
            RuleFor(model => model.SapCategoryUrl)
                .NotEmpty()
                .When(model => model.SyncCategories)
                .WithMessage(localizationService.GetResource("Plugins.Synchronizers.SAPProducts.Fields.SapCategoryUrl.Required"));
            RuleFor(model => model.SapManufacturerUrl)
                .NotEmpty()
                .When(model => model.SyncManufacturers)
                .WithMessage(localizationService.GetResource("Plugins.Synchronizers.SAPProducts.Fields.SapManufacturerUrl.Required"));
        }
    }
}

