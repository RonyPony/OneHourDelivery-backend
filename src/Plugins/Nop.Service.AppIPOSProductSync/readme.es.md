# AppIPOS Sync

> Lea este documento completo antes de realizar cualquier acción con este servicio.

Este servicio fue desarrollado para sincronizar categorías, fabricantes, productos, clientes y pedidos de NopCommerce (versión 4.30) con el ERP AppIPOS.

## Caracteristicas:

Este servicio sincroniza categorías, fabricantes, productos, clientes y pedidos en un intervalo de tiempo (configurado en appsettings.json).
Junto al intervalo de tiempo, se pueden configurar los módulos que se sincronizarán.

### Cómo configurar:

En el archivo appsettings.json puede configurar lo siguiente:

- Sección "ConnectionStrings"
    - <b> DefaultConnection: </b> cadena de conexión de la base de datos erp.

- Sección "Settings"
    - <b> ServiceTimerInterval: </b> intervalo (en minutos) en el que se ejecutará el servicio (30 por defecto).
    - <b> NopCommerceApiURL: </b> URL base del sitio NopCommerce con el que se comunicará el servicio.
    - <b> DefaultWarehouseId: </b> ID de almacén (del Erp) que se tendrá en cuenta al sincronizar productos, la cantidad de stock del producto se tomará de ese ID de almacén.
    - <b> SyncProducts: </b> configúrelo como True si desea sincronizar productos, junto con sus fabricantes y categorías.
    - <b> SyncOrders:</b> configúrelo como True si desea sincronizar pedidos, junto con los productos y los clientes. 

- Sección "ApiAuthCredentials"
    - <b> Username: </b> nombre de usuario de NopCommerce que se utilizará para autenticarse en Nop.Api para realizar solicitudes.
    - <b> Contraseña: </b> Contraseña de usuario de NopCommerce que se utilizará para autenticarse en Nop.Api para realizar solicitudes.

### Cómo instalar:

Para instalar este servicio, consulte [esta guía] (https://docs.microsoft.com/en-us/dotnet/framework/windows-services/how-to-install-and-uninstall-services).