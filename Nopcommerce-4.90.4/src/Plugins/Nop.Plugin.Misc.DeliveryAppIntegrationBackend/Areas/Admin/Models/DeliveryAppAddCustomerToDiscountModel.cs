using Nop.Web.Framework.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Areas.Admin.Models
{
    /// <summary>
    /// Represents a customer model to add to the discount
    /// </summary>
    public partial record DeliveryAppAddCustomerToDiscountModel : BaseNopModel
    {
        #region Ctor

        /// <summary>
        /// Creates an instance of <see cref="DeliveryAppAddCustomerToDiscountModel"/>
        /// </summary>
        public DeliveryAppAddCustomerToDiscountModel()
        {
            SelectedCustomerIds = new List<int>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Represents the discount id.
        /// </summary>
        public int DiscountId { get; set; }

        /// <summary>
        /// Represents the customers id assigned to discount.
        /// </summary>
        public IList<int> SelectedCustomerIds { get; set; }

        #endregion
    }
}
