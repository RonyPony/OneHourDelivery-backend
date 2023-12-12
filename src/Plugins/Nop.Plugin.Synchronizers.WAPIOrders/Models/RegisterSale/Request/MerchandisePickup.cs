namespace Nop.Plugin.Synchronizers.WAPIOrders.Models.RegisterSale.Request
{
    /// <summary>
    /// Represents the withdrawal of merchandise structure expected by WAPI.
    /// </summary>
    public sealed class MerchandisePickup
    {
        /// <summary>
        /// Type of withdrawal. Allowed values are "Delivery" and "InStore".
        /// </summary>
        public string TypePickup { get; set; }

        /// <summary>
        /// Code of the store where the order is prepared, in cases that typePickup is "InStore" the code of the store 
        /// where it is going to be removed, in the cases of "Delivery" put the code of the store that will prepare the order.
        /// </summary>
        public string StorePickup { get; set; }

        /// <summary>
        /// Delivery structure expected by WAPI. In cases where it is withdrawal in store, the value is <see cref="null"/>.
        /// </summary>
        public Delivery Delivery { get; set; }
    }
}
