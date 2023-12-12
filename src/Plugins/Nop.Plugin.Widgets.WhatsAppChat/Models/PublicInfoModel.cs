using Nop.Web.Framework.Models;

namespace Nop.Plugin.Widgets.WhatsAppChat.Models
{
    /// <summary>
    /// Represents the model for working with the WhatsAppChat information.
    /// </summary>
    public class PublicInfoModel : BaseNopModel
    {
        /// <summary>
        /// Represents The phone number that the WhatsApp message is going to send.
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// Represents the title that comes up in the chat.
        /// </summary>
        public string HeaderTitle { get; set; }
        /// <summary>
        /// Represents the message that comes out as sent from the commerce.
        /// </summary>
        public string PopupMessage { get; set; }
        /// <summary>
        /// Represents the message that the client is going to send.
        /// </summary>
        public string Message { get; set; }
    }
}