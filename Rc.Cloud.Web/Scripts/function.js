$.ajaxSetup({
    async: false
});
var isCH = true;
$(document).ready(function () {
    isCH = $("#hd_language").val() == "" ? true : false;
    var menuCon = $(".div_menu_select");
    jQuery.each(jQuery("td"), function (i, td) { if ($(td).html() == "" || $(td).html() == null) { $(td).html("&nbsp;"); } });
    $(".tr_con_001").hover(function () { $(this).css("background", "#E2EFF5"); }, function () { $(this).css("background", "#fff"); });
    $(".tr_con_002").hover(function () { $(this).css("background", "#E2EFF5"); }, function () { $(this).css("background", "#F7F7F7"); });



    /*
ajax WebMethod 2015-1-13
*/
    $.ajaxWebService = function (url, dataMap, fnSuccess, fnError, asyncT) {
        if (asyncT != false) {
            asyncT = true;
        }
        var idx = '';
        $.ajax({
            type: "POST",
            async: asyncT,
            contentType: "application/json; charset=utf-8",
            url: url,
            data: dataMap,
            dataType: "json",
            beforeSend: function () {
                idx = layer.load();
            },
            success: fnSuccess,
            complete: function () {
                layer.close(idx);
            },
            error: fnError
        });
    }

    $(".div_right_001").click(function () {
        if ($("#sideBar").css("display") != "none") {
            $("#sideBar").hide();
            $(".div_right_001").attr("title", "显示左边菜单");
            $(".div_right_001").attr("class", "div_right_002");
            $("body").css("backgroundPosition", "-198px 0px");
            $("body").attr("class", "body2");


        }
        else {

            $("#sideBar").show();
            $(".div_right_002").attr("title", "隐藏左边菜单");
            $(".div_right_002").attr("class", "div_right_001");
            $(".div_right_content2").attr("class", "div_right_content");
            $("body").css("backgroundPosition", "2px 0px");
            $("body").attr("class", "body1");


        }
    });
});
//获得obj元素的绝对距离中都left值，如果传递obj1，返回的是obj.left-obj1.width/2
function getPositionLeft(obj, obj1) {
    var w = obj.offsetWidth, h = obj.offsetHeight;
    //从目标元素开始向外遍历，累加top和left值   
    for (var t = obj.offsetTop, l = obj.offsetLeft; obj = obj.offsetParent;) {
        l += obj.offsetLeft;
    }
    if (obj1 != undefined) {
        var w = obj1.width;
        t -= w / 2;
    }
    else { l -= 65; }

    return l;

}

function Handel(isSuccess, mes) {

    layer.msg(mes, {
        icon: isSuccess,
        time: 2000 //2秒关闭（如果不配置，默认是3秒）
    }, function () {
        if (isSuccess == 2) return false;
        //do something
        if (isSuccess == 1) window.location.href = "classList.aspx";

    });
}
function SetDialogPosition(dialogId, top) {

    //    $("#" + dialogId).css("left", (document.body.clientWidth - $("#" + dialogId).width()) / 2);
    var obj = $("#" + dialogId).parents().filter("body");
    //    alert(obj.width());
    //    $("#" + dialogId).css("left", ($("#" + dialogId).parent().width() - $("#" + dialogId).width()) / 2);
    $("#" + dialogId).css("left", (obj.width() - $("#" + dialogId).width()) / 2);
    if (top != undefined) {
        $("#" + dialogId).css("top", $(window).scrollTop() + top);
    }
    else {

        $("#" + dialogId).css("top", $(window).scrollTop() + 100);
    }

}
function SetDialogPositionFixed(dialogId, top) {

    //    $("#" + dialogId).css("left", (document.body.clientWidth - $("#" + dialogId).width()) / 2);
    var obj = $("#" + dialogId).parents().filter("body");
    //    $("#" + dialogId).css("left", ($("#" + dialogId).parent().width() - $("#" + dialogId).width()) / 2);
    $("#" + dialogId).css("left", (obj.width() - $("#" + dialogId).width()) / 2);
    if (top != undefined) {

        $("#" + dialogId).css("top", top + 30);
        $("#" + dialogId).css("position", "fixed");
    }
    else {
        //$("#" + dialogId).css("top", $(window).scrollTop() + 100);

        $("#" + dialogId).css("top", 100);
        $("#" + dialogId).css("position", "fixed");

    }

}
function SetDialogPositionTop(dialogId, top) {
    var obj = $("#" + dialogId).parents().filter("body");
    //    alert(obj.width());
    //    alert($("#" + dialogId).width());
    //    alert((obj.width() - $("#" + dialogId).width()) / 2);
    $("#" + dialogId).css("left", (obj.width() - $("#" + dialogId).width()) / 2);
    $("#" + dialogId).css("top", $(window).scrollTop() + top);
}

//Menu
function m_id(id) {
    return document.getElementById(id);
}
function shutoropen(menucount) {
    if (m_id('menu_' + menucount).style.display == 'none') {
        $('#menu_' + menucount).slideDown("slow");
    } else {
        m_id('menu_' + menucount).style.display = 'none';
    }
}
//Ajax请求时，弹出来的提示
function showAjaxTips(message) {
    var ajaxTipObj = document.getElementById("ajaxTips");
    if (ajaxTipObj == undefined) {
        ajaxTipObj = document.createElement("div");
    }
    else {
        ajaxTipObj.style.display = "block";
        return;
    }
    var cWidth = document.body.clientWidth;
    var cHeight = document.body.clientHeight;
    ajaxTipObj.id = "ajaxTips";
    ajaxTipObj.style.width = "200px";
    ajaxTipObj.style.height = "50px";
    ajaxTipObj.style.background = "#FFFFFF";
    //ajaxTipObj.style.color = "#fff";
    ajaxTipObj.style.border = "10px solid #B9B9B9"
    ajaxTipObj.style.position = "absolute";
    ajaxTipObj.style.top = cHeight / 2 - 100 + "px";
    ajaxTipObj.style.left = (cWidth - 200) / 2 + "px";
    ajaxTipObj.style.filter = "alpha(opacity=90)";
    ajaxTipObj.align = "center";
    ajaxTipObj.style.paddingTop = "20px";
    ajaxTipObj.style.zIndex = "250";
    if (navigator.appName.indexOf("Explorer") > -1) {
        ajaxTipObj.innerText = message;
    } else {
        ajaxTipObj.textContent = message;
    }
    document.body.appendChild(ajaxTipObj);
    jQuery("#ajaxTips").show();
    ShowDocumentDivBG();
}
//Ajax请求完成时，关闭提示
function closeAjaxTips() {
    jQuery("#ajaxTips").hide();
    //var obj = document.getElementById("ajaxTips");
    //document.body.removeChild(obj);
    CloseDocumentDivBG();
}
//弹出页面遮照层
function ShowDocumentDivBG() {
    var bgObj;
    var sWidth, sHeight;
    sWidth = document.body.clientWidth;
    sHeight = document.body.clientHeight;
    bgObj = document.getElementById("div_documentbg");
    bgObj.style.top = "0";
    bgObj.style.background = "#FFF";
    bgObj.style.opacity = "0.4";
    bgObj.style.filter = "filter: alpha(opacity=40)";
    bgObj.style.left = "0";
    bgObj.style.width = jQuery("body").width() + "px";
    if (jQuery("body").height() < jQuery(window).height()) {
        bgObj.style.height = jQuery(window).height() + "px";
    }
    else {
        bgObj.style.height = sHeight + "px";
    }
    bgObj.style.zIndex = "200";
    $("#div_documentbg").show();
}
//弹出页面遮照层
function ShowDocumentDivBG_Opacity0() {
    var bgObj;
    var sWidth, sHeight;
    sWidth = document.body.clientWidth;
    sHeight = document.body.clientHeight;
    bgObj = document.getElementById("div_documentbg");
    bgObj.style.top = "0";
    bgObj.style.background = "#FFFFFF";
    bgObj.style.opacity = "0";
    bgObj.style.filter = "filter: alpha(opacity=0)";
    bgObj.style.left = "0";
    bgObj.style.width = jQuery("body").width() + "px";
    if (jQuery("body").height() < jQuery(window).height()) {
        bgObj.style.height = jQuery(window).height() + "px";
    }
    else {
        bgObj.style.height = sHeight + "px";
    }
    bgObj.style.zIndex = "200";
    $("#div_documentbg").show();
}
//弹出页面遮照层
function ShowDocumentDivBG2() {
    ShowDocumentDivBG();
}
function GetUrlParms() {
    var args = new Object();
    var query = location.search.substring(1);
    var pairs = query.split("&");
    for (var i = 0; i < pairs.length; i++) {
        var pos = pairs[i].indexOf('=');
        if (pos == -1) continue;
        var argname = pairs[i].substring(0, pos);
        var value = pairs[i].substring(pos + 1);
        args[argname] = unescape(value);
    }
    return args;
}
//弹出页面遮照层
function CloseDocumentDivBG() {
    $("#div_documentbg").hide();
    jQuery("#div_addLRItem").hide();
    jQuery("#div_modifyLRItem").hide();
    jQuery("#div_ImportLRItem").hide();
    jQuery("#div_ShowLogDetail").hide();
}
function CloseDialog(id) {
    if (id != undefined) {
        CloseDialogById(id);
        CloseDocumentDivBG();
        //jQuery(".div_ShowDailg").hide();
    }
    else {
        jQuery(".div_ShowDailg").hide();
        CloseDocumentDivBG();
    }

}
function CloseDialogById(divId) {
    jQuery("#" + divId).hide();
}
function CloseDialogList(id) {
    $("#" + id + "").hide();
    if (!$(".div_ShowDailg").is(":visible")) { CloseDocumentDivBG(); }
}
function CloseAddShengQing() { CloseDocumentDivBG(); jQuery("#div_ShowDailg_modifypwd").hide(); }
function mouseStyle(obj, mou) {
    obj.style.cursor = mou;
}
var flag = false;
function myDivDown(objPanel) {
    var firstX = event.clientX; //鼠标点下时坐标    
    var firstY = event.clientY; //鼠标点下时坐标
    var tempX = objPanel.offsetLeft; //当前Div相对于正文区域的X坐标
    var tempY = objPanel.offsetTop; //当前Div相对于正文区域的Y坐标
    flag = true;
    document.onmousemove = function () {
        if (flag == false)
        { return false; }
        var lastX = event.clientX;
        var lastY = event.clientY;

        //clientX: 相对于客户区域的x坐标位置，不包括滚动条，就是正文区域。
        //offsetx：设置或者是得到鼠标相对于目标事件的父元素的内边界在x坐标上的位置。
        //screenX: 相对于用户屏幕。
        var mY = tempY + lastY - firstY; //当前Div区域+(鼠标点下的Y坐标-鼠标移动到的Y坐标)
        var length = objPanel.style.top.length - 2;
        var intY = parseInt(objPanel.style.top.substr(objPanel.style.top, length));
        objPanel.style.top = mY;

        var mX = tempX + lastX - firstX; //当前Div区域+(鼠标点下的X坐标-鼠标移动到的X坐标)
        length = objPanel.style.left.length - 2;

        var intX = parseInt(objPanel.style.left.substr(objPanel.style.left, length));
        objPanel.style.left = mX;
    }
    document.onmouseup = function () {
        flag = false;
    }
}

/*关闭窗体*/
function closeWP() {
    var Browser = navigator.appName;
    var indexB = Browser.indexOf('Explorer');

    if (indexB > 0) {
        var indexV = navigator.userAgent.indexOf('MSIE') + 5;
        var Version = navigator.userAgent.substring(indexV, indexV + 1);

        if (Version >= 7) {
            window.open('', '_self', '');
            window.close();
        }
        else if (Version == 6) {
            window.opener = null;
            window.close();
        }
        else {
            window.opener = '';
            window.close();
        }

    }
    else {
        window.close();
    }
}

function SearchLeaveCheck() {
    var us = $("#MainContent_txtUserNameL").val();
    var orgid = $("#MainContent_hdUserOrigId").val();
    var orgStr = $("#MainContent_txt_UserOrg").val();
    window.location.href = "LeaveApplicationCheck.aspx?pi=1&pagesize=" + $("#hdpagesize").val() + "&us=" + us + "&orgid=" + orgid + "&orgstr=" + orgStr;
}
function PageIndexChange(sel) { window.location.href = $(sel).val(); }
function PageSizeChange(sel) { window.location.href = $(sel).val(); }
function SearchLeaveByCheckPast() {
    var us = $("#MainContent_txtUserNameL").val();
    var orgid = $("#MainContent_hdUserOrigId").val();
    var orgStr = $("#MainContent_txt_UserOrg").val();
    window.location.href = "LeaveApplicationCheckPast.aspx?pi=1&pagesize=" + $("#hdpagesize").val() + "&us=" + us + "&orgid=" + orgid + "&orgstr=" + orgStr;
}
function SetBack() {
    var args = new Object();
    args = GetUrlParms();
    if (args["backurl"] != undefined) {
        //         $(".div_backpage").hide();
        //         var offsetTop = 400 + $(window).scrollTop() + "px";
        //         $(".div_backpage").animate({ top: offsetTop }, { duration: 0, queue: false });
        //         var offsetLeft = document.body.clientWidth - 70;
        //         $(".div_backpage").css({ left: offsetLeft });
        var backurl = args["backurl"].replace(/\+/g, "&");
        $(".div_backpage,#btn_backpage").click(function () { window.location.href = backurl; });
    }
}


function SearchDateByCheck(date) {
    var st = $("#" + date).val();
    var r = /^(\d{1,4})(-|\/)(\d{1,2})\2(\d{1,2})$/;
    if (st != "" && !r.test(st)) {
        alert("您的时间格式输入有误，请重新输入。");
        $("#" + date).focus();
        $("#" + date).select();
        return false;
    }
    return true;
}
function SearchLog() {
    if (SearchDateByCheck("MainContent_txtStartDate")) {
        return SearchDateByCheck("MainContent_txtEndDate");
    }
}
/*功能:提示用户可输入的备注字数
日期：3-2
*/
function RemarkKeyPress(txt) {
    var v = $(txt).val();
    if (500 - v.length < 0) {
        $("#span_inputNum").html("0");
        $(txt).val($(txt).val().substring(0, 500));
    } else { $("#span_inputNum").html(500 - v.length); }

}
/*功能:显示Log详细
日期：3-14
*/
function ShowLogContent(eventId) {
    ShowDocumentDivBG();
    jQuery("#div_ShowLogDetail").show();
    jQuery("#div_log_content").html(jQuery("#span_log_" + eventId).html());
    TRHover();
}




var publicclass = new Object();

publicclass.AlertInit = function (div_Pop, div_Pop_Title) {
    $("#" + div_Pop).easydrag();
    $("#" + div_Pop).setHandler(div_Pop_Title);
};

publicclass.AlertShow = function (div_Pop, div_Pop_Title) {
    SetDialogPosition(div_Pop);
    ShowDocumentDivBG();
    $("#" + div_Pop).show();
};



//给下拉列表框赋值
function SetSelectOjb(objID, strJson, selectedID, first, isShowFirst) {
    var obj = $("#" + objID + "");

    var option;
    var strFirst = "";
    if (first == undefined) {
        strFirst = "--请选择--";
    }
    else {
        strFirst = first;
    }
    if (isShowFirst != "false") {
        option = $("<option>").text(strFirst).val("-1");
    }
    obj.empty();
    obj.append(option);


    if (strJson == "{\"ds\":]}") {

    }
    else {
        var objJson = eval("(" + strJson + ")");
        for (var i = 0; i < objJson.ds.length; i++) {
            var option;
            if (selectedID == objJson.ds[i].Value) {
                option = $("<option  selected='selected'>").text(objJson.ds[i].Text).val(objJson.ds[i].Value);
            }
            else {
                option = $("<option>").text(objJson.ds[i].Text).val(objJson.ds[i].Value);
            }
            obj.append(option);
        }
    }

}
//清除元素
function RemoveByID(id) {

    $("#" + id).remove();
}

//省市医院三级联动 Begin
//下拉列表加载省（初始化）
function SetProvinceToSel_First(strObj1, strObj2, strObj3, hidObj1, hidObj2, hidObj3) {
    jQuery.post("../Ajax/SysAjax.aspx", { key: "GetProvinceToJson", net4: Math.random() },
            function (data) {

                SetSelectOjb(strObj1, data, "-1", "--请选择--");
            });
    //根据省隐藏域的值选中省
    $("#" + strObj1).val($("#" + hidObj1).val().split('|')[0]);
    //根据选中省的值加载市列表
    SetCityToSel(strObj1, strObj2, strObj3, hidObj1, hidObj2, hidObj3, '1');
}
////下拉列表加载省（页面刷新时）
//function SetProvinceToSel(strObj1, strObj2, strObj3, hidObj1, hidObj2, hidObj3) {
//    jQuery.post("../Ajax/SysAjax.aspx", { key: "GetProvinceToJson", net4: Math.random() },
//            function (data) {
//                SetSelectOjb(strObj1, data, "-1", "--请选择--");
//            });
//    //设置默认选中的省
//    $("#" + strObj1).val($("#" + hidObj1).val().split('|')[0]);
//    //清除隐藏域中的市与医院
//    $("#" + hidObj2).val("-1|");
//    $("#" + hidObj3).val("-1|");
//    //根据选中值加载市列表
//    SetCityToSel(strObj1, strObj2, strObj3, hidObj1, hidObj2, hidObj3);
//}
//下拉列表加载市（first=1为初始化，不为1时为点击省时加载市）
function SetCityToSel(strObj1, strObj2, strObj3, hidObj1, hidObj2, hidObj3, first) {
    var province = $("#" + strObj1).val();
    jQuery.post("../Ajax/sysAjax.aspx", { key: "GetCityToJson", province: province, net4: Math.random() },
            function (data) {
                SetSelectOjb(strObj2, data, "-1", "--请选择--");
            });

    //设置默认选中的市
    $("#" + strObj2).val($("#" + hidObj2).val().split('|')[0]);
    //清除隐藏域中县
    if (first != 1) {
        $("#" + hidObj3).val("-1|");
    }
    //根据选中值加载医院列表
    SetCountryToSel(strObj1, strObj2, strObj3, hidObj1, hidObj2, hidObj3, first);
    //给省的隐藏域赋值
    $("#" + hidObj1).val($("#" + strObj1).find("option:selected").val() + "|" + $("#" + strObj1).find("option:selected").text());
}

//下拉列表加载县
function SetCountryToSel(strObj1, strObj2, strObj3, hidObj1, hidObj2, hidObj3, first) {
    var province = $("#" + strObj1).val();
    var city = $("#" + strObj2).val();
    jQuery.post("../Ajax/sysAjax.aspx", { key: "GetCountryToJson", province: province, city: city, net4: Math.random() },
            function (data) {
                SetSelectOjb(strObj3, data, "-1", "--请选择--");
            });
    //设置选中值
    $("#" + strObj3).val($("#" + hidObj3).val().split('|')[0]);
    //给市的隐藏域赋值
    $("#" + hidObj2).val($("#" + strObj2).find("option:selected").val() + "|" + $("#" + strObj2).find("option:selected").text());
}
//给医院的隐藏载赋值
function SetCountryToHid(strObj3, hidObj3) {
    $("#" + hidObj3).val($("#" + strObj3).find("option:selected").val() + "|" + $("#" + strObj3).find("option:selected").text());
}
//省市医院三级联动 end

//省市二级联动 Begin
//下拉列表加载省（初始化）
function SetProvinceToSel_First_2(strObj1, strObj2, hidObj1, hidObj2) {
    jQuery.post("../Ajax/sysAjax.aspx", { key: "GetProvinceToJson", net4: Math.random() },
            function (data) {
                SetSelectOjb(strObj1, data, "-1", "--请选择--");
            });
    //根据省隐藏域的值选中省
    $("#" + strObj1).val($("#" + hidObj1).val().split('|')[0]);
    //根据选中省的值加载市列表
    SetCityToSel_2(strObj1, strObj2, hidObj1, hidObj2);
}


//下拉列表加载省（点击）
function SetProvinceToSel_2(strObj1, strObj2, hidObj1, hidObj2) {


    //清除隐藏域中的市
    $("#" + hidObj2).val("-1|");
    //给省的隐藏载赋值
    $("#" + hidObj1).val($("#" + strObj1).find("option:selected").val() + "|" + $("#" + strObj1).find("option:selected").text());
    //根据选中值加载市列表
    SetCityToSel_2(strObj1, strObj2, hidObj1, hidObj2);
}
//下拉列表加载市（初始化）
function SetCityToSel_2(strObj1, strObj2, hidObj1, hidObj2) {
    //SetProvinceToSel_2(strObj1, strObj2, hidObj1, hidObj2);
    var province = $("#" + strObj1).val();
    jQuery.post("../Ajax/sysAjax.aspx", { key: "GetCityToJson", province: province, net4: Math.random() },
            function (data) {
                SetSelectOjb(strObj2, data, "-1", "--请选择--");
            });
    //设置默认选中的市
    $("#" + strObj2).val($("#" + hidObj2).val().split('|')[0]);

    //给省的隐藏域赋值
    $("#" + hidObj1).val($("#" + strObj1).find("option:selected").val() + "|" + $("#" + strObj1).find("option:selected").text());
}

//给市的隐藏载赋值
function SetCityToHid(strObj1, hidObj1) {
    $("#" + hidObj1).val($("#" + strObj1).find("option:selected").val() + "|" + $("#" + strObj1).find("option:selected").text());
}
//省市二级联动 End


//二级联动通用 Begin
//一级下拉列表加载（初始化）
function SetFirstToSel_Init_2(strObj1, strObj2, hidObj1, hidObj2, strFirstAjax, strSecondAjax) {
    jQuery.post("../Ajax/sysAjax.aspx", { key: strFirstAjax, net4: Math.random() },
            function (data) {
                SetSelectOjb(strObj1, data, "-1", "--请选择--");
            });
    //根据一级隐藏域的值选中二级下拉列表
    $("#" + strObj1).val($("#" + hidObj1).val().split('|')[0]);
    //根据选中一级下拉列表的值加载二级下拉列表
    SetSecondToSel_2(strObj1, strObj2, hidObj1, hidObj2, strSecondAjax);
}
//一级下拉列表加载（点击）
function SetFirstToSel_2(strObj1, strObj2, hidObj1, hidObj2, strFirstAjax, strSecondAjax) {
    //清除隐藏域中的二级已选中数据
    $("#" + hidObj2).val("-1|");
    //给省的隐藏载赋值
    $("#" + hidObj1).val($("#" + strObj1).find("option:selected").val() + "|" + $("#" + strObj1).find("option:selected").text());
    //根据选中值加载二级下拉列表
    SetSecondToSel_2(strObj1, strObj2, hidObj1, hidObj2, strSecondAjax);
}
//二级下拉列表加载
function SetSecondToSel_2(strObj1, strObj2, hidObj1, hidObj2, strSecondAjax) {
    //SetFirstToSel_2(strObj1, strObj2, hidObj1, hidObj2);
    var FirstSelected = $("#" + strObj1).val();
    jQuery.post("../Ajax/sysAjax.aspx", { key: strSecondAjax, f: FirstSelected, net4: Math.random() },
            function (data) {
                SetSelectOjb(strObj2, data, "-1", "--请选择--");
            });
    //设置默认选中的二级下拉列表
    $("#" + strObj2).val($("#" + hidObj2).val().split('|')[0]);

    //给一级的隐藏域赋值
    $("#" + hidObj1).val($("#" + strObj1).find("option:selected").val() + "|" + $("#" + strObj1).find("option:selected").text());
}

//给二级的隐藏载赋值
function SetSecondToHid(strObj1, hidObj1) {
    $("#" + hidObj1).val($("#" + strObj1).find("option:selected").val() + "|" + $("#" + strObj1).find("option:selected").text());
}
//通用二级联动 End


//------------------数据验证
String.prototype.len = function ()
{ return this.replace(/[^\x00-\xff]/g, "rr").length; }
// String.prototype.sub = function (n) {
//     if (this.length <= n) {
//         return this;
//     }
//     else {
//         return this.substr(0, n) + "...";
//     }
// }; 
String.prototype.len = function ()
{ return this.replace(/[^\x00-\xff]/g, "rr").length; }
String.prototype.sub = function (n) {
    var r = /[^\x00-\xff]/g;
    if (this.replace(r, "mm").length <= n)
        return this;
    var m = n; //Math.floor(n/2); 
    for (var i = m; i < this.length; i++) {
        if (this.substr(0, i).replace(r, "mm").length >= n)
        { return this.substr(0, i) + ".."; }
    } return this;
};
//网站公用js验证
//字符串验证前，可根据情况过滤两边空格
var js_validate = new Object();
js_validate.IsEmpty = function (str_input) {
    if (str_input == null) {
        return 0;
    }
    return str_input.length == 0;
};
//整数
js_validate.IsInt = function (str_input) {
    var pattern = /^-?\d+$/;
    return js_validate.IsMatch(str_input, pattern);
};
//非负整数
js_validate.IsPositiveInt = function (str_input) {
    var pattern = /^\d+$/;
    return js_validate.IsMatch(str_input, pattern);
};
//浮点数
js_validate.IsFloat = function (str_input) {
    var pattern = /^\d+(.\d+)?$/;
    return js_validate.IsMatch(str_input, pattern);
};
//时间[时:分:秒]
js_validate.IsTime = function (str_input) {
    var pattern = /^([0-1]\d|2[0-3]):[0-5]\d(:[0-5]\d)?$/;
    return js_validate.IsMatch(str_input, pattern);;
};
//时间[时:分]
js_validate.IsHourMinute = function (str_input) {
    var pattern = /^([0-1]?\d|2?[0-3])(:|：)[0-5]?\d$/;
    return js_validate.IsMatch(str_input, pattern);;
};
//日期 从1900年开始
js_validate.IsDateTime = function (str_input) {
    //年份-（大月-日）|（非2小月-日）|（2月-日）
    var pattern = /^19\d\d-((0?1|3|5|7|8)|(10|12)-(1\d|2\d|3[0-1]))|((0?2|4|6|9)|11-(1\d|2\d|30))|(0?2-(1\d|2\d))$/;
    return js_validate.IsMatch(str_input, pattern);
};
//手机号码8-20
js_validate.IsMobileNum = function (str_input) {
    var pattern = /^\d{8,20}$/;
    return js_validate.IsMatch(str_input, pattern);
};
js_validate.IsMobileNum2 = function (str_input) {
    var regexMobile = /^13[0-9]{1}[0-9]{8}$|^15[9]{1}[0-9]{8}$|^1[3,5]{1}[0-9]{1}[0-9]{8}$|^147[0-9]{8}$|^18[0-9][0-9]{8}$/; //手机
    return js_validate.IsMatch(str_input, regexMobile);
}
//电话号码6-30
js_validate.IsTelNum = function (str_input) {
    var pattern = /^(\d{2,4}-)?(\d{2,6}-)?\d{7,12}(-\d{1,6})?$/;
    return js_validate.IsMatch(str_input, pattern);
};

//Email 2011-12-27 modbyli
js_validate.IsEmail = function (str_input) {
    //var pattern = /^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+$/;
    var pattern = /^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
    return js_validate.IsMatch(str_input, pattern);
};
//邮编
js_validate.IsZipCode = function (str_input) {
    var pattern = /^\d{6}$/;
    return js_validate.IsMatch(str_input, pattern);
};

//身份证号15 
js_validate.is15Number = function (str_input) {
    var pattern = /^d{8}((0[1-9])|(1[012]))(0[1-9]|[12][0-9]|3[01])d{3}$/;
    return js_validate.IsMatch(str_input, pattern);
}
//身份证号18
js_validate.is18Number = function (str_input) {
    var pattern = /^(\d{6})(18|19|20)?(\d{2})([01]\d)([0123]\d)(\d{3})(\d|X)?$/;
    return js_validate.IsMatch(str_input, pattern);
}

//字符长度
js_validate.IsLength = function (str_input, min_length, max_length) {
    return js_validate.LengthBetween(str_input, min_length, max_length);
};
//数字取值范围
js_validate.IsNumBetween = function (str_input, min_length, max_length) {
    var pattern = /^\d+$/;
    if (js_validate.IsMatch(str_input, pattern)) {
        return js_validate.LengthBetween(str_input, min_length, max_length);
    }
    else {
        return false;
    }
};


//长度范围 范围为空则匹配任何长度
js_validate.LengthBetween = function (str_input, min_length, max_length) {
    var leng = str_input.length;
    if (min_length != null) {
        if (leng < min_length) {
            return false;
        }
    }
    if (max_length != null) {
        if (leng > max_length) {
            return false;
        }
    }
    return true;
}
//自定义正则
js_validate.IsMatch = function (str_input, pattern) {
    return pattern.test(str_input);
}
//过滤换行符
js_validate.ReplaceNR = function (str_input) {
    var regR = /\r/g;
    var regN = /\n/g;
    var str = str_input.replace(regR, "").replace(regN, "");
    return str;
}
js_validate.Filter = function (str_input) {
    var str = "";
    var pattern = new RegExp("[`~!#$^&*()=|{}':;',\\[\\]<>/?~！#￥……&*（）——|{}★○◎◇◆■→※☆▄▆【】‘；：”“'。，、？ 　]");
    for (var i = 0; i < str_input.length; i++) {
        str = str + str_input.substr(i, 1).replace(pattern, '');
    }
    return str;
}
//用户注册验证用户名格式2016-06-03TS
js_validate.VerifyUserNameFormat = function (str_input) {
    var pattern = /^[a-zA-Z0-9_\u4E00-\u9FA5]{2,50}$/;
    if (js_validate.IsEmail(str_input) || js_validate.IsMobileNum2(str_input) || pattern.test(str_input)) {
        return true;
    }
    else {
        return false;
    }
}
//------------------数据验证结束




function VisHeaderDiv(con) {
    if (con.innerHTML.indexOf("up") >= 0) {
        document.getElementById("header").style.display = "none";
        con.innerHTML = "<img src=\"../Images/images003/down.png\" />";
        SetCookie("headerVisStatue", "UP");
    }
    else {
        document.getElementById("header").style.display = "";
        con.innerHTML = "<img src=\"../Images/images003/up.png\" />";
        SetCookie("headerVisStatue", "DOWN");
    }
}


function SetCookie(name, value) {
    var hours = 24; //表示保存多少小时  
    var exp = new Date();
    exp.setTime(exp.getTime() + hours * 60 * 60 * 1000);
    document.cookie = name + "=" + escape(value) + ";expires=" + exp.toGMTString() + ";path=/;";
}

function GetCookie(name) {
    //return document.cookie;

    var arr = document.cookie.match(new RegExp("(^| )" + name + "=([^;]*)(;|$)"));
    if (arr != null)
        return unescape(arr[2]);

    return null;
}



//初始化菜单方法
function InitAppMenuMethod() {
    var lr_systembtn = $("#lr_systembtn");
    var lr_menu = $("#lr_menu");
    lr_systembtn.mouseenter(function () {
        t_delay = setTimeout(function () {
            lr_menu.fadeIn("slow");
        }, 200);
    });
    lr_systembtn.mouseleave(function () {
        clearTimeout(t_delay);
        lr_menu.fadeOut("slow");
    });
}

//锁定表头和列
function FixTable(TableID, FixColumnNumber, width, height) {


    /// <summary>
    ///     锁定表头和列
    /// </summary>
    /// <param name="TableID" type="String">
    ///     要锁定的Table的ID
    /// </param>
    /// <param name="FixColumnNumber" type="Number">
    ///     要锁定列的个数
    /// </param>
    /// <param name="width" type="Number">
    ///     显示的宽度
    /// </param>
    /// <param name="height" type="Number">
    ///     显示的高度
    /// </param>
    if ($("#" + TableID + "_tableLayout").length != 0) {
        $("#" + TableID + "_tableLayout").before($("#" + TableID));
        $("#" + TableID + "_tableLayout").empty();
    }
    else {
        $("#" + TableID).after("<div id='" + TableID + "_tableLayout' style='overflow:hidden;height:" + height + "px; width:" + width + "px;'></div>");
    }

    $('<div id="' + TableID + '_tableFix"></div>'
    + '<div id="' + TableID + '_tableHead"></div>'
    + '<div id="' + TableID + '_tableColumn"></div>'
    + '<div id="' + TableID + '_tableData"></div>').appendTo("#" + TableID + "_tableLayout");


    var oldtable = $("#" + TableID);

    var tableFixClone = oldtable.clone(true);
    tableFixClone.attr("id", TableID + "_tableFixClone");
    $("#" + TableID + "_tableFix").append(tableFixClone);
    var tableHeadClone = oldtable.clone(true);
    tableHeadClone.attr("id", TableID + "_tableHeadClone");
    $("#" + TableID + "_tableHead").append(tableHeadClone);
    var tableColumnClone = oldtable.clone(true);
    tableColumnClone.attr("id", TableID + "_tableColumnClone");
    $("#" + TableID + "_tableColumn").append(tableColumnClone);
    $("#" + TableID + "_tableData").append(oldtable);

    $("#" + TableID + "_tableLayout table").each(function () {
        $(this).css("margin", "0");
    });


    var HeadHeight = $("#" + TableID + "_tableHead thead").height();
    HeadHeight += 2;
    $("#" + TableID + "_tableHead").css("height", HeadHeight);
    $("#" + TableID + "_tableFix").css("height", HeadHeight);


    var ColumnsWidth = 0;
    var ColumnsNumber = 0;
    $("#" + TableID + "_tableColumn tr:last td:lt(" + FixColumnNumber + ")").each(function () {
        ColumnsWidth += $(this).outerWidth(true);
        ColumnsNumber++;
    });
    ColumnsWidth += 2;
    if ($.browser.msie) {
        switch ($.browser.version) {
            case "7.0":
                if (ColumnsNumber >= 3) ColumnsWidth--;
                break;
            case "8.0":
                if (ColumnsNumber >= 2) ColumnsWidth--;
                break;
        }
    }
    $("#" + TableID + "_tableColumn").css("width", ColumnsWidth);
    $("#" + TableID + "_tableFix").css("width", ColumnsWidth);


    $("#" + TableID + "_tableData").scroll(function () {
        $("#" + TableID + "_tableHead").scrollLeft($("#" + TableID + "_tableData").scrollLeft());
        $("#" + TableID + "_tableColumn").scrollTop($("#" + TableID + "_tableData").scrollTop());
    });

    $("#" + TableID + "_tableFix").css({ "overflow": "hidden", "position": "relative", "z-index": "50", "background-color": "White" });
    $("#" + TableID + "_tableHead").css({ "overflow": "hidden", "width": width - 17, "position": "relative", "z-index": "45", "background-color": "White" });
    $("#" + TableID + "_tableColumn").css({ "overflow": "hidden", "height": height - 17, "position": "relative", "z-index": "40", "background-color": "White" });
    $("#" + TableID + "_tableData").css({ "overflow": "scroll", "width": width, "height": height, "position": "relative", "z-index": "35" });

    if ($("#" + TableID + "_tableHead").width() > $("#" + TableID + "_tableFix table").width()) {
        $("#" + TableID + "_tableHead").css("width", $("#" + TableID + "_tableFix table").width());
        $("#" + TableID + "_tableData").css("width", $("#" + TableID + "_tableFix table").width() + 17);
    }
    if ($("#" + TableID + "_tableColumn").height() > $("#" + TableID + "_tableColumn table").height()) {
        $("#" + TableID + "_tableColumn").css("height", $("#" + TableID + "_tableColumn table").height());
        $("#" + TableID + "_tableData").css("height", $("#" + TableID + "_tableColumn table").height() + 17);
    }

    $("#" + TableID + "_tableFix").offset($("#" + TableID + "_tableLayout").offset());
    $("#" + TableID + "_tableHead").offset($("#" + TableID + "_tableLayout").offset());
    $("#" + TableID + "_tableColumn").offset($("#" + TableID + "_tableLayout").offset());
    $("#" + TableID + "_tableData").offset($("#" + TableID + "_tableLayout").offset());

    $("#" + TableID + "_tableLayout").css("height", height);


}
function getUrlVar(name) {
    var val = getUrlVars()[name];
    if (val == null || val == undefined) {
        val = "";
    }
    return val;
}
function getUrlVars() {
    var vars = [], hash;
    var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for (var i = 0; i < hashes.length; i++) {
        hash = hashes[i].split('=');
        vars.push(hash[0]);
        vars[hash[0]] = hash[1];
    }
    return vars;
}

