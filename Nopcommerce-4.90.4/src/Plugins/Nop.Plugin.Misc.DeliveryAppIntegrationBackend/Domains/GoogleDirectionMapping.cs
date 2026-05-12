using Nop.Core;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains
{
    /// <summary>
    /// Represent google direcction mapping.
    /// </summary>
    public class GoogleDirectionMapping : BaseEntity
    {

        /// <summary>
        /// Indicate de order id.
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// Indicate the google resource.
        /// </summary>
        public string GoogleResource { get; set; }

        /// <summary>
        /// Indicate the destination type.
        /// </summary>
        public int DestinationType { get; set; }

        /// <summary>
        /// Indicate request number.
        /// </summary>
        public int RequestNumber { get; set; }

        /// <summary>
        /// Indicate the register date.
        /// </summary>
        public DateTime CreateOnUtc { get; set; }
    }
}
