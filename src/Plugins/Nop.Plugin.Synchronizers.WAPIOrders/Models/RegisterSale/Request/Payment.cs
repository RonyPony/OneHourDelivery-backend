namespace Nop.Plugin.Synchronizers.WAPIOrders.Models.RegisterSale.Request
{
    /// <summary>
    /// Represents a sale payment.
    /// </summary>
    public sealed class Payment
    {
        /// <summary>
        /// Indicates the correlative line number of payment.
        /// </summary>
        public int PLine { get; set; }

        /// <summary>
        /// Indicates the payment method code.
        /// </summary>
        public string PCode { get; set; }

        /// <summary>
        /// Indicates the payment method description.
        /// </summary>
        public string PDescription { get; set; }

        /// <summary>
        /// Indicates the payment amount.
        /// </summary>
        public decimal PAmt { get; set; }

        /// <summary>
        /// Indicates the authentication code obtained from authorizeConsume process. 
        /// </summary>
        public string AuthCod { get; set; }
    }
}
