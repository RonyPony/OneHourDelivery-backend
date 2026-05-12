using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Services
{
    /// <summary>
    /// Represents notification center contract.
    /// </summary>
    public interface INotificationCenterService
    {
        /// <summary>
        /// Sends a notification with the information provided in the <paramref name="notificationRequest"/> parameter. If the request was successful saves an order note. This does not guarantee that the notification has been sent.
        /// </summary>
        /// <param name="notificationRequest">A <see cref="NotificationRequest"/> containing all the information to send this notification.</param>
        void SendNotification(NotificationRequest notificationRequest);

        /// <summary>
        /// Consume notificacion Api to send notification of driver coordinate.
        /// </summary>
        /// <param name="driverRequest">the driver request info</param>
        void SendDriverCoordinateTrackingUpdate(DriverLocationInfoRequest driverRequest);

        /// <summary>
        /// Consume notificacion Api send a notification to change the status.
        /// </summary>
        /// <param name="orderId">the order identifier</param>
        void SendOrderStatusTrackingUpdate(int orderId);
    }
}
