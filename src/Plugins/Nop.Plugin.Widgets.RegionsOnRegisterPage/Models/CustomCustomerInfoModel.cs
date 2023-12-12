using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Mvc.ModelBinding;
using Nop.Web.Models.Customer;
using System.Collections.Generic;

namespace Nop.Plugin.Widgets.RegionsOnRegisterPage.Models
{
    /// <summary>
    /// Represents the model to work with the Region information.
    /// </summary>
    public class CustomCustomerInfoModel : CustomerInfoModel
    {
        /// <summary>
        /// Represents the model to work with the Region information.
        /// </summary>
        [NopResourceDisplayName("Plugin.Widgets.RegionsOnRegisterPage.Title")]
        public int RegionID { get; set; }

        /// <summary>
        /// Represent a list of regions
        /// </summary>
        [NopResourceDisplayName("Plugin.Widgets.RegionsOnRegisterPage.Title")]
        public IList<SelectListItem> Regions { get; set; }
    }
}