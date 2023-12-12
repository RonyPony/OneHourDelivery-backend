using System.Collections.Generic;

namespace Nop.Plugin.Payments.AzulPaymentPage.Domains
{
    /// <summary>
    /// Represents the model that provides the different resources used by AZUL payment page plug-in.
    /// </summary>
    public static class AzulLocaleResources
    {
        /// <summary>
        /// Represents the local resources for displaying the plugin information in English (en-US).
        /// </summary>
        public static Dictionary<string, string> EnglishResources = new Dictionary<string, string>
        {
            ["Plugins.Payments.AzulPaymentPage.PaymentMethodDescription"] = "Azul Payment Page",

            ["Plugins.Payments.AzulPaymentPage.Fields.Url"] = "Main site",
            ["Plugins.Payments.AzulPaymentPage.Fields.Url.Hint"] = "Main URL used for requesting the payment page",

            ["Plugins.Payments.AzulPaymentPage.Fields.AlternativeUrl"] = "Alternative site",
            ["Plugins.Payments.AzulPaymentPage.Fields.AlternativeUrl.Hint"] = "Alternative URL used for requesting the payment page if the main URL is not available.",

            ["Plugins.Payments.AzulPaymentPage.Fields.MerchantId"] = "Merchant ID",
            ["Plugins.Payments.AzulPaymentPage.Fields.MerchantId.Hint"] = "Store identification number assigned when the store is affiliated with AZUL.",

            ["Plugins.Payments.AzulPaymentPage.Fields.MerchantName"] = "Merchant name",
            ["Plugins.Payments.AzulPaymentPage.Fields.MerchantName.Hint"] = "Name that will be displayed in the payment page.",

            ["Plugins.Payments.AzulPaymentPage.Fields.MerchantType"] = "Merchant type",
            ["Plugins.Payments.AzulPaymentPage.Fields.MerchantType.Hint"] = "The commerce type (just for information purpose).",

            ["Plugins.Payments.AzulPaymentPage.Fields.AuthKey"] = "Authorization key",
            ["Plugins.Payments.AzulPaymentPage.Fields.AuthKey.Hint"] = "The authorization key for hashing and requesting the AZUL payment page.",

            ["Plugins.Payments.AzulPaymentPage.Fields.CurrencyCode"] = "Currency code",
            ["Plugins.Payments.AzulPaymentPage.Fields.CurrencyCode.Hint"] = "Currency used for a transaction. This value is supplied by AZUL with the environment data access information.",

            ["Plugins.Payments.AzulPaymentPage.Fields.ApprovedUrl"] = "Approved URL",
            ["Plugins.Payments.AzulPaymentPage.Fields.ApprovedUrl.Hint"] = "URL used for redirecting the customer when the payment has been approved by AZUL.",

            ["Plugins.Payments.AzulPaymentPage.Fields.DeclinedUrl"] = "Declined URL",
            ["Plugins.Payments.AzulPaymentPage.Fields.DeclinedUrl.Hint"] = "URL used for redirecting the customer when the payment has been declined by AZUL.",

            ["Plugins.Payments.AzulPaymentPage.Fields.CancelUrl"] = "Cancel URL",
            ["Plugins.Payments.AzulPaymentPage.Fields.CancelUrl.Hint"] = "URL used for redirecting the customer when the transaction is canceled by the customer.",

            ["Plugins.Payments.AzulPaymentPage.Fields.Locale"] = "Language",
            ["Plugins.Payments.AzulPaymentPage.Fields.Locale.Hint"] = "Language used for displaying the AZUL payment page.",

            ["Plugins.Payments.AzulPaymentPage.Fields.UseCustomField1"] = "Use Custom Field 1",
            ["Plugins.Payments.AzulPaymentPage.Fields.UseCustomField1.Hint"] = "Determines whether to use this custom field.",

            ["Plugins.Payments.AzulPaymentPage.Fields.CustomField1Label"] = "Label for Custom Field 1",
            ["Plugins.Payments.AzulPaymentPage.Fields.CustomField1Label.Hint"] = "Description used for displaying this custom field.",

            ["Plugins.Payments.AzulPaymentPage.Fields.CustomField1Value"] = "Value for Custom Field 1",
            ["Plugins.Payments.AzulPaymentPage.Fields.CustomField1Value.Hint"] = "Value used when displaying this custom field.",

            ["Plugins.Payments.AzulPaymentPage.Fields.UseCustomField2"] = "Use Custom Field 2",
            ["Plugins.Payments.AzulPaymentPage.Fields.UseCustomField2.Hint"] = "Determines whether to use this custom field.",

            ["Plugins.Payments.AzulPaymentPage.Fields.CustomField2Label"] = "Label for Custom Field 2",
            ["Plugins.Payments.AzulPaymentPage.Fields.CustomField2Label.Hint"] = "Description used for displaying this custom field.",

            ["Plugins.Payments.AzulPaymentPage.Fields.CustomField2Value"] = "Value for Custom Field 2",
            ["Plugins.Payments.AzulPaymentPage.Fields.CustomField2Value.Hint"] = "Value used when displaying this custom field.",

            ["Plugins.Payments.AzulPaymentPage.Fields.ShowTransactionResult"] = "Show Transaction Result",
            ["Plugins.Payments.AzulPaymentPage.Fields.ShowTransactionResult.Hint"] = "Determines whether to show the transaction result.",

            ["Plugins.Payments.AzulPaymentPage.Instructions"] = @"
                    <p>
	                    <b>If you're using this payment method ensure that AZUL provided you with all the information you need for fill this form.</b>
	                    <br />
	                    <br />Once you have all the information needed for completing this form you can proceed with it.<br />
	                    <br />1. Fill all the required information with the info provided by AZUL.
	                    <br />2. Once you completed the required info click Save.
	                    <br />
                    </p>",

            ["Plugins.Payments.AzulPaymentPage.IsRequired"] = "This field is required for you be able to save this form.",

            ["Plugins.Payments.AzulPaymentPage.UrlMaxLengthExceeded"] = "This url exceeded the max length (150 characters) required for a URL.",

            ["Plugins.Payments.AzulPaymentPage.CurrencyCodeMaxLengthExceeded"] = "This currency code exceeded the max length (3 characters) required for a currency code.",

            ["Plugins.Payments.AzulPaymentPage.MaxLengthExceeded"] = "This field exceeded the max length (15 characters) required.",

            ["Plugins.Payments.AzulPaymentPage.TransactionResultTitle"] = "Transaction Completed",

            ["Plugins.Payments.AzulPaymentPage.TransactionCanceledMessage"] = "The transaction has been canceled by you. If you are having any issue with the platform please contact us.",

            ["Plugins.Payments.AzulPaymentPage.TransactionDeclinedMessage"] = "For some reason the transaction has been declined.",

            ["Plugins.Payments.AzulPaymentPage.TransactionErrorMessage"] = "For some reason the transaction has been declined.",

            ["Plugins.Payments.AzulPaymentPage.OrderNumber"] = "Order number",

            ["Plugins.Payments.AzulPaymentPage.TotalAmount"] = "Total amount",

            ["Plugins.Payments.AzulPaymentPage.Result"] = "Result",

            ["Plugins.Payments.AzulPaymentPage.AdditionalInformation"] = "Additional information",

            ["Plugins.Payments.AzulPaymentPage.PaymentInfoDescription"] = @"Dear customer, <br> Once the order is placed, you will be redirected to <b>AZUL Payment Page</b> to process your payment information.<br> Thank you so much!",

            ["Plugins.Payments.AzulPaymentPage.TransactionErrorMessage"] = "There was an error while processing the transaction.",

            ["Plugins.Payments.AzulPaymentPage.Fields.OrderStatus"] = "Order status",
            ["Plugins.Payments.AzulPaymentPage.Fields.OrderStatus.Hint"] = "The status an order will has after processing the payment.",

            ["Plugins.Payments.AzulPaymentPage.Fields.MarkAsPaid"] = "Mark as paid",
            ["Plugins.Payments.AzulPaymentPage.Fields.MarkAsPaid.Hint"] = "The order will be mark as paid or not after processing the payment.",
        };

        /// <summary>
        /// Represents the local resources for displaying the plug-in information in Spanish (es-*).
        /// </summary>
        public static Dictionary<string, string> SpanishResources = new Dictionary<string, string>
        {
            ["Plugins.Payments.AzulPaymentPage.PaymentMethodDescription"] = "Página de pago de AZUL",

            ["Plugins.Payments.AzulPaymentPage.Fields.Url"] = "Site principal",
            ["Plugins.Payments.AzulPaymentPage.Fields.Url.Hint"] = "URL principal utilizada para soliciar la página de pago.",

            ["Plugins.Payments.AzulPaymentPage.Fields.AlternativeUrl"] = "Site alterno",
            ["Plugins.Payments.AzulPaymentPage.Fields.AlternativeUrl.Hint"] = "URL alterna utilizada para solicitar la página de pago en caso de que el URL principal no esté disponible.",

            ["Plugins.Payments.AzulPaymentPage.Fields.MerchantId"] = "ID del comercio",
            ["Plugins.Payments.AzulPaymentPage.Fields.MerchantId.Hint"] = "Id de la tienda suministrado por AZUL para solicitar la página de pago.",

            ["Plugins.Payments.AzulPaymentPage.Fields.MerchantName"] = "Nombre del comercio",
            ["Plugins.Payments.AzulPaymentPage.Fields.MerchantName.Hint"] = "Nombre a desplegar en la página de pago.",

            ["Plugins.Payments.AzulPaymentPage.Fields.MerchantType"] = "Tipo de comercio",
            ["Plugins.Payments.AzulPaymentPage.Fields.MerchantType.Hint"] = "A que tipo de actividad comercial se decida la empresa (de carácter informativo).",

            ["Plugins.Payments.AzulPaymentPage.Fields.AuthKey"] = "Llave de autorización",
            ["Plugins.Payments.AzulPaymentPage.Fields.AuthKey.Hint"] = "Llave de autorización para encriptar y solicitar la pagina de pago de AZUL",

            ["Plugins.Payments.AzulPaymentPage.Fields.CurrencyCode"] = "Código de moneda",
            ["Plugins.Payments.AzulPaymentPage.Fields.CurrencyCode.Hint"] = "Moneda utilizada para la transacción. Este valor es proporcionado por AZUL, junto a los dados de acceso.",

            ["Plugins.Payments.AzulPaymentPage.Fields.ApprovedUrl"] = "URL de aprobación",
            ["Plugins.Payments.AzulPaymentPage.Fields.ApprovedUrl.Hint"] = "URL donde será enviado el cliente final al finalizar la transacción con resultado aprobado.",

            ["Plugins.Payments.AzulPaymentPage.Fields.DeclinedUrl"] = "URL de rechazo",
            ["Plugins.Payments.AzulPaymentPage.Fields.DeclinedUrl.Hint"] = "URL donde será enviado el cliente final al concluir la transacción con resultado declinado.",

            ["Plugins.Payments.AzulPaymentPage.Fields.CancelUrl"] = "URL de cancelación",
            ["Plugins.Payments.AzulPaymentPage.Fields.CancelUrl.Hint"] = "URL donde será enviado el cliente final al cancelar la transacción.",

            ["Plugins.Payments.AzulPaymentPage.Fields.Locale"] = "Idioma",
            ["Plugins.Payments.AzulPaymentPage.Fields.Locale.Hint"] = "Idioma utilizado para desplegar la información en la página de pago de AZUL.",

            ["Plugins.Payments.AzulPaymentPage.Fields.UseCustomField1"] = "Usar campo personalizado 1",
            ["Plugins.Payments.AzulPaymentPage.Fields.UseCustomField1.Hint"] = "Determina si se usa este campo personalizado.",

            ["Plugins.Payments.AzulPaymentPage.Fields.CustomField1Label"] = "Descripción para el campo personalizado 1",
            ["Plugins.Payments.AzulPaymentPage.Fields.CustomField1Label.Hint"] = "Descripción utilizada para mostrar este campo personalizado.",

            ["Plugins.Payments.AzulPaymentPage.Fields.CustomField1Value"] = "Valor para el campo personalizado 1",
            ["Plugins.Payments.AzulPaymentPage.Fields.CustomField1Value.Hint"] = "Valor que tendrá el campo personalizado.",

            ["Plugins.Payments.AzulPaymentPage.Fields.UseCustomField2"] = "Usar campo personalizado 2",
            ["Plugins.Payments.AzulPaymentPage.Fields.UseCustomField2.Hint"] = "Determina si se usa este campo personalizado.",

            ["Plugins.Payments.AzulPaymentPage.Fields.CustomField2Label"] = "Descripción para el campo personalizado 2",
            ["Plugins.Payments.AzulPaymentPage.Fields.CustomField2Label.Hint"] = "Descripción utilizada para mostrar este campo personalizado.",

            ["Plugins.Payments.AzulPaymentPage.Fields.CustomField2Value"] = "Valor para el campo personalizado 2",
            ["Plugins.Payments.AzulPaymentPage.Fields.CustomField2Value.Hint"] = "Valor que tendrá el campo personalizado.",

            ["Plugins.Payments.AzulPaymentPage.Fields.ShowTransactionResult"] = "Mostrar resultado de la transacción",
            ["Plugins.Payments.AzulPaymentPage.Fields.ShowTransactionResult.Hint"] = "Determina si se muestra el resultado de la transacción.",

            ["Plugins.Payments.AzulPaymentPage.Instructions"] = @"
                    <p>
	                    <b>Si utiliza este método de pago, asegúrese de que AZUL le haya proporcionado toda la información que necesita para completar este formulario.</b>
	                    <br />
	                    <br />Una vez que tenga toda la información necesaria para completar este formulario, puede continuar con proceso.<br />
	                    <br />1. Complete toda la información requerida con la información provista por AZUL.
	                    <br />2. Una vez que haya completado la información requerida, haga clic en Guardar.
	                    <br />
                    </p>",

            ["Plugins.Payments.AzulPaymentPage.IsRequired"] = "Este campo es requerido para usted poder guardar el formulario.",

            ["Plugins.Payments.AzulPaymentPage.UrlMaxLengthExceeded"] = "Esta URL excede el número de caracteres requeridos (150 caracteres) para una URL.",

            ["Plugins.Payments.AzulPaymentPage.CurrencyCodeMaxLengthExceeded"] = "Este código de moneda excede el número de caracteres requeridos (3 caracteres) para un código de moneda.",

            ["Plugins.Payments.AzulPaymentPage.MaxLengthExceeded"] = "Este campo excede el número de caracteres requeridos (15 caracteres).",

            ["Plugins.Payments.AzulPaymentPage.TransactionResultTitle"] = "Transacción Finalizada",

            ["Plugins.Payments.AzulPaymentPage.TransactionCanceledMessage"] = "La transacción ha sido cancelada por usted. Si tiene algún problema con la plataforma, contáctenos.",

            ["Plugins.Payments.AzulPaymentPage.TransactionDeclinedMessage"] = "Por alguna razón, la transacción ha sido rechazada.",

            ["Plugins.Payments.AzulPaymentPage.OrderNumber"] = "Número de orden",

            ["Plugins.Payments.AzulPaymentPage.TotalAmount"] = "Monto total",

            ["Plugins.Payments.AzulPaymentPage.Result"] = "Resultado de la transacción",

            ["Plugins.Payments.AzulPaymentPage.AdditionalInformation"] = "Información adicional",

            ["Plugins.Payments.AzulPaymentPage.PaymentInfoDescription"] = @"Estimado cliente, <br> Una vez realizada la order, usted será redirgido hacia <b>AZUL Payment Page</b> para procesar su información de pago. <br> ¡Muchas gracias!",

            ["Plugins.Payments.AzulPaymentPage.TransactionErrorMessage"] = "Se produjo un error al procesar la transacción.",

            ["Plugins.Payments.AzulPaymentPage.Fields.OrderStatus"] = "Estado de la orden",
            ["Plugins.Payments.AzulPaymentPage.Fields.OrderStatus.Hint"] = "El estado que tendrá la order después de procesar el pago.",

            ["Plugins.Payments.AzulPaymentPage.Fields.MarkAsPaid"] = "Marcar como paga",
            ["Plugins.Payments.AzulPaymentPage.Fields.MarkAsPaid.Hint"] = "La orden se marcará como pagada después de procesar el pago.",
        };
    }
}
