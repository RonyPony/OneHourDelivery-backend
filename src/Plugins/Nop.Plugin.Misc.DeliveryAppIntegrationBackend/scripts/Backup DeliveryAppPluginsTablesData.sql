-- Delete backup Data
if OBJECT_ID('BkVendor_Warehouse_Mapping') is not null
drop table BkVendor_Warehouse_Mapping

if OBJECT_ID('BkWarehouse_UserCreator_Mapping') is not null
drop table BkWarehouse_UserCreator_Mapping

if OBJECT_ID('BkVendor_Review_Mappings') is not null
drop table BkVendor_Review_Mappings

if OBJECT_ID('BkCustomer_Rating_Mapping') is not null
drop table BkCustomer_Rating_Mapping

if OBJECT_ID('BkOrder_Rating_Mapping') is not null
drop table BkOrder_Rating_Mapping

if OBJECT_ID('BkOrder_DeliveryStatus_Mapping') is not null
drop table BkOrder_DeliveryStatus_Mapping

if OBJECT_ID('BkMessenger_DeclinedOrder_Mapping') is not null
drop table BkMessenger_DeclinedOrder_Mapping

if OBJECT_ID('BkOrderPendingToClosePayment') is not null
drop table BkOrderPendingToClosePayment

if OBJECT_ID('BkOrderPendingToClosePaymentItem') is not null
drop table BkOrderPendingToClosePaymentItem

if OBJECT_ID('BkGoogle_Direction_Mapping') is not null
drop table BkGoogle_Direction_Mapping

if OBJECT_ID('BkDriver_Location_Info_Mapping') is not null
drop table BkDriver_Location_Info_Mapping

if OBJECT_ID('BkVendorDiscount') is not null
drop table BkVendorDiscount

if OBJECT_ID('BkCustomer_Favorite_Mapping') is not null
drop table BkCustomer_Favorite_Mapping

if OBJECT_ID('BkDriver_Rating_Mapping') is not null
drop table BkDriver_Rating_Mapping

if OBJECT_ID('BkOrder_PaymentCollectionStatus_Mapping') is not null
drop table BkOrder_PaymentCollectionStatus_Mapping

if OBJECT_ID('BkCustomer_Pending_Review_Mapping') is not null
drop table BkCustomer_Pending_Review_Mapping

if OBJECT_ID('BkCustomer_Discount_Mapping') is not null
drop table BkCustomer_Discount_Mapping

select * into BkVendor_Warehouse_Mapping from [dbo].[Vendor_Warehouse_Mapping] 
select * into BkWarehouse_UserCreator_Mapping from [dbo].[Warehouse_UserCreator_Mapping]
select * into BkVendor_Review_Mappings from [dbo].[Vendor_Review_Mappings]
select * into BkCustomer_Rating_Mapping from [dbo].[Customer_Rating_Mapping]
select * into BkOrder_Rating_Mapping from [dbo].[Order_Rating_Mapping]
select * into BkOrder_DeliveryStatus_Mapping from [dbo].[Order_DeliveryStatus_Mapping]
select * into BkMessenger_DeclinedOrder_Mapping from [dbo].[Messenger_DeclinedOrder_Mapping]
select * into BkOrderPendingToClosePayment from [dbo].[OrderPendingToClosePayment]
select * into BkOrderPendingToClosePaymentItem from [dbo].[OrderPendingToClosePaymentItem]
			  
select * into BkGoogle_Direction_Mapping from [dbo].[Google_Direction_Mapping]
select * into BkDriver_Location_Info_Mapping from [dbo].[Driver_Location_Info_Mapping]
select * into BkVendorDiscount from [dbo].[VendorDiscount]
select * into BkCustomer_Favorite_Mapping from [dbo].[Customer_Favorite_Mapping]
select * into BkDriver_Rating_Mapping from [dbo].[Driver_Rating_Mapping]

select * into BkOrder_PaymentCollectionStatus_Mapping from [dbo].[Order_PaymentCollectionStatus_Mapping]

select * into BkCustomer_Pending_Review_Mapping from [dbo].[Customer_Pending_Review_Mapping]

select * into BkCustomer_Discount_Mapping from [dbo].[Customer_Discount_Mapping]






