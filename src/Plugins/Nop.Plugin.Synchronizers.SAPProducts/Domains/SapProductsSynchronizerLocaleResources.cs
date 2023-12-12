using System.Collections.Generic;

namespace Nop.Plugin.Synchronizers.SAPProducts.Domains
{
    /// <summary>
    /// Represents the model that provides the different resources used by SAP Products Synchronizer plugin.
    /// </summary>
    public static class SapProductsSynchronizerLocaleResources
    {
        /// <summary>
        /// Represents the local resources for displaying the plugin information in English (en-US).
        /// </summary>
        public static Dictionary<string, string> EnglishResources = new Dictionary<string, string>
        {
            ["Plugins.Synchronizers.SAPProducts.Instructions"] = @"
                    <p>
	                    <b>If you're using synchronizer, please follow this instructions in order to configure it correctly.</b>
	                    <br />
	                    <br />Once you have all the information needed for completing this form you can proceed with it.<br />
	                    <br />1. Fill all the required information with the info provided by the person responsible for the SAP Environment.
	                    <br />2. Once you completed the required info click Save.
	                    <br />
                    </p>
                    <p>
                        <b>Note:</b> If an URL that you want to used receives any parameter, you need to replace the parameter name with {n} 
                                 where n represents the position of the parameter, this position is 0 base.
                        <br />
                        <b>For example: http://domain/api/sap/businesspartners/{0}/addresses/{1} </b>
                        <br />
                    </p>",

            ["Plugins.Synchronizers.SAPProducts.Fields.SapProductUrl"] = "SAP Product URL",
            ["Plugins.Synchronizers.SAPProducts.Fields.SapProductUrl.Hint"] = "URL used for requesting the products from SAP environment.",

            ["Plugins.Synchronizers.SAPProducts.Fields.AuthenticationHeaderScheme"] = "Authentication header scheme",
            ["Plugins.Synchronizers.SAPProducts.Fields.AuthenticationHeaderScheme.Hint"] = "Authentication header scheme used in the request's header.",

            ["Plugins.Synchronizers.SAPProducts.Fields.AuthenticationHeaderParameter"] = "Authentication header parameter",
            ["Plugins.Synchronizers.SAPProducts.Fields.AuthenticationHeaderParameter.Hint"] = "Authentication header parameter that will be at the request's header.",

            ["Plugins.Synchronizers.SAPProducts.Fields.SyncManufacturers"] = "Synchronize Manufacturers",
            ["Plugins.Synchronizers.SAPProducts.Fields.SyncManufacturers.Hint"] = "Check if the plugin must sync manufacturers.",

            ["Plugins.Synchronizers.SAPProducts.Fields.SyncCategories"] = "Synchronize Categories",
            ["Plugins.Synchronizers.SAPProducts.Fields.SyncCategories.Hint"] = "Check if the plugin must sync categories.",

            ["Plugins.Synchronizers.SAPProducts.Fields.SapCategoryUrl"] = "SAP Category URL",
            ["Plugins.Synchronizers.SAPProducts.Fields.SapCategoryUrl.Hint"] = "URL used for requesting the categories from SAP environment.",

            ["Plugins.Synchronizers.SAPProducts.Fields.SapManufacturerUrl"] = "SAP Manufacturer URL",
            ["Plugins.Synchronizers.SAPProducts.Fields.SapManufacturerUrl.Hint"] = "URL used for requesting the manufacturers from SAP environment.",

            ["Plugins.Synchronizers.SAPProducts.Fields.SapCategoryUrl.Required"] = "SAP Category URL is required.",
            ["Plugins.Synchronizers.SAPProducts.Fields.SapManufacturerUrl.Required"] = "SAP Manufacturer URL is required.",

            ["Plugins.Synchronizers.SAPProducts.Exceptions.PluginNotConfigured"] = "Plugin not configured correctly.",
            ["Plugins.Synchronizers.SAPProducts.Exceptions.ProductsSync"] = "Error syncing products.",
            ["Plugins.Synchronizers.SAPProducts.Exceptions.GettingProductsFromSap"] = "Error getting products from SAP API.",
            ["Plugins.Synchronizers.SAPProducts.Exceptions.GettingCategoriesFromSap"] = "Error getting categories from SAP API.",
            ["Plugins.Synchronizers.SAPProducts.Exceptions.GettingManufacturersFromSap"] = "Error getting manufacturers from SAP API.",
            ["Plugins.Synchronizers.SAPProducts.Exceptions.CategoriesSync"] = "Error syncing categories.",
            ["Plugins.Synchronizers.SAPProducts.Exceptions.ManufacturersSync"] = "Error syncing manufacturers.",

            ["Plugins.Synchronizers.SAPProducts.Fields.AssignItemCodeFieldAsSku"] = "Assign ItemCode field as SKU",
            ["Plugins.Synchronizers.SAPProducts.Fields.AssignItemCodeFieldAsSku.Hint"] = "Indicates if the plugin must assign the ItemCode field as SKU when mapping the products form SAP.",

            ["Plugins.Synchronizers.SAPProducts.Fields.UpdateProductInformation"] = "Update product information",
            ["Plugins.Synchronizers.SAPProducts.Fields.AssignItemCodeFieldAsSku.Hint"] = "Indicates if the plugin must update the information of the products that are already synchronized."
        };

        /// <summary>
        /// Represents the local resources for displaying the plug-in information in Spanish (es-*).
        /// </summary>
        public static Dictionary<string, string> SpanishResources = new Dictionary<string, string>
        {
            ["Plugins.Synchronizers.SAPProducts.Instructions"] = @"
                    <p>
	                    <b>Si está utilizando el sincronizador, siga estas instrucciones para configurarlo correctamente.</b>
	                    <br />
	                    <br />Una vez que tenga toda la información necesaria para completar este formulario, puede continuar.<br />
	                    <br />1. Complete toda la información requerida con la información proporcionada por el responsable del Ambiente SAP.
	                    <br />2. Una vez que haya completado la información requerida, haga clic en Guardar.
	                    <br />
                    </p>
                    <p>
                        <b>Nota:</b> si una URL que desea utilizar recibe algún parámetro, debe reemplazar el nombre del parámetro con {n}
                                  donde n representa la posición del parámetro, esta posición es base 0.
                        <br />
                        <b>Por ejemplo: http://domain/api/sap/businesspartners/{0}/addresses/{1} </b>
                        <br />
                    </p>",

            ["Plugins.Synchronizers.SAPProducts.Fields.SapProductUrl"] = "URL de Productos de SAP",
            ["Plugins.Synchronizers.SAPProducts.Fields.SapProductUrl.Hint"] = "URL principal utilizada para soliciar los productos que se encuentran en el ambiente de SAP.",

            ["Plugins.Synchronizers.SAPProducts.Fields.AuthenticationHeaderScheme"] = "Esquema cabecera de autenticación",
            ["Plugins.Synchronizers.SAPProducts.Fields.AuthenticationHeaderScheme.Hint"] = "Esquema de encabezado de autenticación utilizado en el encabezado de la solicitud.",

            ["Plugins.Synchronizers.SAPProducts.Fields.AuthenticationHeaderParameter"] = "Parámetro cabecera de autenticación",
            ["Plugins.Synchronizers.SAPProducts.Fields.AuthenticationHeaderParameter.Hint"] = "Parámetro de encabezado de autenticación que estará en el encabezado de la solicitud.",

            ["Plugins.Synchronizers.SAPProducts.Fields.SyncManufacturers"] = "Sincronizar fabricantes",
            ["Plugins.Synchronizers.SAPProducts.Fields.SyncManufacturers.Hint"] = "Marque si el plugin debe sincronizar a los fabricantes.",

            ["Plugins.Synchronizers.SAPProducts.Fields.SyncCategories"] = "Sincronizar categorías",
            ["Plugins.Synchronizers.SAPProducts.Fields.SyncCategories.Hint"] = "Marque si el plugin debe sincronizar las categorías.",

            ["Plugins.Synchronizers.SAPProducts.Fields.SapCategoryUrl"] = "URL de Categorías de SAP",
            ["Plugins.Synchronizers.SAPProducts.Fields.SapCategoryUrl.Hint"] = "URL principal utilizada para soliciar las categorías que se encuentran en el ambiente de SAP.",

            ["Plugins.Synchronizers.SAPProducts.Fields.SapManufacturerUrl"] = "URL de Fabricantes de SAP",
            ["Plugins.Synchronizers.SAPProducts.Fields.SapManufacturerUrl.Hint"] = "URL principal utilizada para soliciar los fabricantes que se encuentran en el ambiente de SAP.",

            ["Plugins.Synchronizers.SAPProducts.Fields.SapCategoryUrl.Required"] = "El campo SAP Category URL es requerido.",
            ["Plugins.Synchronizers.SAPProducts.Fields.SapManufacturerUrl.Required"] = "El campo SAP Manufacturer URL es requerido.",

            ["Plugins.Synchronizers.SAPProducts.Exceptions.PluginNotConfigured"] = "El plugin no está configurado correctamente.",
            ["Plugins.Synchronizers.SAPProducts.Exceptions.ProductsSync"] = "Error sincronizando los productos.",
            ["Plugins.Synchronizers.SAPProducts.Exceptions.GettingProductsFromSap"] = "Ha ocurrido un error obteniendo los productos desde el ambiente de SAP.",
            ["Plugins.Synchronizers.SAPProducts.Exceptions.GettingCategoriesFromSap"] = "Ha ocurrido un error obteniendo las categorías desde el ambiente de SAP.",
            ["Plugins.Synchronizers.SAPProducts.Exceptions.GettingManufacturersFromSap"] = "Ha ocurrido un error obteniendo los fabricantes desde el ambiente de SAP.",
            ["Plugins.Synchronizers.SAPProducts.Exceptions.CategoriesSync"] = "Error sincronizando las categorías.",
            ["Plugins.Synchronizers.SAPProducts.Exceptions.ManufacturersSync"] = "Error sincronizando los fabricantes.",

            ["Plugins.Synchronizers.SAPProducts.Fields.AssignItemCodeFieldAsSku"] = "Asignar campo ItemCode como SKU",
            ["Plugins.Synchronizers.SAPProducts.Fields.AssignItemCodeFieldAsSku.Hint"] = "Indica si el plugin debe asignar el campo ItemCode como SKU al mapear los productos desde SAP.",

            ["Plugins.Synchronizers.SAPProducts.Fields.UpdateProductInformation"] = "Actualizar la información del producto",
            ["Plugins.Synchronizers.SAPProducts.Fields.AssignItemCodeFieldAsSku.Hint"] = "Indica si el plugin debe actualizar la información de los productos que ya están sincronizados."
        };
    }
}
