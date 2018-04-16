function CloseDiv() {
    CloseDialog();
}

$("#div_Pop_CommonDict").easydrag();
$("#div_Pop_CommonDict").setHandler("div_Pop_CommonDict_Title");
//弹出功效
function showCommonDict(userControlID, hidUserControlID, D_Type, Regional_Dict_ID) {
    ShowDocumentDivBG_Opacity0();
    $("#div_Pop_CommonDict").css({ height: "500px", width: "600px" });
    SetDialogPositionTop("div_Pop_CommonDict", 30);

    $("#div_Pop_CommonDict").show();
    if (D_Type == "1") {
        $("#div_Pop_CommonDict_Title_Teft").html("选择日常饮食");
    }
    else if (D_Type == "2") {
        $("#div_Pop_CommonDict_Title_Teft").html("选择作用部位");
    }
    else if (D_Type == "38") {
        $("#div_Pop_CommonDict_Title_Teft").html("选择抗生素");
    }
    else if (D_Type == "39") {
        $("#div_Pop_CommonDict_Title_Teft").html("选择细菌");
    }
    //方法名称是：GetDataList_+控件的名称
    //D_Type:1日常饮食控件,2作用部位控件，38 抗生素，39 细菌控件；
    //GetDataList_XUCommonDict(userControlID, hidUserControlID, D_Type);
    GetDataList_XUPM_DrugAntibiosisLevel_Standard(userControlID, hidUserControlID, '0', Regional_Dict_ID);
}