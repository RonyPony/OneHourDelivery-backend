using Nop.Plugin.Payments.CyberSource.Domains;
using Nop.Plugin.Payments.CyberSource.Helpers;
using Nop.Plugin.Payments.CyberSource.Models;
using System.Collections.Generic;

namespace Nop.Plugin.Payments.CyberSource.Services
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDeliveryAppPaymentService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        RemotePost GetPostForPaymentForm(int orderId);

        /// <summary>
        ///  Register the token payment when the transaction is complete.
        /// </summary>
        /// <param name="customerPayment">customer's payment request</param>
        void InsertTokenPayment(CustomerPaymentTokenMapping customerPayment);

        /// <summary>
        /// Validates a card already exist before registering again
        /// </summary>
        /// <param name="customerId">customer Id</param>
        /// <param name="cardLastFourDigits">Last four digits of the card </param>
        /// <param name="cardExpirationDate">Expiration date MM-yyyy</param>
        /// <returns></returns>
        bool ValidateCardAlreadyExist(int customerId, string cardLastFourDigits, string cardExpirationDate);

        /// <summary>
        ///  Retrive the value of evaluation.
        /// </summary>
        /// <param name="customerId">Customer's id</param>
        /// <param name="cardDigits">Customer's payment card digits</param>
        /// <returns>a boolean value</returns>
        CustomerPaymentTokenMapping CallAlreadyCustomerRegisteredCard(int customerId, string cardDigits, string cardExpirationDate);

        /// <summary>
        /// Returns a list of te customer registered cards
        /// </summary>
        /// <param name="customerId">Id of the customer</param>
        /// <returns></returns>
        IEnumerable<RegisteredCard> GetCustomerRegisteredCards(int customerId);

        /// <summary>
        /// Deletes a registeres card
        /// </summary>
        /// <param name="cardToDelete">Card to be deleted</param>
        void DeleteRegisteredCard(int customerId, RegisteredCard cardToDelete);
    }
}
