using Nop.Core.Domain.Orders;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains;
using System.Collections.Generic;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models
{
    /// <summary>
    /// Represents a result of the order payment collection validation.
    /// </summary>
    public sealed class ValidateOrdersForPaymentCollectionResult
    {
        /// <summary>
        /// Indicates if the validations were successfully done.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Indicates the error messages of the validations in case there are any.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// The orders that passed all validations.
        /// </summary>
        public IList<Order> Orders { get; set; }

        /// <summary>
        /// Orders that passed all validations
        /// </summary>
        public IList<OrderPendingToClosePayment> OrdersPendingToClose { get; set; }
    }
}
