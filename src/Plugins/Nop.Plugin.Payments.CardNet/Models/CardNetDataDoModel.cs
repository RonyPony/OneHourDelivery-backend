namespace Nop.Plugin.Payments.CardNet.Models
{
    /// <summary>
    /// Contains information about invoice number and tax amount
    /// </summary>
    public sealed class CardNetDataDoModel
    {

        /// <summary>
        /// NopCommerce invoice (order) number
        /// </summary>
        public string Invoice { get; set; }

        /// <summary>
        /// Transaction tax amount
        /// </summary>
        public int Tax { get; set; }
    }
}
