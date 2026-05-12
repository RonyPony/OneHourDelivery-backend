using System;
using System.Collections.Generic;
using System.Text;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models
{
    /// <summary>
    /// Represent driver location info .
    /// </summary>
    public sealed class DriverLocationInfoRequest
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
        /// Get or set longitude.
        /// </summary>
        public decimal? Longitude { get; set; }

        /// <summary>
        /// Validate if driven contact with client.
        /// </summary>
        public bool CanContactDriver { get; set; }
    }
}
