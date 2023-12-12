namespace Nop.Plugin.Synchronizers.WAPIOrders.Models.RegisterSale.Request
{
    /// <summary>
    /// Represents the delivery structure expected by WAPI.
    /// </summary>
    public sealed class Delivery
    {
        /// <summary>
        /// Indicates the delivery type. Allowed values are "own" and "thirdParty".
        /// </summary>
        public string TypeDelivery { get; set; }

        /// <summary>
        /// Tracking code in cases where delivery is perform through a third party company.
        /// </summary>
        public string TrackingID { get; set; }

        /// <summary>
        /// Code of the company in charge of the delivery.
        /// </summary>
        public string ThirdPartyCode { get; set; }

        /// <summary>
        /// Name of person who receives.
        /// </summary>
        public string WhoReceives { get; set; }

        /// <summary>
        /// Telephone number of who receives or where the delivery is delivered.
        /// </summary>
        public string Telephone { get; set; }

        /// <summary>
        /// Delivery address.
        /// </summary>
        public string Direction { get; set; }

        /// <summary>
        /// Special instructions, which may be location of reference, delivery instructions, etc.
        /// </summary>
        public string SpecialInstruction { get; set; }
    }
}
