using Nop.Plugin.Widgets.GoogleMapsIntegration.Helpers;
using System.Collections.Generic;

namespace Nop.Plugin.Widgets.GoogleMapsIntegration.Domains
{
    /// <summary>
    /// Represents the model that provides the different resources used by Google Maps Integration plugin.
    /// </summary>
    public static class GoogleMapsIntegrationLocaleResources
    {
        /// <summary>
        /// Represents the local resources for displaying the plugin information in English (en-US).
        /// </summary>
        public static Dictionary<string, string> EnglishResources = new Dictionary<string, string>
        {
            #region Instructions

            [$"{Defaults.ResourcesNamePrefix}.Instructions"] = @"
                <p>Google Maps Integration allows you the possibility of using some of the Google Maps Platform APIs into your store.
                <br />
                <br />
                Follow the next steps to enable Google Maps integration:
                <br />
                <ul>
                    <li><a href=https://cloud.google.com/maps-platform/#get-started target=_blank>Create a Google Cloud Platform account</a> and follow the tutorials to create a project and set up your billing account.</li>
                    <li>Then, you'll need to set up an API key with access to Places API, Maps JavaScript API and Geocoding API for this plugin to work correctly.</li>
                    <li>Once you're done setting up your API key, copy the API key into the 'Google API Key' box below.</li>
                    <li>Click the 'Save' button below and Google Maps Platform will be integrated into your store.</li>
                </ul>
                <br />
                </p>",

            #endregion

            #region Fields

            [$"{Defaults.ResourcesNamePrefix}.Fields.ApiKey"] = "Google API Key",
            [$"{Defaults.ResourcesNamePrefix}.Fields.ApiKey.Hint"] = "Unique identifier that authenticates requests associated to Google Maps APIs.",
            [$"{Defaults.ResourcesNamePrefix}.Fields.ApiKey.Required"] = "Google API Key is required.",

            [$"{Defaults.ResourcesNamePrefix}.Fields.AddressAutocompleteEnabled"] = "Enable address autocomplete",
            [$"{Defaults.ResourcesNamePrefix}.Fields.AddressAutocompleteEnabled.Hint"] = "Check to indicate that address autocomplete search box is enabled in address forms.",

            [$"{Defaults.ResourcesNamePrefix}.Fields.SelectAddresFromMapEnabled"] = "Enable pick address from map",
            [$"{Defaults.ResourcesNamePrefix}.Fields.SelectAddresFromMapEnabled.Hint"] = "Check to indicate that Google Maps is enabled in address forms. This allows the user to pick the address from the map.",

            [$"{Defaults.ResourcesNamePrefix}.Fields.GetAddressFromCoordinatesEnabled"] = "Enable geocoding",
            [$"{Defaults.ResourcesNamePrefix}.Fields.GetAddressFromCoordinatesEnabled.Hint"] = "Check to indicate that addresses' latitude and longitude geocoding is enabled in address forms.",

            [$"{Defaults.ResourcesNamePrefix}.Fields.Latitude"] = "Latitude",
            [$"{Defaults.ResourcesNamePrefix}.Fields.Latitude.Hint"] = "Geographic coordinate that specifies the north–south position of a point on the Earth's surface. This field supports a max of 7 decimal numbers after decimal point.",
            [$"{Defaults.ResourcesNamePrefix}.Fields.Latitude.Required"] = "Latitude is required.",
            [$"{Defaults.ResourcesNamePrefix}.Fields.Latitude.NotValid"] = "Latitude not valid. Range from -999.9999999 to 999.9999999.",

            [$"{Defaults.ResourcesNamePrefix}.Fields.Longitude"] = "Longitude",
            [$"{Defaults.ResourcesNamePrefix}.Fields.Longitude.Hint"] = "Geographic coordinate that specifies the east–west position of a point on the Earth's surface. This field supports a max of 7 decimal numbers after decimal point.",
            [$"{Defaults.ResourcesNamePrefix}.Fields.Longitude.Required"] = "Longitude is required.",
            [$"{Defaults.ResourcesNamePrefix}.Fields.Longitude.NotValid"] = "Longitude is not valid. Range from -999.9999999 to 999.9999999.",

            [$"{Defaults.ResourcesNamePrefix}.Fields.MapBoundariesEnabled"] = "Enable geographic boundaries",
            [$"{Defaults.ResourcesNamePrefix}.Fields.MapBoundariesEnabled.Hint"] = "Check to indicate that the map and the address search will be bond to specific geographic boundaries.",

            [$"{Defaults.ResourcesNamePrefix}.Fields.NorthBound"] = "Northbound",
            [$"{Defaults.ResourcesNamePrefix}.Fields.NorthBound.Hint"] = "North end point of the limits.",
            [$"{Defaults.ResourcesNamePrefix}.Fields.NorthBound.Required"] = "The northbound is required.",

            [$"{Defaults.ResourcesNamePrefix}.Fields.SouthBound"] = "Southbound",
            [$"{Defaults.ResourcesNamePrefix}.Fields.SouthBound.Hint"] = "South end point of the limits.",
            [$"{Defaults.ResourcesNamePrefix}.Fields.SouthBound.Required"] = "The southbound is required.",

            [$"{Defaults.ResourcesNamePrefix}.Fields.WestBound"] = "Westbound",
            [$"{Defaults.ResourcesNamePrefix}.Fields.WestBound.Hint"] = "West end point of the limits.",
            [$"{Defaults.ResourcesNamePrefix}.Fields.WestBound.Required"] = "The westbound is required.",

            [$"{Defaults.ResourcesNamePrefix}.Fields.EastBound"] = "Eastbound",
            [$"{Defaults.ResourcesNamePrefix}.Fields.EastBound.Hint"] = "East end point of the limits.",
            [$"{Defaults.ResourcesNamePrefix}.Fields.EastBound.Required"] = "The eastbound is required.",

            [$"{Defaults.ResourcesNamePrefix}.Fields.DefaultLatLngEnabled"] = "Enable default place",
            [$"{Defaults.ResourcesNamePrefix}.Fields.DefaultLatLngEnabled.Hint"] = "Check to enable a default place to be marked when map is loaded with no address.",

            [$"{Defaults.ResourcesNamePrefix}.Fields.DefaultLatitude"] = "Place latitude",
            [$"{Defaults.ResourcesNamePrefix}.Fields.DefaultLatitude.Hint"] = "The latitude for the default place.",
            [$"{Defaults.ResourcesNamePrefix}.Fields.DefaultLatitude.Required"] = "The default place's latitude is required.",

            [$"{Defaults.ResourcesNamePrefix}.Fields.DefaultLongitude"] = "Place longitude",
            [$"{Defaults.ResourcesNamePrefix}.Fields.DefaultLongitude.Hint"] = "The longitude for the default place.",
            [$"{Defaults.ResourcesNamePrefix}.Fields.DefaultLongitude.Required"] = "The default place's longitude is required.",

            [$"{Defaults.ResourcesNamePrefix}.Text.Marker.Tip"] = @"<b>Tip:</b> You can drag and drop the marker of the map to change the address.",

            #endregion

            #region Inputs

            [$"{Defaults.ResourcesNamePrefix}.Inputs.AddressAutocomplete"] = "Search address",
            [$"{Defaults.ResourcesNamePrefix}.Inputs.AddressAutocomplete.Hint"] = "Use this field to search for an address. When you find and select the address you're looking for, it will automatically fill out some of the address fields bellow.",

            [$"{Defaults.ResourcesNamePrefix}.Inputs.GeocoordinatesSearch"] = "Search geocoordinates",
            [$"{Defaults.ResourcesNamePrefix}.Inputs.GeocoordinatesSearch.Hint"] = "Use this field to search for an address by it's geo coordinates. This will automatically fill out some of the address fields bellow.",
            [$"{Defaults.ResourcesNamePrefix}.Inputs.GeocoordinatesSearch.Placeholder"] = "E.g.: lat:-25.363, lng:131.044",

            [$"{Defaults.ResourcesNamePrefix}.Inputs.Search"] = "Search"

            #endregion
        };

        /// <summary>
        /// Represents the local resources for displaying the plugin information in Spanish (es-).
        /// </summary>
        public static Dictionary<string, string> SpanishResources = new Dictionary<string, string>
        {
            #region Instructions

            [$"{Defaults.ResourcesNamePrefix}.Instructions"] = @"
                <p>Google Maps Integration le permite la posibilidad de utilizar algunas de las APIs de Google Maps Platform en su tienda.
                <br />
                <br />
                Siga los siguientes pasos para habilitar Google Maps integration:
                <br />
                <ul>
                    <li><a href=https://cloud.google.com/maps-platform/#get-started target=_blank>Crea una cuenta de Google Cloud Platform</a> y siga los tutoriales para crear un proyecto y configurar su cuenta de facturación.</li>
                    <li>Luego, deberá configurar un API Key con acceso a Places API, Maps JavaScript API y Geocoding API para que este plugin funcione correctamente..</li>
                    <li>Una vez que haya terminado de configurar su API Key, cópiela en el cuadro 'API Key' qué está más abajo.</li>
                    <li>Haga clic en el botón 'Guardar' y listo, Google Maps Platform se integrará en su tienda.</li>
                </ul>
                <br />
                </p>",

            #endregion

            #region Fields

            [$"{Defaults.ResourcesNamePrefix}.Fields.ApiKey"] = "API Key",
            [$"{Defaults.ResourcesNamePrefix}.Fields.ApiKey.Hint"] = "Identificador único que autentica las solicitudes asociadas a las API de Google Maps.",
            [$"{Defaults.ResourcesNamePrefix}.Fields.ApiKey.Required"] = "El API Key es requerida.",

            [$"{Defaults.ResourcesNamePrefix}.Fields.AddressAutocompleteEnabled"] = "Habilitar el autocompletado de direcciones",
            [$"{Defaults.ResourcesNamePrefix}.Fields.AddressAutocompleteEnabled.Hint"] = "Marque para indicar que el cuadro de búsqueda de autocompletar de direcciones está habilitado en los formularios de direcciones.",

            [$"{Defaults.ResourcesNamePrefix}.Fields.SelectAddresFromMapEnabled"] = "Habilitar selección de dirección desde el mapa",
            [$"{Defaults.ResourcesNamePrefix}.Fields.SelectAddresFromMapEnabled.Hint"] = "Marque para indicar que Google Maps está habilitado en los formularios de dirección. Esto permite al usuario seleccionar la dirección desde el mapa.",

            [$"{Defaults.ResourcesNamePrefix}.Fields.GetAddressFromCoordinatesEnabled"] = "Habilitar codificación geográfica",
            [$"{Defaults.ResourcesNamePrefix}.Fields.GetAddressFromCoordinatesEnabled.Hint"] = "Marque para indicar que la codificación geográfica de latitud y longitud de las direcciones está habilitada en los formularios de direcciones.",

            [$"{Defaults.ResourcesNamePrefix}.Fields.Latitude"] = "Latitud",
            [$"{Defaults.ResourcesNamePrefix}.Fields.Latitude.Hint"] = "Coordenada geográfica que especifica la posición norte-sur de un punto en la superficie de la Tierra. Este campo admite un máximo de 7 números decimales después del punto decimal.",
            [$"{Defaults.ResourcesNamePrefix}.Fields.Latitude.Required"] = "La Latitud es requerida.",
            [$"{Defaults.ResourcesNamePrefix}.Fields.Latitude.NotValid"] = "La Latitud es inválida. Valores aceptados desde -999.9999999 hasta 999.9999999.",

            [$"{Defaults.ResourcesNamePrefix}.Fields.Longitude"] = "Longitud",
            [$"{Defaults.ResourcesNamePrefix}.Fields.Longitude.Hint"] = "Coordenada geográfica que especifica la posición este-oeste de un punto en la superficie de la Tierra. Este campo admite un máximo de 7 números decimales después del punto decimal.",
            [$"{Defaults.ResourcesNamePrefix}.Fields.Longitude.Required"] = "La Longitud es requerida.",
            [$"{Defaults.ResourcesNamePrefix}.Fields.Longitude.NotValid"] = "La Longitud es inválida. Valores aceptados desde -999.9999999 hasta 999.9999999.",

            [$"{Defaults.ResourcesNamePrefix}.Fields.MapBoundariesEnabled"] = "Habilitar límites geográficos",
            [$"{Defaults.ResourcesNamePrefix}.Fields.MapBoundariesEnabled.Hint"] = "Marque para indicar que el mapa y la búsqueda de direcciones estarán vinculados a límites geográficos específicos.",

            [$"{Defaults.ResourcesNamePrefix}.Fields.NorthBound"] = "Dirección norte",
            [$"{Defaults.ResourcesNamePrefix}.Fields.NorthBound.Hint"] = "Extremo norte de los límites.",
            [$"{Defaults.ResourcesNamePrefix}.Fields.NorthBound.Required"] = "La dirección norte es requerida.",

            [$"{Defaults.ResourcesNamePrefix}.Fields.SouthBound"] = "Dirección sur",
            [$"{Defaults.ResourcesNamePrefix}.Fields.SouthBound.Hint"] = "Extremo sur de los límites.",
            [$"{Defaults.ResourcesNamePrefix}.Fields.SouthBound.Required"] = "La dirección sur es requerida.",

            [$"{Defaults.ResourcesNamePrefix}.Fields.WestBound"] = "Dirección oeste",
            [$"{Defaults.ResourcesNamePrefix}.Fields.WestBound.Hint"] = "Extremo oeste de los límites.",
            [$"{Defaults.ResourcesNamePrefix}.Fields.WestBound.Required"] = "La dirección oeste es requerida.",

            [$"{Defaults.ResourcesNamePrefix}.Fields.EastBound"] = "Dirección este",
            [$"{Defaults.ResourcesNamePrefix}.Fields.EastBound.Hint"] = "Extremo este de los límites.",
            [$"{Defaults.ResourcesNamePrefix}.Fields.EastBound.Required"] = "La dirección este es requerida.",

            [$"{Defaults.ResourcesNamePrefix}.Fields.DefaultLatLngEnabled"] = "Habilitar lugar predeterminado",
            [$"{Defaults.ResourcesNamePrefix}.Fields.DefaultLatLngEnabled.Hint"] = "Marque para permitir que se marque un lugar predeterminado en el mapa cuando este se carga sin dirección.",

            [$"{Defaults.ResourcesNamePrefix}.Fields.DefaultLatitude"] = "Latitud del lugar",
            [$"{Defaults.ResourcesNamePrefix}.Fields.DefaultLatitude.Hint"] = "La latitud del lugar predeterminado.",
            [$"{Defaults.ResourcesNamePrefix}.Fields.DefaultLatitude.Required"] = "La latitud del lugar predeterminado es requerida.",

            [$"{Defaults.ResourcesNamePrefix}.Fields.DefaultLongitude"] = "Longitud del lugar",
            [$"{Defaults.ResourcesNamePrefix}.Fields.DefaultLongitude.Hint"] = "La longitud del lugar predeterminado.",
            [$"{Defaults.ResourcesNamePrefix}.Fields.DefaultLongitude.Required"] = "La longitud del lugar predeterminado es requerida.",

            [$"{Defaults.ResourcesNamePrefix}.Text.Marker.Tip"] = @"<b>Tip:</b> Puede arrastrar y soltar el marcador del mapa para cambiar la dirección.",

            #endregion

            #region Inputs

            [$"{Defaults.ResourcesNamePrefix}.Inputs.AddressAutocomplete"] = "Buscar dirección",
            [$"{Defaults.ResourcesNamePrefix}.Inputs.AddressAutocomplete.Hint"] = "Utilice este campo para buscar una dirección. Cuando encuentre y seleccione la dirección que está buscando, automáticamente se completarán algunos de los campos de dirección más abajo.",

            [$"{Defaults.ResourcesNamePrefix}.Inputs.GeocoordinatesSearch"] = "Coordenadas geográficas",
            [$"{Defaults.ResourcesNamePrefix}.Inputs.GeocoordinatesSearch.Hint"] = "Utilice este campo para buscar una dirección por sus coordenadas geográficas. Esto completará de forma automática algunos de los campos de dirección más abajo.",
            [$"{Defaults.ResourcesNamePrefix}.Inputs.GeocoordinatesSearch.Placeholder"] = "Ej.: lat:-25.363, lng:131.044",

            [$"{Defaults.ResourcesNamePrefix}.Inputs.Search"] = "Buscar"

            #endregion
        };
    }
}
