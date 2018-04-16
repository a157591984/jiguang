
$("#div_Pop_CommonDict").easydrag();
$("#div_Pop_CommonDict").setHandler("div_Pop_CommonDict_Title");
//
function showCommonDict(userControlID, hidUserControlID, D_Type, D_Expand, Hospital) {
    //        ShowDocumentDivBG_Opacity0();
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
    //【0】选择模式         0多选，1单选 默认：多选
    //【1】返回值模式       0按正常模式返回值，1以TABLE方式返回  默认：按正常模式返回值
    //【2】编辑模式         0无编辑，1只添加，2只修改，3添加与修改 默认：无编辑
    GetDataList_XUV_CommonDict(userControlID, hidUserControlID, D_Type, D_Expand, Hospital);
}


function AgeKeyUp(id1, id2, length) {
    var age1 = $.trim($("#" + id1).val());
    $("#" + id1).val(age1);
    if (age1.length > 0 && age1.length == length) {
        $("#" + id2).focus();
    }
}
