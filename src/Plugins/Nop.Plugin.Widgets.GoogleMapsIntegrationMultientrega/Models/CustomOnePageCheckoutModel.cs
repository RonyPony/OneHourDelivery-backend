using Nop.Web.Framework.Models;
using Nop.Web.Models.Checkout;

namespace Nop.Plugin.Widgets.GoogleMapsIntegrationMultientrega.Models
{
    /// <summary>
    /// Represents a customized one page checkout model (<see cref="OnePageCheckoutModel"/>).
    /// </summary>
    public partial class CustomOnePageCheckoutModel : BaseNopModel
    {
        /// <summary>
        /// Indicates if shipping is required.
        /// </summary>
        public bool ShippingRequired { get; set; }

        /// <summary>
        /// Indicates if billing address checkout step is disabled.
        /// </summary>
        public bool DisableBillingAddressCheckoutStep { get; set; }

        /// <inheritdoc/>
        public CheckoutBillingGeoCoordinatesAddressModel CheckoutBillingGeoCoordinatesAddressModel { get; set; }
    }
}
