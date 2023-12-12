using Nop.Data.Mapping;
using Nop.Plugin.Widgets.GoogleMapsIntegrationMultientrega.Domains;
using System;
using System.Collections.Generic;

namespace Nop.Plugin.Widgets.GoogleMapsIntegrationMultientrega.Mapping
{
    /// <summary>
    /// An implementation of <see cref="INameCompatibility"/> for <see cref="MultientregaDistrictMapping"/>.
    /// </summary>
    public sealed class MultientregaDistrictNameCompatibility : INameCompatibility
    {
        ///<inheritdoc/>
        public Dictionary<Type, string> TableNames => new Dictionary<Type, string>
        {
            { typeof(MultientregaDistrictMapping), "Multientrega_District_Mapping" }
        };

        ///<inheritdoc/>
        public Dictionary<(Type, string), string> ColumnName => new Dictionary<(Type, string), string>
        {
            { (typeof(MultientregaDistrictMapping), "MultientregaId"), "MultientregaId" },
            { (typeof(MultientregaDistrictMapping), "MultientregaProvinceId"), "MultientregaProvinceId" },
            { (typeof(MultientregaDistrictMapping), "Name"), "Name" }
        };
    }
}
