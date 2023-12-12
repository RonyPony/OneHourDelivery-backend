using Nop.Data.Mapping;
using Nop.Plugin.Widgets.GoogleMapsIntegrationMultientrega.Domains;
using System;
using System.Collections.Generic;

namespace Nop.Plugin.Widgets.GoogleMapsIntegrationMultientrega.Mapping
{
    /// <summary>
    /// An implementation of <see cref="INameCompatibility"/> for <see cref="MultientregaNeighborhoodMapping"/>.
    /// </summary>
    public sealed class MultientregaNeighborhoodNameCompatibility : INameCompatibility
    {
        ///<inheritdoc/>
        public Dictionary<Type, string> TableNames => new Dictionary<Type, string>
        {
            { typeof(MultientregaNeighborhoodMapping), "Multientrega_Neighborhood_Mapping" }
        };

        ///<inheritdoc/>
        public Dictionary<(Type, string), string> ColumnName => new Dictionary<(Type, string), string>
        {
            { (typeof(MultientregaNeighborhoodMapping), "MultientregaId"), "MultientregaId" },
            { (typeof(MultientregaNeighborhoodMapping), "MultientregaTownshipId"), "MultientregaTownshipId" },
            { (typeof(MultientregaNeighborhoodMapping), "Name"), "Name" },
            { (typeof(MultientregaNeighborhoodMapping), "FullName"), "FullName" },
            { (typeof(MultientregaNeighborhoodMapping), "LocXLon"), "LocXLon" },
            { (typeof(MultientregaNeighborhoodMapping), "LocYLat"), "LocYLat" },
            { (typeof(MultientregaNeighborhoodMapping), "Status"), "Status" },
            { (typeof(MultientregaNeighborhoodMapping), "UsrCrea"), "UsrCrea" },
            { (typeof(MultientregaNeighborhoodMapping), "DateCrea"), "DateCrea" },
            { (typeof(MultientregaNeighborhoodMapping), "UsrModifica"), "UsrModifica" },
            { (typeof(MultientregaNeighborhoodMapping), "DateModifica"), "DateModifica" }
        };
    }
}
