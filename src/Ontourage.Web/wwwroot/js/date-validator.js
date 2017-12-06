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

class DateValidationHandler {
    constructor(dateFromId, dateToId, formId, errorId, errorMessage) {
        this.dateFromId = dateFromId;
        this.dateToId = dateToId;
        this.formId = formId;
        this.errorId = errorId;
        this.errorMessage = errorMessage;
    }

    init() {
        var self = this;
        $(this.formId).submit(function (event) {

            var departureDate = $(this.dateFromId).val();
            var arrivalDate = $(this.dateToId).val();

            var validator = new DateValidator(departureDate, arrivalDate);

            if (validator.isValid()) {
                self.showError(false);
                return;
            }

            event.preventDefault();
            self.showError(true);
        });
    }

    showError(show) {
        var arrivalError = $(this.errorId);
        arrivalError.text(this.errorMessage);
        arrivalError.visible(show);
    }
}
