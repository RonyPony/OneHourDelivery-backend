

INSERT INTO Vendor_Warehouse_Mapping
select VendorId, WarehouseId from BkVendor_Warehouse_Mapping 

INSERT INTO Warehouse_UserCreator_Mapping
select WarehouseId, CustomerId from BkWarehouse_UserCreator_Mapping 


INSERT INTO Vendor_Review_Mappings
select VendorId, CustomerId, Rating, Comment from BkVendor_Review_Mappings 


INSERT INTO Customer_Rating_Mapping
select  CreatorCustomerId, RatedCustomerId, Rate, Comment, CreateOnUtc from BkCustomer_Rating_Mapping 


INSERT INTO Order_Rating_Mapping
select OrderId, Rate, Comment, CreatedOnUtc from BkOrder_Rating_Mapping 


INSERT INTO Order_DeliveryStatus_Mapping
select CustomerId, OrderId, DeliveryStatusId, AwaitingForMessengerDate, AcceptedByMessengerDate, DeliveryInProgressDate, DeliveredDate, DeclinedByStoreDate, MessageToDeclinedOrder from BkOrder_DeliveryStatus_Mapping


INSERT INTO Messenger_DeclinedOrder_Mapping
select CustomerId, OrderId, DeclinedDate, DeclinedMessage from BkMessenger_DeclinedOrder_Mapping


INSERT INTO OrderPendingToClosePayment 
select CustomOrderNumber, BillingAddressId, VendorId, CustomerId, PickupAddressId, ShippingAddressId, OrderId, OrderGuid, StoreId, PickupInStore, OrderStatusId, VendorPaymentStatusId, DriverPaymentStatusId, PaymentMethodSystemName, CustomerCurrencyCode, CurrencyRate, CustomerTaxDisplayTypeId, VatNumber, OrderSubtotalInclTax, OrderSubtotalExclTax, OrderSubTotalDiscountInclTax, OrderSubTotalDiscountExclTax, OrderShippingInclTax, OrderShippingExclTax, PaymentMethodAdditionalFeeInclTax, PaymentMethodAdditionalFeeExclTax, TaxRates, OrderTax, OrderDiscount, OrderTotal, OrderTotalAdministrativePercentage, OrderTotalAdministrativeProfitAmount, OrderTotalVendorPercentage, OrderTotalVendorProfitAmount, RefundedAmount, RewardPointsHistoryEntryId, CheckoutAttributeDescription, CheckoutAttributesXml, CustomerLanguageId, AffiliateId, CustomerIp, AllowStoringCreditCardNumber, CardType, CardName, CardNumber, MaskedCreditCardNumber, CardCvv2, CardExpirationMonth, CardExpirationYear, AuthorizationTransactionId, AuthorizationTransactionCode, AuthorizationTransactionResult, CaptureTransactionId, CaptureTransactionResult, SubscriptionTransactionId, PaidDateUtc, ShippingMethod, ShippingRateComputationMethodSystemName, CustomValuesXml, Deleted, CreatedOnUtc, ShippingTaxAdministrativePercentage, ShippingTaxAdministrativeProfitAmount, ShippingTaxMessengerPercentage, ShippingTaxMessengerProfitAmount, RedeemedRewardPointsEntryId from BkOrderPendingToClosePayment


INSERT INTO OrderPendingToClosePaymentItem
select OrderId, ProductId, OrderItemGuid, Quantity, UnitPriceInclTax, UnitPriceExclTax, PriceInclTax, PriceExclTax, DiscountAmountInclTax, DiscountAmountExclTax, OriginalProductCost, AttributeDescription, AttributesXml, DownloadCount, IsDownloadActivated, LicenseDownloadId, ItemWeight, RentalStartDateUtc, RentalEndDateUtc from BkOrderPendingToClosePaymentItem 
	
INSERT INTO Google_Direction_Mapping
select  OrderId, GoogleResource, DestinationType, RequestNumber, CreateOnUtc from BkGoogle_Direction_Mapping 


INSERT INTO Driver_Location_Info_Mapping
select [OrderId], [Latitude], [Longitude], [DeliveryStatus], [DestinationType], [CreatedOnUtc] from BkDriver_Location_Info_Mapping 


INSERT INTO VendorDiscount
select Vendor_Id, Discount_Id from BkVendorDiscount 


INSERT INTO Customer_Favorite_Mapping
select CustomerId, VendorId, CreatedOnUtc from BkCustomer_Favorite_Mapping 


INSERT INTO Driver_Rating_Mapping
select DriverId, CustomerId, OrderId, Rating, RatingType, CreatedOnUtc from BkDriver_Rating_Mapping


INSERT INTO Order_PaymentCollectionStatus_Mapping
select OrderId, CustomerId, OrderTotal, PaymentCollectionStatusId, CreatedOnUtc, CollectedByCustomerId, CollectedOnUtc from BkOrder_PaymentCollectionStatus_Mapping

INSERT INTO Customer_Pending_Review_Mapping
select [CustomerId], [OrderId], [VendorId], [PendingReviewStatus], [CreatedOnUtc] from BkCustomer_Pending_Review_Mapping


INSERT INTO [dbo].[Customer_Discount_Mapping]
select [CustomerId], [DiscountId], [CreatedAtUtc] from BkCustomer_Discount_Mapping
