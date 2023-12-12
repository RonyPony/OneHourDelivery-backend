using Nop.Core.Domain.Customers;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models;
using System;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Services
{
    /// <summary>
    /// Represents a contract for the services of this plugin.
    /// </summary>
    public interface ICustomerAddressGeocodingServices
    {
        /// <summary>
        /// Retrieves a the date of birth from <see cref="RegisterCustomerRequest"/>.
        /// </summary>
        /// <param name="request">An instance of <see cref="RegisterCustomerRequest"/>.</param>
        /// <returns>A <see cref="DateTime"/> if the date of birth was parsed successfully, <see cref="null"/> otherwise.</returns>
        DateTime? ParseDateOfBirth(RegisterCustomerRequest request);

        /// <summary>
        /// Retrieves a country id by the country name.
        /// </summary>
        /// <param name="countryName">The country name.</param>
        /// <returns>An <see cref="int"/> with the country id if the country was found, <see cref="null"/> otherwise.</returns>
        int? TryGetCountryIdByNameOrDefault(string countryName);

        /// <summary>
        /// Retrieves a state/province id by the state/province name and the country id.
        /// </summary>
        /// <param name="stateProvinceName">The state/province name.</param>
        /// <param name="countryId">The country id.</param>
        /// <returns>An <see cref="int"/> with the state/province id if the state/province was found, <see cref="null"/> otherwise.</returns>
        int? TryGetStateProvinceIdByNameAndCountryIdOrDefault(string stateProvinceName, int? countryId);

        /// <summary>
        /// Checks if an email is already registered.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>A <see cref="bool"/> indicating wheter the given email is already registered or not.</returns>
        bool EmailIsAlreadyRegistered(string email);

        /// <summary>
        /// Checks if a phone number is already registered.
        /// </summary>
        /// <param name="phoneNumber">The phone number.</param>
        /// <returns>A <see cref="bool"/> indicating wheter the given phone number is already registered or not.</returns>
        bool PhoneNumberIsAlreadyRegistered(string phoneNumber);
    }
}
