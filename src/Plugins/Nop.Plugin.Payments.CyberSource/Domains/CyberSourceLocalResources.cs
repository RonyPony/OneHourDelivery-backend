using System.Collections.Generic;

namespace Nop.Plugin.Payments.CyberSource.Domains
{
    /// <summary>
    /// Provides language resources used by CyberSource payment plug-in.
    /// </summary>
    public static class CyberSourceLocalResources
    {
        /// <summary>
        /// Local resources used by CyberSource plugin to display information in English (en-US)
        /// </summary>
        public static Dictionary<string, string> EnglishResources = new Dictionary<string, string>
        {
            ["Plugins.Payments.CyberSource.RedirectionTip"] = "You will be redirected to CyberSource site to complete the order.",
            ["Plugins.Payments.CyberSource.MerchantId"] = "Merchant ID",
            ["Plugins.Payments.CyberSource.MerchantId.Hint"] = "Enter merchant ID.",
            ["Plugins.Payments.CyberSource.CyberSourcePaymentUrl"] = "CyberSourcePaymentUrl",
            ["Plugins.Payments.CyberSource.CyberSourcePaymentUrl.Hint"] = "Enter CyberSource URL.",
            ["Plugins.Payments.CyberSource.RedirectUrl"] = "Redirect url",
            ["Plugins.Payments.CyberSource.RedirectUrl.Hint"] = "Url to which the client will be redirected to after payment (typically https://[yourDomain]/PaymentCyberSource/Complete).",
            ["Plugins.Payments.CyberSource.Access_key"] = "Access key",
            ["Plugins.Payments.CyberSource.Access_key.Hint"] = "Enter Access key.",
            ["Plugins.Payments.CyberSource.Transaction_type"] = "Transaction type",
            ["Plugins.Payments.CyberSource.Transaction_type.Hint"] = "Enter Transaction type, generally it should be 'sale'",
            ["Plugins.Payments.CyberSource.Currency"] = "Currency",
            ["Plugins.Payments.CyberSource.Currency.Hint"] = "Enter Currency.",
            ["Plugins.Payments.CyberSource.Locale"] = "Locale",
            ["Plugins.Payments.CyberSource.Locale.Hint"] = "Enter Locale.",
            ["Plugins.Payments.CyberSource.Secret_key"] = "Secret Key",
            ["Plugins.Payments.CyberSource.Secret_key.Hint"] = "Enter secret key.",
            ["Plugins.Payments.CyberSource.SerialNumber"] = "Serial Number",
            ["Plugins.Payments.CyberSource.SerialNumber.Hint"] = "Enter serial number.",
            ["Plugins.Payments.CyberSource.AdditionalFee"] = "Additional fee",
            ["Plugins.Payments.CyberSource.AdditionalFee.Hint"] = "Enter additional fee to charge your customers.",
            ["Plugins.Payments.CyberSource.PaymentMethodDescription"] = "You will be redirected to CyberSource site to complete the order.",
            ["Plugins.Payments.CyberSource.OrderStatus"] = "Order status",
            ["Plugins.Payments.CyberSource.OrderStatus.Hint"] = "The status an order will have after processing the payment.",
            ["Plugins.Payments.CyberSource.MarkAsPaid"] = "Mark as paid",
            ["Plugins.Payments.CyberSource.MarkAsPaid.Hint"] = "Determines whether or not the order will be marked as paid after processing the payment.",
            ["Plugins.Payments.CyberSource.CreditCardIsMasked"] = "Mask card number",
            ["Plugins.Payments.CyberSource.CreditCardIsMasked.Hint"] = "Choose if you want to mask the card number when processing a payment.",
            ["Plugins.Payments.CyberSource.CybersourceEnvironment"] = "Cybersource environment",
            ["Plugins.Payments.CyberSource.CybersourceEnvironment.Hint"] = "Indicate if the Cybersource environment is 'test' or 'live'.",

            ["Plugins.Payments.CyberSource.TransactionCanceledMessage"] = "The transaction has been canceled by you. If you are having any issue with the platform please contact us.",
            ["Plugins.Payments.CyberSource.TransactionDeclinedMessage"] = "For some reason the transaction has been declined.",
            ["Plugins.Payments.CyberSource.TransactionErrorMessage"] = "For some reason the transaction has been declined.",
            ["Plugins.Payments.CyberSource.CheckoutCompletedPaymentDeclinedMessage"] = "Your order was generated correctly, but the card was rejected. Please try placing your order again or try a new payment option.",
            ["Plugins.Payments.CyberSource.CheckoutCompletedPaymentDeclinedTitle"] = "Something went wrong...",

            ["Plugins.Payments.CyberSource.Name"] = "Name",
            ["Plugins.Payments.CyberSource.CardNumber"] = "Card number",
            ["Plugins.Payments.CyberSource.Expiry"] = "Expiry Date",
            ["Plugins.Payments.CyberSource.SecurityCode"] = "CVC/CVV",
            ["Plugins.Payments.CyberSource.ConfirmAndPay"] = "Confirm and Pay",

            ["Plugins.Payments.CyberSource.PhoneInvalid"] = "Phone number is invalid",
        };

        /// <summary>
        /// Local resources used by CyberSource plugin to display information in Spanish (es-*)
        /// </summary>
        public static Dictionary<string, string> SpanishResources = new Dictionary<string, string>
        {
            ["Plugins.Payments.CyberSource.RedirectionTip"] = "Será redirigido a la página de pago de CyberSource para completar el pago.",
            ["Plugins.Payments.CyberSource.MerchantId"] = "ID del comerciante",
            ["Plugins.Payments.CyberSource.MerchantId.Hint"] = "Merchant ID de CyberSource.",
            ["Plugins.Payments.CyberSource.CyberSourcePaymentUrl"] = "Url página de pago",
            ["Plugins.Payments.CyberSource.CyberSourcePaymentUrl.Hint"] = "URL de la página de pago de CyberSource.",
            ["Plugins.Payments.CyberSource.RedirectUrl"] = "Url de redireccionamiento",
            ["Plugins.Payments.CyberSource.RedirectUrl.Hint"] = "Url a la que se redireccionará al cliente luego de realizar el pago (típicamente https://[dominio]/PaymentCyberSource/Complete).",
            ["Plugins.Payments.CyberSource.Access_key"] = "Llave de acceso",
            ["Plugins.Payments.CyberSource.Access_key.Hint"] = "Access key de CyberSource.",
            ["Plugins.Payments.CyberSource.Transaction_type"] = "Tipo de transacción",
            ["Plugins.Payments.CyberSource.Transaction_type.Hint"] = "El tipo de transacción, por lo general debería ser 'sale'.",
            ["Plugins.Payments.CyberSource.Currency"] = "Divisa",
            ["Plugins.Payments.CyberSource.Currency.Hint"] = "Divisa en la que se realizarán los cobros (por lo general, 'USD').",
            ["Plugins.Payments.CyberSource.Locale"] = "Idioma",
            ["Plugins.Payments.CyberSource.Locale.Hint"] = "Idioma en que se desplegará la página de pago, por lo general 'en'.",
            ["Plugins.Payments.CyberSource.Secret_key"] = "Llave secreta",
            ["Plugins.Payments.CyberSource.Secret_key.Hint"] = "Secret key de CyberSource.",
            ["Plugins.Payments.CyberSource.SerialNumber"] = "Número de serie",
            ["Plugins.Payments.CyberSource.SerialNumber.Hint"] = "Serial number de CyberSource.",
            ["Plugins.Payments.CyberSource.AdditionalFee"] = "Cargo adicional",
            ["Plugins.Payments.CyberSource.AdditionalFee.Hint"] = "Cargo adicional que se le cobrará a los clientes por transacción.",
            ["Plugins.Payments.CyberSource.PaymentMethodDescription"] = "Será redirigido al sitio de CyberSource para completar el pedido.",
            ["Plugins.Payments.CyberSource.OrderStatus"] = "Estado de la orden",
            ["Plugins.Payments.CyberSource.OrderStatus.Hint"] = "El estado que tendrá la orden después de procesar el pago.",
            ["Plugins.Payments.CyberSource.MarkAsPaid"] = "Marcar como paga",
            ["Plugins.Payments.CyberSource.MarkAsPaid.Hint"] = "La orden se marcará como pagada después de procesar el pago.",
            ["Plugins.Payments.CyberSource.CreditCardIsMasked"] = "Enmascarar número de tarjeta",
            ["Plugins.Payments.CyberSource.CreditCardIsMasked.Hint"] = "Elija si desea enmascarar el número de tarjeta cuando se procese el pago.",
            ["Plugins.Payments.CyberSource.CybersourceEnvironment"] = "Ambiente de Cybersource",
            ["Plugins.Payments.CyberSource.CybersourceEnvironment.Hint"] = "Indique si el ambiente de Cybersourse es 'test' (pruebas) o 'live' (producción).",

            ["Plugins.Payments.CyberSource.TransactionCanceledMessage"] = "La transacción ha sido cancelada por usted. Si tiene algún problema con la plataforma, contáctenos.",
            ["Plugins.Payments.CyberSource.TransactionDeclinedMessage"] = "Por alguna razón, la transacción ha sido rechazada.",
            ["Plugins.Payments.CyberSource.TransactionErrorMessage"] = "Por alguna razón, la transacción ha sido rechazada.",
            ["Plugins.Payments.CyberSource.CheckoutCompletedPaymentDeclinedMessage"] = "Su orden fue generada correctamente, pero la tarjeta fue rechazada. Intente realizar su pedido nuevamente o pruebe una nueva opción de pago.",
            ["Plugins.Payments.CyberSource.CheckoutCompletedPaymentDeclinedTitle"] = "Ha ocurrido un problema...",

            ["Plugins.Payments.CyberSource.Name"] = "Nombre",
            ["Plugins.Payments.CyberSource.CardNumber"] = "Número de tarjeta",
            ["Plugins.Payments.CyberSource.Expiry"] = "Fecha de expiración",
            ["Plugins.Payments.CyberSource.SecurityCode"] = "CVC/CVV",
            ["Plugins.Payments.CyberSource.ConfirmAndPay"] = "Confirmar y Pagar",

            ["Plugins.Payments.CyberSource.PhoneInvalid"] = "Número de teléfono o celular es inválido",
        };

    }
}
