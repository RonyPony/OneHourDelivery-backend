# Windows Service Synchronization of grupo estrella database

This Windows Service was developed to synchronize the data of the grupo estrella database with NopCommerce (version 4.30).

## Characteristics:

This Windows Service synchronizes the categories and products in the grupo estrella ERP database with the NopCommerce databases (version 4.30) of its website.
It also saves logs in the log file and in the windows event viewer under the 'Aplication' folder with the source name of 'Nop.Service.GrupoEstrellaProductSync'.

### How to install:

By installing the windows service executable via command lines:
- InstallUtil.exe [Path of the installation folder] Nop.Service.GrupoEstrellaProductSync.exe

    OR
- cd [Path of the installation folder] Nop.Service.GrupoEstrellaProductSync.exe install

### How to configure:

In the appsettings.json file there are the following configuration parameters

Under the <b>'ConnectionStrings'</b>  tag
1. Parameter <b>'StardentDatabase'</b>  is used for the Connection Strings of the grupo estrella database

Under the <b>'OrderSyncSettings'</b>  tag the parameters are default values sent to the stored procedure, the parameter names in the configuration file
are the same names those parameters have in the stored procedure.

Under the <b>'Settings'</b>  tag
1. Parameter <b>'ServiceTimerInterval'</b>  is used for the time interval in minutes
2. Parameter <b>'LogFolderName'</b>  is used for the name of the log file folder
3. Parameter <b>'LogFileName'</b>  is used for the name of the log file
4. Parameter <b>'NopCommerceApiURL'</b>  is used to put the url of the NopCommerce Api Rest service
5. Parameter <b>'SyncProducts'</b>  is used for sync Products
6. Parameter <b>'SyncClients'</b>  is used for sync Clients

Under the <b>'ApiAuthCredentials'</b>  tag

1. Parameter <b>'Username'</b> NopCommerce user name that will be used to authenticate to Nop.Api in order to make requests.
1. Parameter <b>'Password'</b> NopCommerce user password that will be used to authenticate to Nop.Api in order to make requests.
