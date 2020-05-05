//function bindParentCloseEvent(parentWindowId, originalParentState, searchWindowState, gotParentWindow) {
//    var parentWnd = getParentWindow(parentWindowId);
//    if (parentWnd) {
//        parentWnd.bind('close', function (e) {
//            restoreParentWindowState(parentWindowId, originalParentState, searchWindowState, gotParentWindow);
//        });

//    }
//}

//function setFoundValues(referenceObjectId, foundId, foundText, callBackFunction, parentWindowId, searchWindowState, originalParentState, gotParentWindow) {
//    var searchTextContainerId = "#" + referenceObjectId + "TextContainer";
//    var searchValueContainerId = "#" + referenceObjectId;
//    var searchWindowId = "#" + referenceObjectId + "SearchWindow";
//    $(searchValueContainerId).val(foundId);
//    $(searchTextContainerId).val(foundText);
//    closeWindow(searchWindowId);
//    if (callBackFunction != null && window[callBackFunction]) {
//        window[callBackFunction]();
//    }
//}

//function getParentWindow(parentWindowId) {
//    if (!parentWindowId || parentWindowId == "")
//        return;
//    parentWindowId = parentWindowId.replace("#", "");

//    var wnd = null;
//    wnd = $("#" + parentWindowId).data("kendoWindow");
//    if (!wnd)
//        try {
//            wnd = parent.$("#" + parentWindowId).data("kendoWindow");
//        } catch (e) {
//            try {
//                wnd = parent.parent.$("#" + parentWindowId).data("kendoWindow");
//            } catch (ex) {
//                wnd = $("#" + parentWindowId).data("kendoWindow");
//            }
//        }
//    return wnd;
//}


//function openWindow(targetWindowId, targetUrl) {
//    var windowWidget = $(targetWindowId).data("kendoWindow");
//    $(targetWindowId).html('');
//    windowWidget.refresh({
//        url: targetUrl
//    });

//    windowWidget.center();
//    windowWidget.open();
//}

//function closeWindow(targetWindowId) {
//    var windowWidget = $(targetWindowId).data("kendoWindow");
//    if (windowWidget)
//        windowWidget.close();
//}


//function calculateNewWindowState(originalWindowState, targetWindowState, isForOpening) {
//    var returnValue = null;
//    if (isForOpening) {
//        var widthDifference = (targetWindowState.width - originalWindowState.width);
//        var newWidth = widthDifference <= 0 ? originalWindowState.width : targetWindowState.width;
//        var heightDifference = (targetWindowState.height - originalWindowState.height);
//        var newHeight = heightDifference <= 0 ? originalWindowState.height : targetWindowState.height;

//        var newLeft = widthDifference <= 0 ? originalWindowState.left : (originalWindowState.left - (widthDifference / 2));
//        var newTop = heightDifference <= 0 ? originalWindowState.top : (originalWindowState.top - (heightDifference / 2));

//        returnValue = { height: newHeight, width: newWidth, left: newLeft, top: newTop };
//    }
//    else {
//        returnValue = { height: originalWindowState.height, width: originalWindowState.width, left: originalWindowState.left, top: originalWindowState.top };
//    }
//    return returnValue;
//}


//function getWindowState(parentWindowId, isFromParent) {
//    var parentWindow = getParentWindow(parentWindowId);
//    if (!parentWindow)
//        return;
//    var wrapper = parentWindow.wrapper;
//    var returnValue = null;

//    var retHeight = parseFloat(wrapper.css("height").replace("px", ""));
//    var retWidth = parseFloat(wrapper.css("width").replace("px", ""));
//    retWidth = !isFromParent ? retWidth + 50 : retWidth;
//    retHeight = !isFromParent ? retHeight + 100 : retHeight;
//    var retLeft = parseFloat(wrapper.css("left").replace("px", ""));
//    var retTop = parseFloat(wrapper.css("top").replace("px", ""));

//    returnValue = { height: retHeight, width: retWidth, left: retLeft, top: retTop };

//    return returnValue;
//}

//function setParentWindowState(parentWindowId, targetWindowId, targetUrl, originalState, targetState, gotParentWindow) {
//    if (gotParentWindow) {
//        var parentWindow = getParentWindow(parentWindowId);
//        if (!parentWindow)
//            return;
//        var wrapper = parentWindow.wrapper;
//    }

//    if (gotParentWindow==true && shouldResizeParentWindow) {
//        var newParentWindowState = calculateNewWindowState(originalState, targetState, true);
//        $(wrapper, window.parent.document).animate({
//            top: newParentWindowState.top,
//            height: newParentWindowState.height
//        }, 500, function () {
//            $(wrapper, window.parent.document).animate({
//                left: newParentWindowState.left,
//                width: newParentWindowState.width
//            }, 500, function () {
//                openWindow(targetWindowId, targetUrl);

//            });
//        });

//    }
//    else {
//        openWindow(targetWindowId, targetUrl);
//    }
//}

//function restoreParentWindowState(parentWindowId, originalParentState, searchWindowState, gotParentWindow) {  
//    if (gotParentWindow) {
//        var parentWindow = getParentWindow(parentWindowId);
//        if (!parentWindow)
//            return;
//        var wrapper = parentWindow.wrapper;
//    }
//    if (gotParentWindow == true && shouldResizeParentWindow(originalParentState, searchWindowState)) {
//        var newParentWindowState = calculateNewWindowState(originalParentState, searchWindowState, false);
//        $(wrapper, window.parent.document).animate({
//            top: newParentWindowState.top,
//            height: newParentWindowState.height
//        }, 500, function () {
//            $(wrapper, window.parent.document).animate({
//                left: newParentWindowState.left,
//                width: newParentWindowState.width
//            });
//        });
//    }
//}


//function shouldResizeParentWindow(originalWindowState, targetWindowState) {
//    if (targetWindowState.width <= originalWindowState.width && targetWindowState.height <= originalWindowState.height)
//        return false;
//    return true;
//}
function bindParentCloseEvent(parentWindowId, originalParentState, searchWindowState, gotParentWindow) {
    var parentWnd = getParentWindow(parentWindowId);
    if (parentWnd) {
        parentWnd.bind('close', function (e) {
            restoreParentWindowState(parentWindowId, originalParentState, searchWindowState, gotParentWindow);
        });

    }
}

function setFoundValues(referenceObjectId, foundId, foundText, callBackFunction, parentWindowId, searchWindowState, originalParentState, gotParentWindow) {
    var searchTextContainerId = "#" + referenceObjectId + "TextContainer";
    var searchValueContainerId = "#" + referenceObjectId;
    var searchWindowId = "#" + referenceObjectId + "SearchWindow";
    $(searchValueContainerId).val(foundId);
    $(searchTextContainerId).val(foundText);
    closeWindow(searchWindowId);
    if (callBackFunction != null && window[callBackFunction]) {
        window[callBackFunction]();
    }
}

function getParentWindow(parentWindowId) {
    if (!parentWindowId || parentWindowId == "")
        return;
    parentWindowId = parentWindowId.replace("#", "");

    var wnd = null;
    wnd = $("#" + parentWindowId).data("kendoWindow");
    if (!wnd)
        try {
            wnd = parent.$("#" + parentWindowId).data("kendoWindow");
        } catch (e) {
            try {
                wnd = parent.parent.$("#" + parentWindowId).data("kendoWindow");
            } catch (ex) {
                wnd = $("#" + parentWindowId).data("kendoWindow");
            }
        }
    return wnd;
}


function openWindow(targetWindowId, targetUrl) {
    var windowWidget = $(targetWindowId).data("kendoWindow");
    $(targetWindowId).html('');
    windowWidget.refresh({
        url: targetUrl
    });

    windowWidget.center();
    windowWidget.open();
}

function closeWindow(targetWindowId) {
    var windowWidget = $(targetWindowId).data("kendoWindow");
    if (windowWidget)
        windowWidget.close();
}


function calculateNewWindowState(originalWindowState, targetWindowState, isForOpening) {
    var returnValue = null;
    if (isForOpening) {
        var widthDifference = (targetWindowState.width - originalWindowState.width);
        var newWidth = widthDifference <= 0 ? originalWindowState.width : targetWindowState.width;
        var heightDifference = (targetWindowState.height - originalWindowState.height);
        var newHeight = heightDifference <= 0 ? originalWindowState.height : targetWindowState.height;

        var newLeft = widthDifference <= 0 ? originalWindowState.left : (originalWindowState.left - (widthDifference / 2));
        var newTop = heightDifference <= 0 ? originalWindowState.top : (originalWindowState.top - (heightDifference / 2));

        returnValue = { height: newHeight, width: newWidth, left: newLeft, top: newTop };
    }
    else {
        returnValue = { height: originalWindowState.height, width: originalWindowState.width, left: originalWindowState.left, top: originalWindowState.top };
    }
    return returnValue;
}


function getWindowState(parentWindowId, isFromParent) {
    var parentWindow = getParentWindow(parentWindowId);
    if (!parentWindow)
        return;
    var wrapper = parentWindow.wrapper;
    var returnValue = null;

    var retHeight = parseFloat(wrapper.css("height").replace("px", ""));
    var retWidth = parseFloat(wrapper.css("width").replace("px", ""));
    retWidth = !isFromParent ? retWidth + 50 : retWidth;
    retHeight = !isFromParent ? retHeight + 100 : retHeight;
    var retLeft = parseFloat(wrapper.css("left").replace("px", ""));
    var retTop = parseFloat(wrapper.css("top").replace("px", ""));

    returnValue = { height: retHeight, width: retWidth, left: retLeft, top: retTop };

    return returnValue;
}

function setParentWindowState(parentWindowId, targetWindowId, targetUrl, originalState, targetState, gotParentWindow) {
    if (gotParentWindow) {
        var parentWindow = getParentWindow(parentWindowId);
        if (!parentWindow)
            return;
        var wrapper = parentWindow.wrapper;
    }

    if (gotParentWindow == true && shouldResizeParentWindow) {
        var newParentWindowState = calculateNewWindowState(originalState, targetState, true);
        $(wrapper, window.parent.document).animate({
            top: newParentWindowState.top,
            height: newParentWindowState.height
        }, 500, function () {
            $(wrapper, window.parent.document).animate({
                left: newParentWindowState.left,
                width: newParentWindowState.width
            }, 500, function () {
                openWindow(targetWindowId, targetUrl);

            });
        });

    }
    else {
        openWindow(targetWindowId, targetUrl);
    }
}

function restoreParentWindowState(parentWindowId, originalParentState, searchWindowState, gotParentWindow) {
    if (gotParentWindow) {
        var parentWindow = getParentWindow(parentWindowId);
        if (!parentWindow)
            return;
        var wrapper = parentWindow.wrapper;
    }
    if (gotParentWindow == true && shouldResizeParentWindow(originalParentState, searchWindowState)) {
        var newParentWindowState = calculateNewWindowState(originalParentState, searchWindowState, false);
        $(wrapper, window.parent.document).animate({
            top: newParentWindowState.top,
            height: newParentWindowState.height
        }, 500, function () {
            $(wrapper, window.parent.document).animate({
                left: newParentWindowState.left,
                width: newParentWindowState.width
            });
        });
    }
}


function shouldResizeParentWindow(originalWindowState, targetWindowState) {
    if (targetWindowState.width <= originalWindowState.width && targetWindowState.height <= originalWindowState.height)
        return false;
    return true;
}
