using Nop.Web.Framework.Models;

namespace Nop.Plugin.Payments.AzulPaymentPage.Models
{
    /// <summary>
    /// Represents the model for working with the payment information.
    /// </summary>
    public sealed class AzulPaymentInfoModel : BaseNopModel
    {
        /// <summary>
        /// Represents the customer order identification.
        /// </summary>
        public string OrderId { get; set; }

        /// <summary>
        /// Represents the message if there is any error.
        /// </summary>
        public string Errors { get; set; }
    }
}
