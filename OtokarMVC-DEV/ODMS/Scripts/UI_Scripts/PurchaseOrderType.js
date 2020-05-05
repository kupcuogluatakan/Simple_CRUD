$(document).ready(function () {

    $("#ProposalType").keypress(function () {
        if (IsNumeric($("#ProposalType").val())) {
            $("#ProposalType").val('');
        }
    });

    $("#ProposalType").keyup(function () {
        if (IsNumeric($("#ProposalType").val())) {
            $("#ProposalType").val('');
        }
    });

    $("#ProposalType").bind('keyup', function (e) {
        $("#ProposalType").val(($("#ProposalType").val()).toUpperCase());
    });
});

function IsNumeric(input) {
    return (input - 0) == input && ('' + input).replace(/^\s+|\s+$/g, "").length > 0;
}

