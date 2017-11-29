class PaymentCheckPrinter {
    constructor(containerId) {
        this.containerId = containerId;
    }

    print() {
        var printContent = document.getElementById(this.containerId).innerHTML;
        var originalContent = document.body.innerHTML;

        document.body.innerHTML = printContent;

        window.print();

        document.body.innerHTML = originalContent;
    }
}

var printButton = document.getElementById('print-check');

printButton.onclick = function() {
    var printer = new PaymentCheckPrinter('content');
    printer.print();
}