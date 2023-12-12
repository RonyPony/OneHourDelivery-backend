using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains.Enums;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Helpers;
using Nop.Services.Localization;
using System.Collections.Generic;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains
{
    /// <summary>
    /// Represents the model that provides the different resources used by Contacts Confirmation Plugin.
    /// </summary>
    public static class LocaleResources
    {
        /// <summary>
        /// Represents the local resources for displaying the plugin information in English (en-US).
        /// </summary>
        public static Dictionary<string, string> EnglishResources = new Dictionary<string, string>
        {
            #region Generals

            ["Admin.Orders.DeliveryInfo"] = "Delivery information",
            ["Admin.Orders.PaymentCollection"] = "Payment collection",
            ["Admin.Orders.Fields.MarkAsCollected"] = "Mark as collected",
            ["Admin.Order.Driver.Updated"] = "Driver updated.",
            ["Plugin.Misc.CustomerAddressGeocoding.Fields.Email.AlreadyRegistered"] = "The email is already registered.",
            ["Plugin.Misc.CustomerAddressGeocoding.Fields.Phone.AlreadyRegistered"] = "The phone number is already registered.",
            [$"{Defaults.VerificationCodeMessageSubject}"] = "Verification Code",

            [$"{Defaults.LocaleResourcesPrefix}.Admin.Orders.Fields.VendorPaymentStatus"] = "Vendor payment status",
            [$"{Defaults.LocaleResourcesPrefix}.Admin.Orders.Fields.VendorPaymentStatus.Hint"] = "Search by specific vendor payment status.",

            [$"{Defaults.LocaleResourcesPrefix}.Admin.Orders.List.VendorPaymentStatus"] = "Vendor payment statuses",

            [$"{Defaults.LocaleResourcesPrefix}.Admin.Orders.Fields.DriverPaymentStatus"] = "Driver payment status",
            [$"{Defaults.LocaleResourcesPrefix}.Admin.Orders.Fields.DriverPaymentStatus.Hint"] = "Search by specific driver payment status.",

            [$"{Defaults.LocaleResourcesPrefix}.Admin.Orders.List.DriverPaymentStatus"] = "Driver payment statuses",
            [$"{Defaults.LocaleResourcesPrefix}.Admin.OrdersPendingToClosePayment"] = "Orders pending to close payment",
            [$"{Defaults.LocaleResourcesPrefix}.Admin.VendorOrdersEarningList"] = "Vendor orders earnings",
            [$"{Defaults.LocaleResourcesPrefix}.Admin.Orders.Fields.Vendor"] = "Vendor",
            [$"{Defaults.LocaleResourcesPrefix}.OrdersPending.OrderShippingTax"] = "Shipping tax",
            [$"{Defaults.LocaleResourcesPrefix}.OrdersPending.ShippingTaxAdministrativeProfitAmount"] = "Shipping tax admin %",
            [$"{Defaults.LocaleResourcesPrefix}.OrdersPending.ShippingTaxMessengerProfitAmount"] = "Shipping tax driver %",
            [$"{Defaults.LocaleResourcesPrefix}.OrdersPending.OrderSubtotalExclTax"] = "Order subtotal",
            [$"{Defaults.LocaleResourcesPrefix}.OrdersPending.OrderTotalAdministrativeProfitAmount"] = "Order subtotal admin %",
            [$"{Defaults.LocaleResourcesPrefix}.OrdersPending.OrderTotalVendorProfitAmount"] = "Order subtotal vendor %",

            [$"{Defaults.LocaleResourcesPrefix}.OrdersPending.DriverCustomerEmail"] = "Driver",
            [$"{Defaults.LocaleResourcesPrefix}.OrdersPending.DriverCustomerEmail.Hint"] = "Search by specific driver.",

            [$"{Defaults.LocaleResourcesPrefix}.Plugin.Misc.DeliveryAppIntegrationBackend.Review.Like"] = "Like",
            [$"{Defaults.LocaleResourcesPrefix}.Plugin.Misc.DeliveryAppIntegrationBackend.Review.Dislike"] = "Dislike",
            [$"{Defaults.LocaleResourcesPrefix}.Plugin.Misc.DeliveryAppIntegrationBackend.Review.Name"] = "Name",
            [$"{Defaults.LocaleResourcesPrefix}.Plugin.Misc.DeliveryAppIntegrationBackend.Review.ApprovalPorcent"] = "Approval (%)",

            #endregion

            #region Vendor Warehouse

            [$"{Defaults.LocaleResourcesPrefix}.VendorWarehouse.Button.SelectWarehouse"] = "Select warehouse",
            [$"{Defaults.LocaleResourcesPrefix}.VendorWarehouse.ModalTitle.SelectWarehouse"] = "Select warehouse",
            [$"{Defaults.LocaleResourcesPrefix}.VendorWarehouse.SelectWarehouse"] = "Select a warehouse",
            [$"{Defaults.LocaleResourcesPrefix}.VendorWarehouse.Warehouse"] = "Warehouse",
            [$"{Defaults.LocaleResourcesPrefix}.VendorWarehouse.Warehouse.Hint"] = "The warehouse for this vendor.",
            [$"{Defaults.LocaleResourcesPrefix}.VendorWarehouse.Warehouse.Required"] = "The warehouse is required",
            [$"{Defaults.LocaleResourcesPrefix}.VendorWarehouse.Warehouse.SelectedSuccessfully"] = "Warehouse selected successfully.",
            [$"{Defaults.LocaleResourcesPrefix}.VendorWarehouse.Warning.VendorNotBoundToCustomer"] = "Vendor not bound to a customer, warehouse assigning won't be available for this vendor.",
            [$"{Defaults.LocaleResourcesPrefix}.VendorWarehouse.Admin.Tip"] = "This value is saved automatically when a warehouse is selected.",

            #endregion

            #region Configuration

            #region General

            [$"{Defaults.LocaleResourcesPrefix}.Generic.Config.Panel.Title"] = "Generals",

            [$"{Defaults.LocaleResourcesPrefix}.Generics.AdministrativeProfit"] = "Shipping tax administrative profit %",
            [$"{Defaults.LocaleResourcesPrefix}.Generics.AdministrativeProfit.Hint"] = "Indicates the delivery profit percentage for the administration. If leaving it empty default value will be 0% of the delivery cost for the administration.",
            [$"{Defaults.LocaleResourcesPrefix}.Generics.AdministrativeProfit.RangeInvalid"] = "The shipping tax administrative profit % accepts numbers from 0 to 100 inclusive.",

            [$"{Defaults.LocaleResourcesPrefix}.Generics.MessengerProfit"] = "Shipping tax driver profit %",
            [$"{Defaults.LocaleResourcesPrefix}.Generics.MessengerProfit.Hint"] = "Indicates the delivery profit percentage for the driver. If leaving it empty default value will be 0% of the delivery cost for the driver.",
            [$"{Defaults.LocaleResourcesPrefix}.Generics.MessengerProfit.RangeInvalid"] = "The shipping tax driver profit % accepts numbers from 0 to 100 inclusive.",

            [$"{Defaults.LocaleResourcesPrefix}.Generics.Validation.ProfitMismatch"] = "The sum of both profits must be from 0% to 100% of the shipping tax.",

            [$"{Defaults.LocaleResourcesPrefix}.Generics.NotificationCenterUrl"] = "Url for notifications",
            [$"{Defaults.LocaleResourcesPrefix}.Generics.NotificationCenterUrl.Hint"] = "Url to use for the connection with the notification service.",
            [$"{Defaults.LocaleResourcesPrefix}.Generics.NotificationCenterUrl.Required"] = "The url for notification is required.",

            [$"{Defaults.LocaleResourcesPrefix}.Generics.NotificationDriverTrackingUrl"] = "Url for tracking driver notifications",
            [$"{Defaults.LocaleResourcesPrefix}.Generics.NotificationDriverTrackingUrl.Hint"] = "Url to use for the connection with the notification service.",
            [$"{Defaults.LocaleResourcesPrefix}.Generics.NotificationDriverTrackingUrl.Required"] = "The url for notification is required.",

            [$"{Defaults.LocaleResourcesPrefix}.Generics.MaxMoneyAmountDriverCanCarry"] = "Max money amount the driver can carry",
            [$"{Defaults.LocaleResourcesPrefix}.Generics.MaxMoneyAmountDriverCanCarry.Hint"] = "The max money amount the driver can carry with him.",
            [$"{Defaults.LocaleResourcesPrefix}.Generics.MaxMoneyAmountDriverCanCarry.Required"] = "The max money amount the driver can carry is required.",

            #endregion

            #endregion

            #region Admin menu

            [$"{Defaults.LocaleResourcesPrefix}.AdminMenu.Main.Title"] = "Delivery App Integration",
            [$"{Defaults.LocaleResourcesPrefix}.AdminMenu.PendingOrders.Title"] = "Orders pending to pay",
            [$"{Defaults.LocaleResourcesPrefix}.AdminMenu.TracingOrders.Title"] = "Tracing orders",
            [$"{Defaults.LocaleResourcesPrefix}.AdminMenu.VendorOrdersEarningList.Title"] = "Vendor orders earnings",
            [$"{Defaults.LocaleResourcesPrefix}.AdminMenu.Settings.Title"] = "Settings",

            #endregion

            #region Order tracing

            [$"{Defaults.LocaleResourcesPrefix}.Admin.Orders.Fields.AwaitingForMessengerDate"] = "Awating for driver date",
            [$"{Defaults.LocaleResourcesPrefix}.Admin.Orders.Fields.AwaitingForMessengerDate.Hint"] = "Indicates the date and time when the order was ready to pickup.",

            [$"{Defaults.LocaleResourcesPrefix}.Admin.Orders.Fields.AcceptedByMessengerDate"] = "Accepted by messenger date",
            [$"{Defaults.LocaleResourcesPrefix}.Admin.Orders.Fields.AcceptedByMessengerDate.Hint"] = "Indicates the date and time when the order was accepted by a driver.",

            [$"{Defaults.LocaleResourcesPrefix}.Admin.Orders.Fields.MessageToDeclinedOrder"] = "Order declined message",
            [$"{Defaults.LocaleResourcesPrefix}.Admin.Orders.Fields.MessageToDeclinedOrder.Hint"] = "Message from the store indicating why the order was declined.",

            [$"{Defaults.LocaleResourcesPrefix}.Admin.Orders.Fields.DeliveryInProgressDate"] = "Delivery in progress date",
            [$"{Defaults.LocaleResourcesPrefix}.Admin.Orders.Fields.DeliveryInProgressDate.Hint"] = "Indicates the date and time when the order was retrieved by the driver from the store.",

            [$"{Defaults.LocaleResourcesPrefix}.Admin.Orders.Fields.DeliveredDate"] = "Delivered date",
            [$"{Defaults.LocaleResourcesPrefix}.Admin.Orders.Fields.DeliveredDate.Hint"] = "Indicates the date and time when the order was delivered.",

            [$"{Defaults.LocaleResourcesPrefix}.Admin.Orders.Fields.DeclinedByStoreDate"] = "Declined by the store date",
            [$"{Defaults.LocaleResourcesPrefix}.Admin.Orders.Fields.DeclinedByStoreDate.Hint"] = "Indicates the date and time when the order was declined by the store.",

            [$"{Defaults.LocaleResourcesPrefix}.Admin.Orders.Fields.DeliveryStatusId"] = "Delivery status",
            [$"{Defaults.LocaleResourcesPrefix}.Admin.Orders.Fields.DeliveryStatusId.Hint"] = "Indicates the current order delivery estatus.",

            [$"{Defaults.LocaleResourcesPrefix}.Admin.Orders.Fields.DriverId"] = "Driver",
            [$"{Defaults.LocaleResourcesPrefix}.Admin.Orders.Fields.DriverId.Hint"] = "Indicates the current driver assigned to the order.",

            [$"{Defaults.LocaleResourcesPrefix}.Admin.Orders.Fields.PaymentCollectionStatus"] = "Collection status",
            [$"{Defaults.LocaleResourcesPrefix}.Admin.Orders.Fields.PaymentCollectionStatus.Hint"] = "Filter orders by payment collection status.",

            [$"{Defaults.LocaleResourcesPrefix}.Admin.Orders.Fields.PaymentMethodName"] = "Payment method",
            [$"{Defaults.LocaleResourcesPrefix}.Admin.Orders.Fields.PaymentMethodName.Hint"] = "Filter orders by payment method.",

            #endregion

            #region Delivery status enum

            [$"{NopLocalizationDefaults.EnumLocaleStringResourcesPrefix}{typeof(DeliveryStatus)}.{DeliveryStatus.AwaitingForMessenger}"] = "Awaiting for driver",
            [$"{NopLocalizationDefaults.EnumLocaleStringResourcesPrefix}{typeof(DeliveryStatus)}.{DeliveryStatus.AssignedToMessenger}"] = "Assigned to driver",
            [$"{NopLocalizationDefaults.EnumLocaleStringResourcesPrefix}{typeof(DeliveryStatus)}.{DeliveryStatus.OrderPreparationCompleted}"] = "Order preparation completed",
            [$"{NopLocalizationDefaults.EnumLocaleStringResourcesPrefix}{typeof(DeliveryStatus)}.{DeliveryStatus.DeliveryInProgress}"] = "Delivery in progress",
            [$"{NopLocalizationDefaults.EnumLocaleStringResourcesPrefix}{typeof(DeliveryStatus)}.{DeliveryStatus.Delivered}"] = "Delivered",
            [$"{NopLocalizationDefaults.EnumLocaleStringResourcesPrefix}{typeof(DeliveryStatus)}.{DeliveryStatus.DeclinedByStore}"] = "Declined by store",

            #endregion

            #region Payment collection status enum

            [$"{NopLocalizationDefaults.EnumLocaleStringResourcesPrefix}{typeof(PaymentCollectionStatus)}.{PaymentCollectionStatus.DoesNotApply}"] = "Doesn't apply",
            [$"{NopLocalizationDefaults.EnumLocaleStringResourcesPrefix}{typeof(PaymentCollectionStatus)}.{PaymentCollectionStatus.Collected}"] = "Payment collected",
            [$"{NopLocalizationDefaults.EnumLocaleStringResourcesPrefix}{typeof(PaymentCollectionStatus)}.{PaymentCollectionStatus.NotCollected}"] = "Payment not collected",

            #endregion

            #region Order payment collection

            [$"{Defaults.LocaleResourcesPrefix}.Admin.Orders.Fields.PaymentCollectionStatusId"] = "Payment collection status",
            [$"{Defaults.LocaleResourcesPrefix}.Admin.Orders.Fields.PaymentCollectionStatusId.Hint"] = "The estatus of the payment collection.",

            [$"{Defaults.LocaleResourcesPrefix}.Admin.Orders.Fields.CollectedByCustomerId"] = "Collected by",
            [$"{Defaults.LocaleResourcesPrefix}.Admin.Orders.Fields.CollectedByCustomerId.Hint"] = "The person that collected the payment of the order.",

            [$"{Defaults.LocaleResourcesPrefix}.Admin.Orders.Fields.CollectedOnUtc"] = "Collected on",
            [$"{Defaults.LocaleResourcesPrefix}.Admin.Orders.Fields.CollectedOnUtc.Hint"] = "The date that the collection of the payment of the order was made.",

            [$"{Defaults.LocaleResourcesPrefix}.Admin.CollectSelectedOrders"] = "Collect selected orders",

            #endregion

            #region Payment Collection

            #region Error

            [$"{Defaults.LocaleResourcesPrefix}.PaymentCollection.SelectAnOrderError"] = "Must select at least one order.|",
            [$"{Defaults.LocaleResourcesPrefix}.PaymentCollection.CollectionNotRequiredForSelectedOrders"] = "Orders with ids: {0} does not apply for payment collection.|",
            [$"{Defaults.LocaleResourcesPrefix}.PaymentCollection.SelectedOrdersAlreadyCollected"] = "Orders with ids: {0} are already collected.|",
            [$"{Defaults.LocaleResourcesPrefix}.PaymentCollection.PaymentCollectionNotFoundForSelectedOrders"] = "Orders with ids: {0} has no collection status to update.|",
            [$"{Defaults.LocaleResourcesPrefix}.PaymentCollection.SelectedOrdersPaymentMethod"] = "Payment method for order ids: {0} must be cash.|",
            [$"{Defaults.LocaleResourcesPrefix}.PaymentCollection.SelectedOrdersNotFound"] = "Orders with ids: {0} were not found.|",
            [$"{Defaults.LocaleResourcesPrefix}.PaymentCollection.DeliveryStatusNotFoundForSelectedOrders"] = "Orders with ids: {0} has no delivery status.|",
            [$"{Defaults.LocaleResourcesPrefix}.PaymentCollection.SelectedOrdersNotDelivered"] = "Delivery status for order ids: {0} must be delivered.|",
            [$"{Defaults.LocaleResourcesPrefix}.PaymentCollection.SelectedOrdersMustHaveSameDriver"] = "Selected orders must be delivered by the same driver.|",

            [$"{Defaults.LocaleResourcesPrefix}.PaymentCollection.PaymentCollectionAlreadyRegistered"] = "Payment collection status already registered for order {0}",
            [$"{Defaults.LocaleResourcesPrefix}.PaymentCollection.PaymentCollectionNotFound"] = "Order {0} does not have a payment collection status to update.",

            #endregion

            #region Success

            [$"{Defaults.LocaleResourcesPrefix}.PaymentCollection.OrderCollected"] = "Order collected successfully.",
            [$"{Defaults.LocaleResourcesPrefix}.PaymentCollection.SelectedOrdersCollected"] = "Orders: {0} have been collected successfully.",

            #endregion

            #endregion

            #region Assign coupon to Customer
            ["Admin.Promotions.Discounts.AppliedToCustomers.AddNew"] = "Add a new customer",
            ["admin.promotions.discounts.appliedtocustomer"] = "Applied to customers",

            #endregion
        };

        /// <summary>
        /// Represents the local resources for displaying the plugin information in Spanish (es-).
        /// </summary>
        public static Dictionary<string, string> SpanishResources = new Dictionary<string, string>
        {
            #region Generals

            ["Admin.Orders.DeliveryInfo"] = "Información del delivery",
            ["Admin.Orders.PaymentCollection"] = "Cobro del pago",
            ["Admin.Orders.Fields.MarkAsCollected"] = "Marcar como cobrado",
            ["Admin.Order.Driver.Updated"] = "Motorizado actualizado.",
            ["Plugin.Misc.CustomerAddressGeocoding.Fields.Email.AlreadyRegistered"] = "El correo electrónico ya se encuentra registrado.",
            ["Plugin.Misc.CustomerAddressGeocoding.Fields.Phone.AlreadyRegistered"] = "El número de teléfono ya se encuentra registrado.",
            ["plugin.misc.deliveryappintegrationbackend.admin.paytovendors"] = "Pagar a proveedor",
            ["plugin.misc.deliveryappintegrationbackend.admin.paytodrivers"] = "Pagar a motorizado",
            [$"{Defaults.VerificationCodeMessageSubject}"] = "Código de Verificación",

            [$"{Defaults.LocaleResourcesPrefix}.Admin.Orders.Fields.VendorPaymentStatus"] = "Estado pago suplidor",
            [$"{Defaults.LocaleResourcesPrefix}.Admin.Orders.Fields.VendorPaymentStatus.Hint"] = "Buscar por un estado de pago a suplidor específico.",

            [$"{Defaults.LocaleResourcesPrefix}.Admin.Orders.List.VendorPaymentStatus"] = "Estados pago suplidor",

            [$"{Defaults.LocaleResourcesPrefix}.Admin.Orders.Fields.DriverPaymentStatus"] = "Estado pago mensajero",
            [$"{Defaults.LocaleResourcesPrefix}.Admin.Orders.Fields.DriverPaymentStatus.Hint"] = "Buscar por un estado de pago a mensajero específico.",

            [$"{Defaults.LocaleResourcesPrefix}.Admin.Orders.List.DriverPaymentStatus"] = "Estados pago mensajero",
            [$"{Defaults.LocaleResourcesPrefix}.Admin.OrdersPendingToClosePayment"] = "Ordenes pendientes de cerrar pago",
            [$"{Defaults.LocaleResourcesPrefix}.Admin.VendorOrdersEarningList"] = "Ganancias de pedidos de proveedores",
            [$"{Defaults.LocaleResourcesPrefix}.Admin.Orders.Fields.Vendor"] = "Suplidor",
            [$"{Defaults.LocaleResourcesPrefix}.OrdersPending.OrderShippingTax"] = "Costo envío",
            [$"{Defaults.LocaleResourcesPrefix}.OrdersPending.ShippingTaxAdministrativeProfitAmount"] = "% adtvo. del costo de envío",
            [$"{Defaults.LocaleResourcesPrefix}.OrdersPending.ShippingTaxMessengerProfitAmount"] = "% motorizado del costo de envío",
            [$"{Defaults.LocaleResourcesPrefix}.OrdersPending.OrderSubtotalExclTax"] = "Subtotal de la orden",
            [$"{Defaults.LocaleResourcesPrefix}.OrdersPending.OrderTotalAdministrativeProfitAmount"] = "% adtvo. del subtotal de la orden",
            [$"{Defaults.LocaleResourcesPrefix}.OrdersPending.OrderTotalVendorProfitAmount"] = "% comercio del subtotal de la orden",

            [$"{Defaults.LocaleResourcesPrefix}.OrdersPending.DriverCustomerEmail"] = "Motorizado",
            [$"{Defaults.LocaleResourcesPrefix}.OrdersPending.DriverCustomerEmail.Hint"] = "Buscar por un motorizado específico.",

            [$"{Defaults.LocaleResourcesPrefix}.Plugin.Misc.DeliveryAppIntegrationBackend.Review.Like"] = "Me gusta",
            [$"{Defaults.LocaleResourcesPrefix}.Plugin.Misc.DeliveryAppIntegrationBackend.Review.Dislike"] = "Me Disgusta",
            [$"{Defaults.LocaleResourcesPrefix}.Plugin.Misc.DeliveryAppIntegrationBackend.Review.Name"] = "Nombre",
            [$"{Defaults.LocaleResourcesPrefix}.Plugin.Misc.DeliveryAppIntegrationBackend.Review.ApprovalPorcent"] = "aprobación (%)",
            #endregion

            #region Vendor Warehouse

            [$"{Defaults.LocaleResourcesPrefix}.VendorWarehouse.Button.SelectWarehouse"] = "Seleccionar almacén",
            [$"{Defaults.LocaleResourcesPrefix}.VendorWarehouse.ModalTitle.SelectWarehouse"] = "Selección de almacén",
            [$"{Defaults.LocaleResourcesPrefix}.VendorWarehouse.SelectWarehouse"] = "Seleccione un almacén",
            [$"{Defaults.LocaleResourcesPrefix}.VendorWarehouse.Warehouse"] = "Almacén",
            [$"{Defaults.LocaleResourcesPrefix}.VendorWarehouse.Warehouse.Hint"] = "El almacén para este proveedor.",
            [$"{Defaults.LocaleResourcesPrefix}.VendorWarehouse.Warehouse.Required"] = "El almacén es requerido",
            [$"{Defaults.LocaleResourcesPrefix}.VendorWarehouse.Warehouse.SelectedSuccessfully"] = "Almacén seleccionado correctamente.",
            [$"{Defaults.LocaleResourcesPrefix}.VendorWarehouse.Warning.VendorNotBoundToCustomer"] = "Proveedor no vinculado a un cliente, la asignación de almacén no estará disponible para este proveedor.",
            [$"{Defaults.LocaleResourcesPrefix}.VendorWarehouse.Admin.Tip"] = "Este valor se guarda automáticamente cuando se selecciona un almacén.",

            #endregion

            #region Configuration

            #region General

            [$"{Defaults.LocaleResourcesPrefix}.Generic.Config.Panel.Title"] = "Generales",

            [$"{Defaults.LocaleResourcesPrefix}.Generics.AdministrativeProfit"] = "% ganancia administrativa por costo de envío",
            [$"{Defaults.LocaleResourcesPrefix}.Generics.AdministrativeProfit.Hint"] = "Indica el porciento de ganancia del costo del envío para la administración. Si se deja vacío, el valor por defecto será 0% del costo de envío para la administración.",
            [$"{Defaults.LocaleResourcesPrefix}.Generics.AdministrativeProfit.RangeInvalid"] = "El % ganancia administrativa por costo de envío solo acepta valores desde 0 hasta 100.",

            [$"{Defaults.LocaleResourcesPrefix}.Generics.MessengerProfit"] = "% ganancia motorizado por costo de envío",
            [$"{Defaults.LocaleResourcesPrefix}.Generics.MessengerProfit.Hint"] = "Indica el porciento de ganancia del costo del envío para el motorizado. Si se deja vacío, el valor por defecto será 0% del costo de envío para el mensajero.",
            [$"{Defaults.LocaleResourcesPrefix}.Generics.MessengerProfit.RangeInvalid"] = "El % ganancia motorizado por costo de envío solo acepta valores desde 0 hasta 100.",

            [$"{Defaults.LocaleResourcesPrefix}.Generics.Validation.ProfitMismatch"] = "La suma de ambas ganancias debe ir entre el 0% y el 100% del costo de envío.",

            [$"{Defaults.LocaleResourcesPrefix}.Generics.NotificationCenterUrl"] = "Url para las notificaciones",
            [$"{Defaults.LocaleResourcesPrefix}.Generics.NotificationCenterUrl.Hint"] = "Url que se debe de utilizar para la conexión con el servicio de notificaciones.",
            [$"{Defaults.LocaleResourcesPrefix}.Generics.NotificationCenterUrl.Required"] = "La url para las notificaciones es requerida.",

            [$"{Defaults.LocaleResourcesPrefix}.Generics.NoficationCenterUrl"] = "Url absoluta de las notificaciones",
            [$"{Defaults.LocaleResourcesPrefix}.Generics.NoficationCenterUrl.Hint"] = "Url que se utilizara para conectarse al servicio de notificaciones.",
            [$"{Defaults.LocaleResourcesPrefix}.Generics.NoficationCenterUrl.Required"] = "La url para las notificaciones es requerida.",

            [$"{Defaults.LocaleResourcesPrefix}.Generics.NotificationDriverTrackingUrl"] = "Url absoluta para el seguimiento del motorizado mediente notificaciones",
            [$"{Defaults.LocaleResourcesPrefix}.Generics.NotificationDriverTrackingUrl.Hint"] = "Url que se utilizara para conectarse al servicio de notificaciones.",
            [$"{Defaults.LocaleResourcesPrefix}.Generics.NotificationDriverTrackingUrl.Required"] = "La url para las notificaciones es requerida.",

            [$"{Defaults.LocaleResourcesPrefix}.Generics.MaxMoneyAmountDriverCanCarry"] = "Cant. máxima de dinero que el mensajero puede llevar",
            [$"{Defaults.LocaleResourcesPrefix}.Generics.MaxMoneyAmountDriverCanCarry.Hint"] = "Cantidad máxima de dinero en efectivo que el mensajero puede llevar.",
            [$"{Defaults.LocaleResourcesPrefix}.Generics.MaxMoneyAmountDriverCanCarry.Required"] = "La cantidad máxima de dinero que el mensajero puede llevar es requerida.",

            #endregion

            #endregion

            #region Admin menu

            [$"{Defaults.LocaleResourcesPrefix}.AdminMenu.Main.Title"] = "Integración con App de Delivery",
            [$"{Defaults.LocaleResourcesPrefix}.AdminMenu.PendingOrders.Title"] = "Ordenes pendientes por pagar",
            [$"{Defaults.LocaleResourcesPrefix}.AdminMenu.TracingOrders.Title"] = "Seguimiento de ordenes",
            [$"{Defaults.LocaleResourcesPrefix}.AdminMenu.VendorOrdersEarningList.Title"] = "Ganancias de pedidos de proveedores",
            [$"{Defaults.LocaleResourcesPrefix}.AdminMenu.Settings.Title"] = "Configuración",

            #endregion

            #region Order tracing

            [$"{Defaults.LocaleResourcesPrefix}.Admin.Orders.Fields.AwaitingForMessengerDate"] = "Fecha pedido listo para recoger",
            [$"{Defaults.LocaleResourcesPrefix}.Admin.Orders.Fields.AwaitingForMessengerDate.Hint"] = "Fecha en la cual el pedido fue marcado como listo para recoger por la tienda.",

            [$"{Defaults.LocaleResourcesPrefix}.Admin.Orders.Fields.AcceptedByMessengerDate"] = "Fecha pedido aceptado por un motorizado",
            [$"{Defaults.LocaleResourcesPrefix}.Admin.Orders.Fields.AcceptedByMessengerDate.Hint"] = "Fecha en la cual el pedido fue aceptado por un motorizado.",

            [$"{Defaults.LocaleResourcesPrefix}.Admin.Orders.Fields.MessageToDeclinedOrder"] = "Razón de rechazo del pedido",
            [$"{Defaults.LocaleResourcesPrefix}.Admin.Orders.Fields.MessageToDeclinedOrder.Hint"] = "Mensaje de la tienda explicando porqué la orden fue rechazada.",

            [$"{Defaults.LocaleResourcesPrefix}.Admin.Orders.Fields.DeliveryInProgressDate"] = "Fecha delivery en curso",
            [$"{Defaults.LocaleResourcesPrefix}.Admin.Orders.Fields.DeliveryInProgressDate.Hint"] = "Fecha en la que el pedido fue recogido de la tienda por el motorizado.",

            [$"{Defaults.LocaleResourcesPrefix}.Admin.Orders.Fields.DeliveredDate"] = "Fecha delivery completado",
            [$"{Defaults.LocaleResourcesPrefix}.Admin.Orders.Fields.DeliveredDate.Hint"] = "Fecha en la que el pedido fue entregado al cliente por parte del motorizado.",

            [$"{Defaults.LocaleResourcesPrefix}.Admin.Orders.Fields.DeclinedByStoreDate"] = "Fecha rechazo del pedido",
            [$"{Defaults.LocaleResourcesPrefix}.Admin.Orders.Fields.DeclinedByStoreDate.Hint"] = "Fecha en la que la tienda rechazó el pedido.",

            [$"{Defaults.LocaleResourcesPrefix}.Admin.Orders.Fields.DeliveryStatusId"] = "Estado del delivery",
            [$"{Defaults.LocaleResourcesPrefix}.Admin.Orders.Fields.DeliveryStatusId.Hint"] = "Estado actual del delivery del pedido.",

            [$"{Defaults.LocaleResourcesPrefix}.Admin.Orders.Fields.DriverId"] = "Motorizado",
            [$"{Defaults.LocaleResourcesPrefix}.Admin.Orders.Fields.DriverId.Hint"] = "Motorizado que actualmente se encuentra asignado al pedido.",

            [$"{Defaults.LocaleResourcesPrefix}.Admin.Orders.Fields.PaymentCollectionStatus"] = "Estado del cobro",
            [$"{Defaults.LocaleResourcesPrefix}.Admin.Orders.Fields.PaymentCollectionStatus.Hint"] = "Filtrar ordenes por el estado del cobro del pago de la orden.",

            [$"{Defaults.LocaleResourcesPrefix}.Admin.Orders.Fields.PaymentMethodName"] = "Método de pago",
            [$"{Defaults.LocaleResourcesPrefix}.Admin.Orders.Fields.PaymentMethodName.Hint"] = "Filtrar ordenes por el método de pago.",

            #endregion

            #region Delivery status enum

            [$"{NopLocalizationDefaults.EnumLocaleStringResourcesPrefix}{typeof(DeliveryStatus)}.{DeliveryStatus.AwaitingForMessenger}"] = "Lista para recoger",
            [$"{NopLocalizationDefaults.EnumLocaleStringResourcesPrefix}{typeof(DeliveryStatus)}.{DeliveryStatus.AssignedToMessenger}"] = "Aceptada por un motorizado",
            [$"{NopLocalizationDefaults.EnumLocaleStringResourcesPrefix}{typeof(DeliveryStatus)}.{DeliveryStatus.OrderPreparationCompleted}"] = "Preparación del pedido completada",
            [$"{NopLocalizationDefaults.EnumLocaleStringResourcesPrefix}{typeof(DeliveryStatus)}.{DeliveryStatus.DeliveryInProgress}"] = "Delivery en curso",
            [$"{NopLocalizationDefaults.EnumLocaleStringResourcesPrefix}{typeof(DeliveryStatus)}.{DeliveryStatus.Delivered}"] = "Delivery completado",
            [$"{NopLocalizationDefaults.EnumLocaleStringResourcesPrefix}{typeof(DeliveryStatus)}.{DeliveryStatus.DeclinedByStore}"] = "Rechazada por la tienda",

            #endregion

            #region Payment collection status enum

            [$"{NopLocalizationDefaults.EnumLocaleStringResourcesPrefix}{typeof(PaymentCollectionStatus)}.{PaymentCollectionStatus.DoesNotApply}"] = "No aplica",
            [$"{NopLocalizationDefaults.EnumLocaleStringResourcesPrefix}{typeof(PaymentCollectionStatus)}.{PaymentCollectionStatus.Collected}"] = "Cobrado",
            [$"{NopLocalizationDefaults.EnumLocaleStringResourcesPrefix}{typeof(PaymentCollectionStatus)}.{PaymentCollectionStatus.NotCollected}"] = "No cobrado",

            #endregion

            #region Order payment collection

            [$"{Defaults.LocaleResourcesPrefix}.Admin.Orders.Fields.PaymentCollectionStatusId"] = "Estado del cobro",
            [$"{Defaults.LocaleResourcesPrefix}.Admin.Orders.Fields.PaymentCollectionStatusId.Hint"] = "Indica si se realizó el cobro del pago de la orden, si no se ha realizado o si no aplica.",

            [$"{Defaults.LocaleResourcesPrefix}.Admin.Orders.Fields.CollectedByCustomerId"] = "Cobrado por",
            [$"{Defaults.LocaleResourcesPrefix}.Admin.Orders.Fields.CollectedByCustomerId.Hint"] = "Indica la persona que realizó el cobro del pago de la orden.",

            [$"{Defaults.LocaleResourcesPrefix}.Admin.Orders.Fields.CollectedOnUtc"] = "Cobrado el",
            [$"{Defaults.LocaleResourcesPrefix}.Admin.Orders.Fields.CollectedOnUtc.Hint"] = "Indica la fecha en la que se realizó el cobro del pago de la orden.",
            
            [$"{Defaults.LocaleResourcesPrefix}.Admin.CollectSelectedOrders"] = "Cobrar ordenes seleccionadas",
            [$"{Defaults.LocaleResourcesPrefix}.Admin.Orders.Fields.Ordertax"] = "Impuesto",
            

            #endregion

            #region Payment Collection

            #region Error

            [$"{Defaults.LocaleResourcesPrefix}.PaymentCollection.SelectAnOrderError"] = "Debe seleccionar al menos una orden.|",
            [$"{Defaults.LocaleResourcesPrefix}.PaymentCollection.CollectionNotRequiredForSelectedOrders"] = "Los pedidos con id: {0} no aplican para cobro.|",
            [$"{Defaults.LocaleResourcesPrefix}.PaymentCollection.SelectedOrdersAlreadyCollected"] = "Los pedidos con id: {0} ya fueron cobrados.|",
            [$"{Defaults.LocaleResourcesPrefix}.PaymentCollection.PaymentCollectionNotFoundForSelectedOrders"] = "No se encontró estado de cobro para los pedidos con id: {0}.|",
            [$"{Defaults.LocaleResourcesPrefix}.PaymentCollection.SelectedOrdersPaymentMethod"] = "El método de pago debe de ser 'Efectivo' para los pedidos con id: {0}.|",
            [$"{Defaults.LocaleResourcesPrefix}.PaymentCollection.SelectedOrdersNotFound"] = "Los pedidos con id: {0} no fueron encontrados.|",
            [$"{Defaults.LocaleResourcesPrefix}.PaymentCollection.DeliveryStatusNotFoundForSelectedOrders"] = "No se encontró estado del envío para los pedidos con id: {0}.|",
            [$"{Defaults.LocaleResourcesPrefix}.PaymentCollection.SelectedOrdersNotDelivered"] = "El estado del envío debe de ser 'Delivery completado' para los pedidos con id: {0}.|",
            [$"{Defaults.LocaleResourcesPrefix}.PaymentCollection.SelectedOrdersMustHaveSameDriver"] = "El delivery debe ser completado por el mismo mensajero para todos los pedidos seleccionados.|",

            [$"{Defaults.LocaleResourcesPrefix}.PaymentCollection.PaymentCollectionAlreadyRegistered"] = "Ya existe un estado de cobro para el pedido {0}.",
            [$"{Defaults.LocaleResourcesPrefix}.PaymentCollection.PaymentCollectionNotFound"] = "No se encontró el estado de cobro para el pedido {0}.",

            #endregion

            #region Success

            [$"{Defaults.LocaleResourcesPrefix}.PaymentCollection.OrderCollected"] = "Pedido cobrado con éxito.",
            [$"{Defaults.LocaleResourcesPrefix}.PaymentCollection.SelectedOrdersCollected"] = "Los pedidos: {0} fueron cobrados con éxito.",

            #endregion

            #endregion

            #region Assign coupon to Customer
            ["Admin.Promotions.Discounts.AppliedToCustomers.AddNew"] = "Agregar un nuevo cliente",
            ["admin.promotions.discounts.appliedtocustomer"] = "Aplicado a un cliente",

            #endregion
        };
    }
}
