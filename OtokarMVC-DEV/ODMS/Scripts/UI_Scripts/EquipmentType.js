
//Grid Search Parameter
function SearchEquipment() {
    return {
        equipmentTypeName: $('#EquipmentTypeName').val(),
    };
}

//Url ReFormat
var url = document.URL.substr(0, document.URL.lastIndexOf('/'));
url = url.substr(0, url.lastIndexOf('/'));


function Delete(Id) {
    DeleteConfirm(function () {
        $.ajax({
            type: "POST",
            url: url + "/EquipmentType/EquipmentTypeDelete",
            data: { equipmentId: Id },
            traditional: true,
            success: function (result) {
                if (result.Status == 1) {
                    var grid = $('#EquipmentTypeGrid').data('kendoGrid');
                    grid.dataSource.read();
                    SetSuccessMessage(result.Message);
                }                                  
                else {
                    SetErrorMessage(result.Message);
                }
            },
            dataType: "json"
        });
    });
}
