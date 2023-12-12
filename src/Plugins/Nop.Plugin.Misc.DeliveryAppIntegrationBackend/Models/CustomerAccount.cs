using Newtonsoft.Json;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models
{
    /// <summary>
    /// Represents the basic account information of a customer.
    /// </summary>
    public sealed class CustomerAccount
    {
        /// <summary>
        /// Indicates the customer id.
        /// </summary>
        [JsonProperty("customerId")]
        public int CustomerId { get; set; }

        /// <summary>
        /// Indicates the customer first name.
        /// </summary>
        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        /// <summary>
        /// Indicates the customer last name.
        /// </summary>
        [JsonProperty("lastName")]
        public string LastName { get; set; }

        /// <summary>
        /// Indicates the customer email.
        /// </summary>
        [JsonProperty("email")]
        public string Email { get; set; }

        /// <summary>
        /// Indicates the customer phone number.
        /// </summary>
        [JsonProperty("phone")]
        public string Phone { get; set; }

        /// <summary>
        /// Indicates the customer picture url.
        /// </summary>
        [JsonProperty("pictureUrl")]
        public string PictureUrl { get; set; }

        /// <summary>
        /// Indicates the customer date of birth.
        /// </summary>
        [JsonProperty("birthday")]
        public string Birthday { get; set; }

        /// <summary>
        /// Indicates the customer vendor id in case is related to a vendor.
        /// </summary>
        [JsonProperty("vendorId")]
        public int VendorId { get; set; }
    }
}
