using System;
using System.Collections.Generic;
using Nop.Data.Mapping;
using Nop.Plugin.Synchronizers.WAPIOrders.Domains;

namespace Nop.Plugin.Synchronizers.WAPIOrders.Mapping
{
    /// <summary>
    /// <see cref="OrderTransactionCodeMapping"/> instance of backward compatibility of table naming.
    /// </summary>
    public partial class NameCompatibility : INameCompatibility
    {
        /// <summary>
        /// Gets table name for mapping with the type.
        /// </summary>
        public Dictionary<Type, string> TableNames => new Dictionary<Type, string>
        {
            { typeof(OrderTransactionCodeMapping), "Order_TransactionCode_Mappings" }
        };

        /// <summary>
        ///  Gets column name for mapping with the entity's property and type.
        /// </summary>
        public Dictionary<(Type, string), string> ColumnName => new Dictionary<(Type, string), string>
        {
            {(typeof(OrderTransactionCodeMapping), "OrderId"), "OrderId"},
            {(typeof(OrderTransactionCodeMapping), "TransactionCode"), "TransactionCode"},
        };
    }
}