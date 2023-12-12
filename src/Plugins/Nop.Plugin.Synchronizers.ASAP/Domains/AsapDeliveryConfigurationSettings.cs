using Nop.Core;
using Nop.Core.Configuration;

namespace Nop.Plugin.Synchronizers.ASAP.Domains
{
    /// <summary>
    /// Represents the entity for storing the ASAP Synchronizer configuration for requesting synchronizing the customers.
    /// </summary>
    public sealed class AsapDeliveryConfigurationSettings : BaseEntity, ISettings
    {
        /// <summary>
        /// Represents the api key in ASAP service.
        /// </summary>
        public string ApiKey { get; set; }

        /// <summary>
        /// Represents the ASAP service URL that the plugin will be connecting to.
        /// </summary>
        public string ServiceUrl { get; set; }

        /// <summary>
        /// Represents the shared secret in ASAP service.
        /// </summary>
        public string SharedSecret { get; set; }

        /// <summary>
        /// Represent the user token in ASAP service.
        /// </summary>
        public string UserToken { get; set; }

        /// <summary>
        /// Represent the email of user account in ASAP service.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Represents the rate given to Delivery ASAP shipping.
        /// </summary>
        public decimal Rate { get; set; }

        /// <summary>
        /// Rpresents default Warehouse of Delivery ASAP plugin.
        /// </summary>
        public int DefaultWarehouseId { get; set; }
        
        /// <summary>
        /// Represents a description of Delivery ASAP plugin.
        /// </summary>
        public string Description { get; set; }
    }
}
