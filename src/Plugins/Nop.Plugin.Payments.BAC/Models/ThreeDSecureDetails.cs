namespace Nop.Plugin.Payments.BAC.Models
{
    public sealed class ThreeDSecureDetails
    {
        public string ECIIndicator { get; set; }
        public string AuthenticationResult { get; set; }
        public string TransactionStain { get; set; }
        public string CAVV { get; set; }
    }
}
