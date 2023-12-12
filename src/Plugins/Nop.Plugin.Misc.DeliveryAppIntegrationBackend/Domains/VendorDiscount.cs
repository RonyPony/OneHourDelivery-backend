using Nop.Core;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains
{
    /// <summary>
    /// Represents a relation between a vendor and a discount.
    /// </summary>
    public sealed class VendorDiscount : BaseEntity
    {
        /// <summary>
        /// Indicates the vendor id.
        /// </summary>
        public int VendorId { get; set; }

        /// <summary>
        /// Indicates the discount id.
        /// </summary>
        public int DiscountId { get; set; }
    }
}
