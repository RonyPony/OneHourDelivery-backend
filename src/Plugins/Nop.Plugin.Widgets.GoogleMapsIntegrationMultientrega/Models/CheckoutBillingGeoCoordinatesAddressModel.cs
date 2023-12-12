using Nop.Web.Framework.Models;

namespace Nop.Plugin.Widgets.GoogleMapsIntegrationMultientrega.Models
{
    /// <summary>
    /// Represents a model for adding and editing customer billing address with Google Maps API's integration for geo coordinates on checkout page.
    /// </summary>
    public partial class CheckoutBillingGeoCoordinatesAddressModel : BaseNopModel
    {
        /// <summary>
        /// Initializes a new instance of <see cref="CheckoutBillingGeoCoordinatesAddressModel"/>.
        /// </summary>
        public CheckoutBillingGeoCoordinatesAddressModel()
        {
            AddressGeoCoordinatesEditModel = new AddressGeoCoordinatesEditModel();
            CheckoutBillingAddressModel = new CheckoutBillingAddressModel();
        }

        /// <inheritdoc/>
        public AddressGeoCoordinatesEditModel AddressGeoCoordinatesEditModel { get; set; }

        ///<inheritdoc/>
        public CheckoutBillingAddressModel CheckoutBillingAddressModel { get; set; }
    }
}
