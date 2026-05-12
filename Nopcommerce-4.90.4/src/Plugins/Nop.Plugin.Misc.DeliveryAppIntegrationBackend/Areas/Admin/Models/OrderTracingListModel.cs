using Nop.Web.Framework.Models;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Areas.Admin.Models
{
    /// <summary>
    /// Represents an order tracing list model
    /// </summary>
    public partial record OrderTracingListModel : BasePagedListModel<OrderTracingModel>
    {

    }
}
