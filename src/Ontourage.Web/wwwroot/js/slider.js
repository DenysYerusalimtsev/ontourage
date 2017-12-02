$(document).ready(function () {
    $('#price-range').slider({
        min: 100,
        max: 1000,
        step: 5,
        value: [250, 450]
    });
});