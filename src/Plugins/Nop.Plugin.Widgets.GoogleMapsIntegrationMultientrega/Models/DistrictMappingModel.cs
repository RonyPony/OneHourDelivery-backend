using Nop.Web.Framework.Models;
using Nop.Plugin.Widgets.GoogleMapsIntegrationMultientrega.Domains;

namespace Nop.Plugin.Widgets.GoogleMapsIntegrationMultientrega.Models
{
    /// <summary>
    /// Represents a model for <see cref="MultientregaDistrictMapping"/> entity.
    /// </summary>
    public sealed class DistrictMappingModel : BaseNopEntityModel
    {
        /// <summary>
        /// Indicates Multientrega's district id.
        /// </summary>
        public string MultientregaId { get; set; }

        /// <summary>
        /// Indicates Multientrega's province id.
        /// </summary>
        public string MultientregaProvinceId { get; set; }

        /// <summary>
        /// Indicates the district name.
        /// </summary>
        public string Name { get; set; }
    }
}
