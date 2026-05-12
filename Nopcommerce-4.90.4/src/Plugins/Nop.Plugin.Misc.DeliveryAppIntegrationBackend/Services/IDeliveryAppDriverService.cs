using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models;
using System.Collections.Generic;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Services
{
    /// <summary>
    /// Represents a contract for the delivery app driver services.
    /// </summary>
    public interface IDeliveryAppDriverService
    {
        /// <summary>
        /// Reteieves a list of orders pending to pay for a driver.
        /// </summary>
        /// <param name="driverId">The customer id of the driver.</param>
        /// <returns>An implementation of <see cref="IList{T}"/> where T is <see cref="OrderPendingToClosePayment"/>.</returns>
        IList<DriverProfitModel> GetOrdersPendingToClosePaymentByDriverId(int driverId);

        /// <summary>
        ///  Valuation of vendors or client.
        /// </summary>
        /// <param name="driverRating">driver request.</param>
        void RegisterDriverRatingValoration(DriverRatingMappingRequest driverRating);

        /// <summary>
        /// Get a driver info assigned to order.
        /// </summary>
        /// <param name="orderId">Order's id.</param>
        /// <returns></returns>
        DriverInfoModel GetDriverInfo(int orderId);

        /// <summary>
        /// Get a driver cash collection info.
        /// </summary>
        /// <param name="driverId">DriverId's id.</param>
        /// <returns>The amount of money his carrying and the limit amount </returns>
        DriverPaymentCollection GetPaymentCollection(int driverId);

    }
}

