using System;
using System.Collections.Generic;
using System.Text;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models
{
    /// <summary>
    /// Represents customer discount model.
    /// </summary>
    public sealed class CustomerDiscountModel
    {
        /// <summary>
        /// Get or set customer's name.
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// Get or set discount's name.
        /// </summary>
        public string DiscountName { get; set; }

        /// <summary>
        /// Get or set discount's amount
        /// </summary>
        public decimal DiscountAmount { get; set; }
    }
}
