using System.Collections.Generic;

namespace Nop.Plugin.Widgets.CustomFieldsValidator.Domains
{
    /// <summary>
    /// Represents the model that provides the different resources used by CustomFieldsValidator plug-in.
    /// </summary>
    public static class CustomFieldsValidatorLocaleResources
    {
        /// <summary>
        /// Represents the local resources for displaying the plugin information in English (en-US).
        /// </summary>
        public static Dictionary<string, string> EnglishResources = new Dictionary<string, string>
        {
            ["Plugins.Widgets.CustomFieldsValidator.RequiredNITMessage"] = "You must enter a value for the NIT.",
            ["Plugins.Widgets.CustomFieldsValidator.NITLengthMessage"] = "The value entered for the NIT is incorrect. The NIT must have 14 positions.",
            ["Plugins.Widgets.CustomFieldsValidator.NITInvalidMessage"] = "The value entered for the NIT is incorrect. The NIT must be a numerical value.",

            ["Plugins.Widgets.CustomFieldsValidator.RequiredNCRMessage"] = "You must enter a value for the NCR.",
            ["Plugins.Widgets.CustomFieldsValidator.NCRInvalidMessage"] = "The value entered for the NCR is incorrect. The NCR must be a numerical value.",
            ["Plugins.Widgets.CustomFieldsValidator.NCRLengthMessage"] = "The value entered for the NCR is incorrect. The NCR must have 7 positions.",

        };

        /// <summary>
        /// Represents the local resources for displaying the plug-in information in Spanish (es-*).
        /// </summary>
        public static Dictionary<string, string> SpanishResources = new Dictionary<string, string>
        {
            ["Plugins.Widgets.CustomFieldsValidator.RequiredNITMessage"] = "Debe de ingresar un valor para el NIT.",
            ["Plugins.Widgets.CustomFieldsValidator.NITInvalidMessage"] = "El valor ingresado para el NIT es incorrecto. El NIT debe de ser un valor numérico.",
            ["Plugins.Widgets.CustomFieldsValidator.NITLengthMessage"] = "El valor ingresado para el NIT es incorrecto. El NIT debe de tener 14 posiciones.",
            
            ["Plugins.Widgets.CustomFieldsValidator.RequiredNCRMessage"] = "Debe de ingresar un valor para el NCR.",
            ["Plugins.Widgets.CustomFieldsValidator.NCRInvalidMessage"] = "El valor ingresado para el NCR es incorrecto. El NCR debe de ser un valor numérico.",
            ["Plugins.Widgets.CustomFieldsValidator.NCRLengthMessage"] = "El valor ingresado para el NCR es incorrecto. El NCR debe de tener 7 posiciones."
        };
    }
}
