var StockMovement = StockMovement || {};
(function (stockMovement) {
    function checkForm() {
        var $rackId = $("#RackId");
        var $partId = $("#PartId");
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
        else if (parseInt($partId.val() || "0") === 0) {
            KS.PageMessage("danger", KS.Resource.WarningPartSelect);
            $("#PartCode").focus();
            return false;
        }
        return true;
    }
    var warehouseCheckCompleted = true;
    var rackCheckCompleted = true;
    stockMovement.PageInit=function() {
        $("#btnSearch").click(function () {
            if (checkForm()===false) {
                return;
            } else {
                $("#frm").submit();
            }
        });
    }
    stockMovement.SearchWareHouse = function() {
        var $warehouseId = $("#WarehouseId");
        var $rackId = $("#RackId");
        var searchRack = $("#WarehouseCode").val().indexOf('&') > 0;
        if (warehouseCheckCompleted == true && $("#WarehouseCode").val().length>0) {
            warehouseCheckCompleted = false;
            KS.Ajax.Post(KS.Url.Action("StockMovement", "SearchWarehouse"), { warehouseCode: $("#WarehouseCode").val() }, function(json) {
                if (json.WarehouseId) {
                    $warehouseId.val(json.WarehouseId);
                    $("#WarehouseCode").val(json.WarehouseCode);}
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
    stockMovement.SearchTargetWareHouse = function () {
        var $warehouseId = $("#TargetWarehouseId");
        var $rackId = $("#TargetRackId");
        var searchRack = $("#TargetWarehouseCode").val().indexOf('&') > 0;
        if (warehouseCheckCompleted == true && $("#TargetWarehouseCode").val().length > 0) {
            warehouseCheckCompleted = false;
            KS.Ajax.Post(KS.Url.Action("StockMovement", "SearchWarehouse"), { warehouseCode: $("#TargetWarehouseCode").val() }, function (json) {
                console.log(parseInt(json.RackId), parseInt($("#RackId").val()));
                if (json.WarehouseId) {
                    $warehouseId.val(json.WarehouseId);
                    $("#TargetWarehouseCode").val(json.WarehouseCode);
                }
                if (json.RackId && parseInt(json.RackId) !== parseInt($("#RackId").val())) {
                    $rackId.val(json.RackId);
                    $("#TargetRackCode").val(json.RackCode);
                } else if (parseInt(json.RackId) === parseInt($("#RackId").val())) {
                    KS.PageMessage("danger", KS.Resource.WarningSelectDifferentRack);
                }
                
            }, function (json) {
                warehouseCheckCompleted = true;
                if (!json.WarehouseId) {
                    $("#TargetWarehouseCode").focus();
                } else if (!json.RackId && searchRack === true) {
                    $("#TargetRackCode").focus();
                }
            }, ".body");
        }
    };
    stockMovement.SearchRack = function() {
        var $warehouseId = $("#WarehouseId");
        var $rackId = $("#RackId");
        if (parseInt($("#WarehouseId").val() || "0") > 0 && rackCheckCompleted == true && $("#RackCode").val().length>0) {
            rackCheckCompleted = false;
            KS.Ajax.Post(KS.Url.Action("StockMovement", "SearchRack"), { warehouseId: $warehouseId.val(), rackCode: $("#RackCode").val() }, function(json) {
                if (json.RackId) {
                    $rackId.val(json.RackId);
                    $("#RackCode").val(json.RackCode);
                    $("#PartCode").focus();
                } else if (json.RackId == $rackId.val()) {
                    KS.PageMessage("danger", KS.Resource.WarningSelectDifferentRack);
                }
            }, function (json) {
                rackCheckCompleted = true;
                if (!json.RackId) {
                    $("#RackCode").focus();
                }
            }, ".body");
        }
    };
    stockMovement.SearchTargetRack = function () {
        var $warehouseId = $("#TargetWarehouseId");
        var $rackId = $("#TargetRackId");
        if (parseInt($("#TargetWarehouseId").val() || "0") > 0 && rackCheckCompleted == true && $("#TargetRackCode").val().length > 0) {
            rackCheckCompleted = false;
            KS.Ajax.Post(KS.Url.Action("StockMovement", "SearchRack"), { warehouseId: $warehouseId.val(), rackCode: $("#TargetRackCode").val() }, function (json) {
                if (json.RackId && $("#RackId").val() != json.RackId) {
                    $rackId.val(json.RackId);
                    $("#TargetRackCode").val(json.RackCode);
                    $("#TargetQuantity").focus();
                }
                else if ($("#RackId").val() == json.RackId) {
                    KS.PageMessage("danger", KS.Resource.WarningSelectDifferentRack);
                }
            }, function (json) {
                rackCheckCompleted = true;
                if (!json.RackId) {
                    $("#TargetRackCode").focus();
                }
            }, ".body");
        }
    };
    stockMovement.SetRackFocus = function(setFocus) {
        if (setFocus === true) {
            $("#PartCode").focus();
        } else {
            $("#RackCode").focus();
        }
    };
    stockMovement.SearchPart = function() {
        var $rackId = $("#RackId");
        var $partId = $("#PartId");
        var $partCode = $("#PartCode");
        if (parseInt($rackId.val() || "0") === 0) {
            $("#RackCode").focus();
            return;
        }
        if (parseInt($rackId.val() || "0") > 0 && $partCode.val().length > 0) {
            KS.Ajax.Post(KS.Url.Action("StockMovement", "SearchPart"), { warehouseId: $("#WarehouseId").val(), rackId: $rackId.val(), partCode: $partCode.val() }, function(json) {
                if (json.PartId) {
                    $partId.val(json.PartId);
                }
            }, function(json) {
                if (!json.PartId) {
                    $partCode.focus();
                }
            }, ".body");
        }
    };
    stockMovement.GotoStockAddressChnage = function() {
        var $rackId = $("#RackId");
        var $partId = $("#PartId");
        var $warehouseId = $("#WarehouseId");
        if (parseInt($warehouseId.val() || "0") === 0) {
            KS.PageMessage("danger", KS.Resource.WarningWarehouseSelect);
            $("#WarehouseCode").focus();
            return;
        } else if (parseInt($rackId.val() || "0") === 0) {
            KS.PageMessage("danger", KS.Resource.WarningRackSelect);
            $("#RackCode").focus();
            return;
        } else if (parseInt($partId.val() || "0") === 0) {
            KS.PageMessage("danger", KS.Resource.WarningPartSelect);
            $("#PartCode").focus();
            return;
        }
        var params = $.param({ warehouseId: $warehouseId.val(), rackId: $rackId.val(), partId: $partId.val() });

        window.location.href = KS.Url.Action("stock-movement", "address-change", null, params);
    };
})(StockMovement);