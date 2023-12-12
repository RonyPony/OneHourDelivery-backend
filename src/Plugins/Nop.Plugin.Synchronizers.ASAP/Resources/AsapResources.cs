using System.Collections.Generic;

namespace Nop.Plugin.Synchronizers.ASAP.Resources
{
    public static class AsapResources
    {
        /// <summary>
        /// Represents the local resources for displaying the plug-in information in Spanish (es-*).
        /// </summary>
        public static Dictionary<string, string> SpanishResources => new Dictionary<string, string>()
        {
            [$"{AsapDeliveryDefaults.AsapConfigurationResources}.Fields.ApiKey"] = "Api-key",
            [$"{AsapDeliveryDefaults.AsapConfigurationResources}.Fields.ApiKey.Hint"] = "Éste Api-Key es el proporcionado por los administradores de ASAP",
            [$"{AsapDeliveryDefaults.AsapConfigurationResources}.Fields.ApiKey.ErrorMessage"] = "El Api-Key es requerido",

            [$"{AsapDeliveryDefaults.AsapConfigurationResources}.Fields.ServiceUrl"] = "Url del servicio web de ASAP",
            [$"{AsapDeliveryDefaults.AsapConfigurationResources}.Fields.ServiceUrl.Hint"] = "Debe colocar aquí la url base del servicio",
            [$"{AsapDeliveryDefaults.AsapConfigurationResources}.Fields.ServiceUrl.ErrorMessage"] = "La Url del servicio es requerida",

            [$"{AsapDeliveryDefaults.AsapConfigurationResources}.Fields.SharedSecret"] = "Secreto compartido (Shared secret)",
            [$"{AsapDeliveryDefaults.AsapConfigurationResources}.Fields.SharedSecret.Hint"] = "Éste SharedSecret es el proporcionado por los administradores de ASAP",
            [$"{AsapDeliveryDefaults.AsapConfigurationResources}.Fields.SharedSecret.ErrorMessage"] = "El secreto compartido es requerido",

            [$"{AsapDeliveryDefaults.AsapConfigurationResources}.Fields.UserToken"] = "Token de usuario",
            [$"{AsapDeliveryDefaults.AsapConfigurationResources}.Fields.UserToken.Hint"] = "Éste Token de usuario es proporcionado por los administradores de ASAP",
            [$"{AsapDeliveryDefaults.AsapConfigurationResources}.Fields.UserToken.ErrorMessage"] = "El token del usuario es requerido",

            [$"{AsapDeliveryDefaults.AsapConfigurationResources}.Fields.Email"] = "Correo electrónico",
            [$"{AsapDeliveryDefaults.AsapConfigurationResources}.Fields.Email.Hint"] = "Correo electrónico",
            [$"{AsapDeliveryDefaults.AsapConfigurationResources}.Fields.Email.ErrorMessage"] = "El correo electronico es requerido",

            [$"{AsapDeliveryDefaults.AsapConfigurationResources}.Fields.Rate"] = "Tarifa",
            [$"{AsapDeliveryDefaults.AsapConfigurationResources}.Fields.Rate.Hint"] = "Representa la tarifa dada al servicio de delivery ASAP",
            [$"{AsapDeliveryDefaults.AsapConfigurationResources}.Fields.Rate.ErrorMessage"] = "La tarifa es requerida",

            [$"{AsapDeliveryDefaults.AsapConfigurationResources}.Fields.DefaultWarehoseId"] = "Almacen predeterminado",
            [$"{AsapDeliveryDefaults.AsapConfigurationResources}.Fields.DefaultWarehoseId.Hint"] = "Debe seleccionar el almacen correspondiente",
            [$"{AsapDeliveryDefaults.AsapConfigurationResources}.Fields.DefaultWarehoseId.ErrorMessage"] = "El almacen es requerido",

            [$"{AsapDeliveryDefaults.AsapConfigurationResources}.Fields.ThrowException"] = "Ha ocurrido un error, el sistema no pudo procesar el envío.",
            [$"{AsapDeliveryDefaults.AsapConfigurationResources}.Fields.Exception"] = "Se produjo un error al solicitar el envío por entrega lo antes posible",

            [$"{AsapDeliveryDefaults.AsapConfigurationResources}.Fields.RequestSuccess"] = "La solicitud de envío por Delivery ASAP se ha realizado correctamente.",

            [$"{AsapDeliveryDefaults.AsapConfigurationResources}.Fields.Required"] = "Todos los campos son requeridos",

            [$"{AsapDeliveryDefaults.AsapConfigurationResources}.Fields.AsapSearchButton"] = "Búsqueda de paquete ASAP",
            
            [$"{AsapDeliveryDefaults.AsapConfigurationResources}.Fields.Description"] = "Descripción",
            [$"{AsapDeliveryDefaults.AsapConfigurationResources}.Fields.Description.Hint"] = "Agregue una descripción",

            [$"{AsapDeliveryDefaults.AsapConfigurationResources}.Fields.Warehouses"] = "Almacenes",
            [$"{AsapDeliveryDefaults.AsapConfigurationResources}.Fields.Warehouses.Hint"] = "Almacenes disponibles",
        };

        /// <summary>
        /// Represents the local resources for displaying the plugin information in English (en-US).
        /// </summary>
        public static Dictionary<string, string> EnglishResources => new Dictionary<string, string>()
        {
            [$"{AsapDeliveryDefaults.AsapConfigurationResources}.Fields.ApiKey"] = "Api-key",
            [$"{AsapDeliveryDefaults.AsapConfigurationResources}.Fields.ApiKey.Hint"] = "This Api - Key is the one provided by the ASAP administrators",
            [$"{AsapDeliveryDefaults.AsapConfigurationResources}.Fields.ApiKey.ErrorMessage"] = "The Api-Key is required",

            [$"{AsapDeliveryDefaults.AsapConfigurationResources}.Fields.ServiceUrl"] = "ASAP service url",
            [$"{AsapDeliveryDefaults.AsapConfigurationResources}.Fields.ServiceUrl.Hint"] = "You must place the base url of the service here",
            [$"{AsapDeliveryDefaults.AsapConfigurationResources}.Fields.ServiceUrl.ErrorMessage"] = "The ASAP service url is required",

            [$"{AsapDeliveryDefaults.AsapConfigurationResources}.Fields.SharedSecret"] = "Shared secret",
            [$"{AsapDeliveryDefaults.AsapConfigurationResources}.Fields.SharedSecret.Hint"] = "This SharedSecret is the one provided by ASAP administrators",
            [$"{AsapDeliveryDefaults.AsapConfigurationResources}.Fields.SharedSecret.ErrorMessage"] = "the shared secret is required",

            [$"{AsapDeliveryDefaults.AsapConfigurationResources}.Fields.UserToken"] = "User token",
            [$"{AsapDeliveryDefaults.AsapConfigurationResources}.Fields.UserToken.Hint"] = "This User Token is provided by ASAP administrators",
            [$"{AsapDeliveryDefaults.AsapConfigurationResources}.Fields.UserToken.ErrorMessage"] = "The user token is required",

            [$"{AsapDeliveryDefaults.AsapConfigurationResources}.Fields.Email"] = "Email",
            [$"{AsapDeliveryDefaults.AsapConfigurationResources}.Fields.Email.Hint"] = "Email",
            [$"{AsapDeliveryDefaults.AsapConfigurationResources}.Fields.Email.ErrorMessage"] = "The email is required",

            [$"{AsapDeliveryDefaults.AsapConfigurationResources}.Fields.Rate"] = "Rate",
            [$"{AsapDeliveryDefaults.AsapConfigurationResources}.Fields.Rate.Hint"] = "Represents the rate given to the ASAP delivery service",
            [$"{AsapDeliveryDefaults.AsapConfigurationResources}.Fields.Rate.ErrorMessage"] = "The Rate is required",

            [$"{AsapDeliveryDefaults.AsapConfigurationResources}.Fields.DefaultWarehoseId"] = "Default Warehose",
            [$"{AsapDeliveryDefaults.AsapConfigurationResources}.Fields.DefaultWarehoseId.Hint"] = "You must select the corresponding warehouse",
            [$"{AsapDeliveryDefaults.AsapConfigurationResources}.Fields.DefaultWarehoseId.ErrorMessage"] = "The Warehouse is required",

            [$"{AsapDeliveryDefaults.AsapConfigurationResources}.Fields.ThrowException"] = "An error occurred, the system could not process the shipment.",
            [$"{AsapDeliveryDefaults.AsapConfigurationResources}.Fields.Exception"] = "Something went wrong while requesting shipment by Delivery ASAP",

            [$"{AsapDeliveryDefaults.AsapConfigurationResources}.Fields.RequestSuccess"] = "Delivery has been successfully requested.",

            [$"{AsapDeliveryDefaults.AsapConfigurationResources}.Fields.Required"] = "All fields are required",

            [$"{AsapDeliveryDefaults.AsapConfigurationResources}.Fields.AsapSearchButton"] = "ASAP package search",
            
            [$"{AsapDeliveryDefaults.AsapConfigurationResources}.Fields.Description"] = "Description",
            [$"{AsapDeliveryDefaults.AsapConfigurationResources}.Fields.Description.Hint"] = "Add a description",

            [$"{AsapDeliveryDefaults.AsapConfigurationResources}.Fields.Warehouses"] = "Warehouses",
            [$"{AsapDeliveryDefaults.AsapConfigurationResources}.Fields.Warehouses.Hint"] = "Avaible warehouses",
        };
    }
}