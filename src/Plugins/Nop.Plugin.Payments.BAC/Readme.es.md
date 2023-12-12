# Plugin de pago de BAC Creadomatic

> Lea este documento completo antes de realizar cualquier acción con este plugin.

Este plugin fue desarrollado para integrar NopCommerce (versión 4.30) con el método de pago del Banco BAC Creadomatic.

## Características:

Este plugin redirecciona al usuario a la página de pago de BAC antes de completar la orden (si este se eligió como método de pago durante el proceso de checkout).
Adicional a esto, guarda logs de las respuestas devueltas por BAC en la base de datos (en una tabla creada por el plugin) junto con información de la transacción, e información de errores (si hubo alguno).

### Cómo instalar:

Existen dos formas de instalar este plugin:

1. Incluyendo el código fuente de su proyecto NopCommerce:
	- Descargue el código fuente del plugin (esta librería de clases).
	- Agréguelo a su proyecto NopCommerce, bajo su carpeta `Plugins` (asegúrese de que **NO ES** la carpeta `Plugins` que está debajo de `Presentation\Nop.Web`).
	- Compile la librería de clases que acaba de agregar a su proyecto.
	- Ejecute NopCommerce.
	- Vaya al área de administración (deberá iniciar sesión como administrador).
	- Vaya a `Configuración> plugins locales`.
	- Busque el plugin llamado "BAC Credomatic payment page".
	- Haga clic en "Instalar".

2. Sin incluir el código fuente:
	- Descargue el código fuente del plugin (esta librería de clases).
	- Agréguelo a su proyecto NopCommerce, bajo su carpeta `Plugins` (asegúrese de que **NO ES** la carpeta `Plugins` que está debajo de `Presentation\Nop.Web`).
	- Compile la librería de clases que acaba de agregar a su proyecto.
	- Vaya a `Presentation\Nop.Web\Plugins` y busque una carpeta llamada `Payments.BAC`.
	- Comprima la carpeta.
	- Ahora puede instalarlo en cualquier instancia de NopCommerce (versión 4.30), para instalarlo, simplemente:
		- Vaya al área de administración (deberá iniciar sesión como administrador).
		- Vaya a `Configuración> plugins locales`.
		- Haga clic en "Cargar plugin o tema".
		- Cargue el archivo zip que contiene el código compilado del plugin.
		- Reinicie NopCommerce.
		- Busque el plugin llamado "BAC Credomatic payment page".
		- Haga clic en "Instalar".

### Cómo configurar:

1. Vaya a `Configuración > Métodos de pago`.
2. Busque el método de pago denominado "BAC Credomatic payment page".
3. Haga clic en "Configurar".

Si está utilizando este método de pago, asegúrese de que BAC Credomatic le haya proporcionado la información requerida en la página de configuración del plugin.
Una vez que tenga toda la información requerida, puede:

1. Completar toda la información requerida con la información proporcionada por BAC Credomatic.
2. Una vez que haya completado la información requerida, haga clic en Guardar.

### Información importante
> Para el uso correcto y la integración de este plugin con las API de BAC Credomatic, debe ejecutar su sitio web NopCommerce utilizando HTTPS.