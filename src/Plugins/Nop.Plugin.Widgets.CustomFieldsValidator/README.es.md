# Documentación: Custom Fields Validator Plugin

> Lea este documento completo antes de realizar cualquier acción con este plugin.

Este plugin fue desarrollado en NopCommerce (versión 4.30) para integrar validaciones a los campos de registro de usuario NIT y NCR.

## Características:

Este plugin sobreescribe el controlador de Customer para añadir las validaciones correspondientes a los campos NIT y NCR utilizando los atributos personalizados del cliente dentro de las configuraciones de clientes.
Valida que el campo NIT sea numérico y de longitud 14 y valida que el campo NCR sea numérico y de longitud 7.
Dichos campos son manejados como requeridos por el plugin automáticamente sin ninguna configuración adicional e incluye los recursos de cadena correspondientes para cada validación.

### Campos

Para el correcto funcionamiento del plugin se requiere que dentro de la configuración del cliente (configuración/configuración/configuración del cliente) se añadan como atributos personalizados del cliente 
los campos NIT y NCR, siendo su tipo de control: caja de texto. (No es necesaria ninguna configuración extra).
 

#### Cómo instalar:

Existen dos formas de instalar este plugin:

1. Incluyendo el código fuente de su proyecto NopCommerce:
	- Descargue el código fuente del plugin (esta librería de clases).
	- Agréguelo a su proyecto NopCommerce, bajo su carpeta `Plugins` (asegúrese de que **NO ES** la carpeta `Plugins` que está debajo de `Presentation\Nop.Web`).
	- Compile la librería de clases que acaba de agregar a su proyecto.
	- Ejecute NopCommerce.
	- Vaya al área de administración (deberá iniciar sesión como administrador).
	- Vaya a `Configuración> plugins locales`.
	- Busque el plugin llamado "Custom Fields Validator".
	- Haga clic en "Instalar".

2. Sin incluir el código fuente:
	- Descargue el código fuente del plugin (esta librería de clases).
	- Agréguelo a su proyecto NopCommerce, bajo su carpeta `Plugins` (asegúrese de que **NO ES** la carpeta `Plugins` que está debajo de `Presentation\Nop.Web`).
	- Compile la librería de clases que acaba de agregar a su proyecto.
	- Vaya a `Presentation\Nop.Web\Plugins` y busque una carpeta llamada `Widgets.CustomFieldsValidator`.
	- Comprima la carpeta.
	- Ahora puede instalarlo en cualquier instancia de NopCommerce (versión 4.30), para instalarlo, simplemente:
		- Vaya al área de administración (deberá iniciar sesión como administrador).
		- Vaya a `Configuración> plugins locales`.
		- Haga clic en "Cargar plugin o tema".
		- Cargue el archivo zip que contiene el código compilado del plugin.
		- Reinicie NopCommerce.
		- Busque el plugin llamado "Custom Fields Validator".
		- Haga clic en "Instalar".

##### Cómo activar:

1. Vaya a `Configuración > Widgets`.
2. Busque el método de pago denominado "Custom Fields Validator".
3. Haga clic en "Editar".
4. Selecciona la casilla "Está activo".
5. Haga clic en "Actualización".