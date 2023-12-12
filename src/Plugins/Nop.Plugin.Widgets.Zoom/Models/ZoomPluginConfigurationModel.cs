using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;
using System.Collections.Generic;

namespace Nop.Plugin.Widgets.Zoom.Models
{
    /// <summary>
    /// Represents the required configuration model for the Widget.Zoom plugin.
    /// </summary>
    public sealed class ZoomPluginConfigurationModel : BaseNopEntityModel
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ZoomPluginConfigurationModel"/>.
        /// </summary>
        public ZoomPluginConfigurationModel()
        {
            AvailableThumbsLocation = new List<SelectListItem>();
        }

        /// <summary>
        /// Indicates if the zoom is enabled.
        /// </summary>
        [NopResourceDisplayName("Plugin.Widgets.Zoom.Settings.ZoomEnabled")]
        public bool ZoomEnabled { get; set; }

        /// <summary>
        /// Indicates if the pictures will be displayed in a carousel when mobile device detected.
        /// </summary>
        [NopResourceDisplayName("Plugin.Widgets.Zoom.Settings.PicturesMobileCarouselEnabled")]
        public bool PicturesMobileCarouselEnabled { get; set; }

        /// <summary>
        /// Indicates if the main picture changes when picture thumbs are hovered.
        /// </summary>
        [NopResourceDisplayName("Plugin.Widgets.Zoom.Settings.ChangeMainPictureOnThumbsHoverEnabled")]
        public bool ChangeMainPictureOnThumbsHoverEnabled { get; set; }

        /// <summary>
        /// Indicates the id of the picture thumbs selected location.
        /// </summary>
        [NopResourceDisplayName("Plugin.Widgets.Zoom.Settings.PictureThumbsLocationId")]
        public int PictureThumbsLocationId { get; set; }

        /// <summary>
        /// Indicates the available picture thumbs location.
        /// </summary>
        public List<SelectListItem> AvailableThumbsLocation { get; set; }
    }
}
