
//公用类库
(function () {
    var phhc = (function () {
        var phhc = function () {
            return phhc.fn.init();
        };

        phhc.fn = phhc.prototype = {
            init: function () {
                return this;
            }
        };

        //
        phhc.extend = phhc.fn.extend = function () {
            var options, name, src, copy,
                    target = arguments[0] || {},
                    i = 1,
                    length = arguments.length;
            if (length === i) {
                target = this;
                --i;
            }

            for (; i < length; i++) {
                if ((options = arguments[i]) != null) {
                    for (name in options) {
                        src = target[name];
                        copy = options[name];

                        if (src === copy) {
                            continue;
                        }
                        if (copy !== undefined) {
                            target[name] = copy;
                        }
                    }
                }
            }
            return target;
        };

        return phhc;
    })();

    window.phhc = window.$x = phhc();
})();


//弹出层扩展
$x.output = $x.output || {};

$x.extend($x.output,
{
    ///	<summary>
    /// 弹出层
    /// <param name="warp" type="string">最外层元素ID
    /// </param>
    /// </summary>
    ShowDiv: function (warp, title, top, w_width, w_height) {
        var _warp = $("#" + warp);
        _warp.easydrag();
        _warp.setHandler(title);

        _warp.css({ width: w_width, height: w_height });

        //处方定位
        this.SetDialogPosition(warp, top);
        _warp.show();
    },
    ///	<summary>
    /// 弹出层
    /// <param name="warp" type="string">最外层元素ID
    /// </param>
    /// </summary>
    ShowDivFixed: function (warp, title, top, w_width, w_height) {
        var _warp = $("#" + warp);
        _warp.easydrag();
        _warp.setHandler(title);

        _warp.css({ width: w_width, height: w_height });

        //处方定位
        this.SetDialogPositionFixed(warp, top);
        _warp.show();
    },
    //显示Iframe
    ShowIframe: function (warp, title, top, w_width, w_height, iframeurl) {
        var _warp = $("#" + warp);
        _warp.easydrag();
        _warp.setHandler(title);

        _warp.css({ width: w_width, height: w_height });

        //给Iframe.Src赋值
        if (iframeurl != undefined && iframeurl != null && iframeurl.length > 0) {
            _warp.find("iframe").attr("src", iframeurl);
        }

        //处方定位
        this.SetDialogPosition(warp, top);
        _warp.show();
    },
    //关闭所有弹出层
    CloseAllDialog: function () {
        CloseDialogByID();
    },
    //关闭弹出层
    CloseDialogByID: function (id) {
        if (id != undefined) {
            jQuery("#" + id).hide();
        }
        else {
            jQuery(".div_ShowDailg").hide();
        }
        CloseDocumentDivBG();
    },
    //关闭背景层
    CloseDocumentDivBG: function () {
        $("#div_documentbg").hide();
    },
    //弹出层位置
    SetDialogPosition: function (dialogId, top) {
        var obj = $("#" + dialogId).parents().filter("body");
        $("#" + dialogId).css("left", (obj.width() - $("#" + dialogId).width()) / 2);
        if (top != undefined) {
            $("#" + dialogId).css("top", $(window).scrollTop() + top);
        }
        else {

            $("#" + dialogId).css("top", $(window).scrollTop() + 100);
        }
    },
    //弹出层绝对定位
    SetDialogPositionFixed: function (dialogId, top) {
        var obj = $("#" + dialogId).parents().filter("body");
        $("#" + dialogId).css("left", (obj.width() - $("#" + dialogId).width()) / 2);
        if (top != undefined) {
            $("#" + dialogId).css("top", top + 30);
            $("#" + dialogId).css("position", "fixed");
        }
        else {
            $("#" + dialogId).css("top", 100);
            $("#" + dialogId).css("position", "fixed");
        }
    }
});