# Documentation: Custom Fields Validator Plugin

> Read this entire document before performing any action with this plugin.

This plugin was developed in NopCommerce (version 4.30) to integrate validations to the NIT and NCR user registration fields.

## Features:

This plugin overwrites the Customer driver to add the corresponding validations to the NIT and NCR fields using the custom customer attributes within the customer settings.
It validates that the NIT field is numeric and length 14 and validates that the NCR field is numeric and length 7.
These fields are handled as required by the plugin automatically without any additional configuration and includes the corresponding string resources for each validation.

### Fields

For the correct functioning of the plugin it is required that within the client configuration (configuration/settings/customer settings) the following are added as custom client attributes 
the NIT and NCR fields, their control type being: text box. (No extra configuration is needed).
 

#### How to install:

There are two ways to install this plugin:

1. By including the source code of your NopCommerce project:
	- Download the source code of the plugin (this class library).
	- Add it to your NopCommerce project, under your `Plugins` folder (make sure that **NOT** the `Plugins` folder under `Presentation\Nop.Web`).
	- Compile the class library you just added to your project.
	- Run NopCommerce.
	- Go to the administration area (you will have to log in as administrator).
	- Go to `Configuration> local plugins`.
	- Look for the plugin called `Custom Fields Validator`.
	- Click on 'Install'.

2. Not including the source code:
	- Download the plugin source code (this class library).
	- Add it to your NopCommerce project, under your `Plugins` folder (make sure that **NOT** the `Plugins` folder under `Presentation\Nop.Web`).
	- Compile the class library you just added to your project.
	- Go to `Presentation\Nop.Web\Plugins` and find a folder called `Widgets.CustomFieldsValidator`.
	- Compress the folder.
	- Now you can install it in any instance of NopCommerce (version 4.30), just to install it:
		- Go to the administration area (you will need to log in as administrator).
		- Go to `Configuration> local plugins`.
		- Click on 'Load plugin or theme'.
		- Load the zip file containing the compiled code of the plugin.
		- Restart NopCommerce.
		- Find the plugin called "Custom Fields Validator".
		- Click on "Install".

##### How to activate:

Go to 'Settings > Widgets'.
2. Find the payment method called "Custom Fields Validator".
3. Click "Edit".
4. Check the box "It is active".
5. Click on "Update".
