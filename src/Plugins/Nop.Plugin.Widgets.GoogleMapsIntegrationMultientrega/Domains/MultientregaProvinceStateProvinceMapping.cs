using Nop.Core;

namespace Nop.Plugin.Widgets.GoogleMapsIntegrationMultientrega.Domains
{
    /// <summary>
    /// Represents the relationship between a Multientrega's province and a nopCommerce StateProvince.
    /// </summary>
    public sealed class MultientregaProvinceStateProvinceMapping : BaseEntity
    {
        /// <summary>
        /// Indicates the state province id.
        /// </summary>
        public int StateProvinceId { get; set; }

        /// <summary>
        /// Indicates the multientrega's province id.
        /// </summary>
        public string MultientregaProvinceId { get; set; }
    }
}
