using System.Collections.Generic;

namespace Nop.Plugin.Synchronizers.SAPOrders.Models
{
    /// <summary>
    /// Represents a base SAP response from the SAP API.
    /// </summary>
    /// <typeparam name="T">Represents any type that will be returned in the Extra property.</typeparam>
    public class BaseSapResponse<T>
    {
        /// <summary>
        /// Represents the result code sent by SAP API.
        /// </summary>
        public int ResultCode { get; set; }

        /// <summary>
        /// Represents the description of the result code sent by SAP API.
        /// </summary>
        public string ResultDescription { get; set; }

        /// <summary>
        /// Represents the Database Id sent by SAP API.
        /// </summary>
        public string DatabaseID { get; set; }

        /// <summary>
        /// Represents the Database description sent by SAP API.
        /// </summary>
        public string Database { get; set; }

        /// <summary>
        /// Represents extra information sent by SAP API.
        /// </summary>
        public T Extra { get; set; }
    }
}
