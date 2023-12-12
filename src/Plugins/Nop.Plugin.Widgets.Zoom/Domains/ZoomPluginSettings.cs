using Nop.Core;
using Nop.Core.Configuration;
using Nop.Plugin.Widgets.Zoom.Domains.Enums;

namespace Nop.Plugin.Widgets.Zoom.Domains
{
    /// <summary>
    /// Represents the required settings for the Widget.Zoom plugin.
    /// </summary>
    public sealed class ZoomPluginSettings : BaseEntity, ISettings
    {
        /// <summary>
        /// Indicates if the zoom is enabled.
        /// </summary>
        public bool ZoomEnabled { get; set; }

        /// <summary>
        /// Indicates if the pictures will be displayed in a carousel when mobile device detected.
        /// </summary>
        public bool PicturesMobileCarouselEnabled { get; set; }

        /// <summary>
        /// Indicates if the main picture changes when picture thumbs are hovered.
        /// </summary>
        public bool ChangeMainPictureOnThumbsHoverEnabled { get; set; }

        /// <summary>
        /// Indicates the id of the picture thumbs selected location.
        /// </summary>
        public int PictureThumbsLocationId { get; set; }

        /// <summary>
        /// Indicates the picture thumbs selected location.
        /// </summary>
        public PictureThumbsLocation PictureThumbsLocation
        {
            get { return (PictureThumbsLocation)PictureThumbsLocationId; }
            set { PictureThumbsLocationId = (int)value; }
        }
    }
}
