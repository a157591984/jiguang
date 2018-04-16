
$("#div_Pop_CommonDict").easydrag();
$("#div_Pop_CommonDict").setHandler("div_Pop_CommonDict_Title");
//
function showCommonDict(userControlID, hidUserControlID, D_Type, D_Expand) {
    //        ShowDocumentDivBG_Opacity0();
    //ShowDocumentDivBG_Opacity0();
    $("#div_Pop_CommonDict").css({ height: "500px", width: "600px" });
    SetDialogPositionTop("div_Pop_CommonDict", 30);
    $("#div_Pop_CommonDict").show();
    if (D_Type == "V001") {
        $("#div_Pop_CommonDict_Title_Teft").html("选择人员");
    }
    if (D_Type == "V002") {
        $("#div_Pop_CommonDict_Title_Teft").html("选择医院");
    }
    else if (D_Type == "V003") {
        $("#div_Pop_CommonDict_Title_Teft").html("选择药物分类");
    }
    else if (D_Type == "38") {
        $("#div_Pop_CommonDict_Title_Teft").html("选择抗生素");
    }
    else if (D_Type == "39") {
        $("#div_Pop_CommonDict_Title_Teft").html("选择细菌");
    }
    //方法名称是：GetDataList_+控件的名称
    //D_Type:1医院,2药物分类
    //D_Expand：扩展属性（以“|”号分隔的多个属性）
    //【0】     
    //【1】返回值模式       0按正常模式返回值，1以TABLE方式返回
    GetDataList_XUV_CommonDict(userControlID, hidUserControlID, D_Type, D_Expand);
}