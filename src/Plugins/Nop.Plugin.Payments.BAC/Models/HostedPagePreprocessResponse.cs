namespace Nop.Plugin.Payments.BAC.Models
{
    /// <summary>
    /// Represents the model used for containing BAC's validation result of a previously sent <see cref="TransactionDetails"/>.
    /// </summary>
    public sealed class HostedPagePreprocessResponse
    {
        /// <summary>
        /// Represents the response code for a <see cref="TransactionDetails"/>.
        /// <para>Possible values:</para>
        /// <para>0 – Preprocessing Successful</para>
        /// <para>01 – Request is empty</para>
        /// <para>02 –Missing transaction details</para>
        /// <para>03 – Missing parameters</para>
        /// <para>04 - Amount Invalid</para>
        /// <para>05 – Preprocessing System Error</para>
        /// <para>06 – Missing CardholderResponseURL</para>
        /// </summary>
        public string ResponseCode { get; set; }

        /// <summary>
        /// Represents the friendly description for a <paramref name="ResponseCode"/>.
        /// <para>Example: “Authorized” when response = 0, otherwise the reason for failure.</para>
        /// </summary>
        public string ResponseCodeDescription { get; set; }

        /// <summary>
        /// Represents the token GUID when Response = 0, else null. This token will be used for later operations
        /// </summary>
        public string SecurityToken { get; set; }
    }
}
