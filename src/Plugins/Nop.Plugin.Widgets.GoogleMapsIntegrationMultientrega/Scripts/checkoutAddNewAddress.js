var addressSection = "";

function addNewAddressButtonHandler() {
    $('div[class="address-grid"] div[class="address-item"]').last().hide();
    $(`div[class="section new-${addressSection}-address"]`).show();

    $([document.documentElement, document.body]).animate({
        scrollTop: $(`div[class="section new-${addressSection}-address"]`).offset().top
    }, 800);
}

$(document).ready(function () {
    if (!($('.field-validation-error').length) && ($('div[class="section select-billing-address"]').is(":visible") ||
        $('div[class="section select-shipping-address"]').is(":visible"))) {
        if ($('div[class="billing-addresses"] div[class="section new-billing-address"]').is(":visible")) {
            addressSection = "billing";
            $('div[class="section new-billing-address"] div[class="title"]').hide();
            $('div[class="section new-billing-address"]').hide();
        }

        if ($('div[class="shipping-addresses"] div[class="section new-shipping-address"]').is(":visible")) {
            addressSection = "shipping";
            $('div[class="section new-shipping-address"] div[class="title"]').hide();
            $('div[class="section new-shipping-address"]').hide();
        }

        if ($(document).has('[class="address-grid"]')) {
            var $addNewAddressButtonElement = $("<input>",
                {
                    "type": "button",
                    "value": title,
                    "class": `button-1 select-${addressSection}-address-button`
                });
            $addNewAddressButtonElement.click(addNewAddressButtonHandler);
            var $selectButtonElement = $("<div>", { "class": "select-button" });
            $selectButtonElement.append($addNewAddressButtonElement);

            var $icon = $("<span>", { "class": "new-address-box-icon" });
            var $addressBoxElement = $("<ul>", { "class": "address-box add-new-address-box" });
            $addressBoxElement.append($icon);
            var $addressItemElement = $("<div>", { "class": "address-item" });
            $addressItemElement.append($addressBoxElement);
            $addressItemElement.append($selectButtonElement);

            $('div[class="address-grid"]').append($addressItemElement);
        }
    } else {
        $([document.documentElement, document.body]).animate({
            scrollTop: $('.field-validation-error').first().offset().top - 200
        }, 800);
    }
});
