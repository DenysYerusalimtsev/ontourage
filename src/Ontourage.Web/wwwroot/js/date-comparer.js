class DateValidator {
    DateValidator(firstDate, secondDate) {
        this.firstDate = firstDate;
        this.secondDate = secondDate;
    }

    isValid() {
        var eDate = new Date(this.firstDate);
        var sDate = new Date(this.secondDate);
        return sDate > eDate;
    }
}

$('#date-filter-form').submit(function (event) {
    event.preventDefault();

    var departureDate = document.getElementById('departure-date').value;
    var arrivalDate = document.getElementById('arrival-time-date').value;

    var validator = new DateValidator(departureDate, arrivalDate);

    return validator.isValid();
});