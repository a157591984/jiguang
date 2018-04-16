﻿var AutoCompleteVectors = "AutoCompleteVectors";
var AutoCompleteObjText = "";
var AutoCompleteDataList = "AutoCompleteDataList";
var AutoCompleteIndexNum = 0;
var AutoCompleteSelectItem;
var AutoCompleteAjaxKey = "AjaxAutoCompletePaged"//ok
var AutoCompleteIsJP = "1";
var AutoCompletePageSize = "10";

//设置选择区的位置在操作区之下
function AutoCompleteUnderText(AutoCompleteObjText, AutoCompleteVectors) {
    //bgObj = document.getElementById("div_documentbg");
    var oHtop = $("#" + AutoCompleteObjText).offset().top;
    var scrollTop = document.body.scrollTop;
    var oHtopt = $("#" + AutoCompleteObjText).height();
    var oHleft = $("#" + AutoCompleteObjText).offset().left;
    var s_top
    if ($.browser.msie && $.browser.version < 8) {
        s_top = (oHtop + oHtopt + 2) - scrollTop;
        $("#" + AutoCompleteVectors).css({ "overflow": "hidden", "position": "absolute", "z-index": "1000", "left": oHleft, "top": s_top, "background-color": "White", "width": "150" });
    }
    else {
        s_top = (oHtop + oHtopt + 2);
        $("#" + AutoCompleteVectors).css({ "overflow": "hidden", "position": "absolute", "z-index": "1000", "left": oHleft, "top": s_top, "background-color": "White" });
    }

}
//设置选择区显示
function AutoCompleteShowDiv(AutoCompleteVectors) {
    $("#" + AutoCompleteVectors).show();
}
/*设置选择区隐藏*/
function AutoCompleteHideDiv(AutoCompleteVectors) {
    $("#" + AutoCompleteVectors).hide();
}
/*点评上下选择，与回车确认的处理*/
function AutoCompletekeyup(e) {
    AutoCompleteSelectItem = $("#" + AutoCompleteDataList).find("li");
    var keyCode = e.keyCode || e.charCode;  //兼容ie firefox等事件
    switch (keyCode) {
        case 38: //上
            AutoCompleteIndexNum--;
            if (AutoCompleteIndexNum < 0) {
                AutoCompleteIndexNum = AutoCompleteSelectItem.length - 1;
                AutoCompleteSelectItem[0].className = "AutoCompleteNoSelected";
                AutoCompleteSelectItem[AutoCompleteSelectItem.length - 1].className = "AutoCompleteSelected";
            }
            else {
                AutoCompleteSelectItem[AutoCompleteIndexNum + 1].className = "AutoCompleteNoSelected";
                AutoCompleteSelectItem[AutoCompleteIndexNum].className = "AutoCompleteSelected";
            }
            break;
        case 40: //下
            AutoCompleteIndexNum++;
            if (AutoCompleteIndexNum > AutoCompleteSelectItem.length - 1) {
                AutoCompleteSelectItem[AutoCompleteIndexNum - 1].className = "AutoCompleteNoSelected";
                AutoCompleteSelectItem[0].className = "AutoCompleteSelected"
                AutoCompleteIndexNum = 0;
            }
            else {
                AutoCompleteSelectItem[AutoCompleteIndexNum - 1].className = "AutoCompleteNoSelected";
                AutoCompleteSelectItem[AutoCompleteIndexNum].className = "AutoCompleteSelected"

            }
            break;
            //回车
        case 13:
            //$("#" + AutoCompleteObjText).val($($("#" + AutoCompleteDataList).find('li')[AutoCompleteIndexNum]).attr("desc"));  //获取html，正则过滤标签，只取其值
            //$("#hid" + AutoCompleteObjText).val($($("#" + AutoCompleteDataList).find('li')[AutoCompleteIndexNum]).attr("id"));
            var text = $($("#" + AutoCompleteDataList).find('li')[AutoCompleteIndexNum]).attr("desc");
            var value = $($("#" + AutoCompleteDataList).find('li')[AutoCompleteIndexNum]).attr("id");
            AutoCompleteSelectedItem($("#" + AutoCompleteObjText), text, value);
            AutoCompleteHideDiv(AutoCompleteVectors);
            return false;
            break;
        default:
            //                var li = AutoCompleteSelectItem;
            //                li[0].className = "AutoCompleteSelected";
            break;
    }
}

/* 初始页面 载入事件*/
$(function () {

    //$(document).click(function (e) {
    //  AutoCompleteHideDiv(AutoCompleteVectors);
    //});
    AutoCompleteClick();/*自动下拉注册事件*/
});
/*在生成控件之后重新加载该方法*/
function AutoCompleteClick() {
    $("input[pAutoComplete='True']").each(function () {
        AutoCompleteObjText = $(this).attr("id");
        if ($(this).attr("pAutoCompleteVectors") != undefined) {
            AutoCompleteVectors = $(this).attr("pAutoCompleteVectors");
        }
        else {
            AutoCompleteVectors = "AutoCompleteVectors";
        }
        if ($(this).attr("pAutoCompleteDataList") != undefined) {
            AutoCompleteDataList = $(this).attr("pAutoCompleteDataList");
        }
        else {
            AutoCompleteDataList = "AutoCompleteDataList";
        }
        if ($(this).attr("pAutoCompleteIsJP") != undefined) {
            AutoCompleteIsJP = $(this).attr("pAutoCompleteIsJP");
        }
        else {
            AutoCompleteIsJP = "1";
        }
        //屏蔽$(document).click 方法
        $("#AutoCompleteVectors").live("click", function (e) {
            return false;
        });

        //点击文本对象
        $("#" + AutoCompleteObjText).live("click", function (e) {

            AutoCompleteObjText = $(this).attr("id");
            if ($(this).attr("pAutoCompleteAjaxKey") != undefined) {
                AutoCompleteAjaxKey = $(this).attr("pAutoCompleteAjaxKey");
            }
            else {
                AutoCompleteAjaxKey = "AjaxAutoCompletePaged"
            }
            if ($(this).attr("pAutoCompletePageSize") != undefined) {
                AutoCompletePageSize = $(this).attr("pAutoCompletePageSize");
            }
            else {
                AutoCompletePageSize = 10;
            }
            //alert(AutoCompleteVectors)
            AutoCompleteUnderText(AutoCompleteObjText, AutoCompleteVectors);
            AutoCompleteGetDataListAll(1, AutoCompletePageSize, 1);
            AutoCompletekeyup(e);
            AutoCompleteShowDiv(AutoCompleteVectors);
            return false;
        });

        //在文本对象中输入数据
        $("#" + AutoCompleteObjText).live("keyup", function (e) {

            AutoCompleteObjText = $(this).attr("id");
            if ($(this).attr("pAutoCompleteAjaxKey") != undefined) {
                AutoCompleteAjaxKey = $(this).attr("pAutoCompleteAjaxKey");
            }
            else {
                AutoCompleteAjaxKey = "AjaxAutoCompletePaged"
            }
            if ($(this).attr("pAutoCompletePageSize") != undefined) {
                AutoCompletePageSize = $(this).attr("pAutoCompletePageSize");
            }
            else {
                AutoCompletePageSize = 10;
            }
            AutoCompleteUnderText(AutoCompleteObjText, AutoCompleteVectors);
            var keyCode = e.keyCode || e.charCode;  //兼容ie firefox等事件

            if (keyCode != 38 && keyCode != 40 && keyCode != 13) {
                if (keyCode == 9) {
                    AutoCompleteGetDataListAll(1, AutoCompletePageSize, 1);
                }
                else {
                    AutoCompleteGetDataListAll(1, AutoCompletePageSize, 0);
                }
                AutoCompleteShowDiv(AutoCompleteVectors);
            }
            else {
                AutoCompletekeyup(e);
            }
        });

        //        //焦点进入文本对象
        //        $("#" + AutoCompleteObjText).focusin(function () {
        //           
        //            AutoCompleteUnderText(AutoCompleteObjText, AutoCompleteVectors);
        //            $("#" + AutoCompleteObjText).select();
        //            //AutoCompleteShowDiv(AutoCompleteVectors);
        //        });
    });
};
var guangbiao_ID;
/*屏蔽回车提交事件*/
$(document).ready(function () {
    jQuery(function ($) {
        $("form").keypress(function (e) {
            if (e.keyCode == 13 && guangbiao_ID!="textarea") {
                e.preventDefault();
            }
        });
    });

    $(document).ready(function () {
        var con = $("input");
        for (var i = 0; i < con.length; i++) {
            $(this).mouseover = function () {
                guangbiao_ID = $(this).attr("type");

            }
        }
    });
    //    $(window).scroll(function (e) {
    //        var oHleft = 0;
    //        var parentID = $("#" + AutoCompleteVectors).attr("id")
    //        var oHleft = $("#" + parentID).offset().left;
    //        var x = $("#" + AutoCompleteVectors).offset().top;
    //        var p = 0, t = 0;
    //        var p = $(this).scrollTop();
    //        AutoCompleteUnderText(parentID, AutoCompleteVectors);
    //        //        if (t > p) {
    //        //            $("#" + AutoCompleteVectors).css({ "overflow": "hidden", "position": "absolute", "z-index": "1000", "left": oHleft, "top": x += t, "background-color": "White" });
    //        //        } else {
    //        //            $("#" + AutoCompleteVectors).css({ "overflow": "hidden", "position": "absolute", "z-index": "1000", "left": oHleft, "top": x -= p, "background-color": "White" });
    //        //        }
    //        //        alert(x+","+t + "," + p);
    //    });
});
//获取分页数据
///isClick 1 当点击时， 1 键入时
function AutoCompleteGetDataListAll(PageIndex, PageSize, isClick) {

    var D_Name = $("#" + AutoCompleteObjText).val();
    var ConditionIn = "";
    var ConditionNotIn = "";
    var pAutoCompleteConditionIn = $("#" + AutoCompleteObjText).attr("pAutoCompleteConditionIn");
    var pAutoCompleteConditionNotIn = $("#" + AutoCompleteObjText).attr("pAutoCompleteConditionNotIn");
    if (pAutoCompleteConditionIn != undefined && pAutoCompleteConditionIn != "") {
        ConditionIn = pAutoCompleteConditionIn;
    }
    if (pAutoCompleteConditionNotIn != undefined && pAutoCompleteConditionNotIn != "") {
        ConditionNotIn = pAutoCompleteConditionNotIn;
    }


    if (isClick == "1") {
        D_Name = "";
    }
    var strHtml = "";
    var strNoData = "";
    $.get("../Ajax/AutoSelectAjax.aspx", {
        key: "AjaxAutoCompletePaged"
         , D_Name: D_Name
         , ConditionIn: ConditionIn
         , ConditionNotIn: ConditionNotIn
         , PageIndex: PageIndex
         , PageSize: PageSize
         , D_Type: AutoCompleteAjaxKey
         , IsPY: AutoCompleteIsJP
         , isClick: isClick
         , net4: Math.random()
    },
         function (data) {
             strHtml = "";
             if (data == "0") {
                 strNoData = "<div class='AutoCompleteNoData'>查询出错</div>";
                 $("#topAutoComplete").html(strNoData);
             }
             else if (data == "2") {
                 strNoData = "<div class='AutoCompleteNoData'>" + $("#" + AutoCompleteObjText).val();
                 strNoData += " 未匹配到数据</div>";
                 $("#topAutoComplete").html(strNoData);
             }
             else {

                 $("#topAutoComplete").html("简拼/汉字或↑↓");
                 objJson = eval("(" + data.split("~")[0] + ")");
                 strHtml = "<ul id='AutoCompleteDataList'>";

                 for (var i = 0; i < objJson.length; i++) {
                     strCss = "";
                     if (i == 0) { strCss = "AutoCompleteSelected"; } else { strCss = "AutoCompleteNoSelected" }
                     strHtml += " <li id='" + objJson[i].Common_Dict_ID + "' desc='" + objJson[i].D_Name + "' class='" + strCss + "'>" + objJson[i].D_Name + "</li>";
                 }
                 strHtml += "</ul>";
                 strHtml += data.split("~")[1];
                 $("#divAutoComplete").html(strHtml);

                 AutoCompleteSelectItem = $('#' + AutoCompleteDataList).find('li');
                 AutoCompleteIndexNum = 0;
                 var li = AutoCompleteSelectItem;
                 li[0].className = "AutoCompleteSelected";
                 for (var i = 0; i < li.length; i++) {
                     li[i].onmouseover = function () {
                         this.className = 'AutoCompleteSelected';
                     }
                     li[i].onmouseout = function () {
                         this.className = "AutoCompleteNoSelected";
                     }
                     li[i].onclick = function () {
                         //$("#" + AutoCompleteObjText).val($(this).attr("desc"));
                         //$("#hid" + AutoCompleteObjText).val($(this).attr("id"));
                         AutoCompleteSelectedItem($("#" + AutoCompleteObjText), $(this).attr("desc"), $(this).attr("id"));
                        AutoCompleteHideDiv(AutoCompleteVectors);
                     }
                 }
             }

         }
        );

}
//页面索引发生变化
function AutoCompleteUcPageIndexChange(PageIndex, PageSize) {
    AutoCompleteGetDataListAll(PageIndex, PageSize)
    return false;

}

function AutoCompleteSelectedItem(con, text, value) {

    var event = $(con).attr("onAutoCompleteSelectedItem");
    //alert(event);
    if (event != undefined && event != "") {
        var fun = eval(event);
        fun(con, text, value);
    }
    else {
        BaseSelectedItem(null, text, value);
    }
}


function BaseSelectedItem(con, text, value) {
    $("#" + AutoCompleteObjText).val(text);
    $("#hid" + AutoCompleteObjText).val(value);
}