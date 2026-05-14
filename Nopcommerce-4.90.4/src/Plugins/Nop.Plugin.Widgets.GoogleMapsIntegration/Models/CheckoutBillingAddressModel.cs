using System.Collections.Generic;
using Nop.Web.Framework.Models;
using Nop.Web.Models.Common;

namespace Nop.Plugin.Widgets.GoogleMapsIntegration.Models
{
    /// <summary>
    /// Represents a custom checkout billing address model.
    /// </summary>
    public partial record CheckoutBillingAddressModel : BaseNopModel
    {
        /// <summary>
        /// Initializes a new instance of <see cref="CheckoutBillingAddressModel"/>.
        /// </summary>
        public CheckoutBillingAddressModel()
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
        /// Indicates if shipping address is the same as billing address.
        /// </summary>
        public bool ShipToSameAddress { get; set; }

        /// <summary>
        /// Indicates if shipping to the same address as billing is allowed.
        /// </summary>
        public bool ShipToSameAddressAllowed { get; set; }

        /// <summary>
        /// Used on one-page checkout page, indicates if new address is preselected.
        /// </summary>
        public bool NewAddressPreselected { get; set; }
    }
}
