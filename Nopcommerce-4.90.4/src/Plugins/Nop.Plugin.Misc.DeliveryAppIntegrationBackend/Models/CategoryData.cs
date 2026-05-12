using Nop.Web.Framework.Models;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models
{
    /// <summary>
    /// Represents category data
    /// </summary>
    public sealed record CategoryData : BaseNopEntityModel
    {
        /// <summary>
        /// Gets or sets the 
        /// </summary>
        public override int Id { get; set; }

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the picture
        /// </summary>
        public string Picture { get; set; }

        /// <summary>
        /// Gets or sets the order
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// Gets or sets the color
        /// </summary>
        public string Color { get; set; }
    }
}
