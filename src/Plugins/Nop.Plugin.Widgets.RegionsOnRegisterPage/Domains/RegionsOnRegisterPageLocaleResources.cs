using System.Collections.Generic;

namespace Nop.Plugin.Widgets.RegionsOnRegisterPage.Domains
{
    /// <summary>
    /// Represents the model that provides the different resources used by RegionsOnRegisterPage plug-in.
    /// </summary>
    public partial  class RegionsOnRegisterPageLocaleResources
    {
        /// <summary>
        /// Represents the local resources for displaying the plugin information in English (en-US).
        /// </summary>
        public static Dictionary<string, string> EnglishResources = new Dictionary<string, string>
        {
            ["Plugin.Widgets.RegionsOnRegisterPage.Title"] = "Region",
            ["Plugin.Widgets.RegionsOnRegisterPage.RequiredErrorMessage"] = "Region is required"

        };

        /// <summary>
        /// Represents the local resources for displaying the plugin information in Spanish (es-).
        /// </summary>
        public static Dictionary<string, string> SpanishResources = new Dictionary<string, string>
        {
            ["Plugin.Widgets.RegionsOnRegisterPage.Title"] = "Región",
            ["Plugin.Widgets.RegionsOnRegisterPage.RequiredErrorMessage"] = "La Región es requerida"
        };
    }
}