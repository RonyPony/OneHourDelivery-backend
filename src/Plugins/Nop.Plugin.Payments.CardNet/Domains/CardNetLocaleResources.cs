using System.Collections.Generic;

namespace Nop.Plugin.Payments.CardNet.Domains
{
    /// <summary>
    /// Provides language resources used by CardNet payment plug-in.
    /// </summary>
    public static class CardNetLocaleResources
    {
        /// <summary>
        /// Local resources used by CardNet plugin to display information in Spanish (es-*)
        /// </summary>
        public static Dictionary<string, string> SpanishResources = new Dictionary<string, string>
        {
            ["Plugins.Payments.CardNet.PaymentMethodDescription"] = "Página de pago CardNet",
            ["Plugins.Payments.CardNet.RoundingWarning"] = "El redondeo durante la calculación de los precios debe estar activado",
            ["Plugins.Payments.CardNet.ConfigurationInstructions"] = @"
                    <p>
	                    <b>Si utilizará este método de pago, asegúrese de que CardNet le haya proporcionado toda la información que necesita para rellenar este formulario.</b>
	                    <br />
	                    <br />Cuando tenga las informaciones requeridas para completar este formulario, puede proceder a rellenarlo.<br />
	                    <br />1. Llene los campos requeridos, con la información proporcionada por CardNet.
	                    <br />2. Una vez haya llenado la información requerida, haga click en Guardar.
	                    <br />
                        <b>Nota: En caso de soportar varios tipos de moneda, asegúrese de configurar la tienda para efectuar las conversiones correspondientes.</b>
                    </p>",

            ["Plugins.Payments.CardNet.Fields.Url"] = "Url",
            ["Plugins.Payments.CardNet.Fields.Url.Hint"] = "La url utilizada para enviar requests de pago a CardNet.",
            ["Plugins.Payments.CardNet.Fields.Url.ErrorMessage"] = "La url es requerida.",

            ["Plugins.Payments.CardNet.Fields.PwCheckoutScriptUrl"] = "Url script PWCheckout",
            ["Plugins.Payments.CardNet.Fields.PwCheckoutScriptUrl.Hint"] = "La url utilizada para importar el script de la librería de PWCheckout (para renderizar el formulario de captura).",
            ["Plugins.Payments.CardNet.Fields.PwCheckoutScriptUrl.ErrorMessage"] = "La url de PWCheckout es requerida.",

            ["Plugins.Payments.CardNet.Fields.PublicApiKey"] = "Llave pública del API",
            ["Plugins.Payments.CardNet.Fields.PublicApiKey.Hint"] = "Llave pública utilizada para solicitar el formulario de captura de datos de CardNet.",
            ["Plugins.Payments.CardNet.Fields.PublicApiKey.ErrorMessage"] = "La llave pública es requerida.",

            ["Plugins.Payments.CardNet.Fields.PrivateApiKey"] = "Llave privada del API",
            ["Plugins.Payments.CardNet.Fields.PrivateApiKey.Hint"] = "Llave privada utilizada para hacer requests al API de CardNet.",
            ["Plugins.Payments.CardNet.Fields.PrivateApiKey.ErrorMessage"] = "La llave privada es requerida.",

            ["Plugins.Payments.CardNet.Fields.CardNetImageUrl"] = "Url imagen de CardNet",
            ["Plugins.Payments.CardNet.Fields.CardNetImageUrl.Hint"] = "Url de la imagen que se mostrará en el formulario de captura de datos de CardNet.",

            ["Plugins.Payments.CardNet.GenericTransactionError"] = "Hubo un error procesando la transacción.",
            ["Plugins.Payments.CardNet.RejectedTransactionError"] = "La transacción fue rechazada.",

            ["Plugins.Payments.CardNet.OrderPaidMessage"] = "La orden ha sido pagada. El código de aprovación de CardNet es:",

            ["Plugins.Payments.CardNet.PaymentButtonText"] = "Pagar con CardNet",
            ["Plugins.Payments.CardNet.PaymentInstructions"] = @"
                <p>
                    Haga click en el botón ""Pagar con CardNet"" para abrir el formulario de captura de datos.
                    <br/>
                    Llene el formulario con las informaciones correspondientes y proceda a terminar su orden.
                    <br/>
                    <b>Nota: </b>Podrá ver su número de autorización de CardNet en la pantalla de detalles de la orden, en la sección de notas de la orden.
                </p>
            ",
            ["Plugins.Payments.CardNet.CurrencyConversionDisclaimer"] = "<b>Si el tipo de moneda seleccionado es diferente a DOP (pesos dominicanos), será convertido automáticamente.</b>"
        };

        /// <summary>
        /// Local resources used by CardNet plugin to display information in English (en-US)
        /// </summary>
        public static Dictionary<string, string> EnglishResources = new Dictionary<string, string>
        {
            ["Plugins.Payments.CardNet.PaymentMethodDescription"] = "CardNet payment page",
            ["Plugins.Payments.CardNet.RoundingWarning"] = "Price rounding during calculation must be activated",
            ["Plugins.Payments.CardNet.ConfigurationInstructions"] = @"
                    <p>
	                    <b>If you're using this payment method, ensure that CardNet provided you with all the information you need for fill this form.</b>
	                    <br />
	                    <br />Once you have all the information needed for completing this form you can proceed with it.<br />
	                    <br />1. Fill all the required information with the info provided by CardNet.
	                    <br />2. Once you completed the required info, click Save.
	                    <br />
                        <b>Note: In case you support different currencies, make sure to configure the store to make corresponding conversions.</b>
                    </p>",

            ["Plugins.Payments.CardNet.Fields.Url"] = "Url",
            ["Plugins.Payments.CardNet.Fields.Url.Hint"] = "Url used to send purchase requests to CardNet.",
            ["Plugins.Payments.CardNet.Fields.Url.ErrorMessage"] = "Url is required.",

            ["Plugins.Payments.CardNet.Fields.PwCheckoutScriptUrl"] = "PWCheckout script Url",
            ["Plugins.Payments.CardNet.Fields.PwCheckoutScriptUrl.Hint"] = "Url used to import the PWCheckout library script (for rendering the capture form).",
            ["Plugins.Payments.CardNet.Fields.PwCheckoutScriptUrl.ErrorMessage"] = "PWCheckout script url is required.",

            ["Plugins.Payments.CardNet.Fields.PublicApiKey"] = "Public API key",
            ["Plugins.Payments.CardNet.Fields.PublicApiKey.Hint"] = "Public key used to request CardNet's payment capture page.",
            ["Plugins.Payments.CardNet.Fields.PublicApiKey.ErrorMessage"] = "Public API key is required.",

            ["Plugins.Payments.CardNet.Fields.PrivateApiKey"] = "Private API key",
            ["Plugins.Payments.CardNet.Fields.PrivateApiKey.Hint"] = "Private key used to make requests to CardNet's API.",
            ["Plugins.Payments.CardNet.Fields.PrivateApiKey.ErrorMessage"] = "Private API key is required.",

            ["Plugins.Payments.CardNet.Fields.CardNetImageUrl"] = "CardNet image Url",
            ["Plugins.Payments.CardNet.Fields.CardNetImageUrl.Hint"] = "Url of the image that will be shown on CardNet's payment capture page.",

            ["Plugins.Payments.CardNet.GenericTransactionError"] = "There was an error processing the transaction.",
            ["Plugins.Payments.CardNet.RejectedTransactionError"] = "The transaction was rejected.",

            ["Plugins.Payments.CardNet.OrderPaidMessage"] = "Order has beed paid. CardNet approval code is:",

            ["Plugins.Payments.CardNet.PaymentButtonText"] = "Pay with CardNet",
            ["Plugins.Payments.CardNet.PaymentInstructions"] = @"
                <p>
                    Click on the ""Pay with CardNet"" button to open the payment information form.
                    <br/>
                    Fill the form with the correspoding information and proceed to finish your order.
                    <br/>
                    <b>Note: </b>You will be able to view your CardNet authorization code in the order's detail page, on the order notes section.
                </p>
            ",
            ["Plugins.Payments.CardNet.CurrencyConversionDisclaimer"] = "<b>If the selected currency type is not DOP (dominican pesos), it will be automatically converted.</b>"
        };
    }
}
