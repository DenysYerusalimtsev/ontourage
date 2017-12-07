class DateValidator {
    constructor(firstDate, secondDate) {
        this.firstDate = firstDate;
        this.secondDate = secondDate;
    }

    isValid() {
        var eDate = new Date(this.firstDate);
        var sDate = new Date(this.secondDate);
        return sDate > eDate;
    }
}

$('#add-edit-form').submit(function (event) {

    var departureDate = document.getElementById('departure-time').value;
    var arrivalDate = document.getElementById('arrival-time').value;

    var validator = new DateValidator(departureDate, arrivalDate);

    if (validator.isValid()) {
        showError(false);
        return;
    }

    event.preventDefault();
    showError(true);
});

function showError(show, selector = '#arrival-date-error') {
    var arrivalError = $(selector);
    arrivalError.text('Дата отправления должна быть раньше, чем дата прибытия.');
    arrivalError.visible(show);
} 