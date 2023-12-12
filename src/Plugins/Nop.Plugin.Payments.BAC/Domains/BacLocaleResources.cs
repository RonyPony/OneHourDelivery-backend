using System.Collections.Generic;

namespace Nop.Plugin.Payments.BAC.Domains
{
    /// <summary>
    /// Represents the model that provides the different resources used by BAC payment method plugin.
    /// </summary>
    public static class BacLocaleResources
    {
        /// <summary>
        /// Represents the local resources for displaying the plugin information in English (en-US).
        /// </summary>
        public static Dictionary<string, string> EnglishResources = new Dictionary<string, string>
        {
            ["Plugins.Payments.BAC.PaymentMethodDescription"] = "Banco América Central (BAC)",

            ["Plugins.Payments.BAC.Instructions"] = @"
                    <p>
	                    <b>If you're using this payment method ensure that the Banco América Central (BAC) provided you with all the information you need for fill this form.</b>
	                    <br />
	                    <br />Once you have all the information needed for completing this form you can proceed with it.<br />
	                    <br />1. Fill all the required information with the info provided by the Banco América Central (BAC).
	                    <br />2. Once you completed the required info click Save.
	                    <br />
                    </p>",

            ["Plugins.Payments.BAC.PaymentInfoDescription"] = @"Dear customer, <br> Once the order is placed, you will be redirected to the <b>Banco América Central (BAC) Payment Page</b> to process your payment information.<br> Thank you so much!",

            ["Plugins.Payments.BAC.Fields.CardHolderResponseUrl"] = "Card holder response Url",
            ["Plugins.Payments.BAC.Fields.CardHolderResponseUrl.Hint"] = "URL for the page where card holder is redirected after transaction completion.",

            ["Plugins.Payments.BAC.Fields.AcquirerId"] = "Acquirer ID",
            ["Plugins.Payments.BAC.Fields.AcquirerId.Hint"] = "Id giving by the BAC for this acquirer.",

            ["Plugins.Payments.BAC.Fields.MerchantId"] = "Merchant ID",
            ["Plugins.Payments.BAC.Fields.MerchantId.Hint"] = "Merchant ID provided by BAC",

            ["Plugins.Payments.BAC.Fields.Currency"] = "Currency",
            ["Plugins.Payments.BAC.Fields.Currency.Hint"] = "The purchase currency ISO 4217 numeric currency code (ex: USD = 840).",

            ["Plugins.Payments.BAC.Fields.CurrencyExponent"] = "Currency exponent",
            ["Plugins.Payments.BAC.Fields.CurrencyExponent.Hint"] = "The number of digits after the decimal point in the purchase amount (i.e. $12.00 = 2).",

            ["Plugins.Payments.BAC.Fields.SignatureMethod"] = "Signature method",
            ["Plugins.Payments.BAC.Fields.SignatureMethod.Hint"] = "The method used for encrypting the signature",

            ["Plugins.Payments.BAC.Fields.CurrencyCode"] = "Currency code",
            ["Plugins.Payments.BAC.Fields.CurrencyCode.Hint"] = "Currency used for a transaction. This value is supplied by AZUL with the environment data access information.",

            ["Plugins.Payments.BAC.IsRequired"] = "This field is required for you be able to save this form.",

            ["Plugins.Payments.BAC.UrlMaxLengthExceeded"] = "This url exceeded the max length (150 characters) required for a URL.",

            ["Plugins.Payments.BAC.AcquirerIdMaxLengthExceeded"] = "This acquirer Id exceeded the max length (11 characters) required for a Acquirer Id.",

            ["Plugins.Payments.BAC.SignatureMethodMaxLengthExceeded"] = "This signature method exceeded the max length (4 characters) required for a signature method.",

            ["Plugins.Payments.BAC.TransactionResultTitle"] = "Transaction Completed",

            ["Plugins.Payments.BAC.TransactionCanceledMessage"] = "The transaction has been canceled by you. If you are having any issue with the platform please contact us.",

            ["Plugins.Payments.BAC.TransactionDeclinedMessage"] = "For some reason the transaction has been declined.",

            ["Plugins.Payments.BAC.TransactionErrorMessage"] = "There was an error while processing the transaction.",

            ["Plugins.Payments.BAC.Fields.OrderStatus"] = "Order status",
            ["Plugins.Payments.BAC.Fields.OrderStatus.Hint"] = "The status an order will has after processing the payment.",

            ["Plugins.Payments.BAC.Fields.MarkAsPaid"] = "Mark as paid",
            ["Plugins.Payments.BAC.Fields.MarkAsPaid.Hint"] = "The order will be mark as paid or not after processing the payment.",

            ["Plugins.Payments.BAC.Fields.GatewayUrl"] = "Gateway url",
            ["Plugins.Payments.BAC.Fields.GatewayUrl.Hint"] = "The url used in the namespace of the xml token request.",

            ["Plugins.Payments.BAC.Fields.HostedPageUrl"] = "Hosted page url",
            ["Plugins.Payments.BAC.Fields.HostedPageUrl.Hint"] = "URL that will be used for displaying the hosted page on the request to the Gateway url has finished.",

            ["Plugins.Payments.BAC.Fields.TokenRequestUrl"] = "Token request url",
            ["Plugins.Payments.BAC.Fields.TokenRequestUrl.Hint"] = "The url used for requesting a token to the BAC.",

            ["Plugins.Payments.BAC.Fields.MerchantPassword"] = "Password",
            ["Plugins.Payments.BAC.Fields.MerchantPassword.Hint"] = "Password provided by the BAC for using their APIs."
        };

        /// <summary>
        /// Represents the local resources for displaying the plug-in information in Spanish (es-*).
        /// </summary>
        public static Dictionary<string, string> SpanishResources = new Dictionary<string, string>
        {
            ["Plugins.Payments.BAC.PaymentMethodDescription"] = "Banco América Central (BAC)",

            ["Plugins.Payments.BAC.Instructions"] = @"
                    <p>
	                    <b>Si utiliza este método de pago, asegúrese de que el Banco América Central (BAC) le haya proporcionado toda la información que necesita para completar este formulario.</b>
	                    <br />
	                    <br />Una vez que tenga toda la información necesaria para completar este formulario, puede continuar con el proceso.<br />
	                    <br />1. Complete toda la información requerida con la información provista por el Banco América Central (BAC).
	                    <br />2. Una vez que haya completado la información requerida, haga clic en Guardar.
	                    <br />
                    </p>",

            ["Plugins.Payments.BAC.PaymentInfoDescription"] = @"Estimado cliente, <br> Una vez realizada la order, usted será redirgido hacia la <b>Pagina de Pago del Banco América Central (BAC)</b> para procesar su información de pago. <br> ¡Muchas gracias!",

            ["Plugins.Payments.BAC.Fields.CardHolderResponseUrl"] = "URL de respuesta para titular de la tarjeta",
            ["Plugins.Payments.BAC.Fields.CardHolderResponseUrl.Hint"] = "URL de la página a la que se redirige al titular de la tarjeta después de completar la transacción.",

            ["Plugins.Payments.BAC.Fields.AcquirerId"] = "ID del adquiriente",
            ["Plugins.Payments.BAC.Fields.AcquirerId.Hint"] = "Id dando por el BAC para este adquirente.",

            ["Plugins.Payments.BAC.Fields.MerchantId"] = "ID del comercio",
            ["Plugins.Payments.BAC.Fields.MerchantId.Hint"] = "Id de la tienda suministrado por el BAC.",

            ["Plugins.Payments.BAC.Fields.Currency"] = "Moneda",
            ["Plugins.Payments.BAC.Fields.Currency.Hint"] = "El código de moneda numérico ISO 4217 de la moneda de compra (por ejemplo, USD = 840).",

            ["Plugins.Payments.BAC.Fields.CurrencyExponent"] = "Exponente de moneda",
            ["Plugins.Payments.BAC.Fields.CurrencyExponent.Hint"] = "El número de dígitos después del punto decimal en el monto de la compra (es decir, $12.00 = 2).",

            ["Plugins.Payments.BAC.Fields.SignatureMethod"] = "Método de firma",
            ["Plugins.Payments.BAC.Fields.SignatureMethod.Hint"] = "El método utilizado para cifrar la firma.",

            ["Plugins.Payments.BAC.IsRequired"] = "Este campo es requerido para usted poder guardar el formulario.",

            ["Plugins.Payments.BAC.UrlMaxLengthExceeded"] = "Esta URL excede el número de caracteres requeridos (150 caracteres) para una URL.",

            ["Plugins.Payments.BAC.AcquirerIdMaxLengthExceeded"] = "Este id de adquiriente excede el número de caracteres requeridos (11 caracteres) para un id de adquiriente.",

            ["Plugins.Payments.BAC.SignatureMethodMaxLengthExceeded"] = "Este método de firma excede el número de caracteres requeridos (4 caracteres) para un método de firma.",

            ["Plugins.Payments.BAC.TransactionCanceledMessage"] = "La transacción ha sido cancelada por usted. Si tiene algún problema con la plataforma, contáctenos.",

            ["Plugins.Payments.BAC.TransactionDeclinedMessage"] = "Favor contactarse con su banco.",

            ["Plugins.Payments.BAC.TransactionErrorMessage"] = "Se produjo un error al procesar la transacción. Favor contactarse con su banco.",

            ["Plugins.Payments.BAC.Fields.OrderStatus"] = "Estado de la order",
            ["Plugins.Payments.BAC.Fields.OrderStatus.Hint"] = "El estado que tendrá la order después de procesar el pago.",

            ["Plugins.Payments.BAC.Fields.MarkAsPaid"] = "Marcar como paga",
            ["Plugins.Payments.BAC.Fields.MarkAsPaid.Hint"] = "La orden se marcará como pagada después de procesar el pago.",

            ["Plugins.Payments.BAC.Fields.GatewayUrl"] = "Gateway url",
            ["Plugins.Payments.BAC.Fields.GatewayUrl.Hint"] = "Url utilizada en el namespace del xml para solicitar el token.",

            ["Plugins.Payments.BAC.Fields.HostedPageUrl"] = "Url pagina de pago",
            ["Plugins.Payments.BAC.Fields.HostedPageUrl.Hint"] = "Url que se utilizará para mostrar la pagina de pago una vez se finalizado el proceso con url principal",

            ["Plugins.Payments.BAC.Fields.TokenRequestUrl"] = "Url solicitud del Token",
            ["Plugins.Payments.BAC.Fields.TokenRequestUrl.Hint"] = "The url used for requesting a token to the BAC.",

            ["Plugins.Payments.BAC.Fields.MerchantPassword"] = "Contraseña",
            ["Plugins.Payments.BAC.Fields.MerchantPassword.Hint"] = "Contraseña suministrada por el BAC para utilizar sus APIs."
        };
    }
}
