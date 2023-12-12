using Nop.Core.Configuration;
using Nop.Core.Domain.Orders;
using Nop.Plugin.Payments.Banrural.Enums;

namespace Nop.Plugin.Payments.Banrural
{
    /// <summary>
    /// Represents the main URL used for requesting the payment page.
    /// </summary>
    public sealed class BanruralSettings : ISettings
    {
        /// <summary>
        /// Represents the Key ID of the Banrural payment page request.
        /// </summary>
        public string KeyID { get; set; }

        /// <summary>
        /// Represents the main URL of the Banrural payment page request.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Represents the cancel URL of the Banrural payment page request.
        /// It is used when the order is canceled.
        /// </summary>
        public string CancelUrl { get; set; }

        /// <summary>
        /// Represents the Complete URL of the Banrural payment page request.
        /// It is used when the order is successfully. 
        /// </summary>
        public string CompleteUrl { get; set; }

        /// <summary>
        /// Represents the Callback URL of the Banrural payment page request.
        /// This Url serves as a webhook, an endpoint htat will be called after a payment success. 
        /// </summary>
        public string CallbackUrl { get; set; }

        /// <summary>
        /// Represents the languages available for displaying the Banrural payment page.
        /// </summary>
        public Locale Locale { get; set; }

        /// <summary>
        /// Represents the selected status an order will has after processing the payment.
        /// </summary>
        public OrderStatus OrderStatus { get; set; }

        /// <summary>
        /// Represents if an order will be mark as paid or not after processing the payment.
        /// </summary>
        public bool MarkAsPaid { get; set; }
    }
}