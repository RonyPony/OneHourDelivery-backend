using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;
using System.Collections.Generic;

namespace Nop.Plugin.Payments.Banrural.Models
{
    /// <summary>
    /// Represents the model used for configuring the Banrural plug-in.
    /// </summary>
    public sealed class BanruralConfigurationModel : BaseNopModel
    {
        /// <summary>
        /// Initializes a new instance of BanruralConfigurationModel class.
        /// </summary>
        public BanruralConfigurationModel() => Locale = new List<SelectListItem>();

        /// <summary>
        /// Represents the Key ID of the Banrural payment page request.
        /// </summary>
        [NopResourceDisplayName("Plugins.Payments.Banrural.Fields.KeyID")]
        public string KeyID { get; set; }

        /// <summary>
        /// Represents the main URL of the Banrural payment page request.
        /// </summary>
        [NopResourceDisplayName("Plugins.Payments.Banrural.Fields.Url")]
        public string Url { get; set; }

        /// <summary>
        /// Represents the cancel URL of the Banrural payment page request.
        /// It is used when the order is canceled.
        /// </summary>
        [NopResourceDisplayName("Plugins.Payments.Banrural.Fields.CancelUrl")]    
        public string CancelUrl { get; set; }

        /// <summary>
        /// Represents the Complete URL of the Banrural payment page request.
        /// It is used when the order is successfully. 
        /// </summary>
        [NopResourceDisplayName("Plugins.Payments.Banrural.Fields.CompleteUrl")]
        public string CompleteUrl { get; set; }

        /// <summary>
        /// Represents the Callback URL of the Banrural payment page request.
        /// This Url serves as a webhook, an endpoint htat will be called after a payment success. 
        /// </summary>
        [NopResourceDisplayName("Plugins.Payments.Banrural.Fields.CallbackUrl")]
        public string CallbackUrl { get; set; }

        /// <summary>
        /// Represents the selected language used for displaying the Banrural payment page.
        /// </summary>
        [NopResourceDisplayName("Plugins.Payments.Banrural.Fields.Locale")]
        public int LocaleId { get; set; }

        /// <summary>
        /// Represents the languages available for displaying the Banrural payment page.
        /// </summary>
        public IList<SelectListItem> Locale { get; set; }

        /// <summary>
        /// Represents the selected status an order will has after processing the payment.
        /// </summary>
        public IList<SelectListItem> OrderStatus { get; set; }

        /// <summary>
        /// Represents the selected status an order will has after processing the payment.
        /// </summary>
        [NopResourceDisplayName("Plugins.Payments.Banrural.Fields.OrderStatus")]
        public int OrderStatusId { get; set; }

        /// <summary>
        /// Represents if an order will be mark as paid or not after processing the payment.
        /// </summary>
        [NopResourceDisplayName("Plugins.Payments.Banrural.Fields.MarkAsPaid")]
        public bool MarkAsPaid { get; set; }
    }
}