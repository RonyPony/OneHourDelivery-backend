using Nop.Data.Mapping;
using Nop.Plugin.Widgets.GoogleMapsIntegrationMultientrega.Domains;
using System;
using System.Collections.Generic;

namespace Nop.Plugin.Widgets.GoogleMapsIntegrationMultientrega.Mapping
{
    /// <summary>
    /// An implementation of <see cref="INameCompatibility"/> for <see cref="MultientregaProvinceMapping"/>.
    /// </summary>
    public sealed class MultientregaProvinceNameCompatibility : INameCompatibility
    {
        ///<inheritdoc/>
        public Dictionary<Type, string> TableNames => new Dictionary<Type, string>
        {
            { typeof(MultientregaProvinceMapping), "Multientrega_Province_Mapping" }
        };

        ///<inheritdoc/>
        public Dictionary<(Type, string), string> ColumnName => new Dictionary<(Type, string), string>
        {
            { (typeof(MultientregaProvinceMapping), "MultientregaId"), "MultientregaId" },
            { (typeof(MultientregaProvinceMapping), "Name"), "Name" },
        };
    }
}
