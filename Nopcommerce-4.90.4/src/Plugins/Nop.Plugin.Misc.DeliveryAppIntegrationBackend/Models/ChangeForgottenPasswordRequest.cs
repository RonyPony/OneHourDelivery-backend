using System;
using System.Collections.Generic;
using System.Text;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models
{
    /// <summary>
    /// Represents the information neeeded to change a forgotten password.
    /// </summary>
    public sealed class ChangeForgottenPasswordRequest
    {
        /// <summary>
        /// Represents the customer email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Represents the new password.
        /// </summary>
        public string NewPassword { get; set; }

        /// <summary>
        /// Represents the new password confirmed.
        /// </summary>
        public string ConfirmNewPassword { get; set; }
    }
}
