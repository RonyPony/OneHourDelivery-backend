using Nop.Core.Domain.Orders;
using Nop.Plugin.Payments.CyberSource.Models;

namespace Nop.Plugin.Payments.CyberSource.Services
{
    /// <summary>
    /// Represents a contract for cybersource plugin services.
    /// </summary>
    public interface ICyberSourceService
    {
        /// <summary>
        /// Creates a remote post and redirects the client to CyberSource payment endpoint.
        /// </summary>
        /// <param name="order"><see cref="Order"/> being processed</param>
        /// <param name="paymentInfoModel">Payment information</param>
        void ProcessTransaction(Order order, CyberSourcePaymentInfoModel paymentInfoModel);

        /// <summary>
        /// Creates a remote post and redirects the client to CyberSource payment endpoint.
        /// </summary>
        /// <param name="order"><see cref="Order"/> being processed</param>
        /// <param name="paymentInfoModel">Payment information</param>
        void ProcessTransactionWithToken(Order order, CyberSourceTokenPaymentInfoModel paymentInfoModel);

        /// <summary>
        /// Takes the customer cards information to register into the CyberSource page and create a token
        /// </summary>
        /// <param name="paymentInfoModel">Customer's payment information</param>
        void RegisterNewCard(CyberSourceRegisterSingleCard paymentInfoModel);

        /// <summary>
        /// Logs to the database.
        /// </summary>
        /// <param name="logEntry">An instance of <see cref="CyberSourceTransactionLog"/></param>
        void Log(CyberSourceTransactionLog logEntry);

        /// <summary>
        /// Sends the order payment declined required notifications.
        /// </summary>
        /// <param name="order">An instance of <see cref="Order"/>.</param>
        void SendPaymentDeclinedNotificationsAndSaveNotes(Order order);
    }
}
