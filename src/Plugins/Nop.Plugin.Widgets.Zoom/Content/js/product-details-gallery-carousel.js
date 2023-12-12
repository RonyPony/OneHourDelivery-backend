$(document).ready(function () {
    if ($(window).width() <= 1000) {
        $('.gallery > .picture').remove();
        $('.gallery > .picture-thumbs').remove();
        $('#picture-carousel').show();
    }
});