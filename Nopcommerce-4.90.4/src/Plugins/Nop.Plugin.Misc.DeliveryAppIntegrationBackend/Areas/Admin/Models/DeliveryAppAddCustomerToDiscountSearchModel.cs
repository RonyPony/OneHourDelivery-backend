using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Areas.Admin.Models
{
    /// <summary>
    /// Represents the customer search model, to assign customer to a discount.
    /// </summary>
    public partial record DeliveryAppAddCustomerToDiscountSearchModel : BaseSearchModel
    {
        /// <summary>
        /// Get or set customer's id.
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Get or set customer's name.
        /// </summary>
        [NopResourceDisplayName("Admin.Customers.Customers.Fields.Email")]
        public string SearchCustomerEmail { get; set; }

        /// <summary>
        /// Get or set customer's active
        /// </summary>
        [NopResourceDisplayName("Admin.Customers.Customers.Fields.Active")]
        public bool Active { get; set; }
    }
}
