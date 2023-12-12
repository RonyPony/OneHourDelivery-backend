using Nop.Plugin.Widgets.GoogleMapsIntegrationMultientrega.Domains;
using Nop.Web.Framework.Models;

namespace Nop.Plugin.Widgets.GoogleMapsIntegrationMultientrega.Models
{
    /// <summary>
    /// Represents a model for edit order address additional data.
    /// </summary>
    public sealed class AddressCoordinatesMultientregaStructureModel : BaseNopModel
    {
        ///<inheritdoc/>
        public MultientregaAddressStructureModel MultientregaAddressStructure { get; set; }

        ///<inheritdoc/>
        public AddressGeoCoordinatesMapping AddressGeoCoordinates { get; set; }
    }
}
