using Nop.Data.Mapping;
using Nop.Plugin.Synchronizers.CommonERPTables.Domains;
using System;
using System.Collections.Generic;

namespace Nop.Plugin.Synchronizers.CommonERPTables.Mapping
{
    /// <summary>
    /// Common ERP instance of backward compatibility of table naming
    /// </summary>
    public sealed class CommonErpTablesNameCompatibility : INameCompatibility
    {
        Dictionary<Type, string> INameCompatibility.TableNames => new Dictionary<Type, string>
        {
            { typeof(ErpCustomersNopCommerceCustomersMapping), "Customer_ERPCustomer_Mapping" },
            { typeof(ErpOrdersNopCommerceOrdersMapping), "Order_ERPOrder_Mapping" },
            { typeof(ErpProductsNopCommerceProductsMapping), "Product_ERPProduct_Mapping" }
        };

        Dictionary<(Type, string), string> INameCompatibility.ColumnName => new Dictionary<(Type, string), string>
        {
            { (typeof(ErpCustomersNopCommerceCustomersMapping), "CustomerId"), "Customer_Id" },
            { (typeof(ErpCustomersNopCommerceCustomersMapping), "ErpCustomerId"), "ErpCustomer_Id" },

            { (typeof(ErpOrdersNopCommerceOrdersMapping), "OrderId"), "Order_Id" },
            { (typeof(ErpOrdersNopCommerceOrdersMapping), "ErpOrderId"), "ErpOrder_Id" },

            { (typeof(ErpProductsNopCommerceProductsMapping), "ProductId"), "Product_Id" },
            { (typeof(ErpProductsNopCommerceProductsMapping), "ErpProductId"), "ErpProduct_Id" }
        };
    }
}