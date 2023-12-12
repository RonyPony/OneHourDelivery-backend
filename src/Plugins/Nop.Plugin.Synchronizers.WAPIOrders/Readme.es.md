# WAPI Orders Synchronizer

Este plugin se desarrolló para sincronizar los pedidos de NopCommerce con WAPI eCommerce.

## Características:

Cuando se realiza el pago de un pedido en nopCommerce, este complemento es responsable de enviar los datos del pedido al ambiente de WAPI eCommerce.

### Cómo instalar:

Existen dos formas de instalar este plugin:

1. Incluyendo el código fuente en su proyecto NopCommerce:
	- Descargue el código fuente del plugin (esta biblioteca de clases).
	- Agréguelo a su proyecto NopCommerce, en la carpeta `Plugins` (asegúrese de que **NO ES** la carpeta `Plugins` que está debajo de `Presentation / Nop.Web`).
	- Compile la biblioteca de clases que acaba de agregar a su proyecto.
	- Ejecute NopCommerce.
	- Diríjase al área de administración (deberá iniciar sesión como administrador).
	- Diríjase a `Configuración > Plugins locales`.
	- Busque el plugin llamado "WAPI Orders Synchronizer".
	- Haga clic en "Instalar".

2. Sin incluir el código fuente:
	- Descarga el código fuente del complemento (esta biblioteca de clases).
	- Agréguelo a su proyecto NopCommerce, en la carpeta `Plugins` (asegúrese de que **NO ES** la carpeta` Plugins` que está debajo de `Presentation / Nop.Web`).
	- Compile la biblioteca de clases que acaba de agregar a su proyecto.
	- Vaya a `Presentation / Nop.Web / Plugins` y busque una carpeta llamada`Synchronizers.WAPIOrders`.
	- Comprima la carpeta.
	- Ahora puede instalarlo en cualquier instancia de NopCommerce (versión 4.30), para instalarlo, simplemente:
		- Diríjase al área de administración (deberá iniciar sesión como administrador).
		- Diríjase a `Configuración > Plugins locales`.
		- Haga clic en "Cargar plugin o tema".
		- Cargue el archivo zip que contiene el código compilado del plugin.
		- Reinicie NopCommerce.
		- Busque el plugin llamado "WAPI Orders Synchronizer".
		- Haga clic en "Instalar".

### Cómo configurar:

1. Diríjase a `Configuración > Plugins locales`.
2. Busque el sincronizador llamado "WAPI Orders Synchronizer".
3. Haga clic en "Configurar".
4. Complete los siguientes campos:
   1. **Nombre de la llave de autorización**: Nombre de la llave secreta utilizada para la autenticación de solicitudes en el ambiente WAPI.
   2. **Valor de la llave de autorización**: Valor de la llave secreta utilizada para la autenticación de solicitudes en el ambiente WAPI.
   3. **URL de registro de orden**: URL que se utilizará para registrar una orden en el ambiente WAPI.
   4. **Código de tienda por defecto**: Código de tienda que se utilizará por defecto cuando el punto de recogida no sea definido.
5. Haga clic en "Guardar".

### Notas:

1. Para los impuestos de los artículos de un pedido, este plugin depende del proveedor de impuestos con el nombre de sistema `Tax.FixedOrByCountryStateZip`. Para que los impuestos se envíen correctamente, debe configurar sus categorías de impuestos a través del proveedor de impuestos mencionado anteriormente y configurar cada producto con su categoría de impuestos correspondiente.
2. Este complemento también depende del método de pago con el nombre de sistema `Payments.CyberSource`.
3. Si su tienda tiene **recoger en tienda** habilitado como método de envío, para que este plugin funcione correctamente con esta opción, deberá escribir su código personalizado de punto de recogida (en caso de tener uno para cada punto de recogida) en el campo de descripción del punto de recogida . Para esto, necesitará el plugin con nombre de sistema `Pickup.PickupInStore`