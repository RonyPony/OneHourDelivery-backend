namespace Nop.Plugin.Synchronizers.WAPIOrders.Models.RegisterSale.Response
{
    /// <summary>
    /// Represents a credit payment method.
    /// </summary>
    public sealed class Credit
    {
        /// <summary>
        /// Indicates the account number.
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// Indicates the authorization code obtained from authorizeConsume.
        /// </summary>
        public string AuthCod { get; set; }

        /// <summary>
        /// Indicates the typeof credit.
        /// </summary>
        public string TypeCredit { get; set; }

        /// <summary>
        /// Indicates the amount before the consumption.
        /// </summary>
        public decimal Previousbalance { get; set; }

        /// <summary>
        /// Indicates the consumption of the credit.
        /// </summary>
        public decimal Debit { get; set; }

        /// <summary>
        /// Indicates the balance of the credit.
        /// </summary>
        public decimal CurrentBalance { get; set; }
    }
}
