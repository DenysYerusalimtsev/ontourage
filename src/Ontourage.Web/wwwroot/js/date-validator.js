class DateValidator {
    constructor(firstDate, secondDate) {
        this.firstDate = firstDate;
        this.secondDate = secondDate;
    }

    isValid() {
        var sDate = new Date(this.firstDate);
        var eDate = new Date(this.secondDate);
        return eDate > sDate;
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

            var departureDate = $(self.dateFromId).val();
            var arrivalDate = $(self.dateToId).val();

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
        arrivalError.visible(true);
    }
}