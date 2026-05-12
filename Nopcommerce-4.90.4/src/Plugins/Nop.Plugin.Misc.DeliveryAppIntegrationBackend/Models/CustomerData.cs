using System.Collections.Generic;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models
{
    /// <summary>
    /// Represents a customer response
    /// </summary>
    public sealed class CustomerData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerData"/> class.
        /// </summary>
        /// <param name="id">The identification of customer</param>
        /// <param name="firstName">The first name of customer</param>
        /// <param name="lastName">The last name of customer</param>
        /// <param name="email">The email of customer</param>
        /// <param name="phoneNumber">The phone number of customer</param>
        /// <param name="imageUrl">The image url of customer</param>
        /// <param name="shippingAddress">The shipping address of customer</param>
        /// <param name="addressAlias">The address alias of customer</param>
        /// <param name="addresses">The array of <see cref="List{AddressData}"/>The addresses of customer</param>
        /// <param name="birthDate">The date of birth of the customer</param>
        public CustomerData(int id, 
            string firstName, 
            string lastName,
            string email,
            string phoneNumber,
            string imageUrl,
            string shippingAddress,
            string addressAlias,
            List<AddressData> addresses,
            string birthDate)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            ImageUrl = imageUrl;
            ShippingAddress = shippingAddress;
            Addresses = addresses;
            AddressAlias = addressAlias;
            BirthDate = birthDate;
        }

        /// <summary>
        /// Gets or sets customer identification
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Gets or sets customer first name
        /// </summary>
        public string  FirstName { get; set; }
        
        /// <summary>
        /// Gets or sets customer last name
        /// </summary>
        public string  LastName { get; set; }
        
        /// <summary>
        /// Gets or sets customer email
        /// </summary>
        public string Email { get; set; }
        
        /// <summary>
        /// Gets or sets customer phone number
        /// </summary>
        public string PhoneNumber { get; set; }
        
        /// <summary>
        /// Gets or sets customer image url
        /// </summary>
        public string ImageUrl { get; set; }
        
        /// <summary>
        /// Gets or sets customer delivery address
        /// </summary>
        public string ShippingAddress { get; set; }

        /// <summary>
        /// Gets or sets customer list of addresses
        /// </summary>
        public List<AddressData> Addresses { get; set; }

        /// <summary>
        /// Gets or sets customer address alias
        /// </summary>
        public string AddressAlias { get; set; }

        /// <summary>
        /// Gets or sets the customer date of birth.
        /// </summary>
        public string BirthDate { get; set; }
    }
}
