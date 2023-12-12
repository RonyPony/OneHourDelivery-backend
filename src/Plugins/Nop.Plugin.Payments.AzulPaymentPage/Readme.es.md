# Plugin de pago de Azul

Este plugin fue desarrollado para integrar NopCommerce (versión 4.30) con el método de pago Azul del Banco Popular Dominicano.

## Características:

Este plugin redirecciona al usuario a la página de pago de Azul antes de completar la orden (si este se eligió como método de pago durante el proceso de checkout).
Adicional a esto, guarda logs de las respuestas devueltas por Azul en la base de datos (en una tabla creada por el plugin) junto con información de la transacción, e información de errores (si hubo alguno).

### Cómo instalar:

Existen dos formas de instalar este plugin:

1. Incluyendo el código fuente en su proyecto NopCommerce:
	- Descargue el código fuente del plugin (esta biblioteca de clases).
	- Agréguelo a su proyecto NopCommerce, en la carpeta `Plugins` (asegúrese de que **NO ES** la carpeta `Plugins` que está debajo de `Presentation / Nop.Web`).
	- Compile la biblioteca de clases que acaba de agregar a su proyecto.
	- Ejecute NopCommerce.
	- Diríjase al área de administración (deberá iniciar sesión como administrador).
	- Diríjase a `Configuración > Plugins locales`.
	- Busque el plugin llamado "AZUL Payment Page".
	- Haga clic en "Instalar".

2. Sin incluir el código fuente:
	- Descarga el código fuente del complemento (esta biblioteca de clases).
	- Agréguelo a su proyecto NopCommerce, en la carpeta `Plugins` (asegúrese de que **NO ES** la carpeta` Plugins` que está debajo de `Presentation / Nop.Web`).
	- Compile la biblioteca de clases que acaba de agregar a su proyecto.
	- Vaya a `Presentation / Nop.Web / Plugins` y busque una carpeta llamada` Payments.AzulPaymentPage`.
	- Comprima la carpeta.
	- Ahora puede instalarlo en cualquier instancia de NopCommerce (versión 4.30), para instalarlo, simplemente:
		- Diríjase al área de administración (deberá iniciar sesión como administrador).
		- Diríjase a `Configuración > Plugins locales`.
		- Haga clic en "Cargar plugin o tema".
		- Cargue el archivo zip que contiene el código compilado del plugin.
		- Reinicie NopCommerce.
		- Busque el plugin llamado "AZUL Payment Page".
		- Haga clic en "Instalar".

### Cómo configurar:

1. Diríjase a `Configuración > Métodos de pago`.
2. Busque el método de pago llamado "AZUL Payment Page".
3. Haga clic en "Configurar".

Si utiliza este método de pago, asegúrese de que AZUL le haya proporcionado toda la información requerida en la página de configuración del plugin.
Una vez que tenga toda la información requerida, podrá:

1. Complete toda la información requerida con la información provista por AZUL.
2. Una vez que haya completado la información requerida, haga clic en Guardar.