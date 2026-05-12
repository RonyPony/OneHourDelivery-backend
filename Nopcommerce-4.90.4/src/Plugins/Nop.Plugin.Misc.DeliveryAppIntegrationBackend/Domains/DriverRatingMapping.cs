using Nop.Core;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains
{
    /// <summary>
    /// Represents driver rating mapping.
    /// </summary>
    public class DriverRatingMapping: BaseEntity
    {
        /// <summary>
        /// Driver's id.
        /// </summary>
        public int DriverId { get; set; }

        /// <summary>
        /// Customer's id.
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Order's id.
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// Driver's evaluation.
        /// </summary>
        public DriverRating Rating { get; set; }

        /// <summary>
        /// Driver's rating type evaluation.
        /// </summary>
        public DriverRatingType RatingType { get; set; }

        /// <summary>
        ///  Register date.
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }
    }
}
