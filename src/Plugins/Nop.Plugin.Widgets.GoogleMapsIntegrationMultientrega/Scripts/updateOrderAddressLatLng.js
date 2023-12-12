$(window).on('load', function () {
    $('button[name="save"]').click(postUpdateOrderAddressLatLng);
});

function postUpdateOrderAddressLatLng() {
    var postData = getPostData();
    $.ajax({
        url: "/GoogleMapsIntegration/UpdateOrderAddressLatLng",
        type: 'POST',
        dataType: "json",
        data: postData,
        success: function (response) {
            alert(response.Message)
        },
        error: function (response) {
            alert(response.Message);
        }
    });
}

function getPostData() {
    const base10Number = 10;
    const data = {
        AddressId: parseInt($('#Address_Id').val(), base10Number),
        Latitude: $('#AddressGeoCoordinates_Latitude').val(),
        Longitude: $('#AddressGeoCoordinates_Longitude').val(),
        __RequestVerificationToken: $('input[name="__RequestVerificationToken"]').val()
    };
    addAntiForgeryToken(data);

    return data;
}