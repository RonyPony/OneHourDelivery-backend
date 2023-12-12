namespace Nop.Service.AppIPOSSync.Models
{
    /// <summary>
    /// Represents the model received from Nop.Api when authenticating
    /// </summary>
    public class TokenResponseModel
    {
        /// <summary>
        /// Auth token
        /// </summary>
        public string access_token { get; set; }

        /// <summary>
        /// Auth scheme
        /// </summary>
        public string token_type { get; set; }
    }
}
