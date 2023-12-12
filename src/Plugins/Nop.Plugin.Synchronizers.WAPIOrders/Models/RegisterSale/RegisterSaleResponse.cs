using Nop.Plugin.Synchronizers.WAPIOrders.Models.RegisterSale.Response;
using System;

namespace Nop.Plugin.Synchronizers.WAPIOrders.Models.RegisterSale
{
    /// <summary>
    /// Represents the response send by WAPI to the eCommerce.
    /// </summary>
    public sealed class RegisterSaleResponse
    {
        /// <summary>
        /// Indicates the version of WAPI.
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// Indicates the response code.
        /// </summary>
        public int CodRes { get; set; }

        /// <summary>
        /// Indicates the response message.
        /// </summary>
        public string MsgRes { get; set; }

        /// <summary>
        /// Indicates the date and time of request process.
        /// </summary>
        public DateTime DateTimeProcess { get; set; }

        /// <summary>
        /// Indicates the response details.
        /// </summary>
        public OResponse OResponse { get; set; }
    }
}
