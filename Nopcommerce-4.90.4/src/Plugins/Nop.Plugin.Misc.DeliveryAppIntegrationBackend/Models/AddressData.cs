using Nop.Web.Framework.Models;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models
{
    /// <summary>
    /// Represents an address data
    /// </summary>
    public sealed record AddressData : BaseNopEntityModel
    {
        /// <summary>
        /// Gets or sets address first name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets address last name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets address country
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets address city
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the address 1
        /// </summary>
        public string Address1 { get; set; }

        /// <summary>
        /// Gets or sets the address 2
        /// </summary>
        public string Address2 { get; set; }

        /// <summary>
        /// Gets or sets latitude
        /// </summary>
        public decimal Latitude { get; set; }

        /// <summary>
        /// Gets or sets longitude
        /// </summary>
        public decimal Longitude { get; set; }
    }
}
