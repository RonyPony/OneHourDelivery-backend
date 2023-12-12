using System.Collections.Generic;

namespace Nop.Plugin.Synchronizers.SAPCustomers.Domains
{
    /// <summary>
    /// Represents the model that provides the different resources used by SAP Customers Synchronizer plugin.
    /// </summary>
    public static class SapCustomersSynchronizerLocaleResources
    {
        /// <summary>
        /// Represents the local resources for displaying the plugin information in English (en-US).
        /// </summary>
        public static Dictionary<string, string> EnglishResources = new Dictionary<string, string>
        {
            ["Plugins.Synchronizers.SAPCustomers.Instructions"] = @"
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

            ["Plugins.Synchronizers.SAPCustomers.Fields.SapCustomerUrl"] = "SAP Customer URL",
            ["Plugins.Synchronizers.SAPCustomers.Fields.SapCustomerUrl.Hint"] = "URL used for requesting the customers from SAP environment.",

            ["Plugins.Synchronizers.SAPCustomers.Fields.SapCustomerRolesUrl"] = "SAP Customer roles URL",
            ["Plugins.Synchronizers.SAPCustomers.Fields.SapCustomerRolesUrl.Hint"] = "URL used for requesting the customers roles from SAP environment.",

            ["Plugins.Synchronizers.SAPCustomers.Fields.SapCustomerAddressUrl"] = "SAP Customer address URL",
            ["Plugins.Synchronizers.SAPCustomers.Fields.SapCustomerAddressUrl.Hint"] = "URL used for requesting the customers address from SAP environment.",

            ["Plugins.Synchronizers.SAPCustomers.Fields.AuthenticationHeaderScheme"] = "Authentication header scheme",
            ["Plugins.Synchronizers.SAPCustomers.Fields.AuthenticationHeaderScheme.Hint"] = "Authentication header scheme used in the request's header.",

            ["Plugins.Synchronizers.SAPCustomers.Fields.AuthenticationHeaderParameter"] = "Authentication header parameter",
            ["Plugins.Synchronizers.SAPCustomers.Fields.AuthenticationHeaderParameter.Hint"] = "Authentication header parameter that will be at the request's header."
        };

        /// <summary>
        /// Represents the local resources for displaying the plug-in information in Spanish (es-*).
        /// </summary>
        public static Dictionary<string, string> SpanishResources = new Dictionary<string, string>
        {
            ["Plugins.Synchronizers.SAPCustomers.Instructions"] = @"
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

            ["Plugins.Synchronizers.SAPCustomers.Fields.SapCustomerUrl"] = "URL Clientes de SAP",
            ["Plugins.Synchronizers.SAPCustomers.Fields.SapCustomerUrl.Hint"] = "URL utilizada para soliciar los clientes que se encuentran en el ambiente de SAP.",

            ["Plugins.Synchronizers.SAPCustomers.Fields.SapCustomerRolesUrl"] = "URL Roles de SAP",
            ["Plugins.Synchronizers.SAPCustomers.Fields.SapCustomerRolesUrl.Hint"] = "URL utilizada para soliciar los roles de los clientes que se encuentran en el ambiente de SAP.",

            ["Plugins.Synchronizers.SAPCustomers.Fields.SapCustomerAddressUrl"] = "URL Direcciones de SAP",
            ["Plugins.Synchronizers.SAPCustomers.Fields.SapCustomerAddressUrl.Hint"] = "URL utilizada para soliciar los direcciones de los clientes que se encuentran en el ambiente de SAP.",

            ["Plugins.Synchronizers.SAPCustomers.Fields.AuthenticationHeaderScheme"] = "Esquema cabecera de autenticación",
            ["Plugins.Synchronizers.SAPCustomers.Fields.AuthenticationHeaderScheme.Hint"] = "Esquema de encabezado de autenticación utilizado en el encabezado de la solicitud.",

            ["Plugins.Synchronizers.SAPCustomers.Fields.AuthenticationHeaderParameter"] = "Parametro cabecera de autenticación",
            ["Plugins.Synchronizers.SAPCustomers.Fields.AuthenticationHeaderParameter.Hint"] = "Parámetro de encabezado de autenticación que estará en el encabezado de la solicitud."
        };
    }
}
