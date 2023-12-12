using Nop.Plugin.Widgets.GoogleMapsIntegrationMultientrega.Domains;

namespace Nop.Plugin.Widgets.GoogleMapsIntegrationMultientrega.Services
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
        AddressGeoCoordinatesMapping GetAddressGeoCoordinates(int addressId);

        /// <summary>
        /// Inserts an address geo-coordinates.
        /// </summary>
        /// <param name="addressGeoCoordinates">An instance of <see cref="AddressGeoCoordinatesMapping"/>.</param>
        /// <param name="addressId">The address id.</param>
        void InsertAddressGeoCoordinates(AddressGeoCoordinatesMapping addressGeoCoordinates, int addressId);

        /// <summary>
        /// Removes an address geo-coordinates.
        /// </summary>
        /// <param name="addressId">The address id.</param>
        void RemoveAddressGeoCoordinates(int addressId);

        #endregion
    }
}
