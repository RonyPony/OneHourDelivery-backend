using Nop.Plugin.Widgets.Zoom.Domains.Enums;
using Nop.Plugin.Widgets.Zoom.Helpers;
using Nop.Services.Localization;
using System.Collections.Generic;

namespace Nop.Plugin.Widgets.Zoom.Domains
{
    /// <summary>
    /// Represents the model that provides the different resources used by Widgets.Zoom plugin.
    /// </summary>
    public static class ZoomPluginLocaleResources
    {
        /// <summary>
        /// Represents the local resources for displaying the plugin information in English (en-US).
        /// </summary>
        public static Dictionary<string, string> EnglishResources = new Dictionary<string, string>
        {
            #region Settings

            [$"{ZoomDefaults.ResourcesNamePrefix}.Settings.ZoomEnabled"] = "Zoom enabled",
            [$"{ZoomDefaults.ResourcesNamePrefix}.Settings.ZoomEnabled.Hint"] = "Check to enable a zoom on product detail main picture.",

            [$"{ZoomDefaults.ResourcesNamePrefix}.Settings.PicturesMobileCarouselEnabled"] = "Picture carousel enabled",
            [$"{ZoomDefaults.ResourcesNamePrefix}.Settings.PicturesMobileCarouselEnabled.Hint"] = "Check to enable the pictures to be displayed in a carousel on mobile devices.",

            [$"{ZoomDefaults.ResourcesNamePrefix}.Settings.ChangeMainPictureOnThumbsHoverEnabled"] = "Hover thumbs to change main picture enabled",
            [$"{ZoomDefaults.ResourcesNamePrefix}.Settings.ChangeMainPictureOnThumbsHoverEnabled.Hint"] = "Check to enable the main picture to change when thumbs are hovered.",

            [$"{ZoomDefaults.ResourcesNamePrefix}.Settings.PictureThumbsLocationId"] = "Picture thumbs location",
            [$"{ZoomDefaults.ResourcesNamePrefix}.Settings.PictureThumbsLocationId.Hint"] = "Indicates the position of the picture thumbs in relation to the main picture. When 'Left' is selected, picture thumbs will be displayed above the main picture on mobile devices, otherwise, picture thumbs will be displayed below.",

            #endregion

            #region Picture thumbs location enum

            [$"{NopLocalizationDefaults.EnumLocaleStringResourcesPrefix}{typeof(PictureThumbsLocation)}.{PictureThumbsLocation.Left}"] = "Left of main picture",
            [$"{NopLocalizationDefaults.EnumLocaleStringResourcesPrefix}{typeof(PictureThumbsLocation)}.{PictureThumbsLocation.Right}"] = "Right of main picture",

            #endregion
        };

        /// <summary>
        /// Represents the local resources for displaying the plugin information in Spanish (es-).
        /// </summary>
        public static Dictionary<string, string> SpanishResources = new Dictionary<string, string>
        {
            #region Settings

            [$"{ZoomDefaults.ResourcesNamePrefix}.Settings.ZoomEnabled"] = "Lupa habilitada",
            [$"{ZoomDefaults.ResourcesNamePrefix}.Settings.ZoomEnabled.Hint"] = "Marque para habilitar la lupa en la imágen principal del producto.",

            [$"{ZoomDefaults.ResourcesNamePrefix}.Settings.PicturesMobileCarouselEnabled"] = "Carousel de imágenes habilitado",
            [$"{ZoomDefaults.ResourcesNamePrefix}.Settings.PicturesMobileCarouselEnabled.Hint"] = "Marque para habilitar que las imágenes se muestren en un carousel cuando esté en un dispositivo móvil.",

            [$"{ZoomDefaults.ResourcesNamePrefix}.Settings.ChangeMainPictureOnThumbsHoverEnabled"] = "Cambiar imagen al hacer hover en las miniaturas habilitado",
            [$"{ZoomDefaults.ResourcesNamePrefix}.Settings.ChangeMainPictureOnThumbsHoverEnabled.Hint"] = "Marque para habilitar que la imágen principal cambie cuando se coloca el puntero del mouse sobre la miniatura.",

            [$"{ZoomDefaults.ResourcesNamePrefix}.Settings.PictureThumbsLocationId"] = "Ubicación de las miniaturas",
            [$"{ZoomDefaults.ResourcesNamePrefix}.Settings.PictureThumbsLocationId.Hint"] = "Indica la posición de las miniaturas de imágenes en relación a la imagen principal. Cuando se selecciona 'Izquierda', las miniaturas de las imágenes se mostrarán sobre la imagen principal en los dispositivos móviles; de lo contrario, las miniaturas de las imágenes se mostrarán debajo.",

            #endregion

            #region Picture thumbs location enum

            [$"{NopLocalizationDefaults.EnumLocaleStringResourcesPrefix}{typeof(PictureThumbsLocation)}.{PictureThumbsLocation.Left}"] = "Izquierda de la imágen principal",
            [$"{NopLocalizationDefaults.EnumLocaleStringResourcesPrefix}{typeof(PictureThumbsLocation)}.{PictureThumbsLocation.Right}"] = "Derecha de la imágen principal",

            #endregion
        };
    }
}
