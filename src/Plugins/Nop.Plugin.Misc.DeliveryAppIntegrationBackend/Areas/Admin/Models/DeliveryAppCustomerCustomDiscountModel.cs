using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Areas.Admin.Models
{
    /// <summary>
    /// Represents the customer custom discount model.
    /// </summary>
    public class DeliveryAppCustomerCustomDiscountModel : BaseNopModel
    {
        /// <summary>
        /// Get or set customer's id.
        /// </summary>

        public int CustomerId { get; set; }

        /// <summary>
        /// Get or set customer's name.
        /// </summary>
        [NopResourceDisplayName("Admin.Customers.CustomerAttributes.Fields.Name")]
        public string CustomerName { get; set; }

        /// <summary>
        /// Get or set discount's id.
        /// </summary>
        [NopResourceDisplayName("Admin.Customers.CustomerAttributes.Fields.Id")]
        public int DiscountId { get; set; }

        /// <summary>
        /// Get or set discount's name
        /// </summary>
        [NopResourceDisplayName("Admin.Promotions.Discounts.Fields.Name")]
        public string DiscountName { get; set; }

        /// <summary>
        /// Get or set discount's amount
        /// </summary>
        [NopResourceDisplayName("Admin.Promotions.Discounts.Fields.Discount")]
        public decimal DiscountAmount { get; set; }
    }
}
