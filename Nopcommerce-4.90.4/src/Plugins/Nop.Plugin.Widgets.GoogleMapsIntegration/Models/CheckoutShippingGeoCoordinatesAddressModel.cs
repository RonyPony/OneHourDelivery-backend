using Nop.Web.Framework.Models;

namespace Nop.Plugin.Widgets.GoogleMapsIntegration.Models
{
    /// <summary>
    /// Represents a model for adding and editing customer shipping address with Google Maps API's integration for geo coordinates on checkout page.
    /// </summary>
    public partial class CheckoutShippingGeoCoordinatesAddressModel : BaseNopModel
    {
        /// <summary>
        /// Initializes a new instance of <see cref="CheckoutShippingGeoCoordinatesAddressModel"/>.
        /// </summary>
        public CheckoutShippingGeoCoordinatesAddressModel()
        {
            AddressGeoCoordinatesEditModel = new AddressGeoCoordinatesEditModel();
            CheckoutShippingAddressModel = new CheckoutShippingAddressModel();
        }

        /// <inheritdoc/>
        public AddressGeoCoordinatesEditModel AddressGeoCoordinatesEditModel { get; set; }

        ///<inheritdoc/>
        public CheckoutShippingAddressModel CheckoutShippingAddressModel { get; set; }
    }
}
