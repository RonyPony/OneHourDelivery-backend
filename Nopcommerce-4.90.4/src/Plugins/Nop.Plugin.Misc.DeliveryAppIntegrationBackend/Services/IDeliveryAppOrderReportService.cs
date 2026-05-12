using Nop.Core.Domain.Orders;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Services
{
    /// <summary>
    /// Order report service interface
    /// </summary>
    public interface IDeliveryAppOrderReportService
    {
        /// <summary>
        /// Get order average report
        /// </summary>
        /// <param name="storeId">Store identifier</param>
        /// <param name="os">Order status</param>
        /// <returns>Result</returns>
        OrderAverageReportLineSummary OrderAverageReport(int storeId, OrderStatus os, int vendorId);
    }
}
