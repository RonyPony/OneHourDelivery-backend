using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains.Enums;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;
using System;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Areas.Admin.Models
{
    /// <summary>
    /// Represents an entity model for <see cref="OrderPaymentCollectionStatus"/>.
    /// </summary>
    public sealed record OrderPaymentCollectionModel : BaseNopEntityModel
    {
        /// <summary>
        /// Indicates the order id.
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// Indicates the date of creation (should be the same as the date the order was accepted by the driver).
        /// </summary>
        [NopResourceDisplayName("Admin.Orders.Fields.CreatedOn")]
        public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// Indicates the customer id of the person who collected the order payment from the driver.
        /// </summary>
        [NopResourceDisplayName("Plugin.Misc.DeliveryAppIntegrationBackend.Admin.Orders.Fields.CollectedByCustomerId")]
        public int CollectedByCustomerId { get; set; }

        /// <summary>
        /// Indicates the customer name of the person who collected the order payment from the driver.
        /// </summary>
        public string CollectedByCustomerName { get; set; }

        /// <summary>
        /// Indicates the date of collection of the order payment.
        /// </summary>
        [NopResourceDisplayName("Plugin.Misc.DeliveryAppIntegrationBackend.Admin.Orders.Fields.CollectedOnUtc")]
        public DateTime CollectedOnUtc { get; set; }

        /// <summary>
        /// Indicates the payment collection status.
        /// </summary>
        [NopResourceDisplayName("Plugin.Misc.DeliveryAppIntegrationBackend.Admin.Orders.Fields.PaymentCollectionStatusId")]
        public PaymentCollectionStatus PaymentCollectionStatus { get; set; }
    }
}
