namespace Nop.Plugin.Synchronizers.ASAP.Enums
{
    /// <summary>
    /// Represents the Delivery Statuses
    /// </summary>
    public enum DeliveryStatus
    {
        /// <summary>
        /// When delivery not yet shipped.
        /// </summary>
        InProgress = 0,

        /// <summary>
        /// When delivery has canceled.
        /// </summary>
        Canceled = 1,

        /// <summary>
        /// When delivery has delivered.
        /// </summary>
        Delivered = 2,

        /// <summary>
        /// When delivery payment failed.
        /// </summary>
        Payment_failed= 3,

        /// <summary>
        /// When delivery has failed.
        /// </summary>
        Failed = 4,

        /// <summary>
        /// when the delivery was returned.
        /// </summary>
        Returned = 5,

        /// <summary>
        /// When delivery not yet shipped.
        /// </summary>
        Confirmed = 6,

        /// <summary>
        /// When delivery is arrival.
        /// </summary>
        Arrival = 100,

        /// <summary>
        /// When delivery is on the way.
        /// </summary>
        OnTheWay = 101
        
    }
}