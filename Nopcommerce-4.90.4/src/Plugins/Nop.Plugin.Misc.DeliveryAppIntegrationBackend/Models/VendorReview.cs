using Nop.Web.Framework.Models;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models
{
    /// <summary>
    /// Represents a Vendor review.
    /// </summary>
    public sealed record VendorReview : BaseNopEntityModel
    {
        /// <summary>
        /// Indicates the rating.
        /// </summary>
        public decimal Rating { get; set; }

        /// <summary>
        /// Indicates the review comment.
        /// </summary>
        public string Comment { get; set; }
    }
}
