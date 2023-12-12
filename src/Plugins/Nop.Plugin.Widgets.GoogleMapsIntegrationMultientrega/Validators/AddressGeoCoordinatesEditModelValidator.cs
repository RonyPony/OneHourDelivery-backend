using FluentValidation;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Customers;
using Nop.Plugin.Widgets.GoogleMapsIntegrationMultientrega.Domains;
using Nop.Plugin.Widgets.GoogleMapsIntegrationMultientrega.Helpers;
using Nop.Plugin.Widgets.GoogleMapsIntegrationMultientrega.Models;
using Nop.Services.Directory;
using Nop.Services.Localization;
using Nop.Web.Framework.Validators;
using System.Linq;

namespace Nop.Plugin.Widgets.GoogleMapsIntegrationMultientrega.Validators
{
    /// <summary>
    /// Validator for <see cref="AddressGeoCoordinatesEditModel"/>.
    /// </summary>
    public sealed class AddressGeoCoordinatesEditModelValidator : BaseNopValidator<AddressGeoCoordinatesEditModel>
    {
        /// <summary>
        /// Initialices a new instance of <see cref="AddressGeoCoordinatesEditModelValidator"/>.
        /// </summary>
        /// <param name="addressSettings">An implementation of <see cref="ILocalizationService"/>.</param>
        /// <param name="customerSettings">An implementation of <see cref="IStateProvinceService"/>.</param>
        /// <param name="localizationService">An instance of <see cref="AddressSettings"/>.</param>
        /// <param name="stateProvinceService">An instance of <see cref="CustomerSettings"/>.</param>
        /// <param name="pluginConfigurationSettings">An instance of <see cref="PluginConfigurationSettings"/>.</param>
        public AddressGeoCoordinatesEditModelValidator(
            ILocalizationService localizationService,
            IStateProvinceService stateProvinceService,
            AddressSettings addressSettings,
            CustomerSettings customerSettings,
            PluginConfigurationSettings pluginConfigurationSettings)
        {
            if (pluginConfigurationSettings.LatLngFieldsRequired)
            {
                RuleFor(editModel => editModel.AddressGeoCoordinatesMapping.Latitude)
                    .NotEmpty()
                    .WithMessage(localizationService.GetResource($"{Defaults.ResourcesNamePrefix}.Fields.Latitude.Required"));
                RuleFor(editModel => editModel.AddressGeoCoordinatesMapping.Longitude)
                    .NotEmpty()
                    .WithMessage(localizationService.GetResource($"{Defaults.ResourcesNamePrefix}.Fields.Longitude.Required"));
            }

            RuleFor(editModel => editModel.AddressGeoCoordinatesMapping.ProvinceId)
                .NotEmpty()
                .NotEqual("0")
                .WithMessage(localizationService.GetResource($"{Defaults.ResourcesNamePrefix}.Fields.ProvinceId.Required"));
            RuleFor(editModel => editModel.AddressGeoCoordinatesMapping.DistrictId)
                .NotEmpty()
                .NotEqual("0")
                .WithMessage(localizationService.GetResource($"{Defaults.ResourcesNamePrefix}.Fields.DistrictId.Required"));
            RuleFor(editModel => editModel.AddressGeoCoordinatesMapping.TownshipId)
                .NotEmpty()
                .NotEqual("0")
                .WithMessage(localizationService.GetResource($"{Defaults.ResourcesNamePrefix}.Fields.TownshipId.Required"));
            RuleFor(editModel => editModel.AddressGeoCoordinatesMapping.NeighborhoodId)
                .NotEmpty()
                .NotEqual("0")
                .WithMessage(localizationService.GetResource($"{Defaults.ResourcesNamePrefix}.Fields.NeighborhoodId.Required"));

            RuleFor(editModel => editModel.Address.FirstName)
                .NotEmpty()
                .WithMessage(localizationService.GetResource("Address.Fields.FirstName.Required"));
            RuleFor(editModel => editModel.Address.LastName)
                .NotEmpty()
                .WithMessage(localizationService.GetResource("Address.Fields.LastName.Required"));
            RuleFor(editModel => editModel.Address.Email)
                .NotEmpty()
                .WithMessage(localizationService.GetResource("Address.Fields.Email.Required"));
            RuleFor(editModel => editModel.Address.Email)
                .EmailAddress()
                .WithMessage(localizationService.GetResource("Common.WrongEmail"));
            if (addressSettings.CountryEnabled)
            {
                RuleFor(editModel => editModel.Address.CountryId)
                    .NotNull()
                    .WithMessage(localizationService.GetResource("Address.Fields.Country.Required"));
                RuleFor(editModel => editModel.Address.CountryId)
                    .NotEqual(0)
                    .WithMessage(localizationService.GetResource("Address.Fields.Country.Required"));
            }
            if (addressSettings.CountryEnabled && addressSettings.StateProvinceEnabled)
            {
                RuleFor(editModel => editModel.Address.StateProvinceId).Must((editModel, context) =>
                {
                    //does selected country has states?
                    var countryId = editModel.Address.CountryId ?? 0;
                    var hasStates = stateProvinceService.GetStateProvincesByCountryId(countryId).Any();

                    if (hasStates)
                    {
                        //if yes, then ensure that state is selected
                        if (!editModel.Address.StateProvinceId.HasValue || editModel.Address.StateProvinceId.Value == 0)
                            return false;
                    }

                    return true;
                }).WithMessage(localizationService.GetResource("Address.Fields.StateProvince.Required"));
            }
            if (addressSettings.CompanyRequired && addressSettings.CompanyEnabled)
            {
                RuleFor(editModel => editModel.Address.Company).NotEmpty().WithMessage(localizationService.GetResource("Account.Fields.Company.Required"));
            }
            if (addressSettings.StreetAddressRequired && addressSettings.StreetAddressEnabled)
            {
                RuleFor(editModel => editModel.Address.Address1).NotEmpty().WithMessage(localizationService.GetResource("Account.Fields.StreetAddress.Required"));
            }
            if (addressSettings.StreetAddress2Required && addressSettings.StreetAddress2Enabled)
            {
                RuleFor(editModel => editModel.Address.Address2).NotEmpty().WithMessage(localizationService.GetResource("Account.Fields.StreetAddress2.Required"));
            }
            if (addressSettings.ZipPostalCodeRequired && addressSettings.ZipPostalCodeEnabled)
            {
                RuleFor(editModel => editModel.Address.ZipPostalCode).NotEmpty().WithMessage(localizationService.GetResource("Account.Fields.ZipPostalCode.Required"));
            }
            if (addressSettings.CountyEnabled && addressSettings.CountyRequired)
            {
                RuleFor(editModel => editModel.Address.County).NotEmpty().WithMessage(localizationService.GetResource("Address.Fields.County.Required"));
            }
            if (addressSettings.CityRequired && addressSettings.CityEnabled)
            {
                RuleFor(editModel => editModel.Address.City).NotEmpty().WithMessage(localizationService.GetResource("Account.Fields.City.Required"));
            }
            if (addressSettings.PhoneRequired && addressSettings.PhoneEnabled)
            {
                RuleFor(editModel => editModel.Address.PhoneNumber).NotEmpty().WithMessage(localizationService.GetResource("Account.Fields.Phone.Required"));
            }
            if (addressSettings.PhoneEnabled)
            {
                RuleFor(editModel => editModel.Address.PhoneNumber).IsPhoneNumber(customerSettings).WithMessage(localizationService.GetResource("Account.Fields.Phone.NotValid"));
            }
            if (addressSettings.FaxRequired && addressSettings.FaxEnabled)
            {
                RuleFor(editModel => editModel.Address.FaxNumber).NotEmpty().WithMessage(localizationService.GetResource("Account.Fields.Fax.Required"));
            }
        }
    }
}
