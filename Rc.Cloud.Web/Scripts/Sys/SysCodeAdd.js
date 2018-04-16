function ShowAlert(str) {
    $.dialog.alert(str);
}
function btnClient_Click() {
    var code = $("#txtSysCode").val();
    if (code.length != 5) {
        ShowAlert("系统编码：请输入5位数字");
        return false;
    }
}