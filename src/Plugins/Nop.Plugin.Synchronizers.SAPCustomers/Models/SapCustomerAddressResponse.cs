using System.Collections.Generic;

namespace Nop.Plugin.Synchronizers.SAPCustomers.Models
{
    /// <summary>
    /// Represents the model class for mapping the SAP customer's address response.
    /// </summary>
    public sealed class SapCustomerAddressResponse : SapBaseResponse
    {
        /// <summary>
        /// Represents some extra information.
        /// </summary>
        public List<SapCustomerAddressModel> Extra { get; set; }
    }
}
