using System.ComponentModel.DataAnnotations.Schema;
using Nop.Core;
using Nop.Core.Configuration;
using Nop.Data;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains
{
    [Table("Address_GeoCoordinates_Mapping")]
    public sealed class AddressGeoCoordinatesMapping : BaseEntity
    {
        /// <summary>
        /// Indicates the address to which the geo coordinates are related.
        /// </summary>
        public int AddressId { get; set; }

        /// <summary>
        /// Indicates the latitude.
        /// </summary>
        [NopResourceDisplayName("Plugin.Widgets.GoogleMapsIntegration.Fields.Latitude")]
        public decimal Latitude { get; set; }

        /// <summary>
        /// Indicates the longitude.
        /// </summary>
        [NopResourceDisplayName("Plugin.Widgets.GoogleMapsIntegration.Fields.Longitude")]
        public decimal Longitude { get; set; }
    }



    public class PluginConfigurationSettings : ISettings
    {
        public string ApiKey { get; set; }
    }
}

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Services
{
    using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains;

    public interface IAddressGeoCoordinatesService
    {
        AddressGeoCoordinatesMapping GetAddressGeoCoordinates(int addressId);
        void InsertAddressGeoCoordinates(AddressGeoCoordinatesMapping addressGeoCoordinates, int addressId = 0);
    }

    public sealed class AddressGeoCoordinatesService : IAddressGeoCoordinatesService
    {
        private readonly IRepository<AddressGeoCoordinatesMapping> _repository;

        public AddressGeoCoordinatesService(IRepository<AddressGeoCoordinatesMapping> repository)
        {
            _repository = repository;
        }

        public AddressGeoCoordinatesMapping GetAddressGeoCoordinates(int addressId)
        {
            return _repository.Table.FirstOrDefault(mapping => mapping.AddressId == addressId);
        }

        public void InsertAddressGeoCoordinates(AddressGeoCoordinatesMapping addressGeoCoordinates, int addressId = 0)
        {
            ArgumentNullException.ThrowIfNull(addressGeoCoordinates);

            if (addressId > 0)
                addressGeoCoordinates.AddressId = addressId;

            var existing = GetAddressGeoCoordinates(addressGeoCoordinates.AddressId);
            if (existing == null)
            {
                _repository.Insert(addressGeoCoordinates);
                return;
            }

            existing.Latitude = addressGeoCoordinates.Latitude;
            existing.Longitude = addressGeoCoordinates.Longitude;
            _repository.Update(existing);
        }
    }
}

namespace Nop.Plugin.Widgets.GoogleMapsIntegration.Helpers
{
    using FluentValidation;
    using Nop.Core.Domain.Customers;

    public static class PhoneNumberValidatorExtensions
    {
        public static IRuleBuilderOptions<T, string> IsPhoneNumber<T>(this IRuleBuilder<T, string> ruleBuilder, CustomerSettings customerSettings)
        {
            return ruleBuilder.Matches(@"^[0-9+\-().\s]{6,32}$");
        }
    }
}
