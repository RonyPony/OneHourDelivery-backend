using Nop.Core;
using System;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains
{
    /// <summary>
    /// Represents customer favorite vendors mapping
    /// </summary>
    public sealed class CustomerFavoriteMapping : BaseEntity
    {
        /// <summary>
        /// Customer's id.
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Vendor's id.
        /// </summary>
        public int VendorId { get; set; }

        
        /// <summary>
        /// Registed date.
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }
    }
}
