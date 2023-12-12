namespace Nop.Plugin.Payments.CardNet.Models
{
    /// <summary>
    /// Model used to set token session variable
    /// </summary>
    public sealed class CardNetTokenModel
    {
        /// <summary>
        /// Token received from CardNet capture page
        /// </summary>
        public string Token { get; set; }
    }
}
