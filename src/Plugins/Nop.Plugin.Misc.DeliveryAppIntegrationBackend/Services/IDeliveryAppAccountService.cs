using Nop.Core.Domain.Customers;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Services
{
    /// <summary>
    /// Represents a contract for the delivery app account services.
    /// </summary>
    public interface IDeliveryAppAccountService
    {
        /// <summary>
        /// Retrieves the basic account information of a customer by the customer email.
        /// </summary>
        /// <param name="email">The emailof the customer.</param>
        /// <returns>An instance of <see cref="CustomerAccount"/>.</returns>
        CustomerAccount GetCustomerAccountByEmail(string email);

        /// <summary>
        /// Retrieves a customer date of birth as a string with the format yyyy/MM/dd.
        /// </summary>
        /// <param name="customer">An instance of <see cref="Customer"/>.</param>
        /// <returns>A <see cref="string"/> containing the date of birth.</returns>
        string GetCustomerDateOfBirthAsString(Customer customer);

        /// <summary>
        /// Inserts an email with verification code to the email's queue.
        /// </summary>
        /// <param name="customer">The customer who receives the message.</param>
        /// <param name="verificationCode">The verification code.</param>
        /// <returns>An instance of <see cref="SendEmailResult"/>.</returns>
        SendEmailResult SendVerificationCode(Customer customer, int verificationCode);

        /// <summary>
        /// Retrieves a value that indicates if a given phone number is already registered.
        /// </summary>
        /// <param name="phoneNumber">The phone number to verify.</param>
        /// <param name="customerId">A customer id.</param>
        /// <returns>
        /// <see cref="true"/> if the contact is found, also <see cref="true"/> when <paramref name="customerId"/> is provided
        /// and the phone number is found by any other customer id, otherwise <see cref="false"/>.
        /// </returns>
        bool PhoneNumberAlreadyRegistered(string phoneNumber, int customerId = 0);

        bool deleteAccount(Customer customer);
    }
}
