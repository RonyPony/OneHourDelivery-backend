# ProductAvailability Plugin

This plugin was developed to integrate NopCommerce (version 4.30) with a product availability service for DDP.

## Features:

This plugin displays a table containing the data of the availability per store of a product on the product details page and also adds a topic page for inventory search in which a customer could enter the SKU of a product and the availability per store will be loaded even if the product isn't registered in the store.

### How to install:

There's two ways to install this plugin:

1. Including source code to your NopCommerce proyect:
	- Download the plugin source code (this class library).
	- Add it to your NopCommerce proyect, under your `Plugins` folder (make sure it **IS NOT** the `Plugins` folder that's under `Presentation/Nop.Web`).
	- Compile the class library you just added to your project.
	- Run NopCommerce.
	- Go to the admin area (you will need to log in as administrator).
	- Go to `Configuration > Local plugins`.
	- Search for the plugin called "Product Availability".
	- Click "Install".

2. Not including source code:
	- Download the plugin source code (this class library).
	- Add it to your NopCommerce proyect, under your `Plugins` folder (make sure it **IS NOT** the `Plugins` folder that's under `Presentation/Nop.Web`).
	- Compile the class library you just added to your project.
	- Go to `Presentation/Nop.Web/Plugins` and locate a folder named `Misc.ProductAvailability`.
	- Zip the folder.
	- Now you can install it in any instance of NopCommerce (version 4.30), to install it, just:
		- Go to the admin area (you will need to log in as administrator).
		- Go to `Configuration > Local plugins`.
		- Click "Upload plugin or theme".
		- Upload the zip file containing the plugin's compiled code.
		- Restart NopCommerce.
		- Search for the plugin called "Product Availability".
		- Click "Install".

### How to configure:

1. Go to `Settings> Local Plugins`.
2. Look for the Plugin called "Product Availability".
3. Click "Configure".
4. Follow the instructions on the configuration page.
5. Once you have finished click on save and you are done

Once you have completed these steps, you can go to the product details page of any product that is published in the store and the availability data will be displayed. Also you will find a new option in the footer of the page called **Inventory search** where you can access the search page mentioned earlier.

### Notes:

1. When this plugin is installed, it adds a new topic page called `ProductAvailability.InventorySearch`, make sure that the property "Search engine friendly page name" has the value "inventory-search" in it, without this you won't be able to access the inventory search page.