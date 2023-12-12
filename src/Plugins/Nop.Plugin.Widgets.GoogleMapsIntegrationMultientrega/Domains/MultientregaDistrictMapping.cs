using Nop.Core;

namespace Nop.Plugin.Widgets.GoogleMapsIntegrationMultientrega.Domains
{
    /// <summary>
    /// Represets the Multientrega's district structure.
    /// </summary>
    public sealed class MultientregaDistrictMapping : BaseEntity
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
