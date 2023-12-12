namespace Nop.Plugin.Widgets.ProductAvailability.Models.Cellar
{
    /// <summary>
    /// Represents a DDP's warehouse.
    /// </summary>
    public sealed class BranchOffice
    {
        /// <summary>
        /// Indicates the name.
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// Indicates the store number.
        /// </summary>
        public string NumTienda { get; set; }

        /// <summary>
        /// Indicates the cellar number.
        /// </summary>
        public string NumBodega { get; set; }
    }
}
