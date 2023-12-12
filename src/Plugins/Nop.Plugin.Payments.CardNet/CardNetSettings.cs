using Nop.Core.Configuration;

namespace Nop.Plugin.Payments.CardNet
{
    public sealed class CardNetSettings : ISettings
    {
        /// <summary>
        /// Main URL used for requesting the payment page.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// PWCheckout script URL used to render the capture page.
        /// </summary>
        public string PwCheckoutScriptUrl { get; set; }

        /// <summary>
        /// Represents the public api key used to call CardNet payment information page.
        /// </summary>
        public string PublicApiKey { get; set; }

        /// <summary>
        /// Represents the private api key used to send payment requests (and other server-to-server operations) to CardNet.
        /// </summary>
        public string PrivateApiKey { get; set; }

        /// <summary>
        /// Represents the url of the image that will be displayed on the CardNet payment page.
        /// </summary>
        public string CardNetImageUrl { get; set; } = "https://www.cardnet.com.do/capp/images/logo_nuevo_x_2.png";
    }
}
