using Nop.Data.Mapping;
using Nop.Plugin.Synchronizers.SAPProducts.Domains;
using System;
using System.Collections.Generic;

namespace Nop.Plugin.Synchronizers.SAPProducts.Mappings
{
    /// <summary>
    /// Manufacturer SapManufacturer instance of backward compatibility of table naming.
    /// </summary>
    public sealed class ManufacturerSapManufacturerNameCompatibility : INameCompatibility
    {
        Dictionary<Type, string> INameCompatibility.TableNames => new Dictionary<Type, string>
        {
            { typeof(ManufacturerSapManufacturerMapping), "Manufacturer_SapManufacturer_Mapping" }
        };

        Dictionary<(Type, string), string> INameCompatibility.ColumnName => new Dictionary<(Type, string), string>
        {
            {(typeof(ManufacturerSapManufacturerMapping), "ManufacturerId"), "Manufacturer_Id"},
            {(typeof(ManufacturerSapManufacturerMapping),  "SapManufacturerCode"), "Sap_Manufacturer_Code"}
        };
    }
}
