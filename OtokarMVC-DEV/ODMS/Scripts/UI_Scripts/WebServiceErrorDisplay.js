
$(document).ready(function () {

    $("#ServiceLogId").val('');

    $("#showSearch").html(resources.Global_Display_Hide_Search_Criteria);
    $("#showSearch").addClass("searchVisible");
    $("#searchDiv").show("slow");   
});

$("body").delegate("#showSearch", "click", function (e) {
    var IsVisible = Boolean($(this).hasClass("searchVisible"));

    if (!IsVisible) {
        $(this).html(resources.Global_Display_Hide_Search_Criteria);
        $(this).addClass("searchVisible");
        $("#searchDiv").show("slow");
    }
    else {
        $(this).html(resources.Global_Display_Search_Criteria);
        $(this).removeClass("searchVisible");
        $("#searchDiv").hide("slow");
    }
});