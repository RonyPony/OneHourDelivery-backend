using Nop.Web.Framework.Models;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Areas.Admin.Models
{
    /// <summary>
    /// Represents the customer discount list.
    /// </summary>
    public sealed record DeliveryAppCustomerDiscountList : BasePagedListModel<DeliveryAppCustomerCustomDiscountModel>
    {
    }
}
