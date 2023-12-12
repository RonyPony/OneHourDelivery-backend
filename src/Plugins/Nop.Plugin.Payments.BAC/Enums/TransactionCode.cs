namespace Nop.Plugin.Payments.BAC.Enums
{
    /// <summary>
    /// The transaction code is a numeric value that allows any combinations of the flags listed below
    /// to be included with the transaction request by summing their corresponding value. 
    /// </summary>
    public enum TransactionCode
    {
        /// <summary>
        /// Represents the flag for a Standard basic Authorization
        /// </summary>
        StandardBasicAuthorization = 0,

        /// <summary>
        /// Represents the flag for including an AVS check in the transaction
        /// </summary>
        IncludeAnAvsCheck = 1,

        /// <summary>
        /// Represents the flag as a $0 AVS verification only transaction
        /// </summary>
        AvsVerificationOnly = 2,

        /// <summary>
        /// Transaction has been previously 3D Secure Authenticated the 3D Secure results will be included in the transaction.
        /// </summary>
        Include3DSecureResults = 4,

        /// <summary>
        /// Flag as a single pass transaction (Authorization and Capture as a single transaction)
        /// </summary>
        SinglePassTransaction = 8,

        /// <summary>
        /// 3DS Only
        /// </summary>
        ThreeDSOnly = 64,

        /// <summary>
        /// Tokenize PAN
        /// </summary>
        TokenizePAN = 128,

        /// <summary>
        /// Hosted Page Auth + 3DS
        /// </summary>
        HostedPageAuthAnd3DS = 256
    }
}
