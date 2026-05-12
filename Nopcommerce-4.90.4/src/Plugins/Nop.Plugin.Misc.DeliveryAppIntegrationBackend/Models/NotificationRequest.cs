using System.Collections.Generic;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models
{
    /// <summary>
    /// Contains all the information necesary to send a notification.
    /// </summary>
    public class NotificationRequest
    {
        /// <summary>
        /// The Id of the customer that will receive this notification.
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// The Id of the order that caused this notification.
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// The name of the app that should receive this notification
        /// </summary>
        public MobileAppPackageName AppPackageName { get; set; }

        /// <summary>
        /// The predefined "readableId"(see notification center) of the template type to send
        /// </summary>
        public NotificationTemplateType TemplateType { get; set; }

        /// <summary>
        /// The additional data sent as payload for this notification.
        /// </summary>
        public IDictionary<string, string> Payload { get; set; }
    }
}
