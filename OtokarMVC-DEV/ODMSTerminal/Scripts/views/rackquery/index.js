var Rack = Rack || {};
(function (rack) {

    function checkForm() {
        var $rackId = $("#RackId");
        var $warehouseId = $("#WarehouseId");
        if (parseInt($warehouseId.val() || "0") === 0) {
            KS.PageMessage("danger", KS.Resource.WarningWarehouseSelect);
            $("#WarehouseCode").focus();
            return false;
        } else if (parseInt($rackId.val() || "0") === 0) {
            KS.PageMessage("danger", KS.Resource.WarningRackSelect);
            $("#RackCode").focus();
            return false;
        }
        return true;
    }

    rack.PageInit = function () {
        $("#btnSearch").click(function () {
            if (checkForm() === false) {
                return;
            } else {
                $("#frmSearch").submit();
            }
        });

    }

    var warehouseCheckCompleted = true;
    var rackCheckCompleted = true;

    rack.SearchWareHouse = function () {
        var $warehouseId = $("#WarehouseId");
        var $rackId = $("#RackId");
        var searchRack = $("#WarehouseCode").val().indexOf('&') > 0;
        if (warehouseCheckCompleted == true && $("#WarehouseCode").val().length > 0) {
            warehouseCheckCompleted = false;
            KS.Ajax.Post(KS.Url.Action("RackQuery", "SearchWarehouse"), { warehouseCode: $("#WarehouseCode").val() }, function (json) {
                if (json.WarehouseId) {
                    $warehouseId.val(json.WarehouseId);
                    $("#WarehouseCode").val(json.WarehouseCode);
                }
                if (json.RackId) {
                    $rackId.val(json.RackId);
                    $("#RackCode").val(json.RackCode);
                }
            }, function (json) {
                warehouseCheckCompleted = true;
                if (!json.WarehouseId) {
                    $("#WarehouseCode").focus();
                } else if (!json.RackId && searchRack === true) {
                    $("#RackCode").focus();
                }
            }, ".body");
        }
    };
    rack.SearchRack = function () {
        var $warehouseId = $("#WarehouseId");
        var $rackId = $("#RackId");
        if (parseInt($("#WarehouseId").val() || "0") > 0 && rackCheckCompleted == true && $("#RackCode").val().length > 0) {
            rackCheckCompleted = false;
            KS.Ajax.Post(KS.Url.Action("RackQuery", "SearchRack"), { warehouseId: $warehouseId.val(), rackCode: $("#RackCode").val() }, function (json) {
                if (json.RackId) {
                    $rackId.val(json.RackId);
                    $("#RackCode").val(json.RackCode);
                }
            }, function (json) {
                rackCheckCompleted = true;
                if (!json.RackId) {
                    $("#RackCode").focus();
                }
            }, ".body");
        }
    };


})(Rack);
