using System.ComponentModel.DataAnnotations;
using Nop.Web.Framework.Mvc.ModelBinding;
using Nop.Web.Framework.Models;

namespace Nop.Plugin.Widgets.WhatsAppChat.Models
{
    /// <summary>
    /// Represents the model used for configuring the WhatsAppChat plug-in.
    /// </summary>
    public class ConfigurationModel : BaseNopModel
    {
        /// <summary>
        /// Represents Active Store Scope Configuration
        /// </summary>
        public int ActiveStoreScopeConfiguration { get; set; }

        /// <summary>
        /// Represents The phone number that the WhatsApp message is going to send.
        /// </summary>
        [NopResourceDisplayName("Plugins.Widgets.WhatsAppChat.Phone")]
        public string Phone { get; set; }
        /// <summary>
        /// Represents override of Phone paramether
        /// </summary>
        public bool Phone_OverrideForStore { get; set; }

        /// <summary>
        /// Represents the title that comes up in the chat.
        /// </summary>
        [NopResourceDisplayName("Plugins.Widgets.WhatsAppChat.HeaderTitle")]
        public string HeaderTitle { get; set; }
        /// <summary>
        /// Represents override of HeaderTitle paramether
        /// </summary>
        public bool HeaderTitle_OverrideForStore { get; set; }

        /// <summary>
        /// Represents the message that comes out as sent from the commerce.
        /// </summary>
        [NopResourceDisplayName("Plugins.Widgets.WhatsAppChat.PopupMessage")]
        public string PopupMessage { get; set; }
        /// <summary>
        /// Represents override of PopupMessage paramether
        /// </summary>
        public bool PopupMessage_OverrideForStore { get; set; }

        /// <summary>
        /// Represents the message that the client is going to send.
        /// </summary>
        [NopResourceDisplayName("Plugins.Widgets.WhatsAppChat.Message")]
        public string Message { get; set; }
        /// <summary>
        /// Represents override of Message paramether
        /// </summary>
        public bool Message_OverrideForStore { get; set; }


    }
}