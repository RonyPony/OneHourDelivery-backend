namespace Nop.Plugin.Synchronizers.SAPCustomers.Models
{
    /// <summary>
    /// Represents the base model class for the SAP's response.
    /// </summary>
    public class SapBaseResponse
    {
        /// <summary>
        /// Represents the result's code.
        /// </summary>
        public int ResultCode { get; set; }

        /// <summary>
        /// Represents the result's description.
        /// </summary>
        public string ResultDescription { get; set; }

        /// <summary>
        /// Represents the database's identification.
        /// </summary>
        public string DatabaseId { get; set; }

        /// <summary>
        /// Represents the database.
        /// </summary>
        public string Database { get; set; }
    }
}
