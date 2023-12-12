Translation results
# CommonERPTables Plugin

> Read this entire document before taking any action with this plugin.

This plugin was developed to create the tables that are needed to integrate NopCommerce in its version 4.30
with a certain ERP.

## Features:

When installing this plugin, the tables that will keep the relationship (mapping) between any ERP and nopCommerce are created, it is also exposed
a URL path so that the information to be stored can be easily sent.

### How to install:

There are two ways to install this plugin:

1. Including the source code of your NopCommerce project:
	- Download the source code of the plugin (this class library).
	- Add it to your NopCommerce project, under your `Plugins` folder (make sure it ** IS NOT ** the` Plugins` folder under `Presentation\Nop.Web`).
	- Compile the class library that you just added to your project.
	- Run NopCommerce.
	- Go to the administration area (you will need to log in as an administrator).
	- Go to `Settings> local plugins`.
	- Look for the plugin called "Common ERP Tables".
	- Click "Install".

2. Not including the source code:
	- Download the source code of the plugin (this class library).
	- Add it to your NopCommerce project, under your `Plugins` folder (make sure it ** IS NOT ** the` Plugins` folder under `Presentation\Nop.Web`).
	- Compile the class library that you just added to your project.
	- Go to `Presentation\Nop.Web\Plugins` and find a folder called` Synchronizers.CommonERPTables`.
	- Zip the folder.
	- Now you can install it in any instance of NopCommerce (version 4.30), to install it, simply:
	- Go to the administration area (you will need to log in as an administrator).
	- Go to `Settings> local plugins`.
	- Click on "Upload plugin or theme".
	- Upload the zip file that contains the compiled code of the plugin.
	- Restart NopCommerce.
	- Look for the plugin called "Common ERP Tables".
	- Click "Install".

### URLs exposed to receive information from the ERP to NopCommerces
> The nomenclature and parameter of each one of the urls that the plugin exposes to save the information that will be mapped is detailed below.

| Entity         | URL                    | HTTP Verb | Parameters								 | Example																	 |
|----------------|------------------------|-----------|------------------------------------------|---------------------------------------------------------------------------|
| Customers		 | [domain]/erp/customers | POST	  | CustomerId (int), ErpCustomerId (string) | https://localhost:44356/erp/customers?CustomerId=2249&ErpCustomerId=C_007 |
| Products       | [domain]/erp/products  | POST	  | ProductId (int), ErpProductId (string)   | https://localhost:44356/erp/customers?ProductId=123&ErpProductId=P_123	 |
| Products       | [domain]/erp/products  | GET		  | ProductId (int)							 | https://localhost:44356/erp/customers?ProductId=123						 |
| Orders         | [domain]/erp/orders    | POST	  | OrderId (int), ErpOrderId (string)		 | https://localhost:44356/erp/orders?OrderId=20&ErpOrderId=ERP_Order_0124	 |