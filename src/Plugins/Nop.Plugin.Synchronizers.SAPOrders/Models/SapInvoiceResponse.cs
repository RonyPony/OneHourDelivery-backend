using System.Collections.Generic;

namespace Nop.Plugin.Synchronizers.SAPOrders.Models
{
    /// <summary>
    /// Response that is received from SAP API when consulting an order.
    /// </summary>
    public sealed class SapInvoiceResponse : BaseSapResponse<List<object>>
    {
    }
}
