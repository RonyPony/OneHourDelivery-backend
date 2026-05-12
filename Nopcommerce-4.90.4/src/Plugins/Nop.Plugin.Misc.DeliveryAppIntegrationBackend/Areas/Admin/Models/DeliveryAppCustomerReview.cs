using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Areas.Admin.Models
{
    /// <summary>
    /// Represent a model for the data of customers review.
    /// </summary>
    public partial record DeliveryAppCustomerReview : BaseNopModel
    {
        /// <summary>
        /// Get or set customer's name
        /// </summary>
        [NopResourceDisplayName("Plugin.Misc.DeliveryAppIntegrationBackend.Review.Name")]
        public string CustomerName { get; set; }

        /// <summary>
        /// Get or set customer's like count valoration
        /// </summary>
        [NopResourceDisplayName("Plugin.Misc.DeliveryAppIntegrationBackend.Review.Like")]
        public int LikeCount { get; set; }

        /// <summary>
        /// Get or set customer's dislike count valoration.
        /// </summary>
        [NopResourceDisplayName("Plugin.Misc.DeliveryAppIntegrationBackend.Review.Dislike")]
        public int DislikeCount { get; set; }

        /// <summary>
        /// Get or set Customer's rating valoration.
        /// </summary>
        [NopResourceDisplayName("Plugin.Misc.DeliveryAppIntegrationBackend.Review.ApprovalPorcent")]
        public float ApprovalPorcent { get; set; }
    }
}
