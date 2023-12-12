namespace Nop.Plugin.Payments.BAC.Models
{
    /// <summary>
    /// This request object needs to get passed when calling the HostedPageAuthorize method when requesting the Hosted Page from the BAC.
    /// </summary>
    public sealed class HostedPagePreprocessRequest
    {
        /// <summary>
        /// Represents the URL for the page where card holder is redirected here after transaction completion.
        /// </summary>
        public string CardHolderResponseURL { get; set; }

        /// <summary>
        /// Represents the transaction details object used for this request.
        /// </summary>
        public TransactionDetails TransactionDetails { get; set; }

       
    }
}
