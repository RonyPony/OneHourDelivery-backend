using Nop.Web.Framework.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Areas.Admin.Models
{
    /// <summary>
    /// Represent a page list model for the data of customers review.
    /// </summary>
    public sealed record DeliveryAppCustomerReviewList : BasePagedListModel<DeliveryAppCustomerReview>
    {
    }
}
