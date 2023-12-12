namespace Nop.Plugin.Synchronizers.ASAP.Models
{
    /// <summary>
    /// Represent the request of ASAP service.
    /// </summary>
    public sealed class AsapRequest
    {
        /// <summary>
        /// Represent the user token.
        /// </summary>
        public string User_token { get; set; }

        /// <summary>
        /// Represent the shared secret.
        /// </summary>
        public string Shared_secret { get; set; }

        /// <summary>
        /// Represent the type id.
        /// </summary>
        public int Type_id { get; set; }

        /// <summary>
        /// Represent a type of delivery.
        /// </summary>
        public int Is_personal { get; set; }

        /// <summary>
        /// Represent a type of delivery.
        /// </summary>
        public int Is_oneway { get; set; }

        /// <summary>
        /// Represent the address for pick up the order.
        /// </summary>
        public string Source_address { get; set; }

        /// <summary>
        /// Represent the latitude for pick up the order.
        /// </summary>
        public string Source_lat { get; set; }

        /// <summary>
        /// Represent the length for pick up the order.
        /// </summary>
        public string Source_long { get; set; }

        /// <summary>
        /// Represent the special instructions for pick up the order.
        /// </summary>
        public string Special_inst { get; set; }

        /// <summary>
        /// Represent the address for delivered the order.
        /// </summary>
        public string Desti_address { get; set; }

        /// <summary>
        /// Represent the latitude for delivered the order.
        /// </summary>
        public string Desti_lat { get; set; }

        /// <summary>
        /// Represent the length for delivered the order.
        /// </summary>
        public string Desti_long { get; set; }

        /// <summary>
        /// Represent the special instructions for delivered.
        /// </summary>
        public string Dest_special_inst { get; set; }

        /// <summary>
        /// Represent the request later.
        /// </summary>
        public int Request_later { get; set; }

        /// <summary>
        /// Represent the last date of executed of request.
        /// </summary>
        public string Request_later_time { get; set; }

        /// <summary>
        /// Represent the vehicle type for shipping order.
        /// </summary>
        public string Vehicle_type { get; set; }
    }
}
