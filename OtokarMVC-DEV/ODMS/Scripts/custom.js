/// <reference path="kendo/kendo.all.min.js" />
var root;
var mvc = mvc || {
    controller: '',
    action: '',
    params: null,
    isSecure: false,
    dataType: 'JSON'
};




(function (context) {
    var controller = null;
    var action = '';
    var params = null;
    var isSecure = false;
    var url = '';
    context.build = function (url, controller, action, params, isSecure, dataType) {
        context.controller = controller;
        context.action = action;
        context.params = params;
        context.isSecure = isSecure;
        if (url == null) {
            if (controller == null)
                context.url = context.action;
            else
                context.url = '/' + context.controller + '/' + context.action;
        } else {
            context.url = url;
        }
    };
    context.post = function (beforePost, onSuccess, onError) {
        if (context.isSecure == true) {
            var securityToken = $('[name=__RequestVerificationToken]').val();
            if (securityToken)
                $.extend(context.params, { __RequestVerificationToken: securityToken });
        }
        if (beforePost) {
            beforePost();
        }
        var request = $.ajax({
            url: context.url,
            type: "POST",
            data: context.params,
            dataType: context.dataType,
            complete: function (jqXHR, textStatus) {
                debugger;
                // inspect the content in the jqXHR object
                // jqXHR.getAllResponseHeaders() for example or related methods
            }
        });
        request.success(function (msg) {
            if (onSuccess)
                onSuccess(msg);
        });
        request.fail(function (msg) {
            if (onError)
                onError(msg);
        });
    };
})(mvc);
$.fn.serializeObject = function () {
    var o = {};
    var a = this.serializeArray();
    $.each(a, function () {
        if (o[this.name] !== undefined) {
            if (!o[this.name].push) {
                o[this.name] = [o[this.name]];
            }
            o[this.name].push(this.value || '');
        } else {
            o[this.name] = this.value || '';
        }
    });
    return o;
};

(function ($) {
    $.fn.odmsAutoComplete = function (url, controlId, options) {
        if (url == '')
            return;
        var defaults = {
            minLength: 3,
            dataType: "POST",
            valueField: "Value",
            textField: "Text",
            loadingImage: '../../Images/loading.gif',
            data: {},
            waitTime: 1000,
            resultsBgColor: '#fff',
            resultsBorderColor: ' #c0c0c0',
            resultsMaxHeigth: "120px",
            afterSelect: function () { }
        };
        var $this = $(this);
        var isWaiting = false;
        $.extend(true, defaults, options);
        if ($this.is("input[type=text]") == false) {
            alert("Wrong input type");
            return;
        }
        var wrap = "<div id='" + $this.attr("id") + "-autoCompleteResultsWrap' class='autoCompleteWrap'style='display:inline:block; position:relative;' />";
        var timeOut;
        $this.wrap(wrap);
        $this.attr("autocomplete", "off");
        if (defaults.loadingImage != '')
            $this.closest('.autoCompleteWrap').append("<img class='autoCompleteLoading' style='width:16px; display:none;' src='" + defaults.loadingImage + "'/>");
        $this.bind("keyup", "", function () {
            var length = $this.val().length;
            if (length >= defaults.minLength) {
                isWaiting = true;
                timeOut = setTimeout(function () {
                    search();
                }, defaults.waitTime);
            } else {
                removeResultsWrap();
            }
        });



        function removeResultsWrap() {
            var $resultWrap = $(".autoCompleteResultsWrap", $this.parents('.autoCompleteWrap').eq(0));
            $resultWrap.remove();
            $this.next('.autoCompleteLoading').hide();
        }

        function search() {
            if (isWaiting == false) {
                clearTimeout(timeOut);
                return;
            }
            removeResultsWrap();
            isWaiting = false;
            $.extend(defaults.data, { searchText: $this.val() });
            $this.next('.autoCompleteLoading').show();
            var request = $.ajax({
                url: url,
                type: defaults.dataType,
                data: defaults.data,
                dataType: "json"
            });
            request.success(function (json) {
                $this.next('.autoCompleteLoading').hide();
                showResults(json);
            });
            request.fail(function () {
                $this.next('.autoCompleteLoading').hide();
            });

        }

        function showResults(json) {
            prepareResultsWrapper(json);

        }

        function prepareResultsWrapper(json) {
            var html = "<div class='autoCompleteResultsWrap' style='position:absolute; display:none; padding:5px; z-index:1000; border-radius:2px;'>";
            var liHtml = "<ul class='autoCompleteResultList'>";
            $(json).each(function (i, e) {

                liHtml += "<li class='autoCompleteListItem' data-item-value='" + e[defaults.valueField] + "'><a class= 'k-link'>" + e[defaults.textField] + "</a></li>";
            });
            liHtml += "</ul>";
            html += liHtml + '</div>';
            $this.parents('.autoCompleteWrap').first().append(html);
            var $resultWrap = $(".autoCompleteResultsWrap", $this.parents('.autoCompleteWrap').eq(0));
            $resultWrap.css({
                "border": "1px solid" + defaults.resultsBorderColor,
                "background-color": defaults.resultsBgColor,
                "min-width": $this.width(),
                "max-width": $this.width() + 100,
                "max-height": defaults.resultsMaxHeigth,
                "overflow": "auto"
            });
            $(".autoCompleteResultList").css({
                "padding": "3px 8px",

            });
            $(".autoCompleteListItem").css({
                "list-style": "none",
                "border-bottom": "1px dotted" + defaults.resultsBorderColor
            });
            $(".autoCompleteListItem a", $resultWrap).click(function () {
                selectItem(this);
            });
            $(document).click(function (e) {
                if (e.target.id != $this.attr("id") + "-autoCompleteResultsWrap") {
                    removeResultsWrap();
                }
            });

            $resultWrap.slideDown();
        }
        function selectItem(e) {
            $this.val($(e).html());
            var control = $("#" + controlId);
            if (control.length == 0) {
                //add hidden for the contol
                var mvcId = controlId.replace("[", "_").replace("]", "_").replace(".", "_");
                var html = "<input type='hidden' name='" + controlId + "' id='" + mvcId + "' />";
                $this.closest('.autoCompleteWrap').append(html);
                control = $("#" + mvcId);
            }
            control.val($(e).parents("li").first().data("item-value"));
            var $resultWrap = $(".autoCompleteResultsWrap", $this.parents('.autoCompleteWrap').eq(0));
            $resultWrap.slideUp(function () {
                $(this).remove();
            });

        }
    };
}(jQuery));


(function ($) {
    $.fn.listSearch = function (controlId, options) {
        var list;
        var $this = $(this);
        var orginalDataSource;
        var foundDataSource;
        var searchText = '';
        var isWaiting = false;
        var opts = {
            textField: 'Text',
            valueField: 'Value',
            waitTime: 50,
            cascadeControlId: '',
            disableButtons: false
        };
        $.extend(true, opts, options);
        function init() {
            list = $("#" + controlId).data("kendoListView");
            orginalDataSource = list.dataSource;
            foundDataSource = null;
            searchText = $this.val();
            if (opts.cascadeControlId != '') {
                $('#' + opts.cascadeControlId).change(function () {
                    $this.val('');
                    list.setDataSource(orginalDataSource);
                    list.dataSource.read();
                });
            }
        }
        function search() {
            if (isWaiting == false) {
                clearTimeout(timeOut);
                return;
            }
            isWaiting = false;
            searchText = $this.val();
            if (opts.disableButtons == true) {
                if (searchText.length > 0) {
                    $("input[type=submit],button").attr("disabled", "disabled");
                } else {
                    $("input[type=submit],button").removeAttr("disabled");
                }
            }
            foundDataSource = new kendo.data.DataSource({
                data: orginalDataSource.data(),
                filter: { field: opts.textField, operator: "contains", value: searchText }
            });
            var listView = $("#" + controlId).data("kendoListView");
            list.setDataSource(foundDataSource);
        }
        init();
        $(this).keyup(function () {
            isWaiting = true;
            timeOut = setTimeout(function () {
                search();
            }, opts.waitTime);

        });
    }
}(jQuery));


//*kendo window generator*//
///ref
(function ($) {
    $.fn.kwnd = function (url, title, options) {
        var opts = {
            iframe: false,
            width: 600,
            height: 400,
            modal: true,
            loading: true,
            onClose: function () { },
            beforeOpen: function () { },
            afterLoad: function () { }
        };
        $.extend(true, opts, options);

        $(this).html('');

        function fixTurkishChars(url) {
            url = replaceAll("ü", "u", url);
            url = replaceAll("ş", "s", url);
            url = replaceAll("ç", "c", url);
            url = replaceAll("ı", "i", url);
            url = replaceAll("ğ", "g", url);
            url = replaceAll("ö", "o", url);

            return url;
        }
        function replaceAll(find, replace, str) {
            return str.replace(new RegExp(find, 'g'), replace);
        }
        var wnd = jQuery(this).kendoWindow({
            "close": function (e) {
                $(this).html('');
                if (opts.onClose) {
                    opts.onClose();
                }
            },
            'open': function (e) {
                $(this).html('');
                if (opts.loading == true) {
                    var html = '<div class="k-loading k-loading-image"></div>';
                    $(this.element).prepend(html);

                    $('.k-loading').show();
                }
            },
            'activate': function (e) {
                if (opts.beforeOpen) {
                    opts.beforeOpen();
                }

            },
            "modal": opts.modal,
            "iframe": opts.iframe,
            "draggable": false,
            "pinned": false,
            "title": title.length > 0 ? title : 'window',
            "resizable": false,
            "content": opts.iframe == true ? fixTurkishChars(url) : {
                url: fixTurkishChars(url),
                dataType: 'html'
            },
            "width": opts.width,
            "height": opts.height,
            "actions": ["Close"],
            refresh: function () {
                if (opts.loading == true)
                    $('.k-loading').hide();
            }
        });
    };
}(jQuery));
function OpenWindow(obj) {
    var wnd = $(obj).data("kendoWindow");
    if (!wnd)
        alert('window is not found');
    wnd.center().open();
}


(function ($) {
    $.validator.unobtrusive.adapters.add('greaterthanorequaldate',
    ['other'], function (options) {
        var getModelPrefix = function (fieldName) {
            return fieldName.substr(0, fieldName.lastIndexOf(".") + 1);
        };

        var appendModelPrefix = function (value, prefix) {
            if (value.indexOf("*.") === 0) {
                value = value.replace("*.", prefix);
            }
            return value;
        }

        var prefix = getModelPrefix(options.element.name),
            other = options.params.other,
            fullOtherName = appendModelPrefix(other, prefix),
            element = $(options.form).find(":input[name=" + fullOtherName +
                "]")[0];

        options.rules['greaterthanorequaldate'] = element;
        if (options.message != null) {
            options.messages['greaterthanorequaldate'] = options.message;
        }
    });
    $.validator.addMethod('greaterthanorequaldate',
        function (value, element, params) {
            var date = $(element).data("kendoDatePicker").value();
            var dateToCompareAgainst = $(params).data("kendoDatePicker").value();
            console.log(element, value, params)
            //if (isNaN(date.getTime()) || isNaN(dateToCompareAgainst.getTime())) {
            //    return false;
            //}
            if (isNaN(dateToCompareAgainst.getTime())) {
                return true;
            }

            return date >= dateToCompareAgainst;
        });

})(jQuery);


function OpenStartDate() {
    var dateStart = $("#StartDate").data("kendoDatePicker");
    var dateEnd = $("#EndDate").data("kendoDatePicker");

    if ($("#EndDate").val()) {
        dateStart.max(dateEnd.value());
    } else {
        if (dateEnd.max()) {
            dateStart.max(dateEnd.max());
        } else {
            dateStart.max(new Date(2099, 0, 1));
        }
    }
}

function OpenEndDate() {
    var dateStart = $("#StartDate").data("kendoDatePicker");
    var dateEnd = $("#EndDate").data("kendoDatePicker");

    if ($("#StartDate").val()) {
        dateEnd.min(dateStart.value());
    } else {
        dateEnd.min(new Date(1900, 0, 1));
    }
}

function PrepareDatePickers() {
    $("#StartDate" + ",#EndDate").keypress(function (evt) {
        var keycode = evt.charCode || evt.keyCode;
        if (keycode == 9) { //allow Tab through
            return true;
        } else {
            return false;
        }
    });
}

function GetFilterNameAndValues(filterId) {
    var $filterWrap = $("#" + filterId);
    var arr = [];
    $("label", $filterWrap).each(function (i, e) {
        var value = GetText($(e).attr("for"));
        arr[i] = { FilterName: $(e).html(), FilterValue: value }
    });
    return arr;
}
function GetText(id) {
    var $id = $("#" + id);
    var propType = $id.prop('type')
    console.log(propType)
    switch (propType) {
        case "text":
            if ($id.data("role")) {
                if ($id.data("role") == "datepicker")
                    return $id.val();
                if ($id.data("role") == "combobox")
                    return $id.data("kendoComboBox").text();
            }
            return $id.val();
        case "textarea":
            return $id.val();
        case "select-multiple":
            if ($id.data("role") == "multiselect") {
                var valuesString = '';
                for (var i = 0; i < $id.data("kendoMultiSelect").dataItems().length; i++) {
                    valuesString += $id.data("kendoMultiSelect").dataItems()[i].Text + ', ';
                }
                if (valuesString.length > 0) {
                    valuesString = valuesString.substring(0, valuesString.length - 2);
                }
                return valuesString;
            }
        case "select":
            return $id.text();
        case "checkbox":
            if ($id.prop("checked")) {
                return "Evet";
            }
            return "Hayır";
        default:
            return "";
    }


}

function SetUpValidation(obj) {
    try {
        var settings = $('#' + obj).validate().settings;
        if (settings) {
            settings.wrapper = "label";
            settings.showErrors = function (errorMap, errorList) {
                this.defaultShowErrors();
                $(".field-validation-error").each(function (i, e) {
                    if ($("a", e).length > 0) return;
                    var html = $("label", e).text().replace("'", "").replace("'", "");
                    var newHtml = "<a title='" + html + "' data-toggle='tool-tip'><img style='width:16px'  class='' src='" + root + "/images/messagebox_warning.png' /> </a>";
                    $(e).html(newHtml);

                    $("a", e).tooltip({ placement: "right" });
                });
            };
        }
        $(".field-validation-error").each(function (i, k) {
            var html = $(k).html().replace("'", "").replace("'", "");
            var newHtml = "<a title='" + html + "' data-toggle='tool-tip'><img style='width:16px'  class='' src='" + root + "/images/messagebox_warning.png' /> </a>";
            $(k).html(newHtml);

            $("a", k).tooltip({ placement: "right", html: true });
        });
    }
    catch (m) { }
}


function moneyFormat(n) {
    return n.toFixed(2).replace(/./g, function (c, i, a) {
        return i > 0 && c !== "." && (a.length - i) % 3 === 0 ? "," + c : c;
    });
}

//onkeypress="return isNumberKey(event)"
function isNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57))
        return false;
    return true;
}

