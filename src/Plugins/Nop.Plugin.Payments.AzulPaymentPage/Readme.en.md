# Azul payment page plugin

This plugin was developed in order to integrate NopCommerce (version 4.30) with Azul payment method from Banco Popular Dominicano.

## Features:

This plugin redirects the user to Azul payment page before completing an order (if chosen as payment method during the checkout process).
Also, it logs responses received by Azul to the database (in a table created by the plugin) along with transaction information, and error information (if any).

### How to install:

There's two ways to install this plugin:

1. Including source code to your NopCommerce proyect:
	- Download the plugin source code (this class library).
	- Add it to your NopCommerce proyect, under your `Plugins` folder (make sure it **IS NOT** the `Plugins` folder that's under `Presentation/Nop.Web`).
	- Compile the class library you just added to your project.
	- Run NopCommerce.
	- Go to the admin area (you will need to log in as administrator).
	- Go to `Configuration > Local plugins`.
	- Search for the plugin called "AZUL Payment Page".
	- Click "Install".

2. Not including source code:
	- Download the plugin source code (this class library).
	- Add it to your NopCommerce proyect, under your `Plugins` folder (make sure it **IS NOT** the `Plugins` folder that's under `Presentation/Nop.Web`).
	- Compile the class library you just added to your project.
	- Go to `Presentation/Nop.Web/Plugins` and locate a folder named `Payments.AzulPaymentPage`.
	- Zip the folder.
	- Now you can install it in any instance of NopCommerce (version 4.30), to install it, just:
		- Go to the admin area (you will need to log in as administrator).
		- Go to `Configuration > Local plugins`.
		- Click "Upload plugin or theme".
		- Upload the zip file containing the plugin's compiled code.
		- Restart NopCommerce.
		- Search for the plugin called "AZUL Payment Page".
		- Click "Install".

### How to configure:

1. Go to `Configuration > Payment methods`.
2. Search for the payment method called "AZUL Payment Page".
3. Click "Configure".

If you're using this payment method ensure that AZUL provided you with the information required in the plugin's configuration page.
Once you have all required information, you can:

1. Fill all the required information with the info provided by AZUL.
2. Once you completed the required info, click Save.