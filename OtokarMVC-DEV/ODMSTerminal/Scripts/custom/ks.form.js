/// <reference path="ks.base.js" />
/// <reference path="ks.common.js" />

(function(form) {
    form.Serialize = function (formId) {
        var $form = $("#" + formId);
       var object = {};
        var input, inputName, inputVal;
        $("#"+formId).find('input').each(function () {
            input = $(this);
            inputName = input.prop('name');
            inputVal = input.val();
            if (inputName != "") {
                switch (input.prop('type')) {
                    case 'hidden':
                        if(input.prop('id'))
                            if (inputName && inputName != "") {
                                object[inputName] = inputVal;
                                break;
                            }
                        break;
                    case 'text':
                        object[inputName] = inputVal;
                        break;
                    case 'password':
                        object[inputName] = inputVal;
                        break;
                    case 'checkbox':
                        inputName = input.prop('id');
                        if (input.prop('checked') == true) {
                            inputVal = true;
                            object[inputName] = inputVal;
                        } else {
                            inputVal = false;
                            object[inputName] = inputVal;
                        }
                        break;
                    case 'radio':
                        inputName = input.prop('name');
                        if (input.prop('checked') == true) {
                            inputVal = input.val();
                            object[inputName] = inputVal;
                        }
                        break;
                    default:
                        break;
                }
            }
        });
        $("#"+formId).find('textarea').each(function () {
            input = $(this);
            inputName = input.prop('name');
            inputVal = input.val();
            if (inputName != "") {
                object[inputName] = inputVal;
            }
        });

        $("#"+formId).find('select').each(function () {
            input = $(this);
            inputName = input.prop('name');
            inputVal = input.val();
            if (input.prop('multiple')) {
                inputName = input.prop('name');
                inputVal = input.val();
                if (inputVal)
                    object[inputName] = inputVal;
            } else {
                object[inputName] = inputVal;
            }
        });
        if (object != {}) {
            return object;
        } else {
            return null;
        }

    };
    form.InitFilterForm = function (formId) {    
        $("#" + formId).find("input,select,textarea,option").each(function (i, e) {
            $(e).attr("id", "Filter_" + $(e).attr("id"));
            $(e).attr("name", "Filter." + $(e).attr("name"));
        });
    };
    form.Post = function (formId, btn, onSuccess, onComplete) {
        var $form = $("#" + formId);
        if (btn) {
            KS.Ajax.AddLoading(btn);
        }
        if ($form.valid()) {
            $('#shader').fadeIn();
            var data = KS.Form.Serialize(formId);
            KS.Ajax.Post($form.attr("action"), data, function (json) {
                if (json.Result == "Success" && onSuccess)
                    onSuccess(json);
            }, function (json) {
                $('#shader').fadeOut();
                if (btn) {
                    KS.Ajax.RemoveLoading(btn);
                }
                if (onComplete)
                    onComplete(json);
            });
        } else if (btn) {
            KS.Ajax.RemoveLoading(btn);
        }

    }
    form.Init = function (formId) {
    }


}(KS.Form));