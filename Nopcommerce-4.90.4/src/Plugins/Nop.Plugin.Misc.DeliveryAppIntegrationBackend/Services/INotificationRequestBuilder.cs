using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Services
{
    /// <summary>
    /// Utility to build <see cref="NotificationTemplateType"/>s.
    /// </summary>
    public interface INotificationRequestBuilder
    {
        /// <summary>
        /// Builds a <see cref="NotificationRequest"/> with the data specific to this <see cref="NotificationTemplateType"/>.
        /// </summary>
        public NotificationRequest Build(NotificationTemplateType type, int orderId);
    }
}