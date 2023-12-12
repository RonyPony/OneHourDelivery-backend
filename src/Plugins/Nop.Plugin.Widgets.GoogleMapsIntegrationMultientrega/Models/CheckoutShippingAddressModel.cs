using System.Collections.Generic;
using Nop.Web.Framework.Models;
using Nop.Web.Models.Checkout;
using Nop.Web.Models.Common;

namespace Nop.Plugin.Widgets.GoogleMapsIntegrationMultientrega.Models
{
    /// <summary>
    /// Represents a custom checkout shipping address model.
    /// </summary>
    public partial class CheckoutShippingAddressModel : BaseNopModel
    {
        /// <summary>
        /// Initializes a new instance of <see cref="CheckoutShippingAddressModel"/>.
        /// </summary>
        public CheckoutShippingAddressModel()
        {
            ExistingAddresses = new List<AddressModel>();
            InvalidExistingAddresses = new List<AddressModel>();
        }

        /// <summary>
        /// Represents the current user valid existing addresses.
        /// </summary>
        public IList<AddressModel> ExistingAddresses { get; set; }

        /// <summary>
        /// Represents the current user invalid existing addresses.
        /// </summary>
        public IList<AddressModel> InvalidExistingAddresses { get; set; }

        /// <summary>
        /// Used on one-page checkout page, indicates if new address is preselected.
        /// </summary>
        public bool NewAddressPreselected { get; set; }

        /// <summary>
        /// Indicates if pickup in store option is enabled.
        /// </summary>
        public bool DisplayPickupInStore { get; set; }

        /// <summary>
        /// Represents the pickup points.
        /// </summary>
        public CheckoutPickupPointsModel PickupPointsModel { get; set; }
    }
}