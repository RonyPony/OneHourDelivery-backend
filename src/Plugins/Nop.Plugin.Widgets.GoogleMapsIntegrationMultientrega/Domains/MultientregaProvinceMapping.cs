using Nop.Core;

namespace Nop.Plugin.Widgets.GoogleMapsIntegrationMultientrega.Domains
{
    /// <summary>
    /// Represets the Multientrega's province structure.
    /// </summary>
    public sealed class MultientregaProvinceMapping : BaseEntity
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
