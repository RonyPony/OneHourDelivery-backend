using Nop.Plugin.Synchronizers.WAPIOrders.Helpers;
using System.Collections.Generic;

namespace Nop.Plugin.Synchronizers.WAPIOrders.Domains
{
    /// <summary>
    /// Represents the model that provides the different resources used by WAPI Orders Synchronizer.
    /// </summary>
    public static class LocaleResources
    {
        /// <summary>
        /// Represents the local resources for displaying the plugin information in English (en-US).
        /// </summary>
        public static Dictionary<string, string> EnglishResources = new Dictionary<string, string>
        {
            [$"{Defaults.LocaleResorcesPrefix}.Configuration.Instructions"] = @"
             <p>
	            <b>If you're using synchronizer, please follow this instructions in order to configure it correctly.</b>
	            <br />
	            <br />Once you have all the information needed for completing this form you can proceed with it.<br />
	            <br />1. Fill all the required information with the info provided by the person responsible for the WAPI Environment.
	            <br />2. Once you completed the required info click Save.
	            <br />
            </p>",

            [$"{Defaults.LocaleResorcesPrefix}.Fields.AuthKeyName"] = "Auth key name",
            [$"{Defaults.LocaleResorcesPrefix}.Fields.AuthKeyName.Hint"] = "Name of the secret API Key used for WAPI request authentication.",
            [$"{Defaults.LocaleResorcesPrefix}.Fields.AuthKeyName.Required"] = "The authorization key name is required",

            [$"{Defaults.LocaleResorcesPrefix}.Fields.AuthKeyValue"] = "Auth key value",
            [$"{Defaults.LocaleResorcesPrefix}.Fields.AuthKeyValue.Hint"] = "Value of the secret API Key used for WAPI request authentication.",
            [$"{Defaults.LocaleResorcesPrefix}.Fields.AuthKeyValue.Required"] = "The authorization key value is required",

            [$"{Defaults.LocaleResorcesPrefix}.Fields.ApiPostUrl"] = "Register sale URL",
            [$"{Defaults.LocaleResorcesPrefix}.Fields.ApiPostUrl.Hint"] = "URL that will be used to register an order to WAPI enviroment.",
            [$"{Defaults.LocaleResorcesPrefix}.Fields.ApiPostUrl.Required"] = "The WAPI register sale URL is required",

            [$"{Defaults.LocaleResorcesPrefix}.Fields.DefaultStorePickupCode"] = "Default store pickup code",
            [$"{Defaults.LocaleResorcesPrefix}.Fields.DefaultStorePickupCode.Hint"] = "The default store pickup code to be used when pickup point is not defined.",
            [$"{Defaults.LocaleResorcesPrefix}.Fields.DefaultStorePickupCode.Required"] = "The default store pickup code is required"
        };

        /// <summary>
        /// Represents the local resources for displaying the plugin information in Spanish (es-).
        /// </summary>
        public static Dictionary<string, string> SpanishResources = new Dictionary<string, string>
        {
            [$"{Defaults.LocaleResorcesPrefix}.Configuration.Instructions"] = @"
             <p>
	            <b>Si está utilizando el sincronizador, siga estas instrucciones para configurarlo correctamente.</b>
	            <br />
	            <br />Una vez que tenga toda la información necesaria para completar este formulario, puede continuar.<br />
	            <br />1. Complete toda la información requerida con la información proporcionada por el responsable del Ambiente WAPI.
	            <br />2. Una vez que haya completado la información requerida, haga clic en Guardar.
	            <br />
            </p>",

            [$"{Defaults.LocaleResorcesPrefix}.Fields.AuthKeyName"] = "Nombre de la llave de autorización",
            [$"{Defaults.LocaleResorcesPrefix}.Fields.AuthKeyName.Hint"] = "Nombre de la llave secreta utilizada para la autenticación de solicitudes en el ambiente WAPI.",
            [$"{Defaults.LocaleResorcesPrefix}.Fields.AuthKeyName.Required"] = "El nombre de la llave de autorización es requerido",

            [$"{Defaults.LocaleResorcesPrefix}.Fields.AuthKeyValue"] = "Valor de la llave de autorización",
            [$"{Defaults.LocaleResorcesPrefix}.Fields.AuthKeyValue.Hint"] = "Valor de la llave secreta utilizada para la autenticación de solicitudes en el ambiente WAPI.",
            [$"{Defaults.LocaleResorcesPrefix}.Fields.AuthKeyValue.Required"] = "El valor de la llave de autorización es requerido",

            [$"{Defaults.LocaleResorcesPrefix}.Fields.ApiPostUrl"] = "URL de registro de orden",
            [$"{Defaults.LocaleResorcesPrefix}.Fields.ApiPostUrl.Hint"] = "URL que se utilizará para registrar una orden en el ambiente WAPI.",
            [$"{Defaults.LocaleResorcesPrefix}.Fields.ApiPostUrl.Required"] = "La URL para el registro de orden es requerida",

            [$"{Defaults.LocaleResorcesPrefix}.Fields.DefaultStorePickupCode"] = "Código de tienda por defecto",
            [$"{Defaults.LocaleResorcesPrefix}.Fields.DefaultStorePickupCode.Hint"] = "Código de tienda que se utilizará por defecto cuando el punto de recogida no sea definido.",
            [$"{Defaults.LocaleResorcesPrefix}.Fields.DefaultStorePickupCode.Required"] = "El código de tienda por defecto es requerido"
        };
    }
}
