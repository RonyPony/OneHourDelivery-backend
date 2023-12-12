using Nop.Core.Configuration;

namespace Nop.Plugin.Widgets.RegionsOnRegisterPage
{
    /// <summary>
    /// Represents the main settings class used for requesting the differents settings of the plugin.
    /// </summary>
    public sealed class RegionsOnRegisterPageSettings: ISettings
    {
        /// <summary>
        /// Represents the ID of a specific region
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Represents the Name of a specific region
        /// </summary>
        public string Name { get; set; }
    }
}