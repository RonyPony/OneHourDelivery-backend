namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models
{
    /// <summary>
    /// Represents the information neeeded to change a customer's password.
    /// </summary>
    public sealed class ChangePasswordRequestModel
    {
        /// <summary>
        /// Represents the old password.
        /// </summary>
        public string OldPassword { get; set; }

        /// <summary>
        /// Represents the new password.
        /// </summary>
        public string NewPassword { get; set; }

        /// <summary>
        /// Represents the new password confirmed.
        /// </summary>
        public string ConfirmNewPassword { get; set; }

        /// <summary>
        /// Choose Whether or not we need to validate if the password meets requirements
        /// </summary>
        public bool ValidateRequest { get; set; } = false;
    }
}
