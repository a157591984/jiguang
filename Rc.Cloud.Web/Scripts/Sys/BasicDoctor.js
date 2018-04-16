function ClosePopByIdAddUser() {
    CloseDialogById('<%=div_Pop.ClientID %>');
}

function showPopAddUser(id) {
    $("#<%=div_Pop.ClientID %>").show();
    if (id == "") {
        $("#div_ShowDailg_Title_left").html("新增用户");
    }
    else {
        $("#div_ShowDailg_Title_left").html("修改用户");
    }
    $("#<%=div_iframe.ClientID %>").html("<iframe  height=\"620\"  frameborder='0' width='100%' style='margin: 0px' src='DoctorInfoAdd.aspx?id=" + id + "'></iframe>");
    SetDialogPosition("<%=div_Pop.ClientID %>", 20);
}
function HandelAddUser(sign, strMessage) {
    if (sign == "1") {
        showTips('操作成功', '', '1');
        CloseDialogById('<%=div_Pop.ClientID %>');
        setTimeout('document.location.reload()', 500);
    }
    else {
        showTipsErr(strMessage, '3')
    }
}