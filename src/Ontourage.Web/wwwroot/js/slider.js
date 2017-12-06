$(document).ready(function () {
    $('#price-range').slider({
        min: 100,
        max: 5000,
        step: 25,
        value: [250, 450]
    });
});

var costFrom = document.getElementById('cost-from');
var costTo = document.getElementById('cost-to');

var button = document.getElementById('price-filter');

button.onclick = function () {
    var values = $('#price-range').val();
    var arr = values.split(',');
    costFrom.value = arr[0];
    costTo.value = arr[1];
}