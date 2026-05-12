using Nop.Core.Domain.Discounts;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains;
using System;
using System.Collections.Generic;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Services
{
    /// <summary>
    /// Represents a contract for the delivery app discount services.
    /// </summary>
    public interface IDeliveryAppDiscountService
    {
        /// <summary>
        /// Gets all discounts
        /// </summary>
        /// <param name="discountType">Discount type; pass null to load all records</param>
        /// <param name="couponCode">Coupon code to find (exact match); pass null or empty to load all records</param>
        /// <param name="discountName">Discount name; pass null or empty to load all records</param>
        /// <param name="showHidden">A value indicating whether to show expired and not started discounts</param>
        /// <param name="startDateUtc">Discount start date; pass null to load all records</param>
        /// <param name="endDateUtc">Discount end date; pass null to load all records</param>
        /// <param name="vendorId">Vendor id; pass 0 to retrieve discounts from all vendors</param>
        /// <returns>Discounts</returns>
        IList<Discount> GetAllDiscounts(DiscountType? discountType = null,
            string couponCode = null, string discountName = null, bool showHidden = false,
            DateTime? startDateUtc = null, DateTime? endDateUtc = null, int vendorId = 0);

        /// <summary>
        /// Attach a discount coupon to a user.
        /// </summary>
        /// <param name="customerDiscountMapping">An instance of <see cref="CustomerDiscountMapping"/></param>
        void AttachCouponDiscountToCustomer(CustomerDiscountMapping customerDiscountMapping);

        /// <summary>
        /// Gets the coupon discount applied to a given customer.
        /// </summary>
        /// <param name="customerId">The customer that the coupon discount will be searched.</param>
        /// <param name="discountId">Discount's id.</param>
        /// <returns>The customer discount mapping. </returns>
        CustomerDiscountMapping GetDiscountAppliedToCustomer(int customerId, int discountId);

        /// <summary>
        /// Deletes a discount-customer mapping record
        /// </summary>
        /// <param name="discountCustomerMapping">Discount-customer mapping</param>
        void DeleteDiscountCustomerMapping(CustomerDiscountMapping discountCustomerMapping);
    }
}
