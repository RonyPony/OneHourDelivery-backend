using Nop.Core;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains
{
    /// <summary>
    /// Represents driver location info.
    /// </summary>
    public  class DriverLocationInfoMapping : BaseEntity
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
        /// Get or set delivery status.
        /// </summary>
        
        public int DeliveryStatus { get; set; }
        /// <summary>
        /// Get or set destination type.
        /// </summary>
        
        public int DestinationType { get; set; }

        /// <summary>
        /// Get or set register date.
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }
    }
}
