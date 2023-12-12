using Nop.Data.Mapping;
using Nop.Plugin.Widgets.GoogleMapsIntegrationMultientrega.Domains;
using System;
using System.Collections.Generic;

namespace Nop.Plugin.Widgets.GoogleMapsIntegrationMultientrega.Mapping
{
    /// <summary>
    /// An implementation of <see cref="INameCompatibility"/> for <see cref="MultientregaTownshipMapping"/>.
    /// </summary>
    public sealed class MultientregaTownshipNameCompatibility : INameCompatibility
    {
        ///<inheritdoc/>
        public Dictionary<Type, string> TableNames => new Dictionary<Type, string>
        {
            { typeof(MultientregaTownshipMapping), "Multientrega_Township_Mapping" }
        };

        ///<inheritdoc/>
        public Dictionary<(Type, string), string> ColumnName => new Dictionary<(Type, string), string>
        {
            { (typeof(MultientregaTownshipMapping), "MultientregaId"), "MultientregaId" },
            { (typeof(MultientregaTownshipMapping), "MultientregaDistrictId"), "MultientregaDistrictId" },
            { (typeof(MultientregaTownshipMapping), "Name"), "Name" }
        };
    }
}
