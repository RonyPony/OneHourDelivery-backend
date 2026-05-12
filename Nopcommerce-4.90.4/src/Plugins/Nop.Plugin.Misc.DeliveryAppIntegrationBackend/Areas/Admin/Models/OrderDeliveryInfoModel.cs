using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains.Enums;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;
using System;
using System.Collections.Generic;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Areas.Admin.Models
{
    /// <summary>
    /// Represents a model for the delivery information of an order.
    /// </summary>
    public sealed record OrderDeliveryInfoModel : BaseNopEntityModel
    {
        /// <summary>
        /// Indicates the order id.
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// Indicates the date and time when the order was ready for pickup.
        /// </summary>
        [NopResourceDisplayName("Plugin.Misc.DeliveryAppIntegrationBackend.Admin.Orders.Fields.AwaitingForMessengerDate")]
        public DateTime? AwaitingForMessengerDate { get; set; }

        /// <summary>
        /// Indicates the date and time when the order was accepted by a messenger.
        /// </summary>
        [NopResourceDisplayName("Plugin.Misc.DeliveryAppIntegrationBackend.Admin.Orders.Fields.AcceptedByMessengerDate")]
        public DateTime? AcceptedByMessengerDate { get; set; }

        /// <summary>
        /// Message to indicate why store declined the order.
        /// </summary>
        [NopResourceDisplayName("Plugin.Misc.DeliveryAppIntegrationBackend.Admin.Orders.Fields.MessageToDeclinedOrder")]
        public string MessageToDeclinedOrder { get; set; }

        /// <summary>
        /// Indicates the date and time when the order was retrieved by the messenger from the store.
        /// </summary>
        [NopResourceDisplayName("Plugin.Misc.DeliveryAppIntegrationBackend.Admin.Orders.Fields.DeliveryInProgressDate")]
        public DateTime? DeliveryInProgressDate { get; set; }

        /// <summary>
        /// Indicates the date and time when the order was delivered.
        /// </summary>
        [NopResourceDisplayName("Plugin.Misc.DeliveryAppIntegrationBackend.Admin.Orders.Fields.DeliveredDate")]
        public DateTime? DeliveredDate { get; set; }

        /// <summary>
        /// Indicates the date and time when the order was declined by the store.
        /// </summary>
        [NopResourceDisplayName("Plugin.Misc.DeliveryAppIntegrationBackend.Admin.Orders.Fields.DeclinedByStoreDate")]
        public DateTime? DeclinedByStoreDate { get; set; }

        /// <summary>
        /// Indicates the delivery status id of the order.
        /// </summary>
        [NopResourceDisplayName("Plugin.Misc.DeliveryAppIntegrationBackend.Admin.Orders.Fields.DeliveryStatusId")]
        public int DeliveryStatusId { get; set; }

        /// <summary>
        /// Indicates the delivery status of the order.
        /// </summary>
        public DeliveryStatus DeliveryStatus
        {
            get => (DeliveryStatus)DeliveryStatusId;
            set => DeliveryStatusId = (int)value;
        }

        /// <summary>
        /// Indicates the id of the driver assigned to the order.
        /// </summary>
        [NopResourceDisplayName("Plugin.Misc.DeliveryAppIntegrationBackend.Admin.Orders.Fields.DriverId")]
        public int DriverId { get; set; }

        /// <summary>
        /// A list of available drivers.
        /// </summary>
        public IList<SelectListItem> AvailableDrivers { get; set; }

    }
}
