using Nop.Web.Areas.Admin.Models.Customers;
using Nop.Web.Framework.Models;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Areas.Admin.Models
{
    /// <summary>
    /// Represents the add customer list model.
    /// </summary>
    public partial record DeliveryAppAddCustomerToListModel : BasePagedListModel<CustomerModel>
    {
    }
}
