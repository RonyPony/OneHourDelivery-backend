using Nop.Plugin.Widgets.GoogleMapsIntegrationMultientrega.Helpers;
using System.Collections.Generic;

namespace Nop.Plugin.Widgets.GoogleMapsIntegrationMultientrega.Domains
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

            [$"{Defaults.ResourcesNamePrefix}.GoogleMaps.Instructions"] = @"
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

            [$"{Defaults.ResourcesNamePrefix}.Multientrega.Instructions"] = @"
                <p>Multientrega Manual allows you to use the Web Service Multientrega Client as shipping method inside the store.
                <br />
                <br />
                Make sure to follow the next indications to configure correctly this service:
                <br />
                <ul>
                    <li>Hire some <a href=https://multientregapanama.com/servicios/ target=_blank>Multientrega service to get a account.</a></li>
                    <li>Fill in the fields of this form.</li>
                    <li>Go to <i>System >> Scheduled task</i> and execute the schedule with name <b>Multientrega's Structure Sync Task</b> to load the territorial structure.</li>
                </ul>
                <br />
                </p>
            ",
            
            [$"{Defaults.ResourcesNamePrefix}.GoogleMaps.Panel.Title"] = "Google Maps Integration",
            [$"{Defaults.ResourcesNamePrefix}.Multientrega.Panel.Title"] = "Multientrega Manual",
            [$"{Defaults.ResourcesNamePrefix}.Multientrega.Provinces.Panel.Title"] = "Provinces structure",
            [$"{Defaults.ResourcesNamePrefix}.Multientrega.Districts.Panel.Title"] = "Districts structure",
            [$"{Defaults.ResourcesNamePrefix}.Multientrega.Townships.Panel.Title"] = "Townships structure",
            [$"{Defaults.ResourcesNamePrefix}.Multientrega.Neighborhoods.Panel.Title"] = "Neighborhoods structure",

            #endregion

            #region Fields

            [$"{Defaults.ResourcesNamePrefix}.Fields.ApiKey"] = "Google API Key",
            [$"{Defaults.ResourcesNamePrefix}.Fields.ApiKey.Hint"] = "Unique identifier that authenticates requests associated to Google Maps APIs.",
            [$"{Defaults.ResourcesNamePrefix}.Fields.ApiKey.Required"] = "Google API Key is required.",

            [$"{Defaults.ResourcesNamePrefix}.Fields.DisplayLatLngFields"] = "Display latitude and longitude fields",
            [$"{Defaults.ResourcesNamePrefix}.Fields.DisplayLatLngFields.Hint"] = "Check to indicates if the latitude and longitude fields will be shown on address form.",

            [$"{Defaults.ResourcesNamePrefix}.Fields.LatLngFieldsRequired"] = "Latitude and longitude fields required",
            [$"{Defaults.ResourcesNamePrefix}.Fields.LatLngFieldsRequired.Hint"] = "Check to Indicate if the latitude and longitude fields are required.",

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

            [$"{Defaults.ResourcesNamePrefix}.Fields.Email"] = "Email",
            [$"{Defaults.ResourcesNamePrefix}.Fields.Email.Hint"] = "User identifier used to log into Multientrega's API.",
            [$"{Defaults.ResourcesNamePrefix}.Fields.Email.Required"] = "The email is required.",

            [$"{Defaults.ResourcesNamePrefix}.Fields.Password"] = "Password",
            [$"{Defaults.ResourcesNamePrefix}.Fields.Password.Hint"] = "Password for the user.",
            [$"{Defaults.ResourcesNamePrefix}.Fields.Password.Required"] = "The password is required.",

            [$"{Defaults.ResourcesNamePrefix}.Fields.BaseUrl"] = "Base URL",
            [$"{Defaults.ResourcesNamePrefix}.Fields.BaseUrl.Hint"] = "URL that goes before each function from Multientrega's API.",
            [$"{Defaults.ResourcesNamePrefix}.Fields.BaseUrl.Required"] = "The base URL is required.",

            [$"{Defaults.ResourcesNamePrefix}.Fields.BranchOffice"] = "Branch office",
            [$"{Defaults.ResourcesNamePrefix}.Fields.BranchOffice.Hint"] = "The branch office ID to use when requesting shipping option tax rate from Multientrega's API.",
            [$"{Defaults.ResourcesNamePrefix}.Fields.BranchOffice.Required"] = "The branch office is required.",

            [$"{Defaults.ResourcesNamePrefix}.Fields.Nit"] = "TIN",
            [$"{Defaults.ResourcesNamePrefix}.Fields.Nit.Hint"] = "Tax identification number.",
            [$"{Defaults.ResourcesNamePrefix}.Fields.Nit.Required"] = "The NIT is required.",

            [$"{Defaults.ResourcesNamePrefix}.Fields.ShippingOptionDescription"] = "Description",
            [$"{Defaults.ResourcesNamePrefix}.Fields.ShippingOptionDescription.Hint"] = "A description for the shipping option.",

            [$"{Defaults.ResourcesNamePrefix}.Fields.ProvinceId"] = "Province",
            [$"{Defaults.ResourcesNamePrefix}.Fields.ProvinceId.Required"] = "The province is required.",

            [$"{Defaults.ResourcesNamePrefix}.Fields.DistrictId"] = "District",
            [$"{Defaults.ResourcesNamePrefix}.Fields.DistrictId.Required"] = "The district is required.",

            [$"{Defaults.ResourcesNamePrefix}.Fields.TownshipId"] = "Township",
            [$"{Defaults.ResourcesNamePrefix}.Fields.TownshipId.Required"] = "The township is required.",

            [$"{Defaults.ResourcesNamePrefix}.Fields.NeighborhoodId"] = "Neighnorhood",
            [$"{Defaults.ResourcesNamePrefix}.Fields.NeighborhoodId.Required"] = "The neighborhood is required.",

            [$"{Defaults.ResourcesNamePrefix}.Fields.Neighborhood.Fullname"] = "Full name",
            [$"{Defaults.ResourcesNamePrefix}.Fields.Neighborhood.Status"] = "Status",
            [$"{Defaults.ResourcesNamePrefix}.Fields.Neighborhood.LocXLon"] = "LocXLon",
            [$"{Defaults.ResourcesNamePrefix}.Fields.Neighborhood.LocYLat"] = "LocYLat",
            [$"{Defaults.ResourcesNamePrefix}.Fields.Neighborhood.DateCrea"] = "Created date",
            [$"{Defaults.ResourcesNamePrefix}.Fields.Neighborhood.UsrCrea"] = "Creator user",
            [$"{Defaults.ResourcesNamePrefix}.Fields.Neighborhood.DateModifica"] = "Modificated date",
            [$"{Defaults.ResourcesNamePrefix}.Fields.Neighborhood.UsrModifica"] = "Modifier user",

            #endregion

            #region Inputs

            [$"{Defaults.ResourcesNamePrefix}.Inputs.AddressAutocomplete"] = "Search address",
            [$"{Defaults.ResourcesNamePrefix}.Inputs.AddressAutocomplete.Hint"] = "Use this field to search for an address. When you find and select the address you're looking for, it will automatically fill out some of the address fields bellow.",

            [$"{Defaults.ResourcesNamePrefix}.Inputs.GeocoordinatesSearch"] = "Search geocoordinates",
            [$"{Defaults.ResourcesNamePrefix}.Inputs.GeocoordinatesSearch.Hint"] = "Use this field to search for an address by it's geo coordinates. This will automatically fill out some of the address fields bellow.",
            [$"{Defaults.ResourcesNamePrefix}.Inputs.GeocoordinatesSearch.Placeholder"] = "E.g.: lat:-25.363, lng:131.044",

            [$"{Defaults.ResourcesNamePrefix}.Inputs.Search"] = "Search",

            [$"{Defaults.ResourcesNamePrefix}.Inputs.SelectAnOption"] = "Select an option",

            #endregion

            #region Order Address

            [$"{Defaults.ResourcesNamePrefix}.OrderAddress.LatLngUpdatedSuccessfuly"] = "Latitude and longitude fields updated successfuly.",
            [$"{Defaults.ResourcesNamePrefix}.OrderAddress.ErrorUpdatingGeoCoordinates"] = "An error occurred updating geo-coordinates.",
            [$"{Defaults.ResourcesNamePrefix}.OrderAddress.InvalidLatLng"] = "Invalid latitude or longitude, please verify your input does not exceed 7 decimal digits.",

            #endregion
        };

        /// <summary>
        /// Represents the local resources for displaying the plugin information in Spanish (es-).
        /// </summary>
        public static Dictionary<string, string> SpanishResources = new Dictionary<string, string>
        {
            #region Instructions

            [$"{Defaults.ResourcesNamePrefix}.GoogleMaps.Instructions"] = @"
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

            [$"{Defaults.ResourcesNamePrefix}.Multientrega.Instructions"] = @"
                <p>Multiantega Manual le permite utilizar el Servicio Web Multiantega Cliente como método de envío dentro de la tienda.
                <br />
                <br />
                Asegúrate de seguir las siguientes indicaciones para configurar correctamente este servicio:
                <br />
                <br />
                <ul>
                    <li>Contrate algún <a href=https://multientregapanama.com/servicios/ target=_blank>servicio de Multientrega y obtenga una cuenta.</a></li>
                    <li>Rellene los campos del formulario.</li>
                    <li>Vaya a <i>Sistema >> Programar tareas</i> y ejecute <b>Multientrega's Structure Sync Task</b> para cargar la estructura territorial utilizada por Multientrega.</li>
                </ul>
                <br />
                </p>
            ",

            [$"{Defaults.ResourcesNamePrefix}.GoogleMaps.Panel.Title"] = "Google Maps Integration",
            [$"{Defaults.ResourcesNamePrefix}.Multientrega.Panel.Title"] = "Multientrega Manual",
            [$"{Defaults.ResourcesNamePrefix}.Multientrega.Provinces.Panel.Title"] = "Estructura de provincias",
            [$"{Defaults.ResourcesNamePrefix}.Multientrega.Districts.Panel.Title"] = "Estructura de distritos",
            [$"{Defaults.ResourcesNamePrefix}.Multientrega.Townships.Panel.Title"] = "Estructura de corregimientos",
            [$"{Defaults.ResourcesNamePrefix}.Multientrega.Neighborhoods.Panel.Title"] = "Estructura de barrios",

            #endregion

            #region Fields

            [$"{Defaults.ResourcesNamePrefix}.Fields.ApiKey"] = "API Key",
            [$"{Defaults.ResourcesNamePrefix}.Fields.ApiKey.Hint"] = "Identificador único que autentica las solicitudes asociadas a las API de Google Maps.",
            [$"{Defaults.ResourcesNamePrefix}.Fields.ApiKey.Required"] = "El API Key es requerida.",

            [$"{Defaults.ResourcesNamePrefix}.Fields.DisplayLatLngFields"] = "Mostrar campos de latitud y lóngitud",
            [$"{Defaults.ResourcesNamePrefix}.Fields.DisplayLatLngFields.Hint"] = "Marque para indicar que los campos de latitud y lóngitud se mostrarán en los formularios de dirección.",

            [$"{Defaults.ResourcesNamePrefix}.Fields.LatLngFieldsRequired"] = "Latitud y longitud requeridos",
            [$"{Defaults.ResourcesNamePrefix}.Fields.LatLngFieldsRequired.Hint"] = "Marque para indicar que los campos de latitud y lóngitud son requeridos.",

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

            [$"{Defaults.ResourcesNamePrefix}.Fields.Email"] = "Correo electrónico",
            [$"{Defaults.ResourcesNamePrefix}.Fields.Email.Hint"] = "Identificador de usuario a utilizar para acceder al API de Multientrega.",
            [$"{Defaults.ResourcesNamePrefix}.Fields.Email.Required"] = "El correo electrónico es requerido.",

            [$"{Defaults.ResourcesNamePrefix}.Fields.Password"] = "Contraseña",
            [$"{Defaults.ResourcesNamePrefix}.Fields.Password.Hint"] = "Contraseña del usuario.",
            [$"{Defaults.ResourcesNamePrefix}.Fields.Password.Required"] = "La contraseña es requerida.",

            [$"{Defaults.ResourcesNamePrefix}.Fields.BaseUrl"] = "URL base",
            [$"{Defaults.ResourcesNamePrefix}.Fields.BaseUrl.Hint"] = "URL que va antes de cada función que se va a consumir desde el API de Multientrega.",
            [$"{Defaults.ResourcesNamePrefix}.Fields.BaseUrl.Required"] = "La URL base es requerida.",

            [$"{Defaults.ResourcesNamePrefix}.Fields.BranchOffice"] = "Sucursal",
            [$"{Defaults.ResourcesNamePrefix}.Fields.BranchOffice.Hint"] = "El ID de la sucursal que se debe usar al solicitar la tasa de impuestos de la opción de envío de la API de Multiantega.",
            [$"{Defaults.ResourcesNamePrefix}.Fields.BranchOffice.Required"] = "La sucursal es requerida.",

            [$"{Defaults.ResourcesNamePrefix}.Fields.Nit"] = "NIT",
            [$"{Defaults.ResourcesNamePrefix}.Fields.Nit.Hint"] = "Número de Identificación Tributaria.",
            [$"{Defaults.ResourcesNamePrefix}.Fields.Nit.Required"] = "El NIT es requerido.",

            [$"{Defaults.ResourcesNamePrefix}.Fields.ShippingOptionDescription"] = "Descripción",
            [$"{Defaults.ResourcesNamePrefix}.Fields.ShippingOptionDescription.Hint"] = "Descripción para la opción de envío.",

            [$"{Defaults.ResourcesNamePrefix}.Fields.ProvinceId"] = "Provincia",
            [$"{Defaults.ResourcesNamePrefix}.Fields.ProvinceId.Required"] = "La provincia es requerida.",

            [$"{Defaults.ResourcesNamePrefix}.Fields.DistrictId"] = "Distrito",
            [$"{Defaults.ResourcesNamePrefix}.Fields.DistrictId.Required"] = "El distrito es requerido.",

            [$"{Defaults.ResourcesNamePrefix}.Fields.TownshipId"] = "Corregimiento",
            [$"{Defaults.ResourcesNamePrefix}.Fields.TownshipId.Required"] = "El corregimiento es requerido.",

            [$"{Defaults.ResourcesNamePrefix}.Fields.NeighborhoodId"] = "Barrio",
            [$"{Defaults.ResourcesNamePrefix}.Fields.NeighborhoodId.Required"] = "El barrio es requerido.",

            [$"{Defaults.ResourcesNamePrefix}.Fields.Neighborhood.Fullname"] = "Nombre completo",
            [$"{Defaults.ResourcesNamePrefix}.Fields.Neighborhood.Status"] = "Estado",
            [$"{Defaults.ResourcesNamePrefix}.Fields.Neighborhood.LocXLon"] = "LocXLon",
            [$"{Defaults.ResourcesNamePrefix}.Fields.Neighborhood.LocYLat"] = "LocYLat",
            [$"{Defaults.ResourcesNamePrefix}.Fields.Neighborhood.DateCrea"] = "Fecha creación",
            [$"{Defaults.ResourcesNamePrefix}.Fields.Neighborhood.UsrCrea"] = "Usuario creador",
            [$"{Defaults.ResourcesNamePrefix}.Fields.Neighborhood.DateModifica"] = "Fecha actualización",
            [$"{Defaults.ResourcesNamePrefix}.Fields.Neighborhood.UsrModifica"] = "Usuario actualizador",

            #endregion

            #region Inputs

            [$"{Defaults.ResourcesNamePrefix}.Inputs.AddressAutocomplete"] = "Buscar dirección",
            [$"{Defaults.ResourcesNamePrefix}.Inputs.AddressAutocomplete.Hint"] = "Utilice este campo para buscar una dirección. Cuando encuentre y seleccione la dirección que está buscando, automáticamente se completarán algunos de los campos de dirección más abajo.",

            [$"{Defaults.ResourcesNamePrefix}.Inputs.GeocoordinatesSearch"] = "Coordenadas geográficas",
            [$"{Defaults.ResourcesNamePrefix}.Inputs.GeocoordinatesSearch.Hint"] = "Utilice este campo para buscar una dirección por sus coordenadas geográficas. Esto completará de forma automática algunos de los campos de dirección más abajo.",
            [$"{Defaults.ResourcesNamePrefix}.Inputs.GeocoordinatesSearch.Placeholder"] = "Ej.: lat:-25.363, lng:131.044",

            [$"{Defaults.ResourcesNamePrefix}.Inputs.Search"] = "Buscar",

            [$"{Defaults.ResourcesNamePrefix}.Inputs.SelectAnOption"] = "Seleccione una opción",

            #endregion

            #region Order Address

            [$"{Defaults.ResourcesNamePrefix}.OrderAddress.LatLngUpdatedSuccessfuly"] = "La latitud y longitud se han actualizado exitosamente.",
            [$"{Defaults.ResourcesNamePrefix}.OrderAddress.ErrorUpdatingGeoCoordinates"] = "Ha ocurrido un error actualizando las geo-coordenadas.",
            [$"{Defaults.ResourcesNamePrefix}.OrderAddress.InvalidLatLng"] = "Latitud o longitud inválida, por favor, verifique que no haya excedido los 7 dígitos decimales.",

            #endregion
        };
    }
}
