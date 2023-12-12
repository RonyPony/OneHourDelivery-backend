using Nop.Core;

namespace Nop.Plugin.Synchronizers.SAPCustomers.Domains
{
    /// <summary>
    /// Represents the model used for creating the mapping table between Customer entity and SapCustomer entity.
    /// </summary>
    public sealed class CustomerSapCustomerMapping : BaseEntity
    {
        /// <summary>
        /// Gets or sets the customer identifier
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the SAP customer identifier
        /// </summary>
        public string SapCustomerId { get; set; }
    }
}
