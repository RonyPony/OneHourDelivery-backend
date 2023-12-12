# WAPI Orders Synchronizer

This plugin was develop in order to sync NopCommerce orders with WAPI eCommerce.

## Features:

When an order is paid in nopCommerce, this plugin is responsible of sending the order data to WAPI environment.

### How to install:

There's two ways to install this plugin:

1. Including source code to your NopCommerce proyect:
	- Download the plugin source code (this class library).
	- Add it to your NopCommerce proyect, under your `Plugins` folder (make sure it **IS NOT** the `Plugins` folder that's under `Presentation/Nop.Web`).
	- Compile the class library you just added to your project.
	- Run NopCommerce.
	- Go to the admin area (you will need to log in as administrator).
	- Go to `Configuration > Local plugins`.
	- Search for the plugin called "WAPI Orders Synchronizer".
	- Click "Install".

2. Not including source code:
	- Download the plugin source code (this class library).
	- Add it to your NopCommerce proyect, under your `Plugins` folder (make sure it **IS NOT** the `Plugins` folder that's under `Presentation/Nop.Web`).
	- Compile the class library you just added to your project.
	- Go to `Presentation/Nop.Web/Plugins` and locate a folder named `Synchronizers.WAPIOrders`.
	- Zip the folder.
	- Now you can install it in any instance of NopCommerce (version 4.30), to install it, just:
		- Go to the admin area (you will need to log in as administrator).
		- Go to `Configuration > Local plugins`.
		- Click "Upload plugin or theme".
		- Upload the zip file containing the plugin's compiled code.
		- Restart NopCommerce.
		- Search for the plugin called "WAPI Orders Synchronizer".
		- Click "Install".

### How to configure:

1. Go to `Configuration > Local plugins`.
2. Search for the synchronizer called "WAPI Orders Synchronizer".
3. Click "Configure".
4. Fill the following fields:
   1. **Auth key name**: Name of the secret API Key used for WAPI request authentication.
   2. **Auth key value**: Value of the secret API Key used for WAPI request authentication.
   3. **Register sale URL**: URL that will be used to register an order to WAPI enviroment.
   4. **Default store pickup code**: The default store pickup code to be used when pickup point is not defined.
5. Click "Save".

### Notes:

1. For the taxes of an order items, this plugin depends on tax provider with system name `Tax.FixedOrByCountryStateZip`. For taxes to be sent correctly you must configure your tax categories through the tax provider mentioned earlier and set each product with it's corresponding tax category.
2. This plugin also depends on payment method with system name `Payments.CyberSource`.
3. If your store has **pickup in store** enabled as shipping method, for this plugin to works correctly, you'll need to write down your custom pickup point code (if has it for each pickup point) in the description field of the pickup point. For this, you'll required the plugin with system name `Pickup.PickupInStore`.