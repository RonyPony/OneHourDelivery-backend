# Complemento ProductAvailability

Este plugin se desarrolló para integrar NopCommerce (versión 4.30) con un servicio de disponibilidad de productos para DDP.

## Caracteristicas:

Este plugin muestra una tabla que contiene los datos de la disponibilidad por tienda de un producto en la página de detalles del producto y también agrega una página de tema para la búsqueda de inventario en la que un cliente podría ingresar el SKU de un producto y la disponibilidad por tienda se cargará incluso si el producto no está registrado en la tienda.

### Cómo instalar:

Hay dos formas de instalar este complemento:

1. Incluyendo el código fuente de su proyecto NopCommerce:
- Descargue el código fuente del complemento (esta biblioteca de clases).
- Agréguelo a su proyecto NopCommerce, bajo su carpeta `Plugins` (asegúrese de que ** NO ES ** la carpeta` Plugins` que está debajo de `Presentation / Nop.Web`).
- Compile la biblioteca de clases que acaba de agregar a su proyecto.
- Ejecute NopCommerce.
- Vaya al área de administración (deberá iniciar sesión como administrador).
- Vaya a `Configuración> Complementos locales`.
- Busque el complemento llamado "Disponibilidad del producto".
- Haga clic en "Instalar".

2. Sin incluir el código fuente:
- Descargue el código fuente del complemento (esta biblioteca de clases).
- Agréguelo a su proyecto NopCommerce, bajo su carpeta `Plugins` (asegúrese de que ** NO ES ** la carpeta` Plugins` que está debajo de `Presentation / Nop.Web`).
- Compile la biblioteca de clases que acaba de agregar a su proyecto.
- Vaya a `Presentation / Nop.Web / Plugins` y localice una carpeta llamada` Misc.ProductAvailability`.
- Comprima la carpeta.
- Ahora puede instalarlo en cualquier instancia de NopCommerce (versión 4.30), para instalarlo, simplemente:
- Vaya al área de administración (deberá iniciar sesión como administrador).
- Vaya a `Configuración> Complementos locales`.
- Haga clic en "Cargar complemento o tema".
- Cargue el archivo zip que contiene el código compilado del complemento.
- Reinicie NopCommerce.
- Busque el complemento llamado "Disponibilidad del producto".
- Haga clic en "Instalar".

### Cómo configurar:

1. Vaya a `Configuración> Complementos locales`.
2. Busque el complemento llamado "Disponibilidad del producto".
3. Haga clic en "Configurar".
4. Siga las instrucciones de la página de configuración.
5. Una vez que haya terminado, haga clic en guardar y ya está.

Una vez que haya completado estos pasos, puede ir a la página de detalles del producto de cualquier producto que esté publicado en la tienda y se mostrarán los datos de disponibilidad. También encontrará una nueva opción en el pie de página de la página llamada ** Búsqueda de inventario ** donde puede acceder a la página de búsqueda mencionada anteriormente.

### Notas:

1. Cuando se instala este plugin, este agrega una nueva página de tema llamada `ProductAvailability.InventorySearch`, asegúrese de que la propiedad "Nombre de página descriptivo del motor de búsqueda" tenga el valor "inventory-search", sin esto no podrá acceder a la página de búsqueda de inventario ya que esto es lo que define la ruta url de ésta pantalla.