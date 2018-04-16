$("#div_Pop").easydrag();
$("#div_Pop").setHandler("div_Pop_Title");

function showInfo(id,handel,sysUser_Name) {
    var page = "SystemLogErrorView.aspx?id=" + id + "&sysUser_Name=" + sysUser_Name;
    if (handel == "1") {//添加
        $("#div_ShowDailg_Title_left").html("添加信息");
    }
    else {
        $("#div_ShowDailg_Title_left").html("查看信息");
    }
    $("#divIfram").html("<iframe id='iframDrugFiled' frameborder='0' width='100%' style='margin: 0px' src='" + page + "'  height='450px'></iframe>");
    $("#div_Pop").show();
    SetDialogPosition("div_Pop");
}
function Handel(sign, error) {
    if (sign == "1") {
        showTips('操作成功', '/Sys/SysCode.aspx', '1');
        CloseDialog();
        CloseDocumentDivBG();
    }
    else {
        showTipsErr('操作失败' + " " + error, '4')
    }
}
function ShowAlert(str) {
    $.dialog.alert(str);
}