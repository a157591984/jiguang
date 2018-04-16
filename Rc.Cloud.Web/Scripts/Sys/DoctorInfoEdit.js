
function AgeKeyUp(id1, id2, length) {
    var age1 = $.trim($("#" + id1).val());
    $("#" + id1).val(age1);
    if (age1.length > 0 && age1.length == length) {
        $("#" + id2).focus();
    }
}
function CheckInput() {
    var namelogin = $.trim($("#txtNamelogin").val());
    if (namelogin.length == 0) {
        $.dialog.alert("用户名不能为空！");
        return fale;
    }
    var pwdlogin = $.trim($("#txtpwdlogin").val());
    var txtNamelogin = $("#txtNamelogin").attr("ReadOnly", "false");
    if (txtNamelogin && pwdlogin.length == 0) {
        $.dialog.alert("密码不能为空！");
        return fale;
    }
    //if (!js_validate.IsEmail($("#txtEmail").val())) {
    //    $.dialog.alert("邮箱格式错误！");
    //    return fale;
    //}
    //if (!js_validate.IsTelNum($("#txtTel").val())) {
    //    $.dialog.alert("电话格式错误！");
    //    return fale;
    //}
    //if (!js_validate.IsMobileNum($("#txtPhone").val())) {
    //    $.dialog.alert("手机格式错误！");
    //    return fale;
    //}
    return true;
}