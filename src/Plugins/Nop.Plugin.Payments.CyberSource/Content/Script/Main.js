$(document).ready(function () {
    const name = document.getElementById('name');
    const cardnumber = document.getElementById('cardnumber');
    const expirydatemonth = document.getElementById('expirydatemonth');
    const expirydateyear = document.getElementById('expirydateyear');
    const securitycode = document.getElementById('securitycode');
    const ccsingle = document.getElementById('ccsingle');

    $('.payment-info-next-step-button').hide();

    $(document).on('accordion_section_opened',
        function (data) {
            if (data.currentSectionId !== 'opc-payment_info') {
                $('.payment-info-next-step-button').show();
            }
        });

    function cybs_dfprofiler() {
        var str;

        if (environment.toLowerCase() == 'live') {
            var org_id = 'k8vif92e';
        } else {
            var org_id = '1snn5n9w';
        }

        var sessionID = new Date().getTime();
        str = "https://h.online-metrix.net/fp/tags.js?org_id=" + org_id + "&session_id=" + merchantId + sessionID + "&m=2";

        var paragraphTM = document.createElement("p");
        str = "background:url(https://h.online-metrix.net/fp/clear.png?org_id=" + org_id + "&session_id=" + merchantId + sessionID + "&m=1)";

        paragraphTM.styleSheets = str;
        paragraphTM.height = "0";
        paragraphTM.width = "0";
        paragraphTM.hidden = "true";

        document.body.appendChild(paragraphTM);

        var img = document.createElement("img");

        str = "https://h.online-metrix.net/fp/clear.png?org_id=" + org_id + "&session_id=" + merchantId + sessionID + "&m=2";
        img.src = str;

        document.body.appendChild(img);

        var tmscript = document.createElement("script");
        tmscript.src = "https://h.online-metrix.net/fp/check.js?org_id=" + org_id + "&session_id=" + merchantId + sessionID;
        tmscript.type = "text/javascript";

        document.body.appendChild(tmscript);

        var objectTM = document.createElement("object");

        objectTM.data = "https://h.online-metrix.net/fp/fp.swf?org_id=" + org_id + "&session_id=" + merchantId + sessionID;
        objectTM.width = "1";
        objectTM.height = "1";
        objectTM.id = "thm_fp";

        var param = document.createElement("param");
        param.name = "movie";
        param.value = "https://h.online-metrix.net/fp/fp.swf?org_id=" + org_id + "&session_id=" + merchantId + sessionID;
        objectTM.appendChild(param);

        str = "https://h.online-metrix.net/fp/tags.js?org_id=" + org_id + "&session_id=" + merchantId + sessionID + "";

        document.body.appendChild(objectTM);

        $("#DeviceFingerprintId").val(sessionID);
    }

    cybs_dfprofiler();

    // Mask the Credit Card Number Input
    var cardnumber_mask = new IMask(cardnumber, {
        mask: [
            {
                mask: '0000 0000 0000 0000',
                regex: '^(5[1-5]\\d{0,2}|22[2-9]\\d{0,1}|2[3-7]\\d{0,2})\\d{0,12}',
                cardtype: 'mastercard'
            },
            {
                mask: '0000 0000 0000 0000',
                regex: '^4\\d{0,15}',
                cardtype: 'visa'
            },
            {
                mask: '0000 0000 0000 0000',
                cardtype: 'Unknown'
            }
        ],
        dispatch: function (appended, dynamicMasked) {
            var number = (dynamicMasked.value + appended).replace(/\D/g, '');

            for (var i = 0; i < dynamicMasked.compiledMasks.length; i++) {
                let re = new RegExp(dynamicMasked.compiledMasks[i].regex);
                if (number.match(re) != null) {
                    return dynamicMasked.compiledMasks[i];
                }
            }
        }
    });

    // Mask the security code
    var securitycode_mask = new IMask(securitycode, {
        mask: '0000',
    });

    // SVGICONS
    let visa_single = `<svg version="1.1" id="Layer_1" xmlns:sketch="http://www.bohemiancoding.com/sketch/ns" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" x="0px" y="0px" width="750px" height="471px" viewBox="0 0 750 471" enable-background="new 0 0 750 471" xml:space="preserve"><title>Slice 1</title><desc>Created with Sketch.</desc><g id="visa" sketch:type="MSLayerGroup"><path id="Shape" sketch:type="MSShapeGroup" fill="#0E4595" d="M278.198,334.228l33.36-195.763h53.358l-33.384,195.763H278.198L278.198,334.228z"/><path id="path13" sketch:type="MSShapeGroup" fill="#0E4595" d="M524.307,142.687c-10.57-3.966-27.135-8.222-47.822-8.222c-52.725,0-89.863,26.551-90.18,64.604c-0.297,28.129,26.514,43.821,46.754,53.185c20.77,9.597,27.752,15.716,27.652,24.283c-0.133,13.123-16.586,19.116-31.924,19.116c-21.355,0-32.701-2.967-50.225-10.274l-6.877-3.112l-7.488,43.823c12.463,5.466,35.508,10.199,59.438,10.445c56.09,0,92.502-26.248,92.916-66.884c0.199-22.27-14.016-39.216-44.801-53.188c-18.65-9.056-30.072-15.099-29.951-24.269c0-8.137,9.668-16.838,30.559-16.838c17.447-0.271,30.088,3.534,39.936,7.5l4.781,2.259L524.307,142.687"/><path id="Path" sketch:type="MSShapeGroup" fill="#0E4595" d="M661.615,138.464h-41.23c-12.773,0-22.332,3.486-27.941,16.234l-79.244,179.402h56.031c0,0,9.16-24.121,11.232-29.418c6.123,0,60.555,0.084,68.336,0.084c1.596,6.854,6.492,29.334,6.492,29.334h49.512L661.615,138.464L661.615,138.464z M596.198,264.872c4.414-11.279,21.26-54.724,21.26-54.724c-0.314,0.521,4.381-11.334,7.074-18.684l3.607,16.878c0,0,10.217,46.729,12.352,56.527h-44.293V264.872L596.198,264.872z"/><path id="path16" sketch:type="MSShapeGroup" fill="#0E4595" d="M232.903,138.464L180.664,271.96l-5.565-27.129c-9.726-31.274-40.025-65.157-73.898-82.12l47.767,171.204l56.455-0.064l84.004-195.386L232.903,138.464"/><path id="path18" sketch:type="MSShapeGroup" fill="#F2AE14" d="M131.92,138.464H45.879l-0.682,4.073c66.939,16.204,111.232,55.363,129.618,102.415l-18.709-89.96C152.877,142.596,143.509,138.896,131.92,138.464"/></g></svg>`;
    let mastercard_single = `<svg id="Layer_1" data-name="Layer 1" xmlns="http://www.w3.org/2000/svg" width="482.51" height="374" viewBox="0 0 482.51 374"> <title>mastercard</title> <g> <path d="M220.13,421.67V396.82c0-9.53-5.8-15.74-15.32-15.74-5,0-10.35,1.66-14.08,7-2.9-4.56-7-7-13.25-7a14.07,14.07,0,0,0-12,5.8v-5h-7.87v39.76h7.87V398.89c0-7,4.14-10.35,9.94-10.35s9.11,3.73,9.11,10.35v22.78h7.87V398.89c0-7,4.14-10.35,9.94-10.35s9.11,3.73,9.11,10.35v22.78Zm129.22-39.35h-14.5v-12H327v12h-8.28v7H327V408c0,9.11,3.31,14.5,13.25,14.5A23.17,23.17,0,0,0,351,419.6l-2.49-7a13.63,13.63,0,0,1-7.46,2.07c-4.14,0-6.21-2.49-6.21-6.63V389h14.5v-6.63Zm73.72-1.24a12.39,12.39,0,0,0-10.77,5.8v-5h-7.87v39.76h7.87V399.31c0-6.63,3.31-10.77,8.7-10.77a24.24,24.24,0,0,1,5.38.83l2.49-7.46a28,28,0,0,0-5.8-.83Zm-111.41,4.14c-4.14-2.9-9.94-4.14-16.15-4.14-9.94,0-16.15,4.56-16.15,12.43,0,6.63,4.56,10.35,13.25,11.6l4.14.41c4.56.83,7.46,2.49,7.46,4.56,0,2.9-3.31,5-9.53,5a21.84,21.84,0,0,1-13.25-4.14l-4.14,6.21c5.8,4.14,12.84,5,17,5,11.6,0,17.81-5.38,17.81-12.84,0-7-5-10.35-13.67-11.6l-4.14-.41c-3.73-.41-7-1.66-7-4.14,0-2.9,3.31-5,7.87-5,5,0,9.94,2.07,12.43,3.31Zm120.11,16.57c0,12,7.87,20.71,20.71,20.71,5.8,0,9.94-1.24,14.08-4.56l-4.14-6.21a16.74,16.74,0,0,1-10.35,3.73c-7,0-12.43-5.38-12.43-13.25S445,389,452.07,389a16.74,16.74,0,0,1,10.35,3.73l4.14-6.21c-4.14-3.31-8.28-4.56-14.08-4.56-12.43-.83-20.71,7.87-20.71,19.88h0Zm-55.5-20.71c-11.6,0-19.47,8.28-19.47,20.71s8.28,20.71,20.29,20.71a25.33,25.33,0,0,0,16.15-5.38l-4.14-5.8a19.79,19.79,0,0,1-11.6,4.14c-5.38,0-11.18-3.31-12-10.35h29.41v-3.31c0-12.43-7.46-20.71-18.64-20.71h0Zm-.41,7.46c5.8,0,9.94,3.73,10.35,9.94H364.68c1.24-5.8,5-9.94,11.18-9.94ZM268.59,401.79V381.91h-7.87v5c-2.9-3.73-7-5.8-12.84-5.8-11.18,0-19.47,8.7-19.47,20.71s8.28,20.71,19.47,20.71c5.8,0,9.94-2.07,12.84-5.8v5h7.87V401.79Zm-31.89,0c0-7.46,4.56-13.25,12.43-13.25,7.46,0,12,5.8,12,13.25,0,7.87-5,13.25-12,13.25-7.87.41-12.43-5.8-12.43-13.25Zm306.08-20.71a12.39,12.39,0,0,0-10.77,5.8v-5h-7.87v39.76H532V399.31c0-6.63,3.31-10.77,8.7-10.77a24.24,24.24,0,0,1,5.38.83l2.49-7.46a28,28,0,0,0-5.8-.83Zm-30.65,20.71V381.91h-7.87v5c-2.9-3.73-7-5.8-12.84-5.8-11.18,0-19.47,8.7-19.47,20.71s8.28,20.71,19.47,20.71c5.8,0,9.94-2.07,12.84-5.8v5h7.87V401.79Zm-31.89,0c0-7.46,4.56-13.25,12.43-13.25,7.46,0,12,5.8,12,13.25,0,7.87-5,13.25-12,13.25-7.87.41-12.43-5.8-12.43-13.25Zm111.83,0V366.17h-7.87v20.71c-2.9-3.73-7-5.8-12.84-5.8-11.18,0-19.47,8.7-19.47,20.71s8.28,20.71,19.47,20.71c5.8,0,9.94-2.07,12.84-5.8v5h7.87V401.79Zm-31.89,0c0-7.46,4.56-13.25,12.43-13.25,7.46,0,12,5.8,12,13.25,0,7.87-5,13.25-12,13.25C564.73,415.46,560.17,409.25,560.17,401.79Z" transform="translate(-132.74 -48.5)"/> <g> <rect x="169.81" y="31.89" width="143.72" height="234.42" fill="#ff5f00"/> <path d="M317.05,197.6A149.5,149.5,0,0,1,373.79,80.39a149.1,149.1,0,1,0,0,234.42A149.5,149.5,0,0,1,317.05,197.6Z" transform="translate(-132.74 -48.5)" fill="#eb001b"/> <path d="M615.26,197.6a148.95,148.95,0,0,1-241,117.21,149.43,149.43,0,0,0,0-234.42,148.95,148.95,0,0,1,241,117.21Z" transform="translate(-132.74 -48.5)" fill="#f79e1b"/> </g> </g></svg>`;

    // Define the color swap function
    const swapColor = function (basecolor) {
        document.querySelectorAll('.lightcolor')
            .forEach(function (input) {
                input.setAttribute('class', '');
                input.setAttribute('class', 'lightcolor ' + basecolor);
            });
        document.querySelectorAll('.darkcolor')
            .forEach(function (input) {
                input.setAttribute('class', '');
                input.setAttribute('class', 'darkcolor ' + basecolor + 'dark');
            });
    };


    // Pop-in the appropriate card icon when detected
    cardnumber_mask.on("accept", function () {
        switch (cardnumber_mask.masked.currentMask.cardtype) {
            case 'visa':
                $("#CardType").val('001');
                ccsingle.innerHTML = visa_single;
                swapColor('lime');
                break;
            case 'mastercard':
                $("#CardType").val('002');
                ccsingle.innerHTML = mastercard_single;
                swapColor('lightblue');
                break;
            default:
                $("#CardType").val('');
                ccsingle.innerHTML = '';
                swapColor('grey');
                break;
        }
    });

    // CREDIT CARD IMAGE JS
    document.querySelector('.preload').classList.remove('preload');
    document.querySelector('.creditcard').addEventListener('click', function () {
        if (this.classList.contains('flipped')) {
            this.classList.remove('flipped');
        } else {
            this.classList.add('flipped');
        }
    })

    // On Input Change Events
    name.addEventListener('input', function () {
        if (name.value.length == 0) {
            document.getElementById('svgname').innerHTML = 'Juan Pérez';
            document.getElementById('svgnameback').innerHTML = 'Juan Pérez';
        } else {
            document.getElementById('svgname').innerHTML = this.value;
            document.getElementById('svgnameback').innerHTML = this.value;
        }
    });

    expirydatemonth.addEventListener('change', function () {
        document.getElementById('svgexpiremonth').innerHTML = expirydatemonth.value + '/';
    });

    expirydateyear.addEventListener('change', function () {
        document.getElementById('svgexpireyear').innerHTML = expirydateyear.value.substring(2);
    });

    cardnumber_mask.on('accept', function () {
        if (cardnumber_mask.value.length == 0) {
            document.getElementById('svgnumber').innerHTML = '0000 0000 0000 0000';
        } else {
            document.getElementById('svgnumber').innerHTML = cardnumber_mask.value;
        }
    });

    securitycode_mask.on('accept', function () {
        if (securitycode_mask.value.length == 0) {
            document.getElementById('svgsecurity').innerHTML = '000';
        } else {
            document.getElementById('svgsecurity').innerHTML = securitycode_mask.value;
        }
    });

    //On Focus Events
    name.addEventListener('focus', function () {
        document.querySelector('.creditcard').classList.remove('flipped');
    });

    cardnumber.addEventListener('focus', function () {
        document.querySelector('.creditcard').classList.remove('flipped');
    });

    expirydatemonth.addEventListener('focus', function () {
        document.querySelector('.creditcard').classList.remove('flipped');
    });

    expirydateyear.addEventListener('focus', function () {
        document.querySelector('.creditcard').classList.remove('flipped');
    });

    securitycode.addEventListener('focus', function () {
        document.querySelector('.creditcard').classList.add('flipped');
    });
});