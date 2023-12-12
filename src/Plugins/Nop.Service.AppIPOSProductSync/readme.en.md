# AppIPOS Sync

> Read this entire document before taking any action with this service.

This service was developed to synchronize NopCommerce (version 4.30) categories, manufacturers, products, clients and orders with AppIPOS ERP.

## Features:

This service syncs categories, manufacturers, products, clients and orders on an interval (configured in appsettings.json).
Alongside the interval, the modules that will be synchronized can be configured.

### How to configure:

In the appsettings.json file you can configure the following:

- ConnectionStrings section
    - <b>DefaultConnection:</b> erp database connection string.

- Settings section
    - <b>ServiceTimerInterval:</b> interval (in minutes) in which the service will run (30 by default).
    - <b>NopCommerceApiURL:</b> base url of the NopCommerce site that the service will communicate with.
    - <b>DefaultWarehouseId:</b> warehouse Id (from the Erp) that will be taken into account when synchronizing products, product stock quantity will be taken from that warehouse Id.
    - <b>SyncProducts:</b> set to True if you want to sync products, alongside with their manufacturers and categories.
    - <b>SyncOrders:</b> set to True if you want to sync orders, alongside with the products and customers.

- ApiAuthCredentials section
    - <b>Username:</b> NopCommerce user name that will be used to authenticate to Nop.Api in order to make requests.
    - <b>Password:</b> NopCommerce user password that will be used to authenticate to Nop.Api in order to make requests.

### How to install:

In order to install this service, refer to [this guide](https://docs.microsoft.com/en-us/dotnet/framework/windows-services/how-to-install-and-uninstall-services).