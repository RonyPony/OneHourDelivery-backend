namespace Nop.Plugin.Synchronizers.SAPProducts.Models.Manufacturers
{
    /// <summary>
    /// Represents the manufacturers included in the response from the SAP API.
    /// </summary>
    public sealed class SapManufacturerModel
    {
        /// <summary>
        /// Indicates the manufacturer's code.
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// Indicates the manufacturer's name.
        /// </summary>
        public string ManufacturerName { get; set; }
    }
}
