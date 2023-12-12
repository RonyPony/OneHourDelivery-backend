using Nop.Plugin.Widgets.GoogleMapsIntegrationMultientrega.Domains;
using Nop.Web.Framework.Models;
using System;

namespace Nop.Plugin.Widgets.GoogleMapsIntegrationMultientrega.Models
{
    /// <summary>
    /// Represents a model for <see cref="MultientregaNeighborhoodMapping"/> entity.
    /// </summary>
    public sealed class NeighborhoodMappingModel : BaseNopEntityModel
    {
        /// <summary>
        /// Indicates Multientrega's neighborhood id.
        /// </summary>
        public string MultientregaId { get; set; }

        /// <summary>
        /// Indicates Multientrega's township id.
        /// </summary>
        public string MultientregaTownshipId { get; set; }

        /// <summary>
        /// Indicates the neighborhood name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Indicates the neighborhood fullname.
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Indicates the neighborhood longitude.
        /// </summary>
        public string LocXLon { get; set; }

        /// <summary>
        /// Indicates the neighborhood latitude.
        /// </summary>
        public string LocYLat { get; set; }

        /// <summary>
        /// Indicates the neighborhood status.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Indicates the neighborhood user creator.
        /// </summary>
        public string UsrCrea { get; set; }

        /// <summary>
        /// Indicates the neighborhood date of creation.
        /// </summary>
        public DateTime? DateCrea { get; set; }

        /// <summary>
        /// Indicates the neighborhood user modifier.
        /// </summary>
        public string UsrModifica { get; set; }

        /// <summary>
        /// Indicates the neighborhood modification date.
        /// </summary>
        public DateTime? DateModifica { get; set; }
    }
}
