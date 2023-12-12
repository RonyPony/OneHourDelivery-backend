using Nop.Data.Mapping;
using Nop.Plugin.Widgets.GoogleMapsIntegrationMultientrega.Domains;
using System;
using System.Collections.Generic;

namespace Nop.Plugin.Widgets.GoogleMapsIntegrationMultientrega.Mapping
{
    /// <summary>
    /// An implementation of <see cref="INameCompatibility"/> for <see cref="MultientregaProvinceStateProvinceMapping"/>.
    /// </summary>
    public sealed class MultientregaProvinceStateProvinceMappingNameCompatibilithy : INameCompatibility
    {
        ///<inheritdoc/>
        public Dictionary<Type, string> TableNames => new Dictionary<Type, string>
        {
            { typeof(MultientregaProvinceStateProvinceMapping), "MultientregaProvince_StateProvince_Mapping" }
        };

        ///<inheritdoc/>
        public Dictionary<(Type, string), string> ColumnName => new Dictionary<(Type, string), string>
        {
            { (typeof(MultientregaProvinceStateProvinceMapping), "StateProvinceId"), "StateProvinceId" },
            { (typeof(MultientregaProvinceStateProvinceMapping), "MultientregaProvinceId"), "MultientregaProvinceId" }
        };
    }
}
