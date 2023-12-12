using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.Payments.CardNet.Models
{
    /// <summary>
    /// CardNet configuration class
    /// </summary>
    public sealed class CardNetConfigurationModel : BaseNopModel
    {
        /// <summary>
        /// Main URL used for requesting the payment page.
        /// </summary>
        [NopResourceDisplayName("Plugins.Payments.CardNet.Fields.Url")]
        public string Url { get; set; }

        /// <summary>
        /// PWCheckout script URL used to render the capture page.
        /// </summary>
        [NopResourceDisplayName("Plugins.Payments.CardNet.Fields.PwCheckoutScriptUrl")]
        public string PwCheckoutScriptUrl { get; set; }

        /// <summary>
        /// Public api key used to call CardNet payment information page.
        /// </summary>
        [NopResourceDisplayName("Plugins.Payments.CardNet.Fields.PublicApiKey")]
        public string PublicApiKey { get; set; }

        /// <summary>
        /// Private api key used to send payment requests (and other server-to-server operations) to CardNet.
        /// </summary>
        [NopResourceDisplayName("Plugins.Payments.CardNet.Fields.PrivateApiKey")]
        public string PrivateApiKey { get; set; }

        /// <summary>
        /// Url of the image that will be displayed on the CardNet payment page.
        /// </summary>
        [NopResourceDisplayName("Plugins.Payments.CardNet.Fields.CardNetImageUrl")]
        public string CardNetImageUrl { get; set; }
    }
}
