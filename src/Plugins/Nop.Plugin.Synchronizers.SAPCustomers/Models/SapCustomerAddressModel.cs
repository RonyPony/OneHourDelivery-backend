namespace Nop.Plugin.Synchronizers.SAPCustomers.Models
{
    /// <summary>
    /// Represents the model class for mapping the SAP customer's address.
    /// </summary>
    public sealed class SapCustomerAddressModel
    {
        /// <summary>
        /// Represents the address identification.
        /// </summary>
        public string AddressId { get; set; }

        /// <summary>
        /// Represents the address's type.
        /// </summary>
        public string AddressType { get; set; }

        /// <summary>
        /// Represents the description/name of the street.
        /// </summary>
        public string Street { get; set; }

        /// <summary>
        /// Represents the description/name of the  block.
        /// </summary>
        public string Block { get; set; }

        /// <summary>
        /// Represents the description/name of the city.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Represents the description/name of the zip code.
        /// </summary>
        public string ZipCode { get; set; }

        /// <summary>
        /// Represents the description/name of the County.
        /// </summary>
        public string County { get; set; }

        /// <summary>
        /// Represents the code of the state.
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// Represents the description/name of the customer's state.
        /// </summary>
        public string StateName { get; set; }

        /// <summary>
        /// Represents the code of the country.
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Represents the description/name of the country.
        /// </summary>
        public string CountryName { get; set; }
    }
}
