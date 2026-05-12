using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Areas.Admin.Models
{
    public partial record DeliveryAppCustomerModel : BaseNopModel
    {
        /// <summary>
        /// Get or set customer's name.
        /// </summary>
        [NopResourceDisplayName("Admin.Customers.Customers.Fields.Email")]
        public string CustomerEmail { get; set; }

        /// <summary>
        /// Get or set customer's active
        /// </summary>
        [NopResourceDisplayName("Admin.Customers.Customers.Fields.Active")]
        public bool Active { get; set; }
    }
}
