using System.Collections.Generic;

namespace Nop.Plugin.Synchronizers.SAPCustomers.Models
{
    /// <summary>
    /// Represents the model class for mapping the SAP customer's response.
    /// </summary>
    public sealed class SapCustomerResponse : SapBaseResponse
    {
        /// <summary>
        /// Represents the some extra information.
        /// </summary>
        public List<SapCustomerModel> Extra { get; set; }
    }
}
