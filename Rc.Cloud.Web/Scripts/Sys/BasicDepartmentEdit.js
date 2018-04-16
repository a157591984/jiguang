$("#div_Pop_CommonDict").easydrag();
$("#div_Pop_CommonDict").setHandler("div_Pop_CommonDict_Title");
function showCommonDict(userControlID, hidUserControlID, D_Type, D_Expand) {
    $("#div_Pop_CommonDict").css({ height: "500px", width: "600px" });
    SetDialogPositionTop("div_Pop_CommonDict", 30);
    $("#div_Pop_CommonDict").show();
    if (D_Type == "V001") {
        $("#div_Pop_CommonDict_Title_Teft").html("选择人员");
    }
    GetDataList_XUV_CommonDict(userControlID, hidUserControlID, D_Type, D_Expand);
}

function DeparmentInput() {
    if ($("#txtName").val() == "") {
        $.dialog.alert("请输入科室名称！");
        return false;
    }
    return true;
}