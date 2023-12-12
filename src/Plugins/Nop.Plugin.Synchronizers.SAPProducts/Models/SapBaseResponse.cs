namespace Nop.Plugin.Synchronizers.SAPProducts.Models
{
    /// <summary>
    /// Represents the base model class for the SAP's response.
    /// </summary>
    public class SapBaseResponse
    {
        /// <summary>
        /// Indicates the response's result code.
        /// </summary>
        public int ResultCode { get; set; }

        /// <summary>
        /// Indicates the response's result description.
        /// </summary>
        public string ResultDescription { get; set; }

        /// <summary>
        /// Indicates the response's database id.
        /// </summary>
        public string DatabaseID { get; set; }

        /// <summary>
        /// Indicates the response's database name.
        /// </summary>
        public string Database { get; set; }
    }
}
