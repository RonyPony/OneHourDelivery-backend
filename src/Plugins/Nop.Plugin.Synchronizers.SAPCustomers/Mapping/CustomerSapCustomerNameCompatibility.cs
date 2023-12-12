using Nop.Data.Mapping;
using Nop.Plugin.Synchronizers.SAPCustomers.Domains;
using System;
using System.Collections.Generic;

namespace Nop.Plugin.Synchronizers.SAPCustomers.Mapping
{
    /// <summary>
    /// Customer SapCustomer instance of backward compatibility of table naming
    /// </summary>
    public sealed class CustomerSapCustomerNameCompatibility : INameCompatibility
    {
        Dictionary<Type, string> INameCompatibility.TableNames => new Dictionary<Type, string>
        {
            { typeof(CustomerSapCustomerMapping), "Customer_SAPCustomer_Mapping" }
        };

        Dictionary<(Type, string), string> INameCompatibility.ColumnName => new Dictionary<(Type, string), string>
        {
            {(typeof(CustomerSapCustomerMapping), "CustomerId"), "Customer_Id"},
            {(typeof(CustomerSapCustomerMapping),  "SapCustomerId"), "SapCustomer_Id"}
        };
    }
}
