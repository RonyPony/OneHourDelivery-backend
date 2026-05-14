using FluentValidation;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Customers;
using Nop.Plugin.Widgets.GoogleMapsIntegration.Helpers;
using Nop.Plugin.Widgets.GoogleMapsIntegration.Models;
using Nop.Services.Directory;
using Nop.Services.Localization;
using Nop.Web.Framework.Validators;
using System.Linq;

namespace Nop.Plugin.Widgets.GoogleMapsIntegration.Validators
{
    /// <summary>
    /// Validator for <see cref="AddressGeoCoordinatesEditModel"/>.
    /// </summary>
    public sealed class AddressGeoCoordinatesEditModelValidator : BaseNopValidator<AddressGeoCoordinatesEditModel>
    {
        /// <summary>
        /// Initialices a new instance of <see cref="AddressGeoCoordinatesEditModelValidator"/>.
        /// </summary>
        /// <param name="addressSettings">An instance of <see cref="ILocalizationService"/>.</param>
        /// <param name="customerSettings">An instance of <see cref="IStateProvinceService"/>.</param>
        /// <param name="localizationService">An implementation of <see cref="AddressSettings"/>.</param>
        /// <param name="stateProvinceService">An implementation of <see cref="CustomerSettings"/>.</param>
        public AddressGeoCoordinatesEditModelValidator(ILocalizationService localizationService,
            IStateProvinceService stateProvinceService,
            AddressSettings addressSettings,
            CustomerSettings customerSettings)
        {
            RuleFor(editModel => editModel.AddressGeoCoordinatesMapping.Latitude)
                .NotEmpty()
                .WithMessage(localizationService.GetResourceAsync($"{Defaults.ResourcesNamePrefix}.Fields.Latitude.Required").GetAwaiter().GetResult());
            RuleFor(editModel => editModel.AddressGeoCoordinatesMapping.Longitude)
                .NotEmpty()
                .WithMessage(localizationService.GetResourceAsync($"{Defaults.ResourcesNamePrefix}.Fields.Longitude.Required").GetAwaiter().GetResult());
            RuleFor(editModel => editModel.Address.FirstName)
                .NotEmpty()
                .WithMessage(localizationService.GetResourceAsync("Address.Fields.FirstName.Required").GetAwaiter().GetResult());
            RuleFor(editModel => editModel.Address.LastName)
                .NotEmpty()
                .WithMessage(localizationService.GetResourceAsync("Address.Fields.LastName.Required").GetAwaiter().GetResult());
            RuleFor(editModel => editModel.Address.Email)
                .NotEmpty()
                .WithMessage(localizationService.GetResourceAsync("Address.Fields.Email.Required").GetAwaiter().GetResult());
            RuleFor(editModel => editModel.Address.Email)
                .EmailAddress()
                .WithMessage(localizationService.GetResourceAsync("Common.WrongEmail").GetAwaiter().GetResult());
            if (addressSettings.CountryEnabled)
            {
                RuleFor(editModel => editModel.Address.CountryId)
                    .NotNull()
                    .WithMessage(localizationService.GetResourceAsync("Address.Fields.Country.Required").GetAwaiter().GetResult());
                RuleFor(editModel => editModel.Address.CountryId)
                    .NotEqual(0)
                    .WithMessage(localizationService.GetResourceAsync("Address.Fields.Country.Required").GetAwaiter().GetResult());
            }
            if (addressSettings.CountryEnabled && addressSettings.StateProvinceEnabled)
            {
                RuleFor(editModel => editModel.Address.StateProvinceId).Must((editModel, context) =>
                {
                    //does selected country has states?
                    var countryId = editModel.Address.CountryId ?? 0;
                    var hasStates = stateProvinceService.GetStateProvincesByCountryIdAsync(countryId).GetAwaiter().GetResult().Any();

                    if (hasStates)
                    {
                        //if yes, then ensure that state is selected
                        if (!editModel.Address.StateProvinceId.HasValue || editModel.Address.StateProvinceId.Value == 0)
                            return false;
                    }

                    return true;
                }).WithMessage(localizationService.GetResourceAsync("Address.Fields.StateProvince.Required").GetAwaiter().GetResult());
            }
            if (addressSettings.CompanyRequired && addressSettings.CompanyEnabled)
            {
                RuleFor(editModel => editModel.Address.Company).NotEmpty().WithMessage(localizationService.GetResourceAsync("Account.Fields.Company.Required").GetAwaiter().GetResult());
            }
            if (addressSettings.StreetAddressRequired && addressSettings.StreetAddressEnabled)
            {
                RuleFor(editModel => editModel.Address.Address1).NotEmpty().WithMessage(localizationService.GetResourceAsync("Account.Fields.StreetAddress.Required").GetAwaiter().GetResult());
            }
            if (addressSettings.StreetAddress2Required && addressSettings.StreetAddress2Enabled)
            {
                RuleFor(editModel => editModel.Address.Address2).NotEmpty().WithMessage(localizationService.GetResourceAsync("Account.Fields.StreetAddress2.Required").GetAwaiter().GetResult());
            }
            if (addressSettings.ZipPostalCodeRequired && addressSettings.ZipPostalCodeEnabled)
            {
                RuleFor(editModel => editModel.Address.ZipPostalCode).NotEmpty().WithMessage(localizationService.GetResourceAsync("Account.Fields.ZipPostalCode.Required").GetAwaiter().GetResult());
            }
            if (addressSettings.CountyEnabled && addressSettings.CountyRequired)
            {
                RuleFor(editModel => editModel.Address.County).NotEmpty().WithMessage(localizationService.GetResourceAsync("Address.Fields.County.Required").GetAwaiter().GetResult());
            }
            if (addressSettings.CityRequired && addressSettings.CityEnabled)
            {
                RuleFor(editModel => editModel.Address.City).NotEmpty().WithMessage(localizationService.GetResourceAsync("Account.Fields.City.Required").GetAwaiter().GetResult());
            }
            if (addressSettings.PhoneRequired && addressSettings.PhoneEnabled)
            {
                RuleFor(editModel => editModel.Address.PhoneNumber).NotEmpty().WithMessage(localizationService.GetResourceAsync("Account.Fields.Phone.Required").GetAwaiter().GetResult());
            }
            if (addressSettings.PhoneEnabled)
            {
                RuleFor(editModel => editModel.Address.PhoneNumber).IsPhoneNumber(customerSettings).WithMessage(localizationService.GetResourceAsync("Account.Fields.Phone.NotValid").GetAwaiter().GetResult());
            }
            if (addressSettings.FaxRequired && addressSettings.FaxEnabled)
            {
                RuleFor(editModel => editModel.Address.FaxNumber).NotEmpty().WithMessage(localizationService.GetResourceAsync("Account.Fields.Fax.Required").GetAwaiter().GetResult());
            }
        }
    }
}
