using System.Collections.Generic;

namespace Nop.Plugin.Widgets.WhatsAppChat.Models
{
    /// <summary>
    /// Provides language resources used by WhatsApp Chat plug-in.
    /// </summary>
    public static class WhatsAppChatLocaleResources
    {
        /// <summary>
        /// Local resources used by WhatsAppChat plugin to display information in Spanish (es-*)
        /// </summary>
        public static Dictionary<string, string> SpanishResources = new Dictionary<string, string>
        {
            ["Plugins.Widgets.WhatsAppChat.Description"] = "Plugin de WhatsApp",
            ["Plugins.Widgets.WhatsAppChat.Warning"] = "Este plugin depende del API de WhatsApp",
            ["Plugins.Widgets.WhatsAppChat.ConfigurationInstructions"] = @"
                    <p>
	                    <b>Para que el plugin funcione de manera correcta:</b>
	                    <br />
	                    <br />1. Debe utilizar el prefijo del país al que pertenece dicho número.
	                    <br />2. El número telefónico utilizado debe tener WhatsApp activo.
                        <br />
	                    <br /><a target='_blank' href='https://es.wikipedia.org/wiki/Anexo:Prefijos_telef%C3%B3nicos_mundiales'>Listado de prefijos telefónicos mundiales<a/><br />
	                    <br />
                    </p>",

            ["Plugins.Widgets.WhatsAppChat.Phone"] = "Número de Teléfono",
            ["Plugins.Widgets.WhatsAppChat.Phone.Hint"] = "El Número de Teléfono al cual se va a enviar el mensaje de whatsapp (Tiene que tener Whatsapp)",
            ["Plugins.Widgets.WhatsAppChat.Phone.ErrorMessage"] = "El Número de Teléfono es requerido.",

            ["Plugins.Widgets.WhatsAppChat.HeaderTitle"] = "Título del chat",
            ["Plugins.Widgets.WhatsAppChat.HeaderTitle.Hint"] = "Aquí va el título que sale arriba en el chat.",

            ["Plugins.Widgets.WhatsAppChat.PopupMessage"] = "Mensaje del comercio",
            ["Plugins.Widgets.WhatsAppChat.PopupMessage.Hint"] = "Aquí es el mensaje que sale como enviado de parte del comercio.",

            ["Plugins.Widgets.WhatsAppChat.Message"] = "Mensaje del cliente",
            ["Plugins.Widgets.WhatsAppChat.Message.Hint"] = "Aquí es el mensaje que sale en el cuadro de texto y que va a enviar el cliente."
        };

        /// <summary>
        /// Local resources used by CardNet plugin to display information in English (en-US)
        /// </summary>
        public static Dictionary<string, string> EnglishResources = new Dictionary<string, string>
        {
            ["Plugins.Widgets.WhatsAppChat.Description"] = "WhatsApp plugin",
            ["Plugins.Widgets.WhatsAppChat.Warning"] = "This plugin depends on WhatsApp's API",
            ["Plugins.Widgets.WhatsAppChat.ConfigurationInstructions"] = @"
                    <p>
	                    <b>For the plugin to work correctly: </b>
	                    <br />
	                    <br />1. You must use the prefix of the country to which this number belongs.
	                    <br />2. The phone number used must have WhatsApp active.
                        <br />
	                    <br /><a target='_blank' href='https://en.wikipedia.org/wiki/List_of_mobile_telephone_prefixes_by_country'>List of mobile telephone prefixes by country<a/><br />
	                    <br />
                    </p>",

            ["Plugins.Widgets.WhatsAppChat.Phone"] = "Phone",
            ["Plugins.Widgets.WhatsAppChat.Phone.Hint"] = "The phone number that the WhatsApp message is going to send (You must have WhatsApp)",
            ["Plugins.Widgets.WhatsAppChat.Phone.ErrorMessage"] = "Telephone number is required.",

            ["Plugins.Widgets.WhatsAppChat.HeaderTitle"] = "Header Title",
            ["Plugins.Widgets.WhatsAppChat.HeaderTitle.Hint"] = "Here is the title that appears above in the chat.",

            ["Plugins.Widgets.WhatsAppChat.PopupMessage"] = "Message of commerce",
            ["Plugins.Widgets.WhatsAppChat.PopupMessage.Hint"] = "Here is the message that comes out as sent from the business.",

            ["Plugins.Widgets.WhatsAppChat.Message"] = "Message of client",
            ["Plugins.Widgets.WhatsAppChat.Message.Hint"] = "Here is the message that the client is going to send."
        };


    }
}
