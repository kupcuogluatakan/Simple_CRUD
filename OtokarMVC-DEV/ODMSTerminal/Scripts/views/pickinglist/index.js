var PickingList = PickingList || {};
(function (picking) {
    picking.PageInit=function() {
        $('table.datatable tr:even').addClass('even');
        $('table.datatable tr:odd').addClass('odd');
        picking.EnableSaveButton();
        $("#btnGo").click(function() {
            picking.FocusRack();
        });
        $("#btnSave").click(function() {
            $("#frmDetails").submit();
        });
    }

    picking.EnableSaveButton=function() {
        var total = 0;
        $("#racklist .text").each(function (i, e) {
            total =total+ parseFloat($(e).val().toString().replace(",",".")||0);
        });
        if (total > 0 && total<=parseFloat($("#LeftQuantity").val()||0)) {
            $("#btnSave").removeAttr("disabled").removeClass("disabled").addClass("success");
        } else {
            $("#btnSave").attr("disabled", "disabled").addClass("disabled").removeClass("success");
        }
    }

    picking.FocusRack=function() {
        var $rackCode = $("#RackCode");
        var tr = $("#racklist tr[data-rackcode='" + $rackCode.val() + "']").eq(0);
        if (tr) {
            $(".text", tr).select().focus();
        }
    }

    picking.SearchWareHouse = function () {
        var $rackCode = $("#RackCode");
        var $warehouseCode = $("#WarehouseCode");
        var searchRack = $warehouseCode.val().indexOf('&') > 0;
        if ($warehouseCode.val().length > 0) {
           if (searchRack === true) {
               var arr = $warehouseCode.val().toString().split("&", 2);
               $warehouseCode.val(arr[0]);
               $rackCode.val(arr[1]);
               $("#btnGo").focus();
           }
        }
    };

    picking.SearchRack=function() {
        var $rackCode = $("#RackCode");
        if ($rackCode.val().length > 0) {
                $("#btnGo").focus();
        }
    }


})(PickingList);