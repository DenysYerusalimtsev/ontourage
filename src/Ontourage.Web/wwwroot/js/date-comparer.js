class Comparer {
    constructor(firstDate, secondDate) {
        this.firstDate = firstDate;
        this.secondDate = secondDate;
    }

    DateCheck() {

        var StartDate = document.getElementById(this.firstDate).value;
        var EndDate = document.getElementById(this.secondDate).value;

        var eDate = new Date(EndDate);
        var sDate = new Date(StartDate);
        if (sDate > eDate) {
            return false;
        }
    }
}

var button = document.getElementById('create');

button.onclick = function () {
    var fdate = document.getElementById('departure-date');
    var sdate = document.getElementById('arrival-time-date');
    var comparer = new Comparer(fdate, sdate);
    comparer.DateCheck();
}