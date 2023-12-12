using FluentValidation;
using Nop.Core.Domain.Customers;
using Nop.Services.Localization;
using Nop.Web.Framework.Validators;
using Nop.Web.Models.Common;

namespace Nop.Plugin.Payments.CyberSource.Validators
{
    /// <summary>
    /// Represents a Fluent Validator for <see cref="AddressModel"/>.
    /// </summary>
    public sealed class AddressModelValidator : BaseNopValidator<AddressModel>
    {
        /// <summary>
        /// Initilizes a new instance of <see cref="AddressModelValidator"/>.
        /// </summary>
        public AddressModelValidator(
            CustomerSettings customerSettings,
            ILocalizationService localizationService)
        {
            RuleFor(model => model.PhoneNumber)
                .MaximumLength(15);
            RuleFor(model => model.PhoneNumber)
                .IsPhoneNumber(customerSettings)
                .WithMessage(localizationService.GetResource("Plugins.Payments.CyberSource.PhoneInvalid"));
            RuleFor(model => model.Email)
                .MaximumLength(255);
            RuleFor(model => model.FirstName)
                .MaximumLength(60);
            RuleFor(model => model.LastName)
                .MaximumLength(60);
        }
    }
}
