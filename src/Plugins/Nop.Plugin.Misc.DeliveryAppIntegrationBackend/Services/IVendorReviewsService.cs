using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models;
using System.Collections.Generic;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Services
{
    /// <summary>
    /// Represents a contract for <see cref="VendorReviewMapping"/> services.
    /// </summary>
    public interface IVendorReviewsService
    {
        /// <summary>
        /// Gets a collection of vendor's reviews.
        /// </summary>
        /// <param name="vendorId">Indicates the vendor id.</param>
        /// <returns>An instance of <see cref="IEnumerable{VendorReview}"/></returns>
        IEnumerable<VendorReview> GetReviewsByVendor(int vendorId);

        /// <summary>
        /// Inserts a vendor review.
        /// </summary>/
        /// <param name="vendorReview">An instance of <see cref="VendorReviewModel"/></param>
        void InsertReview(VendorReviewModel vendorReview);

        /// <summary>
        /// Updates a vendor review.
        /// </summary>/
        /// <param name="vendorReview">An instance of <see cref="VendorReviewMapping"/></param>
        void UpdateReview(VendorReviewMapping vendorReview);
    }
}
