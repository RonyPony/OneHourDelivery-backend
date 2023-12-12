using Nop.Core;

namespace Nop.Plugin.Widgets.GoogleMapsIntegrationMultientrega.Domains
{
    /// <summary>
    /// Represets the Multientrega's township structure.
    /// </summary>
    public sealed class MultientregaTownshipMapping : BaseEntity
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
