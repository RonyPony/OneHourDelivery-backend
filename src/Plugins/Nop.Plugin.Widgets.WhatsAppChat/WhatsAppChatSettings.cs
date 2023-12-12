using Nop.Core.Configuration;

namespace Nop.Plugin.Widgets.WhatsAppChat
{
    /// <summary>
    /// Represents the model used for Setting of the WhatsAppChat plug-in.
    /// </summary>
    public class WhatsAppChatSettings : ISettings
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