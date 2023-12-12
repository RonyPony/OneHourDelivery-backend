using System;

namespace Nop.Plugin.Widgets.GoogleMapsIntegrationMultientrega.Models
{
    /// <summary>
    /// Represents a multientrega Barrio.
    /// </summary>
    public class MultientregaNeighborhood
    {
        /// <summary>
        /// Indicates Barrio id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Indicates Corregimiento id.
        /// </summary>
        public string CorregimientoId { get; set; }

        /// <summary>
        /// Indicates Neighborhood name.
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// Indicates Neighborhood FullName.
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Indicates Neighborhood LocXLon.
        /// </summary>
        public string LocXLon { get; set; }

        /// <summary>
        /// Indicates Neighborhood LocYLat.
        /// </summary>
        public string LocYLat { get; set; }

        /// <summary>
        /// Indicates Neighborhood Estado.
        /// </summary>
        public string Estado { get; set; }

        /// <summary>
        /// Indicates Neighborhood UsrCrea.
        /// </summary>
        public string UsrCrea { get; set; }

        /// <summary>
        /// Indicates Neighborhood FechaCrea.
        /// </summary>
        public DateTime FechaCrea { get; set; }

        /// <summary>
        /// Indicates Neighborhood UsrModifica.
        /// </summary>
        public string UsrModifica { get; set; }

        /// <summary>
        /// Indicates Neighborhood FechaModifica.
        /// </summary>
        public DateTime FechaModifica { get; set; }
    }
}
