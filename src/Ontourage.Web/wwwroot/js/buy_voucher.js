class PriceCalculator {
    constructor(price, count) {
        this.price = price;
        this.count = count;
    }

    calculate() {
        return this.price * this.count;
    }
}

class BuyVoucherHandler {

    constructor() {
        var countRange = document.getElementById("countOrderedVouchers");

        var that = this;
        countRange.onchange = function () {
            that.handle();
        }
    }

    handle() {
        var priceText = document.getElementById("price");
        var countRange = document.getElementById("countOrderedVouchers");
        var totalPriceText = document.getElementById("totalPrice");
        var totalPriceHidden = document.getElementById("totalPriceHidden");

        var result = this.calculate(
            parseInt(priceText.innerText),
            parseInt(countRange.value));

        totalPriceText.innerText = result;
        totalPriceHidden.value = result;
    }

    calculate(price, count) {
        var calculator = new PriceCalculator(price, count)
        return calculator.calculate();
    }
}

var handler = new BuyVoucherHandler();

