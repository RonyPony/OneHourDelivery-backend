using Nop.Plugin.Widgets.GoogleMapsIntegrationMultientrega.Domains;
using Nop.Web.Framework.Models;

namespace Nop.Plugin.Widgets.GoogleMapsIntegrationMultientrega.Models
{
    /// <summary>
    /// Represents a model for <see cref="MultientregaProvinceMapping"/> entity.
    /// </summary>
    public sealed class ProvinceMappingModel : BaseNopEntityModel
    {
        /// <summary>
        /// Indicates Multientrega's province id.
        /// </summary>
        public string MultientregaId { get; set; }

        /// <summary>
        /// Indicates the province name.
        /// </summary>
        public string Name { get; set; }
    }
}
