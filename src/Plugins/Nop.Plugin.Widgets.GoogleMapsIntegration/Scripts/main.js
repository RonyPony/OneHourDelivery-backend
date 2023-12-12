const defaultDecimal = 0.0000000;
const mapZoomToStreetLevel = 17;
const mapZoomToWordLevel = 1;
const mapZoomToCityLevel = 7;
let autocomplete;
let map;
let markers = [];
let btnGeocoordinateSearch;

function initMap() {
    $(document).ready(function () {
        latitudeInput.value = getDecimalFromStringOrDefault(addressLatitude);
        longitudeInput.value = getDecimalFromStringOrDefault(addressLongitude);

        const defaultCoordinates = {
            lat: JSON.parse(defaultLatLngEnabled.toLowerCase()) ? getDecimalFromStringOrDefault(defaultLat) : defaultDecimal,
            lng: JSON.parse(defaultLatLngEnabled.toLowerCase()) ? getDecimalFromStringOrDefault(defaultLng) : defaultDecimal
        };

        const addressCoordinates = {
            lat: getDecimalFromStringOrDefault(addressLatitude),
            lng: getDecimalFromStringOrDefault(addressLongitude)
        };

        const mapBoundaries = JSON.parse(mapBoundariesEnabled.toLowerCase()) ? {
            north: getDecimalFromStringOrDefault(northBound),
            south: getDecimalFromStringOrDefault(southBound),
            west: getDecimalFromStringOrDefault(westBound),
            east: getDecimalFromStringOrDefault(eastBound)
        } : null;

        map = new google.maps.Map(document.getElementById(mapDivId), {
            zoom: mapZoomToStreetLevel,
            center: addressCoordinates,
            restriction: JSON.parse(mapBoundariesEnabled.toLowerCase()) ? {
                latLngBounds: mapBoundaries,
                strictBounds: false
            } : null
        });

        setMarker(addressCoordinates);

        // Looks for device current location
        if (addressCoordinates.lat === defaultDecimal && addressCoordinates.lng === defaultDecimal) {
            if (navigator.geolocation) {
                navigator.geolocation.getCurrentPosition(
                    (position) => {
                        const deviceCoordinates = {
                            lat: position.coords.latitude,
                            lng: position.coords.longitude,
                        }

                        setMarker(deviceCoordinates);
                    },
                    () => {
                        handleLocationError(defaultCoordinates)
                    }
                );
            } else {
                handleLocationError(defaultCoordinates);
            }
        }

        if (JSON.parse(autocompleteEnabled.toLowerCase())) {
            initAutocomplete();
        }

        if (JSON.parse(geocodingEnabled.toLowerCase())) {
            btnGeocoordinateSearch = $(`#btn-${geocoordinatesSearchInputId}`);
            btnGeocoordinateSearch.on("click", () => {
                validateGeoCoordinatesBeforeReverseGeocoding();
            });
        }

        if (JSON.parse(mapEnabled.toLowerCase())) {
            map.addListener("click", (event) => {
                initReverseGeocoding(event.latLng.toJSON(), true);
            });
        }
    });
}

function getDecimalFromStringOrDefault(stringNumber) {
    stringNumber = stringNumber.replace(",", "."); //Fix for Spanish languages.
    return isNaN(parseFloat(stringNumber)) ? defaultDecimal : parseFloat(stringNumber);
}

function setMarker(location) {
    clearMarker();

    var marker = new google.maps.Marker({
        position: location,
        map: map,
        draggable: JSON.parse(mapEnabled.toLowerCase())
    });

    google.maps.event.addListener(marker, 'dragend', function (event) {
        initReverseGeocoding(event.latLng.toJSON(), false);
    });

    map.setCenter(location);
    markers.push(marker);
}

function clearMarker() {
    for (let i = 0; i < markers.length; i++) {
        markers[i].setMap(null);
    }
}

function handleLocationError(coordinates) {
    const mapZoomLevel = coordinates.lat === defaultDecimal && coordinates.lng === defaultDecimal ? mapZoomToWordLevel : mapZoomToStreetLevel;

    setMarker(coordinates);
    map.setZoom(mapZoomLevel);
}

function initReverseGeocoding(position, moveMarker) {
    $.get(`https://maps.googleapis.com/maps/api/geocode/json?latlng=${position.lat},${position.lng}&key=${apiKey}`, (response) => {
        if (moveMarker) {
            setMarker(response.results[0].geometry.location);
            map.setZoom(mapZoomToStreetLevel);
        }

        setInputsValues(response.results[0]);
    });
}

function initAutocomplete() {
    autocomplete = new google.maps.places.Autocomplete(
        document.getElementById(autocompleteInputId),
        {
            types: ['establishment'],
            fields: ['place_id', 'geometry', 'name']
        }
    );

    if (JSON.parse(mapBoundariesEnabled.toLowerCase())) {
        var mapBoundaries = new google.maps.LatLngBounds(
            new google.maps.LatLng(getDecimalFromStringOrDefault(southBound), getDecimalFromStringOrDefault(westBound)),
            new google.maps.LatLng(getDecimalFromStringOrDefault(northBound), getDecimalFromStringOrDefault(eastBound)));

        autocomplete.setBounds(mapBoundaries);
        autocomplete.setOptions({ strictBounds: true })
    }

    autocomplete.addListener('place_changed', onPlaceChanged);
}

function validateGeoCoordinatesBeforeReverseGeocoding() {
    let geocoordinateSearchValue = $(`#${geocoordinatesSearchInputId}`).val().trim();

    if (!geocoordinateSearchValue.includes(',') || !geocoordinateSearchValue.includes('lat:') || !geocoordinateSearchValue.includes('lng:')) {
        alert('Error: Format entered is invalid. Please, follow next pattern: lat:ADDRESS_LATITUDE, lng:ADDRESS_LONGITUDE.');
        return;
    }

    let lat = geocoordinateSearchValue.split(',').shift().replace('lat:', '').trim();
    let lng = geocoordinateSearchValue.split(',').pop().replace('lng:', '').trim();

    if (isNaN(parseFloat(lat)) || isNaN(parseFloat(lng))) {
        alert('Error: Geocoordinates entered are invalid.');
    } else {
        const coordinates = {
            lat: lat,
            lng: lng
        }

        initReverseGeocoding(coordinates, true);
    }
}

function onPlaceChanged() {
    var place = autocomplete.getPlace();

    if (!place.geometry) {
        alert('Error: Invalid selection.')
    } else {
        $.get(`https://maps.googleapis.com/maps/api/geocode/json?place_id=${place.place_id}&key=${apiKey}`, (response) => {
            setMarker(response.results[0].geometry.location);
            map.setZoom(mapZoomToStreetLevel);

            setInputsValues(response.results[0]);
        });
    }
}

function setInputsValues(placeInfo) {
    const decimalPositions = 7;
    latitudeInput.value = placeInfo.geometry.location.lat.toFixed(decimalPositions);
    longitudeInput.value = placeInfo.geometry.location.lng.toFixed(decimalPositions);
    cityInput.value = getAddressComponentByType(placeInfo.address_components, "locality").long_name;
    address1Input.value = placeInfo.formatted_address;

    if (getAddressComponentByType(placeInfo.address_components, "postal_code")) {
        zipPostalCodeInput.value = getAddressComponentByType(placeInfo.address_components, "postal_code").long_name;
    }

    const countryName = getAddressComponentByType(placeInfo.address_components, "country").long_name;
    $.each(countryInput.options, (index, item) => {
        if (item.innerText === countryName) {
            item.selected = true;
        } else {
            item.selected = false;
        }
    });

    $('#' + countryInput.id).trigger('change');
}


function getAddressComponentByType(addressComponents, type) {
    let result;

    $.each(addressComponents, (index, item) => {
        if (item.types.includes(type)) {
            result = item;
        }
    });

    return result;
}