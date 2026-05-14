using System.Threading.Tasks;
using Nop.Plugin.Widgets.GoogleMapsIntegration.Domains;

namespace Nop.Plugin.Widgets.GoogleMapsIntegration.Services
{
    /// <summary>
    /// Contract for address geo-coordinates services.
    /// </summary>
    public partial interface IAddressGeoCoordinatesService
    {
        #region Address GeoCoordinates Mapping

        /// <summary>
        /// Gets an address geo-coordinates.
        /// </summary>
        /// <param name="addressId">The address id</param>
        /// <returns>An instance of <see cref="AddressGeoCoordinatesMapping"/>.</returns>
        Task<AddressGeoCoordinatesMapping> GetAddressGeoCoordinatesAsync(int addressId);

        /// <summary>
        /// Inserts an address geo-coordinates.
        /// </summary>
        /// <param name="addressGeoCoordinates">An instance of <see cref="AddressGeoCoordinatesMapping"/>.</param>
        /// <param name="addressId">The address id.</param>
        Task InsertAddressGeoCoordinatesAsync(AddressGeoCoordinatesMapping addressGeoCoordinates, int addressId);

        /// <summary>
        /// Removes an address geo-coordinates.
        /// </summary>
        /// <param name="addressId">The address id.</param>
        Task RemoveAddressGeoCoordinatesAsync(int addressId);

        #endregion
    }
}
