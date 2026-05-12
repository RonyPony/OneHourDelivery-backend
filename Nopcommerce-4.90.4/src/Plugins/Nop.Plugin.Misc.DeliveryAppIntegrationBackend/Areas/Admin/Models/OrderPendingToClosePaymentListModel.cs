using Nop.Web.Framework.Models;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Areas.Admin.Models
{

    /// <summary>
    /// Represents an order Pending To Close Payment list model
    /// </summary>
    public partial record OrderPendingToClosePaymentListModel : BasePagedListModel<OrderPendingToClosePaymentModel>
    {
    }
}
