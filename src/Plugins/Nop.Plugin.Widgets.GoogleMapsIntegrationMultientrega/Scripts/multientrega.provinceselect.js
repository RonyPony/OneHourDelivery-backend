/*
** Multientrega province select js functions
*/
+function ($) {
    'use strict';
    if ('undefined' == typeof (jQuery)) {
        throw new Error('jQuery JS required');
    }
    function countrySelectHandler() {
        var $this = $(this);
        var selectedItem = $this.val();
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
                'provinceId': selectedItem
            },
            success: function (response) {
                district.html('');
                township.html('');
                neighborhood.html('');

                $.each(response.Data,
                    function (id, option) {
                        if (option.Disabled && option.Selected)
                            district.append("<option value=" + option.Value + " disabled selected>" + option.Text + "</option>");
                        else if (option.Selected)
                            district.append("<option value=" + option.Value + " selected>" + option.Text + "</option>");
                        else
                            district.append("<option value=" + option.Value + ">" + option.Text + "</option>");
                    });

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
    if ($(document).has('[data-trigger="multientrega-province-select"]')) {
        $('select[data-trigger="multientrega-province-select"]').change(countrySelectHandler);
    }
    $.fn.countrySelect = function () {
        this.each(function () {
            $(this).change(countrySelectHandler);
        });
    }
}(jQuery); 