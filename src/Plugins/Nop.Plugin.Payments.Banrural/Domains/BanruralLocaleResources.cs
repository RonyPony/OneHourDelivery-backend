using System.Collections.Generic;

namespace Nop.Plugin.Payments.Banrural.Domains
{
    /// <summary>
    /// Represents the model that provides the different resources used by Banrural plug-in.
    /// </summary>
    public partial class BanruralLocaleResources
    {
        /// <summary>
        /// Represents the local resources for displaying the plugin information in English (en-US).
        /// </summary>
        public static Dictionary<string, string> EnglishResources = new Dictionary<string, string> 
        {
            ["Plugins.Payments.Banrural.PaymentMethodDescription"] = "Banrural Payment Page",

            ["Plugins.Payments.Banrural.Fields.KeyID"] = "Key ID",
            ["Plugins.Payments.Banrural.Fields.KeyID.Hint"] = "The Key ID to request the Banrural payment page.",

            ["Plugins.Payments.Banrural.Fields.Url"] = "Main site",
            ["Plugins.Payments.Banrural.Fields.Url.Hint"] = "Main URL used for requesting the payment page",

            ["Plugins.Payments.Banrural.Fields.CallbackUrl"] = "Callback Url",
            ["Plugins.Payments.Banrural.Fields.CallbackUrl.Hint"] = "It will be executed asynchronously every time a payment has been successful.",

            ["Plugins.Payments.Banrural.Fields.CancelUrl"] = "Cancel URL",
            ["Plugins.Payments.Banrural.Fields.CancelUrl.Hint"] = "URL used for redirecting the customer when the transaction is canceled by the customer.",

            ["Plugins.Payments.Banrural.Fields.CompleteUrl"] = "Approved URL",
            ["Plugins.Payments.Banrural.Fields.CompleteUrl.Hint"] = "It will be executed (redirected) when the end customer has successfully made the payment.",           

            ["Plugins.Payments.Banrural.Fields.Locale"] = "Language",
            ["Plugins.Payments.Banrural.Fields.Locale.Hint"] = "Language used for displaying the Banrural payment page.",

            ["Plugins.Payments.Banrural.Fields.OrderStatus"] = "Order status",
            ["Plugins.Payments.Banrural.Fields.OrderStatus.Hint"] = "The status an order will have after processing the payment.",

            ["Plugins.Payments.Banrural.Fields.MarkAsPaid"] = "Mark as paid",
            ["Plugins.Payments.Banrural.Fields.MarkAsPaid.Hint"] = "Determines whether or not the order will be marked as paid after processing the payment.",

            ["Plugins.Payments.Banrural.Instructions"] = @"
                    <p>
	                    <b>If you're using this payment method ensure that Banrural provided you with all the information you need for fill this form.</b>
	                    <br />
	                    <br />Once you have all the information needed for completing this form you can proceed with it.<br />
	                    <br />1. Fill all the required information with the info provided by Banrural.
	                    <br />2. Once you completed the required info click Save.
	                    <br />
                    </p>",

            ["Plugins.Payments.Banrural.IsRequired"] = "This field is required for you be able to save this form.",

            ["Plugins.Payments.Banrural.UrlMaxLengthExceeded"] = "This url exceeded the max length (150 characters) required for a URL.",

            ["Plugins.Payments.Banrural.CurrencyCodeMaxLengthExceeded"] = "This currency code exceeded the max length (3 characters) required for a currency code.",

            ["Plugins.Payments.Banrural.MaxLengthExceeded"] = "This field exceeded the max length (15 characters) required.",

            ["Plugins.Payments.Banrural.TransactionResultTitle"] = "Transaction Completed",

            ["Plugins.Payments.Banrural.TransactionCanceledMessage"] = "The transaction has been canceled by you. If you are having any issue with the platform please contact us.",

            ["Plugins.Payments.Banrural.TransactionDeclinedMessage"] = "For some reason the transaction has been declined.",

            ["Plugins.Payments.Banrural.TransactionErrorMessage"] = "For some reason the transaction has been declined.",

            ["Plugins.Payments.Banrural.OrderNumber"] = "Order number",

            ["Plugins.Payments.Banrural.TotalAmount"] = "Total amount",

            ["Plugins.Payments.Banrural.Result"] = "Result",

            ["Plugins.Payments.Banrural.AdditionalInformation"] = "Additional information",

            ["Plugins.Payments.Banrural.PaymentInfoDescription"] = @"Dear customer, <br> Once the order is placed, you will be redirected to <b>Banrural Payment Page</b> to process your payment information.<br> Thank you so much!",

            ["Plugins.Payments.Banrural.TransactionErrorMessage"] = "There was an error while processing the transaction."
        };

        /// <summary>
        /// Represents the local resources for displaying the plugin information in Spanish (es-).
        /// </summary>
        public static Dictionary<string, string> SpanishResources = new Dictionary<string, string> 
        {
            ["Plugins.Payments.Banrural.PaymentMethodDescription"] = "Página de pago de Banrural",

            ["Plugins.Payments.Banrural.Fields.KeyID"] = "Key ID",
            ["Plugins.Payments.Banrural.Fields.KeyID.Hint"] = "El Key ID para llamar la página de pago de Banrural.",

            ["Plugins.Payments.Banrural.Fields.Url"] = "Site principal",
            ["Plugins.Payments.Banrural.Fields.Url.Hint"] = "URL principal utilizada para soliciar la página de pago.",

            ["Plugins.Payments.Banrural.Fields.CallbackUrl"] = "URL Callback",
            ["Plugins.Payments.Banrural.Fields.CallbackUrl.Hint"] = "Url que se ejecutara de forma asincrónica cada vez que un cobro se haya pagado con éxito",

            ["Plugins.Payments.Banrural.Fields.CancelUrl"] = "URL de Cancelación",
            ["Plugins.Payments.Banrural.Fields.CancelUrl.Hint"] = "URL donde será enviado el cliente final al cancelar la transacción.",

            ["Plugins.Payments.Banrural.Fields.CompleteUrl"] = "URL de aprobación",
            ["Plugins.Payments.Banrural.Fields.CompleteUrl.Hint"] = "URL donde será enviado el cliente final al finalizar la transacción con resultado aprobado.",

            ["Plugins.Payments.Banrural.Fields.Locale"] = "Idioma",
            ["Plugins.Payments.Banrural.Fields.Locale.Hint"] = "Idioma utilizado para desplegar la información en la página de pago de Banrural.",
            
            ["Plugins.Payments.Banrural.Fields.OrderStatus"] = "Estado de la orden",
            ["Plugins.Payments.Banrural.Fields.OrderStatus.Hint"] = "El estado que tendrá la orden después de procesar el pago.",

            ["Plugins.Payments.Banrural.Fields.MarkAsPaid"] = "Marcar como paga",
            ["Plugins.Payments.Banrural.Fields.MarkAsPaid.Hint"] = "La orden se marcará como pagada después de procesar el pago.",

            ["Plugins.Payments.Banrural.Instructions"] = @"
                    <p>
	                    <b>Si utiliza este método de pago, asegúrese de que Banrural le haya proporcionado toda la información que necesita para completar este formulario.</b>
	                    <br />
	                    <br />Una vez que tenga toda la información necesaria para completar este formulario, puede continuar con proceso.<br />
	                    <br />1. Complete toda la información requerida con la información provista por Banrural.
	                    <br />2. Una vez que haya completado la información requerida, haga clic en Guardar.
	                    <br />
                    </p>",

            ["Plugins.Payments.Banrural.IsRequired"] = "Este campo es requerido para usted poder guardar el formulario.",

            ["Plugins.Payments.Banrural.UrlMaxLengthExceeded"] = "Esta URL excede el número de caracteres requeridos (150 caracteres) para una URL.",

            ["Plugins.Payments.Banrural.CurrencyCodeMaxLengthExceeded"] = "Este código de moneda excede el número de caracteres requeridos (3 caracteres) para un código de moneda.",

            ["Plugins.Payments.Banrural.MaxLengthExceeded"] = "Este campo excede el número de caracteres requeridos (15 caracteres).",

            ["Plugins.Payments.Banrural.TransactionResultTitle"] = "Transacción Finalizada.",

            ["Plugins.Payments.Banrural.TransactionCanceledMessage"] = "La transacción ha sido cancelada por usted. Si tiene algún problema con la plataforma, contáctenos.",

            ["Plugins.Payments.Banrural.TransactionDeclinedMessage"] = "Por alguna razón, la transacción ha sido rechazada.",

            ["Plugins.Payments.Banrural.TransactionErrorMessage"] = "Por alguna razón, la transacción ha sido rechazada.",

            ["Plugins.Payments.Banrural.OrderNumber"] = "Número de orden.",

            ["Plugins.Payments.Banrural.TotalAmount"] = "Monto total.",

            ["Plugins.Payments.Banrural.Result"] = "Resultado de la transacción.",

            ["Plugins.Payments.Banrural.AdditionalInformation"] = "Información adicional.",

            ["Plugins.Payments.Banrural.PaymentInfoDescription"] = @"Estimado cliente, <br> Una vez realizada la order, usted será redirgido hacia <b>Banrural</b> para procesar su información de pago. <br> ¡Muchas gracias!",

            ["Plugins.Payments.Banrural.TransactionErrorMessage"] = "Se produjo un error al procesar la transacción."
        };
    }
}
