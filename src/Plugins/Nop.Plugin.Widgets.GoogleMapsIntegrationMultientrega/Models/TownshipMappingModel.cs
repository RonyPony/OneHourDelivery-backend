using Nop.Plugin.Widgets.GoogleMapsIntegrationMultientrega.Domains;
using Nop.Web.Framework.Models;

namespace Nop.Plugin.Widgets.GoogleMapsIntegrationMultientrega.Models
{
    /// <summary>
    /// Represents a model for <see cref="MultientregaTownshipMapping"/> entity.
    /// </summary>
    public sealed class TownshipMappingModel : BaseNopEntityModel
    {
        /// <summary>
        /// Indicates Multientrega's township id.
        /// </summary>
        public string MultientregaId { get; set; }

        /// <summary>
        /// Indicates Multientrega's district id.
        /// </summary>
        public string MultientregaDistrictId { get; set; }

        /// <summary>
        /// Indicates the township name.
        /// </summary>
        public string Name { get; set; }
    }
}
