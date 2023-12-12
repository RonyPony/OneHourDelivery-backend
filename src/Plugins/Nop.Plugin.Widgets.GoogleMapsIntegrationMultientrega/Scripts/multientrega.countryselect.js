/*
** Multientrega country select js functions
*/
+function ($) {
    'use strict';
    if ('undefined' == typeof (jQuery)) {
        throw new Error('jQuery JS required');
    }
    function countrySelectHandler() {
        var $this = $(this);
        var selectedItem = $this.val();
        var province = $($this.data('province'));
        var district = $($this.data('district'));
        var township = $($this.data('township'));
        var neighborhood = $($this.data('neighborhood'));
        var loading = $($this.data('loading'));
        loading.show();
        $.ajax({
            cache: false,
            type: "GET",
            url: $this.data('url'),
            data: { 
              'countryId': selectedItem
            },
            success: function (response) {
                province.html('');
                district.html('');
                township.html('');
                neighborhood.html('');

                $.each(response.Data,
                    function (id, option) {
                        if (option.Disabled && option.Selected)
                            province.append("<option value=" + option.Value + " disabled selected>" + option.Text + "</option>");
                        else if (option.Selected)
                            province.append("<option value=" + option.Value + " selected>" + option.Text + "</option>");
                        else
                            province.append("<option value=" + option.Value + ">" + option.Text + "</option>");
                    });

                district.append("<option value=0 disabled>N/A</option>");
                township.append("<option value=0 disabled>N/A</option>");
                neighborhood.append("<option value=0 disabled>N/A</option>");
            },
            error: function (response) {
                alert(response.Message);
            },
            complete: function (jqXHR, textStatus) {
                loading.hide();
            }
        });
    }
    if ($(document).has('[data-trigger="multientrega-country-select"]')) {
        $('select[data-trigger="multientrega-country-select"]').change(countrySelectHandler);
    }
    $.fn.countrySelect = function () {
        this.each(function () {
            $(this).change(countrySelectHandler);
        });
    }
}(jQuery); 