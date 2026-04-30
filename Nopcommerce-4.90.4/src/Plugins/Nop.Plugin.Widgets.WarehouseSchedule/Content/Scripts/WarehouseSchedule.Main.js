const disabled = true;
const enabled = false;
const base10Number = 10;
const tokenInput = $('input[name=__RequestVerificationToken]').val();

function editData_warehouse_schedule(dataId) {
    showEditButtons(dataId);
    rowEditMode(dataId, enabled);
}

function confirmEditData_warehouse_schedule(dataId) {
    postWarehouseScheduledDay(dataId);
}

function cancelEditData_warehouse_schedule(dataId) {
    $('#warehouse-schedule').load('/Admin/WarehouseSchedule/GetWarehouseScheduleByWarehouseId?warehouseId=' + parseInt($('[name="WarehouseScheduleMappings[' + dataId + '].WarehouseId"]').val(), base10Number));
}

function postWarehouseScheduledDay(dataId) {
    var postData = getPostData(dataId);
    $.ajax({
        url: "/Admin/WarehouseSchedule/UpdateWarehouseScheduledDay",
        type: 'POST',
        dataType: "json",
        data: postData,
        success: function (response) {
            if (response.Success) {
                hideEditButtons(dataId);
                rowEditMode(dataId, disabled);
            }

            alert(response.Message);
        },
        error: function (response) {
            alert(response.Message);
        }
    });
}

function getPostData(dataId) {
    const data = {
        WarehouseId: parseInt($('[name="WarehouseScheduleMappings[' + dataId + '].WarehouseId"]').val(), base10Number),
        DayId: parseInt($('[name="WarehouseScheduleMappings[' + dataId + '].DayId"]').val(), base10Number),
        IsActive: $('[name="WarehouseScheduleMappings[' + dataId + '].IsActive"]').prop('checked') ? true : false,
        BeginTime: $('[name="WarehouseScheduleMappings[' + dataId + '].BeginTime"]').val(),
        EndTime: $('[name="WarehouseScheduleMappings[' + dataId + '].EndTime"]').val(),
        __RequestVerificationToken: tokenInput
    };
    addAntiForgeryToken(data);

    return data;
}

function showEditButtons(dataId) {
    $('#buttonEdit_warehouse_schedule_' + dataId).hide();
    $('#buttonConfirm_warehouse_schedule_' + dataId).show();
    $('#buttonCancel_warehouse_schedule_' + dataId).show();
}

function hideEditButtons(dataId) {
    $('#buttonEdit_warehouse_schedule_' + dataId).show();
    $('#buttonConfirm_warehouse_schedule_' + dataId).hide();
    $('#buttonCancel_warehouse_schedule_' + dataId).hide();
}

function rowEditMode(dataId, disabledEnabled) {
    $('[name="WarehouseScheduleMappings[' + dataId + '].IsActive"]').prop("disabled", disabledEnabled);
    $('[name="WarehouseScheduleMappings[' + dataId + '].BeginTime"]').prop("disabled", disabledEnabled);
    $('[name="WarehouseScheduleMappings[' + dataId + '].EndTime"]').prop("disabled", disabledEnabled);
}