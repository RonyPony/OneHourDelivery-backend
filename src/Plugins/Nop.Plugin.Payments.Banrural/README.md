# Documentation: Banrural plugin

<div style="text-align: justify">

In this file we will be viewing the corresponding information about the Banrural payment gateway plugin, this plugin is developed for NopCommerce v4.30. Its operation is described as follows: after having previously installed the plugin and having selected the products, a payment method will appear which will redirect us to the Banrural payment page, where we can proceed with the transaction.

# Compilation and installation

To configure the plugin, you need to add the project in the NopCommerce solution with which we are going to work, corresponding to the plugin version, 4.30 in this case, and, therefore, we will need IIS and SQL Server to run the project from Visual Studio. Once this is done, we can install the plugin from the administration, entering Configuration / Local Plugins and, if the previous steps were fulfilled, the Banrural Plugin will appear, once installed, we can proceed to make purchases within our NopCommerce using the method Banrual payment.

# Fields and values

There are several fields that are necessary to correctly carry out any transaction with the Banrural payment method plugin. Some of these fields are configurable from the admin.

>*Below are the configurable fields from the admin:*

1. ***Key ID***: Access key to the Banrural payment page to carry out the transaction. It is found on the Pixelpay page, in the section _Pixelpay -> Preferences -> API Options_. This field is provided by Banrural.

2. ***Main site***: This field represents the URL that redirects from NopCommerce to the Banrural payment method page. This field is provided by Banrural.

3. ***Cancel URL***: This field represents the URL that redirects from Banrural's payment method to NopCommerce, when the customer chooses the option to cancel the order. The nomenclature of this URL should be as follows: ***https://MyDomain.com/banrural/transaction-details***, where **"MyDomain.com"** must be replaced by the domain of your Web page.

4. ***Approved URL***: This field represents the URL that redirects from Banrural's payment method to NopCommerce, when the order is successfully processed. The nomenclature of this URL should be as follows: ***https://MyDomain.com/checkout/completed/***, where **"MyDomain.com"** must be replaced by the domain of your page Web.

5. ***Language***: Language used for displaying the Banrural payment page.

The field **currency** by default is ***HNL*** [(Lempira, monetary unit of Honduras)](https://en.wikipedia.org/wiki/Honduran_lempira), which is the currency used by Banrural by default.

# More information

For more information, visit Banrural's (PixelPay) documentation page by clicking [HERE](https://pixelpay.zendesk.com/hc/en-us/articles/360044306091-Web-and-Mobile-App-Integration) .

<div>