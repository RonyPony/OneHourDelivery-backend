var eventSelectedDate
$(document).ready(function () {
    var turl = '/Catalog' + '/ProductAll';
    var json = "{' empNumber ':'" + 'empId' + "'}";
    $.ajax(
        {
            type: "GET",
            url: turl,
            dataType: 'json',
            async: false,
            contentType: "application/json; charset=utf-8",
            beforeSend: function () { },
            success: function (data) {
                $('#calendar').evoCalendar({
                    theme: 'Royal Navy',
                    todayHighlight: !0,
                    titleFormat: "MM",
                    language: "es",
                    eventDisplayDefault: true,
                    eventListToggler: false,
                    calendarEvents: data
                })


            }
        });

    $('#calendar').on('selectDate', function (event, newDate, oldDate) {
        if (newDate == oldDate && newDate == eventSelectedDate) {
            var turl = '/Catalog' + '/ProductAll';
            $.ajax(
                {
                    type: "GET",
                    url: turl,
                    dataType: 'json',
                    async: false,
                    contentType: "application/json; charset=utf-8",
                    beforeSend: function () { },
                    success: function (data) {
                        $('#calendar').evoCalendar('destroy');
                        $('#calendar').evoCalendar({
                            theme: 'Royal Navy',
                            todayHighlight: !0,
                            titleFormat: "MM",
                            language: "es",
                            eventDisplayDefault: true,
                            eventListToggler: false,
                            calendarEvents: data
                        })


                    }
                });
            $('#calendar').evoCalendar('selectDate', oldDate);
            eventSelectedDate = undefined
        }
    });



    // selectEvent
    $('#calendar').on('selectEvent', function (event, activeEvent) {

        var urlProduct = '/Catalog' + '/Product';
        $.ajax({
            type: 'GET',
            url: urlProduct,
            data: {
                Id: activeEvent.id
            },
            success: function (result) {
                oldEvents = $('.event-list').html();
                $('.event-list').html(result);
                eventSelectedDate = $('#calendar').evoCalendar('getActiveDate');
            }
        });

    });
})