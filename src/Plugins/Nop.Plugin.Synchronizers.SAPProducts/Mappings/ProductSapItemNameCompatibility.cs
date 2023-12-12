using Nop.Data.Mapping;
using Nop.Plugin.Synchronizers.SAPProducts.Domains;
using System;
using System.Collections.Generic;

namespace Nop.Plugin.Synchronizers.SAPProducts.Mappings
{
    /// <summary>
    /// Product SapItem instance of backward compatibility of table naming.
    /// </summary>
    public sealed class ProductSapItemNameCompatibility : INameCompatibility
    {
        Dictionary<Type, string> INameCompatibility.TableNames => new Dictionary<Type, string>
        {
            { typeof(ProductSapItemMapping), "Product_SapItem_Mapping" }
        };

        Dictionary<(Type, string), string> INameCompatibility.ColumnName => new Dictionary<(Type, string), string>
        {
            {(typeof(ProductSapItemMapping), "ProductId"), "Product_Id"},
            {(typeof(ProductSapItemMapping),  "SapItemCode"), "Sap_Item_Code"}
        };
    }
}
