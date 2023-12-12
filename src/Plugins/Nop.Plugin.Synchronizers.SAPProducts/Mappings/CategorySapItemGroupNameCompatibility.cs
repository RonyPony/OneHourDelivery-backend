using Nop.Data.Mapping;
using Nop.Plugin.Synchronizers.SAPProducts.Domains;
using System;
using System.Collections.Generic;

namespace Nop.Plugin.Synchronizers.SAPProducts.Mappings
{
    /// <summary>
    /// Category SapItemGroup instance of backward compatibility of table naming.
    /// </summary>
    public sealed class CategorySapItemGroupNameCompatibility : INameCompatibility
    {
        Dictionary<Type, string> INameCompatibility.TableNames => new Dictionary<Type, string>
        {
            { typeof(CategorySapItemGroupMapping), "Category_SapItemGroup_Mapping" }
        };

        Dictionary<(Type, string), string> INameCompatibility.ColumnName => new Dictionary<(Type, string), string>
        {
            {(typeof(CategorySapItemGroupMapping), "CategoryId"), "Category_Id"},
            {(typeof(CategorySapItemGroupMapping),  "ItemGroupNumber"), "Item_Group_Number"}
        };
    }
}
