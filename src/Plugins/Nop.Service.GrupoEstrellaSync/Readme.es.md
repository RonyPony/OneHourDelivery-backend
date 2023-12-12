# Servicio de Windows Syncronizacion de base de datos de Grupo Estrella

Este Servicio de Windows fue desarrollado para sincronizar los datos de la base de datos de grupo estrella con  NopCommerce (versión 4.30).

## Características:

Este Servicio de Windows sincroniza las categorias y los productos en la base de datos del ERP de grupo estrella con las base de datos de NopCommerce (versión 4.30) de su sito web.
Tambien guarda registros en el archivo de log y en el visor de eventos de windows bajo la carpeta de 'Aplication' con el nombre de fuente de 'Nop.Service.GrupoEstrellaProductSync' .

### Cómo instalar:

Mediante la instalacion del ejecutable de windows service via lineas de comando:
- InstallUtil.exe [Ruta de la carpeta de instalacion]Nop.Service.GrupoEstrellaProductSync.exe

    O
- cd [Ruta de la carpeta de instalacion]Nop.Service.GrupoEstrellaProductSync.exe install

### Cómo configurar:

En el archivo appsettings.json existen los siguientes parametros de configuracion

Bajo el tag de <b>'ConnectionStrings'</b>
1. Parametro <b>'StardentDatabase'</b> sirve para el Connection Strings de la base de datos de grupo estrella

Bajo el tag <b> 'OrderSyncSettings' </b>, los parámetros son valores predeterminados que se envían al procedimiento almacenado de guardar ordenes, los nombres de los parámetros 
en el archivo de configuración son los mismos nombres que tienen esos parámetros en el procedimiento almacenado.

Bajo el tag de <b>'Settings'</b>
1. Parametro <b>'ServiceTimerInterval'</b> sirve para el intervalo de tiempo en minutos
2. Parametro <b>'LogFolderName'</b> sirve para el nombre de la carpeta del archivo de log
3. Parametro <b>'LogFileName'</b> sirve para el nombre del archivo de log
4. Parametro <b>'SyncProducts'</b> sirve para sincronizar Productos
5. Parametro <b>'SyncClients'</b> sirve para sincronizar Clientes

Bajo el tag de <b>"ApiAuthCredentials"</b>
1. Parametro <b>'Username'</b> Nombre de usuario de NopCommerce que se utilizará para autenticarse en Nop.Api para realizar solicitudes.
2. Parametro <b>'Password'</b> Contraseña de usuario de NopCommerce que se utilizará para autenticarse en Nop.Api para realizar solicitudes.

