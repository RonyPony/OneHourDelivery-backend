# Google Maps Integration Widget

This plugin was developed in order to integrate NopCommerce (version 4.90.4) with Google Maps Platform, allowing NopCommerce addresses to manage geo coordinates and allowing the user to access some of the Google Maps most ussefull services.

## Features:

By default, this plugin allows the user to enter latitude and longitude on NopCommerse's address forms, but also it allows the use Google Maps functionalities such as address search, address geocoding and getting an address by placing a marker on a map.
This plugin function as a widget, enabling some fields and a map on pages such as customer address management, order checkout (Checkout by steps and One Page Checkout) and for the admin user on warehouses management.

### How to install:

There's two ways to install this plugin:

1. Including source code to your NopCommerce proyect:
	- Download the plugin source code (this class library).
	- Add it to your NopCommerce proyect, under your `Plugins` folder (make sure it **IS NOT** the `Plugins` folder that's under `Presentation/Nop.Web`).
	- Compile the class library you just added to your project.
	- Run NopCommerce.
	- Go to the admin area (you will need to log in as administrator).
	- Go to `Configuration > Local plugins`.
	- Search for the plugin called "Google Maps Integration".
	- Click "Install".

2. Not including source code:
	- Download the plugin source code (this class library).
	- Add it to your NopCommerce proyect, under your `Plugins` folder (make sure it **IS NOT** the `Plugins` folder that's under `Presentation/Nop.Web`).
	- Compile the class library you just added to your project.
	- Go to `Presentation/Nop.Web/Plugins` and locate a folder named `Widgets.GoogleMapsIntegration`.
	- Zip the folder.
	- Now you can install it in any instance of NopCommerce (version 4.90.4), to install it, just:
		- Go to the admin area (you will need to log in as administrator).
		- Go to `Configuration > Local plugins`.
		- Click "Upload plugin or theme".
		- Upload the zip file containing the plugin's compiled code.
		- Restart NopCommerce.
		- Search for the plugin called "Google Maps Integration".
		- Click "Install".

### How to configure:

1. Go to `Configuration > Widgets`.
2. Search for the payment method called "Google Maps Integration".
3. Click "Configure".
4. Follow the configuration page instructions to get a Google API Key. Or you can visit: [Google Maps Platform. Get started][1]
5. Once you're done setting your account, this plugin requires that your API Key has the permissions for accessing Places API, Maps JavaScript API and Geocoding API.
6. Fill the **Google API Key** input with your API Key.

Once you've completed this steps, you can select which of the functionalities of the plugin you want to enable in your store.
Notice that once the plugin is installed, the fields of latitude and longitude become required on NopCommerce's address forms.

[1]: https://cloud.google.com/maps-platform/#get-started

**Notes:**
- This plugin requires that the culture of the language in which the store is displayed uses the period (.) For decimal numbers.