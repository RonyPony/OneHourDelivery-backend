using Nop.Core;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains
{
    /// <summary>
    /// Represents the relation with customer review and a vendor.
    /// </summary>
    public sealed class VendorReviewMapping: BaseEntity
    {
        /// <summary>
        /// Indicates the vendor id.
        /// </summary>
        public int VendorId { get; set; }

        /// <summary>
        /// Indicates the customer id.
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Indicates the rating given by the client.
        /// </summary>
        public decimal Rating { get; set; }

        /// <summary>
        /// Indicates the review comment given by the client.
        /// </summary>
        public string Comment { get; set; }
    }
}
