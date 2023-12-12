/*
** Multientrega township select js functions
*/
+function ($) {
    'use strict';
    if ('undefined' == typeof (jQuery)) {
        throw new Error('jQuery JS required');
    }
    function countrySelectHandler() {
        var $this = $(this);
        var selectedItem = $this.val();
        var neighborhood = $($this.data('neighborhood'));
        var loading = $($this.data('loading'));
        loading.show();
        $.ajax({
            cache: false,
            type: "GET",
            url: $this.data('url'),
            data: {
                'townshipId': selectedItem
            },
            success: function (response) {
                neighborhood.html('');

                $.each(response.Data,
                    function (id, option) {
                        if (option.Disabled && option.Selected)
                            neighborhood.append("<option value=" + option.Value + " disabled selected>" + option.Text + "</option>");
                        else if (option.Selected)
                            neighborhood.append("<option value=" + option.Value + " selected>" + option.Text + "</option>");
                        else
                            neighborhood.append("<option value=" + option.Value + ">" + option.Text + "</option>");
                    });
            },
            error: function (response) {
                alert(response.Message);
            },
            complete: function (jqXHR, textStatus) {
                loading.hide();
            }
        });
    }
    if ($(document).has('[data-trigger="multientrega-township-select"]')) {
        $('select[data-trigger="multientrega-township-select"]').change(countrySelectHandler);
    }
    $.fn.countrySelect = function () {
        this.each(function () {
            $(this).change(countrySelectHandler);
        });
    }
}(jQuery); 