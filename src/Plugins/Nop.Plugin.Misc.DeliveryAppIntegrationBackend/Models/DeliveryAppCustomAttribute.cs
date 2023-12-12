using Nop.Core.Domain.Catalog;
using System.Collections.Generic;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models
{
    /// <summary>
    /// Represents a custom attribute used by the Delivery App Integration plugin.
    /// </summary>
    public sealed class DeliveryAppCustomAttribute
    {
        /// <summary>
        /// Indicates the name of the attribute.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Indicates the attribute control type.
        /// </summary>
        public AttributeControlType ControlType { get; set; }

        /// <summary>
        /// Indicates the attribute values.
        /// </summary>
        public IList<string> Options { get; set; }
    }
}
