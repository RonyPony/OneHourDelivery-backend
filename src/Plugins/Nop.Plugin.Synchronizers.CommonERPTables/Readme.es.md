# Plugin CommonERPTables

> Lea este documento completo antes de realizar cualquier acción con este plugin.

Este plugin fue desarrollado para la creación de las tablas que se necesitan para poder integrar NopCommerce en su versión 4.30
con un determinado ERP.

## Características:

Al instalar este plugin se crean las tablas que guardaran la relación (mapping) entre cualquier ERP y nopCommerce, también se expone
una ruta URL para que se pueda enviar de manera fácil la información que se quiere almacenar.

### Cómo instalar:

Existen dos formas de instalar este plugin:

1. Incluyendo el código fuente de su proyecto NopCommerce:
	- Descargue el código fuente del plugin (esta librería de clases).
	- Agréguelo a su proyecto NopCommerce, bajo su carpeta `Plugins` (asegúrese de que **NO ES** la carpeta `Plugins` que está debajo de `Presentation\Nop.Web`).
	- Compile la librería de clases que acaba de agregar a su proyecto.
	- Ejecute NopCommerce.
	- Vaya al área de administración (deberá iniciar sesión como administrador).
	- Vaya a `Configuración> plugins locales`.
	- Busque el plugin llamado "Common ERP Tables".
	- Haga clic en "Instalar".

2. Sin incluir el código fuente:
	- Descargue el código fuente del plugin (esta librería de clases).
	- Agréguelo a su proyecto NopCommerce, bajo su carpeta `Plugins` (asegúrese de que **NO ES** la carpeta `Plugins` que está debajo de `Presentation\Nop.Web`).
	- Compile la librería de clases que acaba de agregar a su proyecto.
	- Vaya a `Presentation\Nop.Web\Plugins` y busque una carpeta llamada `Synchronizers.CommonERPTables`.
	- Comprima la carpeta.
	- Ahora puede instalarlo en cualquier instancia de NopCommerce (versión 4.30), para instalarlo, simplemente:
		- Vaya al área de administración (deberá iniciar sesión como administrador).
		- Vaya a `Configuración> plugins locales`.
		- Haga clic en "Cargar plugin o tema".
		- Cargue el archivo zip que contiene el código compilado del plugin.
		- Reinicie NopCommerce.
		- Busque el plugin llamado "Common ERP Tables".
		- Haga clic en "Instalar".

### URLs expuestas para la recepción de informacion desde el ERP hacia NopCommerces
> A continuación se detalla la nomenclatura y parametro de cada una de las url que el plugin expone para guardar la información que se le hará mapping.

| Entidad        | URL                     | Verbo HTTP | Parametros							   | Ejemplo																   |
|----------------|-------------------------|------------|------------------------------------------|---------------------------------------------------------------------------|
| Clientes		 | [dominio]/erp/customers | POST		| CustomerId (int), ErpCustomerId (string) | https://localhost:44356/erp/customers?CustomerId=2249&ErpCustomerId=C_007 |
| Productos      | [dominio]/erp/products  | POST		| ProductId (int), ErpProductId (string)   | https://localhost:44356/erp/customers?ProductId=123&ErpProductId=P_123	   |
| Productos      | [dominio]/erp/products  | GET		| ProductId (int)						   | https://localhost:44356/erp/customers?ProductId=123					   |
| Ordenes        | [dominio]/erp/orders    | POST		| OrderId (int), ErpOrderId (string)	   | https://localhost:44356/erp/orders?OrderId=20&ErpOrderId=ERP_Order_0124   |
