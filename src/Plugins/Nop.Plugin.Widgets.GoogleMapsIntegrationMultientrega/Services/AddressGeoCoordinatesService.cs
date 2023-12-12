using Nop.Data;
using Nop.Plugin.Widgets.GoogleMapsIntegrationMultientrega.Domains;
using Nop.Services.Events;
using System.Linq;

namespace Nop.Plugin.Widgets.GoogleMapsIntegrationMultientrega.Services
{
    /// <summary>
    /// Address geo coordinates services.
    /// </summary>
    public partial class AddressGeoCoordinatesService : IAddressGeoCoordinatesService
    {
        #region Fields

        private readonly IRepository<AddressGeoCoordinatesMapping> _addressGeoCoordinatesMappingRepository;
        private readonly IEventPublisher _eventPublisher;

        #endregion

        #region Ctor

        /// <summary>
        /// Initializes a new instance of <see cref="AddressGeoCoordinatesService"/>.
        /// </summary>
        /// <param name="addressGeoCoordinatesMappingRepository">An implementation of <see cref="IRepository{AddressGeoCoordinatesMapping}"/>.</param>
        /// <param name="eventPublisher">An implementation of <see cref="IEventPublisher"/>.</param>
        public AddressGeoCoordinatesService(
            IRepository<AddressGeoCoordinatesMapping> addressGeoCoordinatesMappingRepository,
            IEventPublisher eventPublisher)
        {
            _addressGeoCoordinatesMappingRepository = addressGeoCoordinatesMappingRepository;
            _eventPublisher = eventPublisher;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets an address geo-coordinates.
        /// </summary>
        /// <param name="addressId">The address id</param>
        /// <returns>An instance of <see cref="AddressGeoCoordinatesMapping"/> or <see cref="null"/> if <paramref name="addressId"/> doesn't have registered geo-coordinates.</returns>
        public AddressGeoCoordinatesMapping GetAddressGeoCoordinates(int addressId)
            => _addressGeoCoordinatesMappingRepository.Table.FirstOrDefault(geoCoordinates => geoCoordinates.AddressId == addressId);

        /// <summary>
        /// Inserts an address geo-coordinates if doesn't exist and updates it if exists.
        /// </summary>
        /// <param name="addressGeoCoordinates">An instance of <see cref="AddressGeoCoordinatesMapping"/>.</param>
        /// <param name="addressId">The address id.</param>
        public void InsertAddressGeoCoordinates(AddressGeoCoordinatesMapping addressGeoCoordinates, int addressId)
        {
            AddressGeoCoordinatesMapping foundGeoCoordinates = _addressGeoCoordinatesMappingRepository.Table.FirstOrDefault(geoCoordinates => geoCoordinates.AddressId == addressId);

            if (foundGeoCoordinates is null)
            {
                var newAddressGeoCoordinates = new AddressGeoCoordinatesMapping
                {
                    AddressId = addressId,
                    Latitude = addressGeoCoordinates.Latitude,
                    Longitude = addressGeoCoordinates.Longitude,
                    ProvinceId = addressGeoCoordinates.ProvinceId,
                    DistrictId = addressGeoCoordinates.DistrictId,
                    TownshipId = addressGeoCoordinates.TownshipId,
                    NeighborhoodId = addressGeoCoordinates.NeighborhoodId
                };

                _addressGeoCoordinatesMappingRepository.Insert(newAddressGeoCoordinates);
                _eventPublisher.EntityInserted(newAddressGeoCoordinates);
            }
            else
            {
                foundGeoCoordinates.Latitude = addressGeoCoordinates.Latitude;
                foundGeoCoordinates.Longitude = addressGeoCoordinates.Longitude;
                foundGeoCoordinates.ProvinceId = addressGeoCoordinates.ProvinceId;
                foundGeoCoordinates.DistrictId = addressGeoCoordinates.DistrictId;
                foundGeoCoordinates.TownshipId = addressGeoCoordinates.TownshipId;
                foundGeoCoordinates.NeighborhoodId = addressGeoCoordinates.NeighborhoodId;

                _addressGeoCoordinatesMappingRepository.Update(foundGeoCoordinates);
                _eventPublisher.EntityUpdated(foundGeoCoordinates);
            }
        }

        /// <summary>
        /// Removes an address geo-coordinates.
        /// </summary>
        /// <param name="addressId">The address id.</param>
        public void RemoveAddressGeoCoordinates(int addressId)
        {
            if (_addressGeoCoordinatesMappingRepository.Table.FirstOrDefault(geoCoordinates => geoCoordinates.AddressId == addressId) is AddressGeoCoordinatesMapping addressGeoCoordinates)
            {
                _addressGeoCoordinatesMappingRepository.Delete(addressGeoCoordinates);
                _eventPublisher.EntityDeleted(addressGeoCoordinates);
            }
        }

        #endregion
    }
}
