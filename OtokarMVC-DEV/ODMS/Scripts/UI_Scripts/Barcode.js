var Barcode = function () { };

var GridName = "";
var Ids = [];

$(document).ready(function () {
    $('.message').hide();

    $("#Barcode").keypress(function (e) {
        if (e.which == 13) {
            Ids = Read();
        }
    });
});

Barcode.prototype.Initiliaze = function (gridName) {
    GridName = gridName;
};

function Read() {
    var myArray = [];
    var isFound = false;

    var grid = $('#' + GridName).data('kendoGrid');

    if ($("#Barcode").val() != "") {

        for (i in grid.dataSource.view()) {
            if ($("#Barcode").val() == grid.dataSource.view()[i].Barcode) {

                myArray[i] = grid.dataSource.view()[i].Id;

                isFound = true;
            }
        }

        if (!isFound) {
            $('.message').show();
        } else {
            $('.message').hide();
        }       
    }

    return myArray;
}

Barcode.prototype.GetIds = function () {
    return Ids;
};