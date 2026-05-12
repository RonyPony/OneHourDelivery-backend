using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains;
using System.Collections.Generic;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Areas.Admin.Factories
{
    /// <summary>
    /// Export manager interface
    /// </summary>
    public partial interface IOrderExportManager
    {
        /// <summary>
        /// Export order list to XML
        /// </summary>
        /// <param name="orders">Orders</param>
        /// <returns>Result in XML format</returns>
        string ExportOrdersToXml(IList<OrderPendingToClosePayment> orders);

        /// <summary>
        /// Export orders to XLSX
        /// </summary>
        /// <param name="orders">Orders</param>
        byte[] ExportOrdersToXlsx(IList<OrderPendingToClosePayment> orders);

        /// <summary>
        /// Export vendor order earning list to XML
        /// </summary>
        /// <param name="orders">Orders</param>
        /// <returns>Result in XML format</returns>
        string ExportVendorOrdersEarningToXml(IList<OrderPendingToClosePayment> orders);

        /// <summary>
        /// Export vendor orders earning to XLSX
        /// </summary>
        /// <param name="orders">Orders</param>
        byte[] ExportVendorOrdersEarningToXlsx(IList<OrderPendingToClosePayment> orders);
    }
}
