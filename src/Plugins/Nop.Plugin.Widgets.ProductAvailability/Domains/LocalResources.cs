using System.Collections.Generic;

namespace Nop.Plugin.Widgets.ProductAvailability.Domains
{
    /// <summary>
    /// Local Resources of Plugin
    /// </summary>
    public static class LocaleResources
    {
        /// <summary>
        /// English Resources.
        /// </summary>
        public static Dictionary<string, string> EnglishResources = new Dictionary<string, string>
        {
            #region Instructions

            [$"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Instructions"] = @"
                <p>Allows you to view the availability of the store's products
                <br />
                <br />
                For the plugin to work, 2 aspects must be taken into account:
                <br />
                <ul>
                    <li>The <Endpoint> field must have the following format: <b>http://domain/servicesddp/inventory.php?reference={0}&token={1}</b></li>
                    <li>Then enter the token</li>
                </ul>
                <br/>
                </p>",

            #endregion

            #region Table

            [$"{ProductAvailabilityDefault.LocaleResourcesPrefix}.sizes"] = "Sizes",
            [$"{ProductAvailabilityDefault.LocaleResourcesPrefix}.branch_offices"] = "Branch Offices",
            [$"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Table.Title"] = "Inventory per store",

            #endregion

            #region Topic Page

            [$"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Topic.InventorySearch.Title"] = "Search inventory",
            [$"{ProductAvailabilityDefault.LocaleResourcesPrefix}.SearchInventory.SearchBox.Placeholder"] = "Product SKU",
            [$"{ProductAvailabilityDefault.LocaleResourcesPrefix}.SearchInventory.SearchBox.Error.OnlyNumbersAllowed"] = "Only numbers allowed",

            #endregion

            #region Configuration

            [$"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Config.ProductInventoryUrl"] = "Product's inventory URL",
            [$"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Config.ProductInventoryUrl.Hint"] = "The url to request the inventory of a product.",
            [$"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Config.ProductInventoryUrl.Required"] = "The product's inventory URL is required",

            [$"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Config.StoresUrl"] = "Stores/Warehouses URL",
            [$"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Config.StoresUrl.Hint"] = "The url to request the stores/warehouses information.",
            [$"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Config.StoresUrl.Required"] = "The stores/warehouses URL is required",

            [$"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Config.UseSameTokenForAllRequest"] = "Use same token for all requests",
            [$"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Config.UseSameTokenForAllRequest.Hint"] = "Check to indicate that all the requests will use the same authorization token.",

            [$"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Config.ProductInventoryUrlToken"] = "Product's inventory URL token",
            [$"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Config.ProductInventoryUrlToken.Hint"] = "The authorization token used for product's inventory requests.",
            [$"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Config.ProductInventoryUrlToken.Required"] = "The product's inventory URL token is required",

            [$"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Config.StoresUrlToken"] = "Stores/Warehouses URL token",
            [$"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Config.StoresUrlToken.Hint"] = "The authorization token used for stores/warehouses requests.",
            [$"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Config.StoresUrlToken.Required"] = "The stores/warehouses URL token is required",

            [$"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Config.Token"] = "Token",
            [$"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Config.Token.Hint"] = "The authorization token used for all requests.",
            [$"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Config.Token.Required"] = "The token is required",

            [$"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Config.InventoryWarehouseId"] = "Inventory warehouse",
            [$"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Config.InventoryWarehouseId.Hint"] = "The warehouse from where the inventory will be requested. When none is selected, the inventory is taken from store number 33 by default.",

            [$"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Config.ProductAttributeId"] = "Product attribute to sync",
            [$"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Config.ProductAttributeId.Hint"] = "The product attribute that will be updated when syncing inventory.",
            [$"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Config.ProductAttributeId.Required"] = "The product attribute to sync is required",

            [$"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Config.InventoryRequestTries"] = "Request tries",
            [$"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Config.InventoryRequestTries.Hint"] = "The max attemps of inventory request's for each product when the request fails.",
            [$"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Config.InventoryRequestTries.Required"] = "The request tries is required",

            [$"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Config.SelectList.Default"] = "Select an option",

            [$"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Config.DisplayStoreAvailabilityOnProductDetailsPage"] = "Display availability per store in product details page.",
            [$"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Config.DisplayStoreAvailabilityOnProductDetailsPage.Hint"] = "Check to display a table on product details page with the product's availability per store.",

            [$"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Config.DeleteExistingProductAttributeValuesOnInventorySunc"] = "Delete existing product attribute values before updating products inventory.",
            [$"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Config.DeleteExistingProductAttributeValuesOnInventorySunc.Hint"] = "Check to indicate that the product attribute values must be deleted when syncing products inventory.",

            [$"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Config.OneSizeProductsIdentifierCode"] = "Identifier code for one size products",
            [$"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Config.OneSizeProductsIdentifierCode.Hint"] = "Indicate the code that identifies the one sized products.",

            #endregion

            #region Exceptions

            [$"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Exception.PluginNotConfigured"] = "Plugin is not configured correctly.",
            [$"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Exception.ErrorRequestingCellars"] = "Error requesting cellars/warehouses/pickup points information from web service.",
            [$"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Exception.ProductAttributeNotFound"] = "Configured product attribute for inventory sync not found.",
            [$"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Exception.InvalidSku"] = "The SKU is invalid.",
            [$"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Exception.ErrorRequestingProductInventory"] = "Some error occurred requesting product inventory.",
            [$"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Exception.RequestNotSuccessful"] = "The request response was an error.",
            [$"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Exception.ProductAttributeInfo"] = "Could not get product attribute information.",
            [$"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Exception.ProductAttributeValue"] = "Could not get product attribute value.",
            [$"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Exception.ProductNotAvailableInSelectedPickupPoint"] = "Product {0} is not available in the selected pickup point.",
            [$"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Exception.ProductQtyNotAvailableInSelectedPickupPoint"] = "Quantity for product {0} is not available in the selected pickup point. Available quantity is {1}",
            [$"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Exception.PickupPointInfo"] = "Could not get pickup point information.",
            [$"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Exception.CellarInfo"] = "Could not get cellar information.",

            #endregion
        };


        /// <summary>
        /// Spanish Resources
        /// </summary>
        public static Dictionary<string, string> SpanishResources = new Dictionary<string, string>
        {
            #region Instructions

            [$"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Instructions"] = @"
                <p>Permite ver la disponibilidad de los productos en la tienda
                <br />
                <br />
                Para que el plugin funcione se deben tomar en cuenta 2 aspectos:
                <br />
                <ul>
                    <li>El campo <Endpoint> debe tener el siguiente formato: <b>http://dominio/servicesddp/inventory.php?reference={0}&token={1}</b></li>
                    <li>Luego ingresar el token</li>
                </ul>
                <br/>
                </p>",

            #endregion

            #region Table

            [$"{ProductAvailabilityDefault.LocaleResourcesPrefix}.sizes"] = "Tallas",
            [$"{ProductAvailabilityDefault.LocaleResourcesPrefix}.branch_offices"] = "Sucursales",
            [$"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Table.Title"] = "Inventario por tienda",

            #endregion

            #region Topic Page

            [$"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Topic.InventorySearch.Title"] = "Búsqueda de inventario",
            [$"{ProductAvailabilityDefault.LocaleResourcesPrefix}.SearchInventory.SearchBox.Placeholder"] = "SKU del producto",
            [$"{ProductAvailabilityDefault.LocaleResourcesPrefix}.SearchInventory.SearchBox.Error.OnlyNumbersAllowed"] = "Sólo se permiten números",

            #endregion

            #region Configuration

            [$"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Config.ProductInventoryUrl"] = "URL inventario de productos",
            [$"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Config.ProductInventoryUrl.Hint"] = "La URL donde se solicitarán los datos del inventario de un producto.",
            [$"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Config.ProductInventoryUrl.Required"] = "La URL inventario de productos es requerida",

            [$"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Config.StoresUrl"] = "URL tiendas/bodegas",
            [$"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Config.StoresUrl.Hint"] = "La URL donde se solicitaránlos datos de las tiendas/bodegas.",
            [$"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Config.StoresUrl.Required"] = "La URL tiendas/bodegas es requerida",

            [$"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Config.UseSameTokenForAllRequest"] = "Usar mismo token",
            [$"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Config.UseSameTokenForAllRequest.Hint"] = "Marque para indicar que todas las URLs utilizarán el mismo token de autorización para las solicitudes.",

            [$"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Config.ProductInventoryUrlToken"] = "Token para URL inventario de productos",
            [$"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Config.ProductInventoryUrlToken.Hint"] = "Token de autorización que se utilizará en las solicitudes de inventario de un producto.",
            [$"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Config.ProductInventoryUrlToken.Required"] = "El token para URL inventario de productos es requerido",

            [$"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Config.StoresUrlToken"] = "Token para URL tiendas/bodegas",
            [$"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Config.StoresUrlToken.Hint"] = "Token de autorización que se utilizará en las solicitudes de datos de las tiendas/bodegas.",
            [$"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Config.StoresUrlToken.Required"] = "El token para URL tiendas/bodegas es requerido",

            [$"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Config.Token"] = "Token",
            [$"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Config.Token.Hint"] = "Token de autorización que se utilizará en todas las solicitudes.",
            [$"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Config.Token.Required"] = "El token es requerido",

            [$"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Config.InventoryWarehouseId"] = "Almacén del inventario",
            [$"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Config.InventoryWarehouseId.Hint"] = "El almacen/bodega de donde se sacará el inventario de los productos. Si ninguno es seleccionado se tomará el almacén de la tienda número 33.0",

            [$"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Config.ProductAttributeId"] = "Atributo de producto a sincronizar",
            [$"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Config.ProductAttributeId.Hint"] = "El atributo de producto que se actualizará durante la sincronización de inventario.",
            [$"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Config.ProductAttributeId.Required"] = "El atributo de producto a sincronizar es requerido",

            [$"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Config.InventoryRequestTries"] = "Intentos de solicitudes",
            [$"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Config.InventoryRequestTries.Hint"] = "La cantidad de intentos para las solicitudes de inventario de cada producto cuando la solicitud es fallida.",
            [$"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Config.InventoryRequestTries.Required"] = "Los intentos de solicitudes es requerido",

            [$"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Config.SelectList.Default"] = "Seleccione una opción",

            [$"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Config.DisplayStoreAvailabilityOnProductDetailsPage"] = "Mostrar disponibilidad por tienda en la página de detalles del producto.",
            [$"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Config.DisplayStoreAvailabilityOnProductDetailsPage.Hint"] = "Marque para mostrar una tabla en la página de detalles del producto con la disponibilidad del producto por tienda.",

            [$"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Config.DeleteExistingProductAttributeValuesOnInventorySunc"] = "Borrar los valores de atributo existentes al sincronizar el inventario",
            [$"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Config.DeleteExistingProductAttributeValuesOnInventorySunc.Hint"] = "Marque para indicar que los valores de atributo de los productos deben ser borrados antes de sincronizar el inventario.",

            [$"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Config.OneSizeProductsIdentifierCode"] = "Código de identificación para productos de talla única",
            [$"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Config.OneSizeProductsIdentifierCode.Hint"] = "Indique el código que identifica los productos de talla única.",

            #endregion

            #region Exceptions

            [$"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Exception.PluginNotConfigured"] = "El plugin no está configurado correctamente.",
            [$"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Exception.ErrorRequestingCellars"] = "Ha ocurrido un error solicitando la información de los almacenes/bodegas.",
            [$"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Exception.ProductAttributeNotFound"] = "El atributo de producto configurado para la sincronización de inventario no fue encontrado.",
            [$"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Exception.InvalidSku"] = "El SKU es inválido.",
            [$"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Exception.ErrorRequestingProductInventory"] = "Ha ocurrido un error solicitando el inventario del producto.",
            [$"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Exception.RequestNotSuccessful"] = "La solicitud no fue exitosa.",
            [$"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Exception.ProductAttributeInfo"] = "No se pudo cargar la información del atributo del producto.",
            [$"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Exception.ProductAttributeValue"] = "No se pudo cargar el valor del atributo del producto.",
            [$"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Exception.ProductNotAvailableInSelectedPickupPoint"] = "El producto {0} no está disponible en el punto de recogida seleccionado.",
            [$"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Exception.ProductQtyNotAvailableInSelectedPickupPoint"] = "La cantidad deseada para el producto {0} no está disponible en el punto de recogida seleccionado. La cantidad disponible es de {1}",
            [$"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Exception.PickupPointInfo"] = "No se pudo cargar la información del punto de recogida.",
            [$"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Exception.CellarInfo"] = "No se pudo cargar la información de la bodega.",

            #endregion
        };
    }
}
