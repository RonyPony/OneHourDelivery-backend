using System;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models
{
    /// <summary>
    /// Represents a request to register a customer.
    /// </summary>
    public class RegisterCustomerRequest
    {
        /// <summary>
        /// Indicates the customer email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Indicates the customer password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// indicates the password confirmation.
        /// </summary>
        public string ConfirmPassword { get; set; }

        /// <summary>
        /// Indicates the customer first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Indicates the customer last name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Indicates the customer day of birth.
        /// </summary>
        public int? DateOfBirthDay { get; set; }

        /// <summary>
        /// Indicates the customer month of birth.
        /// </summary>
        public int? DateOfBirthMonth { get; set; }

        /// <summary>
        /// Indicates the customer year of birth.
        /// </summary>
        public int? DateOfBirthYear { get; set; }

        /// <summary>
        /// Indicates the customer address country id.
        /// </summary>
        public int? CountryId { get; set; }

        /// <summary>
        /// Indicates the customer address country name.
        /// </summary>
        public string CountryName { get; set; }

        /// <summary>
        /// Indicates the customer address state/province id.
        /// </summary>
        public int? StateProvinceId { get; set; }

        /// <summary>
        /// Indicates the customer address state/province name.
        /// </summary>
        public string StateProvinceName { get; set; }

        /// <summary>
        /// Indicates the customer address city.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Indicates the customer address line 1.
        /// </summary>
        public string Address1 { get; set; }

        /// <summary>
        /// Indicates the customer address line 2.
        /// </summary>
        public string Address2 { get; set; }

        /// <summary>
        /// Indicates the customer address zip/postal code.
        /// </summary>
        public string ZipPostalCode { get; set; }

        /// <summary>
        /// Indicates the customer phone number.
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Indicates the customer address latitude.
        /// </summary>
        public decimal Latitude { get; set; }

        /// <summary>
        /// Indicates the customer address longitude.
        /// </summary>
        public decimal Longitude { get; set; }

        /// <summary>
        /// Indicates the customer date of birth.
        /// </summary>
        public DateTime? DateOfBirth { get; set; }
    }
}
