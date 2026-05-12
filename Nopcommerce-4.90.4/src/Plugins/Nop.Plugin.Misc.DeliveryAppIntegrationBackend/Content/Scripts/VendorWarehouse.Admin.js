var vendorId;
var warehouseId;

$(document).ready(function () {
    const base10Number = 10;
    var tokenInput = $('input[name=__RequestVerificationToken]').val();
    vendorId = $("#VendorId");
    warehouseId = $("#WarehouseId");

    $("select#WarehouseId").bind('change', function () {
        if (warehouseId.val() && warehouseId.val() !== "0") {
            var postData = {
                VendorId: parseInt(vendorId.val(), base10Number),
                WarehouseId: parseInt(warehouseId.val(), base10Number),
                __RequestVerificationToken: tokenInput
            };
            addAntiForgeryToken(postData);

            $.ajax({
                url: "/Admin/DeliveryAppShipping/AssignWarehouseToVendor",
                type: 'POST',
                dataType: "json",
                data: postData,
                success: function (data) {
                    if (data.Success) {
                        alert(data.Message);
                    }
                },
                error: function (data) {
                    alert(data.Message);
                }
            });
        }
    });
});