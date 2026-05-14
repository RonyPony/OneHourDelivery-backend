using System.Threading.Tasks;
using Nop.Data;
using Nop.Plugin.Widgets.GoogleMapsIntegration.Domains;

namespace Nop.Plugin.Widgets.GoogleMapsIntegration.Services
{
    /// <summary>
    /// Address geo coordinates services.
    /// </summary>
    public partial class AddressGeoCoordinatesService : IAddressGeoCoordinatesService
    {
        #region Fields

        private readonly IRepository<AddressGeoCoordinatesMapping> _addressGeoCoordinatesMappingRepository;

        #endregion

        #region Ctor

        /// <summary>
        /// Initializes a new instance of <see cref="AddressGeoCoordinatesService"/>.
        /// </summary>
        /// <param name="addressGeoCoordinatesMappingRepository">An implementation of <see cref="IRepository{AddressGeoCoordinatesMapping}"/>.</param>
        public AddressGeoCoordinatesService(IRepository<AddressGeoCoordinatesMapping> addressGeoCoordinatesMappingRepository)
        {
            _addressGeoCoordinatesMappingRepository = addressGeoCoordinatesMappingRepository;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets an address geo-coordinates.
        /// </summary>
        /// <param name="addressId">The address id</param>
        /// <returns>An instance of <see cref="AddressGeoCoordinatesMapping"/> or <see cref="null"/> if <paramref name="addressId"/> doesn't have registered geo-coordinates.</returns>
        public async Task<AddressGeoCoordinatesMapping> GetAddressGeoCoordinatesAsync(int addressId)
            => await _addressGeoCoordinatesMappingRepository.Table.FirstOrDefaultAsync(geoCoordinates => geoCoordinates.AddressId == addressId);

        /// <summary>
        /// Inserts an address geo-coordinates if doesn't exist and updates it if exists.
        /// </summary>
        /// <param name="addressGeoCoordinates">An instance of <see cref="AddressGeoCoordinatesMapping"/>.</param>
        /// <param name="addressId">The address id.</param>
        public async Task InsertAddressGeoCoordinatesAsync(AddressGeoCoordinatesMapping addressGeoCoordinates, int addressId)
        {
            var foundGeoCoordinates = await _addressGeoCoordinatesMappingRepository.Table.FirstOrDefaultAsync(geoCoordinates => geoCoordinates.AddressId == addressId);

            if (foundGeoCoordinates is null)
            {
                var newAddressGeoCoordinates = new AddressGeoCoordinatesMapping
                {
                    AddressId = addressId,
                    Latitude = addressGeoCoordinates.Latitude,
                    Longitude = addressGeoCoordinates.Longitude
                };

                await _addressGeoCoordinatesMappingRepository.InsertAsync(newAddressGeoCoordinates);
            }
            else
            {
                foundGeoCoordinates.Latitude = addressGeoCoordinates.Latitude;
                foundGeoCoordinates.Longitude = addressGeoCoordinates.Longitude;

                await _addressGeoCoordinatesMappingRepository.UpdateAsync(foundGeoCoordinates);
            }
        }

        /// <summary>
        /// Removes an address geo-coordinates.
        /// </summary>
        /// <param name="addressId">The address id.</param>
        public async Task RemoveAddressGeoCoordinatesAsync(int addressId)
        {
            if (await _addressGeoCoordinatesMappingRepository.Table.FirstOrDefaultAsync(geoCoordinates => geoCoordinates.AddressId == addressId) is AddressGeoCoordinatesMapping addressGeoCoordinates)
                await _addressGeoCoordinatesMappingRepository.DeleteAsync(addressGeoCoordinates);
        }

        #endregion
    }
}
