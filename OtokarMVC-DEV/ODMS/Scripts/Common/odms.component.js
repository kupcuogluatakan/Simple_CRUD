(function(component) {
    component.OpenBeginDate = function (beginDatePicker, endDatePicker) {
        console.log("asdşlm");
        var dateStart = $("#" + beginDatePicker).data("kendoDatePicker");
        var dateEnd = $("#" + endDatePicker).data("kendoDatePicker");
        if(dateStart)
        if ($("#" + endDatePicker).val()) {
            dateStart.max(dateEnd.value());
        } else {
            dateStart.max(new Date(3000, 0, 1));
        }
    }
    component.OpenEndDate = function (endDatePicker, beginDatePicker) {
        var dateStart = $("#" + beginDatePicker).data("kendoDatePicker");
        var dateEnd = $("#" + endDatePicker).data("kendoDatePicker");
        if(dateEnd)
        if ($("#" + beginDatePicker).val()) {
            dateEnd.min(dateStart.value());
        } else {
            dateEnd.min(new Date(1900, 0, 1));
        }
    }

}(ODMS.Component));