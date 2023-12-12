using System.Collections.Generic;

namespace Nop.Plugin.Widgets.ProductAvailability.Models
{
    /// <summary>
    /// Represents a model for the response of availability verification on checkout.
    /// </summary>
    public sealed class CheckoutAvailabilityVerificationResult
    {
        /// <summary>
        /// Indicates if the verification was successful.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Indicates the errors occurred while verifying.
        /// </summary>
        public IList<string> Errors { get; set; }
    }
}
