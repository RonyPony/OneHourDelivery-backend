using Nop.Core.Domain.Orders;
using System;
using System.Collections.Generic;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Services
{
    /// <summary>
    /// Represents order pending to pay report services interface.
    /// </summary>
    public interface IOrderPendingToPayReportServices
    {
        /// <summary>
        /// Get order pending to close payment average report.
        /// </summary>
        /// <param name="vendorId">Vendor identifier; pass 0 to ignore this parameter.</param>
        /// <param name="driverId">Driver identifier; pass 0 to ignore this parameter.</param>
        /// <param name="paymentMethodSystemName">Payment method system name; null to load all records.</param>
        /// <param name="psIdsVendor">Store payment status identifiers.</param>
        /// <param name="psIdsDriver">Driver payment status identifiers.</param>
        /// <param name="startTimeUtc">Start date.</param>
        /// <param name="endTimeUtc">End date.</param>
        /// <returns>An instance of <see cref="OrderAverageReportLine"/>.</returns>
        OrderAverageReportLine GetOrderAverageReportLine(int vendorId = 0, int driverId = 0, string paymentMethodSystemName = null,
            List<int> psIdsVendor = null, List<int> psIdsDriver = null, DateTime? startTimeUtc = null, DateTime? endTimeUtc = null);

        /// <summary>
        /// Get profit report
        /// </summary>
        /// <param name="vendorId">Vendor identifier; pass 0 to ignore this parameter.</param>
        /// <param name="driverId">Driver identifier; pass 0 to ignore this parameter.</param>
        /// <param name="paymentMethodSystemName">Payment method system name; null to load all records.</param>
        /// <param name="psIdsVendor">Store payment status identifiers.</param>
        /// <param name="psIdsDriver">Driver payment status identifiers.</param>
        /// <param name="startTimeUtc">Start date.</param>
        /// <param name="endTimeUtc">End date.</param>
        /// <returns>A <see cref="decimal"/>.</returns>
        decimal ProfitReport(int vendorId = 0, int driverId = 0, string paymentMethodSystemName = null,
            List<int> psIdsVendor = null, List<int> psIdsDriver = null, DateTime? startTimeUtc = null, DateTime? endTimeUtc = null);
    }
}
