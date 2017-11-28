class PriceCalculator {
    constructor(price, count, discount) {
        this.price = price;
        this.count = count;
        this.discount = discount;
    }

    calculate() {
        var disc = this.price * this.discount / 100;
        return (this.price - disc) * this.count;
    }
}

class BuyVoucherHandler {

    constructor() {
        var countRange = document.getElementById("countOrderedVouchers");
        var clientSelect = document.getElementById("clientId");

        var that = this;
        countRange.onchange = function () {
            that.handle();
        }

        clientSelect.onchange = function () {
            that.handle();
        }

    }

    handle() {
        var priceText = document.getElementById("price");
        var countRange = document.getElementById("countOrderedVouchers");
        var clientSelect = document.getElementById("clientId");
        var totalPriceText = document.getElementById("totalPrice");
        var totalPriceHidden = document.getElementById("totalPriceHidden");
        var discountText = document.getElementById("percentages");

        var discount = this.getDiscount(clientSelect);

        var result = this.calculate(
            parseInt(priceText.innerText),
            parseInt(countRange.value),
            discount);

        discountText.innerText = discount;
        totalPriceText.innerText = result;
        totalPriceHidden.value = result;
    }

    calculate(price, count, discount) {
        var calculator = new PriceCalculator(price, count, discount);
        return calculator.calculate();
    }

    getDiscount(clientSelect) {
        var selectedOption = clientSelect.options[clientSelect.selectedIndex];
        var discountString = selectedOption.attributes['data-discount'].value;
        return parseInt(discountString);
    }
}

var handler = new BuyVoucherHandler();
