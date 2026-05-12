namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains.Enums
{
    /// <summary>
    /// Represents the different statuses for order payment collection.
    /// </summary>
    public enum PaymentCollectionStatus
    {
        /// <summary>
        /// Indicates that the collection is not required.
        /// </summary>
        DoesNotApply = 1,

        /// <summary>
        /// Indicates that the collection is already done.
        /// </summary>
        Collected = 2,

        /// <summary>
        /// Indicates that the collection is still required.
        /// </summary>
        NotCollected = 3
    }
}
