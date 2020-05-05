var Waybill = Waybill || {};
(function (waybill) {
    var partCheckCompleted = true;
    waybill.PageInit=function() {
            $('table.datatable tr:even').addClass('even');
            $('table.datatable tr:odd').addClass('odd');
    }
    waybill.DeletePart = function (claimDismantledPartId) {
        if (confirm(KS.Resource.ConfirmPartDelete)) {
            $("#DismantledPartId").val(claimDismantledPartId);
            $("#frmDeletePart").submit();
        }
    }
    waybill.SearchPart=function() {
        var $btnSave = $("#btnSave");
        var $partCode = $("#PartCode");
        var $partId = $("#PartId");
        $btnSave.attr("disabled", "disabled");
        if (partCheckCompleted === true && $partCode.val().length > 0) {
            partCheckCompleted = false;
            KS.Ajax.Post(KS.Url.Action("ClaimWaybill", "SearchPart"), { partCode: $partCode.val(), claimWaybillId: $("#ClaimWaybillId").val() }, function (json) {
                if (json.PartId) {
                    $partId.val(json.PartId);
                    $("#ClaimDismantledPartId").val(json.ClaimDismantledPartId);
                    $btnSave.removeAttr("disabled");
                }
            }, function (json) {
                partCheckCompleted = true;
                if (!json.PartId) {
                    $partCode.focus();
                    $("#ClaimDismantledPartId").val("");
                    $btnSave.attr("disabled", "disabled");
                }
            }, ".body");
        }

    }


})(Waybill);