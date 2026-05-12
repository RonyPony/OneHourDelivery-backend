using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Helpers;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models
{
    /// <summary>
    /// Represents each of the registered template types in the notification center.

    /// </summary>
    public class NotificationTemplateType : StringEnum
    {
        private NotificationTemplateType(string value) : base(value) { }

        /// <summary>
        /// Notification to the client when an order is created
        /// </summary>
        public static readonly NotificationTemplateType ClientClientOrderCreated = new NotificationTemplateType("client-client-order-created");

        /// <summary>
        /// Notification to the commerce when an order is created by a client
        /// </summary>
        public static readonly NotificationTemplateType ClientCommerceNewOrder = new NotificationTemplateType("client-commerce-new-order");

        /// <summary>
        /// Notification to the client when the commerce accepts their order.
        /// </summary>
        public static readonly NotificationTemplateType CommerceClientOrderAccepted = new NotificationTemplateType("commerce-client-order-accepted");

        /// <summary>
        /// Notification to the client when the commerce completes an order
        /// </summary>
        public static readonly NotificationTemplateType CommerceClientOrderCompleted = new NotificationTemplateType("commerce-client-order-completed");

        /// <summary>
        /// Notification to the driver when the commerce marks an order as "ready for pickup"
        /// </summary>
        public static readonly NotificationTemplateType CommerceDriverOrderCompleted = new NotificationTemplateType("commerce-driver-order-completed");

        /// <summary>
        /// Notification to the client when the driver accepts their order.
        /// </summary>
        public static readonly NotificationTemplateType DriverClientOrderAccepted = new NotificationTemplateType("driver-client-order-accepted");

        /// <summary>
        /// Notification to the commerce when the driver accepts their order.
        /// </summary>
        public static readonly NotificationTemplateType DriverCommerceOrderAccepted = new NotificationTemplateType("driver-commerce-order-accepted");

        /// <summary>
        /// Notification to the client when the driver picks up their order and is on its way to deliver it.
        /// </summary>
        public static readonly NotificationTemplateType DriverClientOrderRetreived = new NotificationTemplateType("driver-client-order-retreived");

        /// <summary>
        /// Notification to the client when the drvier delivers their order.
        /// </summary>
        public static readonly NotificationTemplateType DriverClientOrderDelivered = new NotificationTemplateType("driver-client-order-delivered");

        /// <summary>
        /// Notification to the commerce when the driver delivers their order to the client.
        /// </summary>
        public static readonly NotificationTemplateType DriverCommerceOrderDelivered = new NotificationTemplateType("driver-commerce-order-delivered");

        /// <summary>
        /// Notification to the client when the commerce cancels their order.
        /// </summary>
        public static readonly NotificationTemplateType CommerceClientOrderCancelled = new NotificationTemplateType("commerce-client-order-cancelled");

        /// <summary>
        /// Notification to the commerce when the driver cancels their order.
        /// </summary>
        public static readonly NotificationTemplateType DriverCommerceOrderCancelled = new NotificationTemplateType("driver-commerce-order-cancelled");
    }
}