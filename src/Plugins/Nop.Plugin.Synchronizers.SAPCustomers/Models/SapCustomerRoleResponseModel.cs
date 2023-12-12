using System.Collections.Generic;

namespace Nop.Plugin.Synchronizers.SAPCustomers.Models
{
    /// <summary>
    /// Represents the model class for mapping the SAP customer's role response.
    /// </summary>
    public sealed class SapCustomerRoleResponseModel : SapBaseResponse
    {
        /// <summary>
        /// Represents some extra information.
        /// </summary>
        public List<SapCustomerRoleModel> Extra { get; set; }
    }
}
