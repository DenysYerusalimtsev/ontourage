class CountValidator {
    constructor(firstNumber, secondNumber) {
        this.firstNumber = firstNumber;
        this.secondNumber = secondNumber;
    }

    isValid() {
        var fNum = parseInt(this.firstNumber);
        var sNum = parseInt(this.secondNumber);
        return fNum <= sNum;
    }
}


class CountValidationHandler {
    constructor(firstNumId, secondNumId, formId, errorId, errorMessage) {
        this.firstNumId = firstNumId;
        this.secondNumId = secondNumId;
        this.formId = formId;
        this.errorId = errorId;
        this.errorMessage = errorMessage;
    }

    init() {
        var self = this;
        $(this.formId).submit(function (event) {

            var countFreeVouchers = $(self.firstNumId).val();
            var countOrderedVouchers = $(self.secondNumId).val();

            var validator = new CountValidator(countOrderedVouchers, countFreeVouchers);

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
