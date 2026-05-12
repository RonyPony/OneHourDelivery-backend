using System;
using System.Collections.Generic;
using System.Text;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models
{
    /// <summary>
    /// Represents Google Direcction request.
    /// </summary>
    public sealed class GoogleDirecctionRequest
    {
        /// <summary>
        /// Get or set order id.
        /// </summary>
        public int OrderId { get; set; }
        /// <summary>
        /// Get or set latitude.
        /// </summary>
        public decimal? Latitude { get; set; }
        /// <summary>
        /// Get or set Longitude.
        /// </summary>
        public decimal? Longitude { get; set; }
    }
}
