namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains.Enums
{
    /// <summary>
    /// Delivery app order's delivery status.
    /// </summary>
    public enum DeliveryStatus
    {
        AwaitingForMessenger = 1,
        AssignedToMessenger = 2,
        OrderPreparationCompleted = 3,
        DeliveryInProgress = 4,
        Delivered = 5,
        DeclinedByStore = 6
    }
}
