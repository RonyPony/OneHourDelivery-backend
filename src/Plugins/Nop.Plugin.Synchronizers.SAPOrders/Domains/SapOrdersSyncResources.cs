using System.Collections.Generic;

namespace Nop.Plugin.Synchronizers.SAPOrders.Domains
{
    /// <summary>
    /// Represents the model that provides the different resources used by SAP Orders Synchronizer plugin.
    /// </summary>
    public static class SapOrdersSyncResources
    {
        /// <summary>
        /// Represents the local resources for displaying the plug-in information in Spanish (es-*).
        /// </summary>
        public static Dictionary<string, string> SpanishResources = new Dictionary<string, string>()
        {
            ["Plugins.Synchronizers.SAPOrders.Instructions"] = @"
             <p>
	            <b>Si está utilizando el sincronizador, siga estas instrucciones para configurarlo correctamente.</b>
	            <br />
	            <br />Una vez que tenga toda la información necesaria para completar este formulario, puede continuar.<br />
	            <br />1. Complete toda la información requerida con la información proporcionada por el responsable del Ambiente SAP.
	            <br />2. Una vez que haya completado la información requerida, haga clic en Guardar.
	            <br />
            </p>
            <p>
                <b>Nota: si una URL que desea utilizar recibe algún parámetro, debe reemplazar el nombre del parámetro con {n}
                          donde n representa la posición del parámetro, esta posición es 0 base <br />
                    Por ejemplo: http: // dominio / api / sap / businesspartners / {0} / direcciones / {1}
                </b>
            </p>",
            ["Plugins.Synchronizers.SAPOrders.Fields.Api.OrdersPostUrl"] = "Url para guardar ordenes",
            ["Plugins.Synchronizers.SAPOrders.Fields.Api.OrdersPostUrl.Hint"] = "Url del servicio web de SAP que se utilizará para guardar las ordenes.",
            ["Plugins.Synchronizers.SAPOrders.Fields.Api.OrdersGetUrl"] = "Url para consultar ordenes",
            ["Plugins.Synchronizers.SAPOrders.Fields.Api.OrdersGetUrl.Hint"] = "Url del servicio web de SAP que se utilizará para consultar el estado de las ordenes, antes de actualizarlas. Escribir '{0}' en el lugar en el que se le enviará el parámetro de búsqueda a la petición GET.",
            ["Plugins.Synchronizers.SAPOrders.Fields.Api.AuthenticationScheme"] = "Esquema de autenticación",
            ["Plugins.Synchronizers.SAPOrders.Fields.Api.AuthenticationScheme.Hint"] = "Esquema de autenticación que se utilizará para conectarse con el servicio web de SAP.",
            ["Plugins.Synchronizers.SAPOrders.Fields.Api.Token"] = "Token del servicio web SAP",
            ["Plugins.Synchronizers.SAPOrders.Fields.Api.Token.Hint"] = "Token que será utilizado como método de autorización para realizar peticiones al servicio web de SAP.",
        };

        /// <summary>
        /// Represents the local resources for displaying the plugin information in English (en-US).
        /// </summary>
        public static Dictionary<string, string> EnglishResources = new Dictionary<string, string>()
        {
            ["Plugins.Synchronizers.SAPOrders.Instructions"] = @"
             <p>
	            <b>If you're using synchronizer, please follow this instructions in order to configure it correctly.</b>
	            <br />
	            <br />Once you have all the information needed for completing this form you can proceed with it.<br />
	            <br />1. Fill all the required information with the info provided by the person responsible for the SAP Environment.
	            <br />2. Once you completed the required info click Save.
	            <br />
            </p>
            <p>
                <b>Note: If an URL that you want to used receives any parameter, you need to replace the parameter name with {n} 
                         where n represents the position of the parameter, this position is 0 base <br />
                   For example: http://domain/api/sap/businesspartners/{0}/addresses/{1}
                </b>
            </p>",
            ["Plugins.Synchronizers.SAPOrders.Fields.Api.OrdersPostUrl"] = "SAP orders save url",
            ["Plugins.Synchronizers.SAPOrders.Fields.Api.OrdersPostUrl.Hint"] = "SAP API url that will be used for sending orders.",
            ["Plugins.Synchronizers.SAPOrders.Fields.Api.OrdersGetUrl"] = "SAP orders consult url",
            ["Plugins.Synchronizers.SAPOrders.Fields.Api.OrdersGetUrl.Hint"] = "SAP API url that will be used for consulting order status, before updating them. Write '{0}' in the place where the search parameter for the GET request will be sent.",
            ["Plugins.Synchronizers.SAPOrders.Fields.Api.AuthenticationScheme"] = "Authorization scheme",
            ["Plugins.Synchronizers.SAPOrders.Fields.Api.AuthenticationScheme.Hint"] = "Authorization scheme that will be used in order to connect to SAP API.",
            ["Plugins.Synchronizers.SAPOrders.Fields.Api.Token"] = "Authorization token",
            ["Plugins.Synchronizers.SAPOrders.Fields.Api.Token.Hint"] = "Token that will be used as authorization method when connecting to SAP API.",
        };
    }
}
