# Google Maps Integration Widget

Este complemento fue desarrollado con el fin de integrar NopCommerce (versión 4.90.4) con Google Maps Platform, permitiendo que las direcciones de NopCommerce administren coordenadas geográficas y permitiendo al usuario acceder a algunos de los servicios más útiles de Google Maps.

## Características:

De forma predeterminada, este complemento permite al usuario ingresar latitud y longitud en los formularios de dirección de NopCommerse, pero también permite el uso de funcionalidades de Google Maps como búsqueda de direcciones, codificación geográfica de direcciones y obtener una dirección colocando un marcador en un mapa.
Este complemento funciona como un widget, habilitando algunos campos y un mapa en páginas como administración de direcciones de clientes, pago de pedidos (pago por pasos y pago de una página) y para el usuario administrador en la gestión de almacenes.

### Cómo instalar:

Existen dos formas de instalar este plugin:

1. Incluyendo el código fuente en su proyecto NopCommerce:
	- Descargue el código fuente del plugin (esta biblioteca de clases).
	- Agréguelo a su proyecto NopCommerce, en la carpeta `Plugins` (asegúrese de que **NO ES** la carpeta `Plugins` que está debajo de `Presentation / Nop.Web`).
	- Compile la biblioteca de clases que acaba de agregar a su proyecto.
	- Ejecute NopCommerce.
	- Diríjase al área de administración (deberá iniciar sesión como administrador).
	- Diríjase a `Configuración > Plugins locales`.
	- Busque el plugin llamado "Google Maps Integration".
	- Haga clic en "Instalar".

2. Sin incluir el código fuente:
	- Descarga el código fuente del complemento (esta biblioteca de clases).
	- Agréguelo a su proyecto NopCommerce, en la carpeta `Plugins` (asegúrese de que **NO ES** la carpeta` Plugins` que está debajo de `Presentation / Nop.Web`).
	- Compile la biblioteca de clases que acaba de agregar a su proyecto.
	- Vaya a `Presentation / Nop.Web / Plugins` y busque una carpeta llamada`Widgets.GoogleMapsIntegration`.
	- Comprima la carpeta.
	- Ahora puede instalarlo en cualquier instancia de NopCommerce (versión 4.90.4), para instalarlo, simplemente:
		- Diríjase al área de administración (deberá iniciar sesión como administrador).
		- Diríjase a `Configuración > Plugins locales`.
		- Haga clic en "Cargar plugin o tema".
		- Cargue el archivo zip que contiene el código compilado del plugin.
		- Reinicie NopCommerce.
		- Busque el plugin llamado "Google Maps Integration".
		- Haga clic en "Instalar".

### Cómo configurar:

1. Diríjase a `Configuración > Widgets`.
2. Busque el método de pago llamado "Google Maps Integration".
3. Haga clic en "Configurar".
4. Siga las instrucciones de la página de configuración para obtener una clave API de Google. O puede visitar: [Google Maps Platform. Get started] [1]
5. Una vez que haya terminado de configurar su cuenta, este complemento requiere que su clave API tenga los permisos para acceder a Places API, Maps JavaScript API and Geocoding API.
6. Complete el campo **API Key** con su clave API.

Una vez que haya completado estos pasos, puede seleccionar cuál de las funcionalidades del complemento desea habilitar en su tienda.
Tenga en cuenta que una vez que se instala el complemento, los campos de latitud y longitud se vuelven obligatorios en los formularios de dirección de NopCommerce.

[1]: https://cloud.google.com/maps-platform/#get-started

**Notas:**
- Este plugin requiere que la cultura del idioma en el que se muestra la tienda utilice el punto (.) para los números decimales.