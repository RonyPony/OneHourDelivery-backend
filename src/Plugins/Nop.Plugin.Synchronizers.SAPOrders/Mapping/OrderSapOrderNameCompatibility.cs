using Nop.Data.Mapping;
using Nop.Plugin.Synchronizers.SAPOrders.Domains;
using System;
using System.Collections.Generic;

namespace Nop.Plugin.Synchronizers.SAPOrders.Mapping
{
    /// <summary>
    /// Order SapOrder instance of backward compatibility of table naming
    /// </summary>
    public sealed class OrderSapOrderNameCompatibility : INameCompatibility
    {
        Dictionary<Type, string> INameCompatibility.TableNames => new Dictionary<Type, string>
        {
            { typeof(OrderSapOrderMapping), "Order_SAPOrder_Mapping" }
        };

        Dictionary<(Type, string), string> INameCompatibility.ColumnName => new Dictionary<(Type, string), string>
        {
            {(typeof(OrderSapOrderMapping), "OrderId"), "Order_Id"},
            {(typeof(OrderSapOrderMapping),  "SapOrderId"), "Sap_Order_Id"}
        };
    }
}
