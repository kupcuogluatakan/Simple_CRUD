var ClaimWaybill = ClaimWaybill || {};

(function (claimWaybill) {
    var _claimPeriodId = 0;
    claimWaybill.PageInit = function (claimPeriodId) {
        _claimPeriodId = claimPeriodId;
        $("#btnNew").click(function() {
            window.location = KS.Url.Action("claim-waybill", "new-waybill", null) + "/" + _claimPeriodId;
        });
        $("#btnGo").click(function () {
            if ($("#ClaimWaybillId").val() === null) {
                KS.PageMessage("alert", KS.Resource.SelectWaybill);
                return;
            }
            window.location = KS.Url.Action("claim-waybill", "waybill", null) + "/" + _claimPeriodId + "/" + $("#ClaimWaybillId").val();

        });
    }
})(ClaimWaybill);
