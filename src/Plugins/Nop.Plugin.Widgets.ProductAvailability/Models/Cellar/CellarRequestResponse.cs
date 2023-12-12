using System.Collections.Generic;

namespace Nop.Plugin.Widgets.ProductAvailability.Models.Cellar
{
    /// <summary>
    /// Represents a response from cellar request.
    /// </summary>
    public sealed class CellarRequestResponse
    {
        /// <summary>
        /// Indicates if the request was successful.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Indicates the list of cellars.
        /// </summary>
        public IList<BranchOffice> Sucursales { get; set; }
    }
}
