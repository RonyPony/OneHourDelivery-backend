using FluentValidation;
using Nop.Core;
using Nop.Core.Domain.Customers;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Services;
using Nop.Services.Directory;
using Nop.Services.Localization;
using Nop.Web.Framework.Validators;
using System;
using System.Linq;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Validators
{
    /// <summary>
    /// Represents a validator for <see cref="RegisterCustomerRequest"/>.
    /// </summary>
    public sealed class RegisterCustomerRequestValidator : BaseNopValidator<RegisterCustomerRequest>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="RegisterCustomerRequestValidator"/>.
        /// </summary>
        /// <param name="localizationService">An implementation of <see cref="ILocalizationService"/>.</param>
        /// <param name="stateProvinceService">An implementation of <see cref="IStateProvinceService"/>.</param>
        /// <param name="customerSettings">An instance of <see cref="CustomerSettings"/>.</param>
        /// <param name="customerAddressGeocodingServices">An implementation of <see cref="ICustomerAddressGeocodingServices"/>.</param>
        public RegisterCustomerRequestValidator(
            ILocalizationService localizationService,
            IStateProvinceService stateProvinceService,
            CustomerSettings customerSettings,
            ICustomerAddressGeocodingServices customerAddressGeocodingServices)
        {
            RuleFor(x => x.Email).Must(email => !customerAddressGeocodingServices.EmailIsAlreadyRegistered(email))
                .WithMessage(localizationService.GetResource("Plugin.Misc.CustomerAddressGeocoding.Fields.Email.AlreadyRegistered"));
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

            RuleFor(x => x.Password).IsPassword(localizationService, customerSettings);
            RuleFor(x => x.ConfirmPassword).NotEmpty().WithMessage(localizationService.GetResource("Account.Fields.ConfirmPassword.Required"));
            RuleFor(x => x.ConfirmPassword).Equal(x => x.Password).WithMessage(localizationService.GetResource("Account.Fields.Password.EnteredPasswordsDoNotMatch"));

            if (customerSettings.CountryEnabled && customerSettings.CountryRequired)
            {
                RuleFor(x => x.CountryId)
                    .NotEqual(0)
                    .WithMessage(localizationService.GetResource("Account.Fields.Country.Required"))
                    .When(x => string.IsNullOrEmpty(x.CountryName) || string.IsNullOrWhiteSpace(x.CountryName));
            }

            if (customerSettings.CountryEnabled &&
                customerSettings.StateProvinceEnabled &&
                customerSettings.StateProvinceRequired)
            {
                RuleFor(x => x.StateProvinceId).Must((x, context) =>
                {
                    //does selected country have states?
                    var hasStates = stateProvinceService.GetStateProvincesByCountryId(x.CountryId.Value).Any();

                    if (hasStates)
                    {
                        //if yes, then ensure that a state is selected
                        if (x.StateProvinceId == 0)
                            return false;
                    }

                    return true;
                }).WithMessage(localizationService.GetResource("Account.Fields.StateProvince.Required"))
                .When(x => x.CountryId.HasValue && (string.IsNullOrEmpty(x.StateProvinceName) || string.IsNullOrWhiteSpace(x.StateProvinceName)));
            }

            if (customerSettings.DateOfBirthEnabled && customerSettings.DateOfBirthRequired)
            {
                //entered?
                RuleFor(x => x.DateOfBirth).Must((x, context) =>
                {
                    var dateOfBirth = customerAddressGeocodingServices.ParseDateOfBirth(x);
                    if (!dateOfBirth.HasValue)
                        return false;

                    return true;
                }).WithMessage(localizationService.GetResource("Account.Fields.DateOfBirth.Required"));

                //minimum age
                RuleFor(x => x).Must((x, context) =>
                {
                    var dateOfBirth = customerAddressGeocodingServices.ParseDateOfBirth(x);
                    if (dateOfBirth.HasValue && customerSettings.DateOfBirthMinimumAge.HasValue &&
                        CommonHelper.GetDifferenceInYears(dateOfBirth.Value, DateTime.Today) <
                        customerSettings.DateOfBirthMinimumAge.Value)
                        return false;

                    return true;
                }).WithMessage(string.Format(localizationService.GetResource("Account.Fields.DateOfBirth.MinimumAge"), customerSettings.DateOfBirthMinimumAge));
            }

            if (customerSettings.CityRequired && customerSettings.CityEnabled)
            {
                RuleFor(x => x.City).NotEmpty().WithMessage(localizationService.GetResource("Account.Fields.City.Required"));
            }

            if (customerSettings.StreetAddressRequired && customerSettings.StreetAddressEnabled)
            {
                RuleFor(x => x.Address1).NotEmpty().WithMessage(localizationService.GetResource("Account.Fields.StreetAddress.Required"));
            }

            if (customerSettings.StreetAddress2Required && customerSettings.StreetAddress2Enabled)
            {
                RuleFor(x => x.Address2).NotEmpty().WithMessage(localizationService.GetResource("Account.Fields.StreetAddress2.Required"));
            }

            if (customerSettings.ZipPostalCodeRequired && customerSettings.ZipPostalCodeEnabled)
            {
                RuleFor(x => x.ZipPostalCode).NotEmpty().WithMessage(localizationService.GetResource("Account.Fields.ZipPostalCode.Required"));
            }

            if (customerSettings.PhoneRequired && customerSettings.PhoneEnabled)
            {
                RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage(localizationService.GetResource("Account.Fields.Phone.Required"));
            }

            if (customerSettings.PhoneEnabled)
            {
                RuleFor(x => x.PhoneNumber).IsPhoneNumber(customerSettings).WithMessage(localizationService.GetResource("Account.Fields.Phone.NotValid"));
                RuleFor(x => x.PhoneNumber).Must(phone => !customerAddressGeocodingServices.PhoneNumberIsAlreadyRegistered(phone))
                    .WithMessage(localizationService.GetResource("Plugin.Misc.CustomerAddressGeocoding.Fields.Phone.AlreadyRegistered"));
            }

            RuleFor(x => x.Latitude).NotEmpty().WithMessage(localizationService.GetResource($"{Helpers.Defaults.ResourcesNamePrefix}.Fields.Latitude.Required"));
            RuleFor(x => x.Latitude).InclusiveBetween(-999.9999999m, 999.9999999m).WithMessage(localizationService.GetResource($"{Helpers.Defaults.ResourcesNamePrefix}.Fields.Longitude.NotValid"));

            RuleFor(x => x.Longitude).NotEmpty().WithMessage(localizationService.GetResource($"{Helpers.Defaults.ResourcesNamePrefix}.Fields.Longitude.Required"));
            RuleFor(x => x.Longitude).InclusiveBetween(-999.9999999m, 999.9999999m).WithMessage(localizationService.GetResource($"{Helpers.Defaults.ResourcesNamePrefix}.Fields.Longitude.NotValid"));
        }
    }
}
