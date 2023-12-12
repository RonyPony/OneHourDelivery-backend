using FluentValidation;
using Nop.Plugin.Widgets.ProductAvailability.Models;
using Nop.Services.Localization;
using Nop.Web.Framework.Validators;

namespace Nop.Plugin.Widgets.ProductAvailability.Validators
{
    /// <summary>
    /// Represents a validator for <see cref="SearchInventoryModel"/>.
    /// </summary>
    public sealed class SearchInventoryModelValidator : BaseNopValidator<SearchInventoryModel>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="SearchInventoryModelValidator"/>.
        /// </summary>
        /// <param name="_localizationService">An mplementation of <see cref="ILocalizationService"/>.</param>
        public SearchInventoryModelValidator(ILocalizationService _localizationService)
        {
            RuleFor(model => model.ProductSku)
                .NotEmpty();
            RuleFor(model => model.ProductSku)
                .MinimumLength(8);
        }
    }
}
