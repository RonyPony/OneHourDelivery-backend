using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Areas.Admin.Models
{
    /// <summary>
    /// Represent a model for the data of vendor review.
    /// </summary>
    public partial record DeliveryAppVendorReviewValoration : BaseNopModel
    {
        /// <summary>
        /// Get or set vendor's name.
        /// </summary>
        [NopResourceDisplayName("Plugin.Misc.DeliveryAppIntegrationBackend.Review.Name")]
        public string VendorName { get; set; }

        /// <summary>
        /// Get or set vendor's count valoration
        /// </summary>
        [NopResourceDisplayName("Plugin.Misc.DeliveryAppIntegrationBackend.Review.Like")]
        public int LikeCount { get; set;}

        /// <summary>
        /// Get or set vendor's count valoration
        /// </summary>
        [NopResourceDisplayName("Plugin.Misc.DeliveryAppIntegrationBackend.Review.Like.Dislike")]
        public int DislikeCount { get; set; }

        /// <summary>
        /// Get or set vendor's rating valoration.
        /// </summary>
        [NopResourceDisplayName("Plugin.Misc.DeliveryAppIntegrationBackend.Review.ApprovalPorcent")]
        public float ApprovalPorcent { get; set; }

    }
}
