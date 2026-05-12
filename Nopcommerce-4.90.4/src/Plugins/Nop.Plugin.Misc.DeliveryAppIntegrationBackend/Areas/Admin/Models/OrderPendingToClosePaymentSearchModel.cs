using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Areas.Admin.Models
{
    /// <summary>
    /// Represents an order  Pending To Close Payment search model
    /// </summary>
    public partial record OrderPendingToClosePaymentSearchModel : BaseSearchModel
    {
        #region Ctor

        public OrderPendingToClosePaymentSearchModel()
        {
            AvailableOrderStatuses = new List<SelectListItem>();
            AvailableVendorPaymentStatuses = new List<SelectListItem>();
            AvailableDriverPaymentStatuses = new List<SelectListItem>();
            AvailableStores = new List<SelectListItem>();
            AvailableVendors = new List<SelectListItem>();
            AvailableDrivers = new List<SelectListItem>();
            AvailableWarehouses = new List<SelectListItem>();
            AvailablePaymentMethods = new List<SelectListItem>();
            AvailableCountries = new List<SelectListItem>();
            OrderStatusIds = new List<int>();
            VendorPaymentStatusIds = new List<int>();
            DriverPaymentStatusIds = new List<int>();
        }

        #endregion

        #region Properties

        [NopResourceDisplayName("Admin.Orders.List.StartDate")]
        [UIHint("DateNullable")]
        public DateTime? StartDate { get; set; }

        [NopResourceDisplayName("Admin.Orders.List.EndDate")]
        [UIHint("DateNullable")]
        public DateTime? EndDate { get; set; }

        [NopResourceDisplayName("Admin.Orders.List.OrderStatus")]
        public IList<int> OrderStatusIds { get; set; }

        [NopResourceDisplayName("Plugin.Misc.DeliveryAppIntegrationBackend.Admin.Orders.Fields.VendorPaymentStatus")]
        public IList<int> VendorPaymentStatusIds { get; set; }

        [NopResourceDisplayName("Plugin.Misc.DeliveryAppIntegrationBackend.Admin.Orders.Fields.DriverPaymentStatus")]
        public IList<int> DriverPaymentStatusIds { get; set; }

        [NopResourceDisplayName("Admin.Orders.List.PaymentMethod")]
        public string PaymentMethodSystemName { get; set; }

        [NopResourceDisplayName("Admin.Orders.List.Store")]
        public int StoreId { get; set; }

        [NopResourceDisplayName("Admin.Orders.List.Vendor")]
        public int VendorId { get; set; }

        [NopResourceDisplayName("Plugin.Misc.DeliveryAppIntegrationBackend.OrdersPending.DriverCustomerEmail")]
        public int DriverId { get; set; }

        [NopResourceDisplayName("Admin.Orders.List.Warehouse")]
        public int WarehouseId { get; set; }

        [NopResourceDisplayName("Admin.Orders.List.Product")]
        public int ProductId { get; set; }

        [NopResourceDisplayName("Admin.Orders.List.BillingEmail")]
        public string BillingEmail { get; set; }

        [NopResourceDisplayName("Admin.Orders.List.BillingPhone")]
        public string BillingPhone { get; set; }

        public bool BillingPhoneEnabled { get; set; }

        [NopResourceDisplayName("Admin.Orders.List.BillingLastName")]
        public string BillingLastName { get; set; }

        [NopResourceDisplayName("Admin.Orders.List.BillingCountry")]
        public int BillingCountryId { get; set; }

        [NopResourceDisplayName("Admin.Orders.List.OrderNotes")]
        public string OrderNotes { get; set; }

        [NopResourceDisplayName("Admin.Orders.List.GoDirectlyToNumber")]
        public string GoDirectlyToCustomOrderNumber { get; set; }

        public bool IsLoggedInAsVendor { get; set; }

        public IList<SelectListItem> AvailableOrderStatuses { get; set; }

        public IList<SelectListItem> AvailableVendorPaymentStatuses { get; set; }

        public IList<SelectListItem> AvailableDriverPaymentStatuses { get; set; }

        public IList<SelectListItem> AvailableStores { get; set; }

        public IList<SelectListItem> AvailableVendors { get; set; }

        public IList<SelectListItem> AvailableDrivers { get; set; }

        public IList<SelectListItem> AvailableWarehouses { get; set; }

        public IList<SelectListItem> AvailablePaymentMethods { get; set; }

        public IList<SelectListItem> AvailableCountries { get; set; }

        public bool HideStoresList { get; set; }

        #endregion
    }
}
