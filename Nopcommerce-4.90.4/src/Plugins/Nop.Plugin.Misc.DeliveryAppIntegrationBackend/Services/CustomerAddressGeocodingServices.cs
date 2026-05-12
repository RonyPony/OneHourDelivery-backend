using Nop.Core.Domain.Common;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Directory;
using Nop.Data;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models;
using System;
using System.Linq;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Services
{
    /// <summary>
    /// Represents the services implementation for this plugin.
    /// </summary>
    public sealed class CustomerAddressGeocodingServices : ICustomerAddressGeocodingServices
    {
        #region Fields

        private readonly IRepository<Country> _countryRepository;
        private readonly IRepository<StateProvince> _stateProvinceRepository;
        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<GenericAttribute> _genericAttributeRepository;

        #endregion

        #region Ctor

        /// <summary>
        /// Initializes a new instance of <see cref="CustomerAddressGeocodingServices"/>.
        /// </summary>
        /// <param name="countryRepository">An implementation of <see cref="IRepository{T}"/> where T is <see cref="Country"/>.</param>
        /// <param name="stateProvinceRepository">An implementation of <see cref="IRepository{T}"/> where T is <see cref="StateProvince"/>.</param>
        /// <param name="customerRepository">An implementation of <see cref="IRepository{T}"/> where T is <see cref="Customer"/>.</param>
        /// <param name="genericAttributeRepository">An implementation of <see cref="IRepository{T}"/> where T is <see cref="GenericAttribute"/>.</param>
        /// <param name="emailAccountService">An implementation of <see cref="IRepository{T}"/> where T is <see cref="GenericAttribute"/>.</param>
        public CustomerAddressGeocodingServices(
            IRepository<Country> countryRepository,
            IRepository<StateProvince> stateProvinceRepository,
            IRepository<Customer> customerRepository,
            IRepository<GenericAttribute> genericAttributeRepository
            )
        {
            _countryRepository = countryRepository;
            _stateProvinceRepository = stateProvinceRepository;
            _customerRepository = customerRepository;
            _genericAttributeRepository = genericAttributeRepository;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Retrieves a the date of birth from <see cref="RegisterCustomerRequest"/>.
        /// </summary>
        /// <param name="request">An instance of <see cref="RegisterCustomerRequest"/>.</param>
        /// <returns>A <see cref="DateTime"/> if the date of birth was parsed successfully, <see cref="null"/> otherwise.</returns>
        public DateTime? ParseDateOfBirth(RegisterCustomerRequest request)
        {
            if (request.DateOfBirth != null)
                return request.DateOfBirth;

            if (!request.DateOfBirthYear.HasValue || !request.DateOfBirthMonth.HasValue || !request.DateOfBirthDay.HasValue)
                return null;

            DateTime? dateOfBirth = null;
            try
            {
                dateOfBirth = new DateTime(request.DateOfBirthYear.Value, request.DateOfBirthMonth.Value, request.DateOfBirthDay.Value);
            }
            catch { }

            return dateOfBirth;
        }

        /// <summary>
        /// Retrieves a country id by the country name.
        /// </summary>
        /// <param name="countryName">The country name.</param>
        /// <returns>An <see cref="int"/> with the country id if the country was found, <see cref="null"/> otherwise.</returns>
        public int? TryGetCountryIdByNameOrDefault(string countryName)
        {
            if (string.IsNullOrEmpty(countryName) || string.IsNullOrWhiteSpace(countryName))
                return null;

            var country = _countryRepository.Table.Where(c => c.Name == countryName).FirstOrDefault();

            if (country is null)
                return null;

            return country.Id;
        }

        /// <summary>
        /// Retrieves a state/province id by the state/province name and the country id.
        /// </summary>
        /// <param name="stateProvinceName">The state/province name.</param>
        /// <param name="countryId">The country id.</param>
        /// <returns>An <see cref="int"/> with the state/province id if the state/province was found, <see cref="null"/> otherwise.</returns>
        public int? TryGetStateProvinceIdByNameAndCountryIdOrDefault(string stateProvinceName, int? countryId)
        {
            if (string.IsNullOrEmpty(stateProvinceName) || string.IsNullOrWhiteSpace(stateProvinceName) || countryId is null)
                return null;

            var stateProvince = _stateProvinceRepository.Table.Where(s => s.Name == stateProvinceName && s.CountryId == countryId).FirstOrDefault();

            if (stateProvince is null)
                return null;

            return stateProvince.Id;
        }

        /// <summary>
        /// Checks if an email is already registered.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>A <see cref="bool"/> indicating wheter the given email is already registered or not.</returns>
        public bool EmailIsAlreadyRegistered(string email)
        {
            return _customerRepository.Table.FirstOrDefault(customer => customer.Email == email && !customer.Deleted) != null;
        }


        /// <summary>
        /// Checks if a phone number is already registered.
        /// </summary>
        /// <param name="phoneNumber">The phone number.</param>
        /// <returns>A <see cref="bool"/> indicating wheter the given phone number is already registered or not.</returns>
        public bool PhoneNumberIsAlreadyRegistered(string phoneNumber)
        {
            string customerKeyGroup = new Customer().GetType().Name;

            return _genericAttributeRepository.Table
                .FirstOrDefault(attribute => attribute.KeyGroup == customerKeyGroup
                                             && attribute.Key == "Phone"
                                             && attribute.Value == phoneNumber) != null;
        }

        #endregion
    }
}
