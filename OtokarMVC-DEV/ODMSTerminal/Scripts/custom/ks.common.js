/// <reference path="ks.base.js" />
(function (ks) {
    ks.Ajax = {};
    ks.Ajax.Post = function (url, data, onSuccess, onComplete, btn) {
        if (btn) {
            ks.Ajax.AddLoading(btn);
        }
        var ctype = 'application/json; charset=UTF-8';
        $.ajax(url, {
            processData:  false,
            data: JSON.stringify(data),
            global: true,
            type: 'POST',
            dataType: "json",
            traditional: true,
            contentType: ctype,
            cache: false,
            error: function (jqXHR, textStatus, errorThrown) {
                var json = jqXHR;
                try {
                    json = JSON.parse(jqXHR.responseText);
                    KS.PageMessage("danger", json.Message);
                } catch (e) {
                    if (jqXHR.status===404) {
                        KS.PageMessage("danger", "İçerik bulunamadı.");
                    } else {
                        KS.PageMessage("danger", "Bir hata oluştu.");
                    }
  
                }
                
            },
            success: function (e) {
                if (e.Message && e.Result) {
                    if (e.Result == "Success")
                        KS.PageMessage("success", e.Message);
                    else {
                        KS.PageMessage("danger", e.Message);
                    }
                }
                if (onSuccess) {
                    onSuccess(e);
                }
            },
            complete: function (e) {
                if (btn) {
                    ks.Ajax.RemoveLoading(btn);
                }
                if (onComplete) {
                    e=JSON.parse(e.responseText);
                    onComplete(e);
                }
            }
        });
    };

    ks.Ajax.Get = function (url, data, onSuccess, onComplete, btn) {
        $('#shader').fadeIn();
        if (btn)
            ks.Ajax.AddLoading(btn);
        $.ajax(url, {
            data: data,
            global: true,
            type: 'GET',
            cache: false,
            dataType: "html",
            error: function (jqXHR, textStatus, errorThrown) {

                var json = "";
                try {
                    json = JSON.parse(jqXHR.responseText);
                } catch (e) {

                }
                if (json != "") {
                    KS.PageMessage("danger", json.Message);
                } else if (jqXHR.status == 500) {
                    KS.PageMessage("danger", JSON.parse(jqXHR.responseText).Message);
                } else if (jqXHR.status == 404) {
                    KS.PageMessage("danger", "İçerik bulunamadı.");
                }
            },
            success: function (html) {
                onSuccess(html);
            },
            complete: function () {
                $('#shader').fadeOut();
                if (btn)
                    ks.Ajax.RemoveLoading(btn);
                if (onComplete)
                    onComplete();
            }
        });
    };
     function spinner(className, isHide) {
            if (isHide == true) {
                $(className).find('.spinner').remove();

            }
            else {
                $(className).append('<div class="spinner"></div>');
                $(className).find('.spinner').show();
                if (!($(className).css('position') == 'absolute' || $(className).css('position') == 'relative' || $(className).css('position') == 'fixed')) {
                    $(className).css('position', 'relative');
                }


            }
        }
    ks.Ajax.AddLoading = function () {
        spinner("#content", false);

    }
    ks.Ajax.RemoveLoading = function () {
        spinner(".body", true);
    }

   

    ks.PageMessage = function (type, message) {
        $("#pageMessage").html("").removeClass("text-success").removeClass("text-danger").removeClass("hide");
        if (type == "success")
            $("#pageMessage").addClass("text-success");
        else {
            $("#pageMessage").addClass("text-danger");
        }

        $("#pageMessage").html(message);
        $("#pageMessage").show();
        //Otomatik kapanmasın
        //window.setTimeout(function () {
        //    $("#pageMessage").hide(300, "linear", function () {
        //        ks.PageMessage.Clear();
        //    });
        //}, 4000);
    }

    ks.PageMessage.Clear = function () {
        $("#pageMessage").addClass("hide").html("");
    }


}(KS));

//Common Functions

(function (ks) {

    ks.CloseAjaxContent = function () {
        $("#loaded-content").fadeOut(function () {
            $("#loaded-content").empty();
            $("#page-content").fadeIn();
        });
    };

    ks.ConfirmCloseAjaxContent = function () {
        KS.Common.PopupConfirm('Kapatmak istediğinize emin misiniz?', "Onay", function () {
            ks.CloseAjaxContent();
        });
    };


    ks.QueryStringToJson = function (str) {
        if (str === '') return {};
        var data = {};
        var item;
        if (str.indexOf('&') > 0) {
            var pairs = str.split('&');
            for (var pair in pairs) {
                if (pairs.hasOwnProperty(pair)) {
                    item = pair.split('=');
                    if (item[0]) {
                        data['' + item[0] + ''] = decodeURIComponent(item[1]);
                    }
                }
            }
        }
        else {
            item = str.split('=');
            if (item[0]) {
                data['' + item[0] + ''] = decodeURIComponent(item[1]);
            }
        }
        return data;

    }

    ks.PopupConfirm = function (message, title, confirmFunc) {

        KS.Confirm({
            message: message,
            title: title,
            confirmFunc: confirmFunc

        });
    }
    ks.ParseDate = function (strDate) {
        if (strDate) {
            var from = strDate.split(".");
            f = new Date(from[2].substring(0, 4), from[1] - 1, from[0]);
            return f;
        }
    }
    ks.FormatDateToString = function(date) {
        var dd = date.getDate();
        var mm = date.getMonth() + 1; //January is 0!
        var yyyy = date.getFullYear();

        if (dd < 10) {
            dd = '0' + dd
        }

        if (mm < 10) {
            mm = '0' + mm
        }

        return dd + '.' + mm + '.' + yyyy;
        
    };
    ks.InitPage = function () {
        KS.Ajax.RemoveLoading();
    }
    ks.Focus=function(selector) {
        $(selector).focus();
    }

})(KS.Common);

(function (url) {
    url.Base = "/"; //TODO set inside layout -> KS.Url.Base ='@Url.Content("~")';

    url.Action = function (controller, action, area, params) {
        var uri = '';

        if (area)
            uri = url.Base + area + "/" + controller + "/" + action;
        else {
            uri = url.Base + controller + "/" + action;
        }
        if (params) {
            uri += "?" + params;
        }
        return uri;
    };

})(KS.Url);

