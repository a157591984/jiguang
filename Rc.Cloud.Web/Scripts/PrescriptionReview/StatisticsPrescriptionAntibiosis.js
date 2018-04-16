$("#div_Pop_CommonDict").easydrag();
$("#div_Pop_CommonDict").setHandler("div_Pop_CommonDict_Title");
function showCommonDict(userControlID, hidUserControlID, D_Type) {
    //        ShowDocumentDivBG_Opacity0();
    SetDialogPositionTop("div_Pop_CommonDict", 30);
    $("#div_Pop_CommonDict").show();
    if (D_Type == "V001") {
        $("#div_Pop_CommonDict_Title_Teft").html("选择医生");
    }
    else if (D_Type == "V006") {
        $("#div_Pop_CommonDict_Title_Teft").html("选择药物");
    }
    else if (D_Type == "V007") {
        $("#div_Pop_CommonDict_Title_Teft").html("选择抗菌药物");
    }
    else if (D_Type == "V008") {
        $("#div_Pop_CommonDict_Title_Teft").html("选择科室");
    }
    //方法名称是：GetDataList_+控件的名称
    //D_Type:1医院,2药物分类
    var Hospital = arguments[4] ? arguments[4] : "";
    GetDataList_XUV_CommonDict(userControlID, hidUserControlID, D_Type, "", Hospital);
}