using Nop.Core;
using Nop.Core.Configuration;
using Nop.Data;

namespace Nop.Plugin.Widgets.GoogleMapsIntegration.Domains
{
    public class AddressGeoCoordinatesMapping : BaseEntity
    {
        public int AddressId { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
    }

    public class PluginConfigurationSettings : ISettings
    {
        public string ApiKey { get; set; }
    }
}

namespace Nop.Plugin.Widgets.GoogleMapsIntegration.Services
{
    using Nop.Plugin.Widgets.GoogleMapsIntegration.Domains;

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

