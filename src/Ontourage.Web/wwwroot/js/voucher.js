class VoucherPrinter {
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

var printVoucherButton = document.getElementById('print-voucher');

printVoucherButton.onclick = function() {
    var printer = new VoucherPrinter('voucher-content');
    printer.print();
}