using FluentValidation;
using Nop.Core.Domain.Customers;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Services;
using Nop.Plugin.Widgets.GoogleMapsIntegration.Helpers;
using Nop.Services.Directory;
using Nop.Services.Localization;
using Nop.Web.Framework.Validators;
using System.Linq;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Validators
{
    /// <summary>
    /// Represents a validator for <see cref="UpdateCustomerRequestValidator"/>.
    /// </summary>
    public class UpdateCustomerRequestValidator : BaseNopValidator<UpdateCustomerRequest>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="UpdateCustomerRequestValidator"/>.
        /// </summary>
        /// <param name="localizationService">An implementation of <see cref="ILocalizationService"/>.</param>
        /// <param name="stateProvinceService">An implementation of <see cref="IStateProvinceService"/>.</param>
        /// <param name="customerSettings">An instance of <see cref="CustomerSettings"/>.</param>
        /// <param name="customerAddressGeocodingServices">An implementation of <see cref="ICustomerAddressGeocodingServices"/>.</param>
        public UpdateCustomerRequestValidator(
            ILocalizationService localizationService,
            IStateProvinceService stateProvinceService,
            CustomerSettings customerSettings,
            ICustomerAddressGeocodingServices customerAddressGeocodingServices)
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage(localizationService.GetResource("Account.Fields.Email.Required"));
            RuleFor(x => x.Email).EmailAddress().WithMessage(localizationService.GetResource("Common.WrongEmail"));

            if (customerSettings.FirstNameEnabled && customerSettings.FirstNameRequired)
            {
                RuleFor(x => x.FirstName).NotEmpty().WithMessage(localizationService.GetResource("Account.Fields.FirstName.Required"));
            }
            if (customerSettings.LastNameEnabled && customerSettings.LastNameRequired)
            {
                RuleFor(x => x.LastName).NotEmpty().WithMessage(localizationService.GetResource("Account.Fields.LastName.Required"));
            }

            if (customerSettings.PhoneRequired && customerSettings.PhoneEnabled)
            {
                RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage(localizationService.GetResource("Account.Fields.Phone.Required"));
            }

            if (customerSettings.PhoneEnabled)
            {
                RuleFor(x => x.PhoneNumber).IsPhoneNumber(customerSettings).WithMessage(localizationService.GetResource("Account.Fields.Phone.NotValid"));
            }

        }
    }
}

