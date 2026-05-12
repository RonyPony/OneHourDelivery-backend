using Nop.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models
{
    /// <summary>
    /// Present Coordinate request for Store and Customer rating.
    /// </summary>
    public class CustomerRatingModel 
    {
        /// <summary>
        /// Get or set the id of the ranked customer
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Get or set the rating of the customer.
        /// </summary>
        public decimal? Rating { get; set; }
    }
}
