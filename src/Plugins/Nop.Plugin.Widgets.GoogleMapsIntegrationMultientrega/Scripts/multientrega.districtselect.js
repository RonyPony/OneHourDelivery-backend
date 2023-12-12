/*
** Multientrega district select js functions
*/
+function ($) {
    'use strict';
    if ('undefined' == typeof (jQuery)) {
        throw new Error('jQuery JS required');
    }
    function countrySelectHandler() {
        var $this = $(this);
        var selectedItem = $this.val();
        var township = $($this.data('township'));
        var neighborhood = $($this.data('neighborhood'));
        var loading = $($this.data('loading'));
        loading.show();
        $.ajax({
            cache: false,
            type: "GET",
            url: $this.data('url'),
            data: {
                'districtId': selectedItem
            },
            success: function (response) {
                township.html('');
                neighborhood.html('');

                $.each(response.Data,
                    function (id, option) {
                        if (option.Disabled && option.Selected)
                            township.append("<option value=" + option.Value + " disabled selected>" + option.Text + "</option>");
                        else if (option.Selected)
                            township.append("<option value=" + option.Value + " selected>" + option.Text + "</option>");
                        else
                            township.append("<option value=" + option.Value + ">" + option.Text + "</option>");
                    });

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
    if ($(document).has('[data-trigger="multientrega-district-select"]')) {
        $('select[data-trigger="multientrega-district-select"]').change(countrySelectHandler);
    }
    $.fn.countrySelect = function () {
        this.each(function () {
            $(this).change(countrySelectHandler);
        });
    }
}(jQuery); 