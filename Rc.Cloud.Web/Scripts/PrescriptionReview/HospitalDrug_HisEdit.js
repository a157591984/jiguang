var exists=false;

function AjaxPost(id, hisid, drugid) {
    $.post("../Ajax/Ajax_HospitalDrug_His.aspx", { ID: id, HisID: hisid, DrugID: drugid },
    function (result) {
        if (result.toString().toLowerCase().indexOf("true") >= 0)
            exists = true;
        else
            exists = false;
    });

    if (exists) {
        if (confirm("此商品药物已经关联其他HIS，是否继续关联？")) {
            return true;
        }
        else
        {
            return false;
        }
    }
    return true;
}


function CloseDiv() {
    CloseDialog();
}


$("#div_Pop_CommonDict").easydrag();
$("#div_Pop_CommonDict").setHandler("div_Pop_CommonDict_Title");
function showCommonDict(userControlID, hidUserControlID, D_Type) {
    //        ShowDocumentDivBG_Opacity0();
    SetDialogPositionTop("div_Pop_CommonDict", 10);
    $("#div_Pop_CommonDict").show();
    if (D_Type == "V001") {
        $("#div_Pop_CommonDict_Title_Teft").html("选择医生");
    }
    else if (D_Type == "V006") {
        $("#div_Pop_CommonDict_Title_Teft").html("选择药物");
    }
    else if (D_Type == "V013") {
        $("#div_Pop_CommonDict_Title_Teft").html("选择抗菌药物");
    }
    else if (D_Type == "V008") {
        $("#div_Pop_CommonDict_Title_Teft").html("选择科室");
    }
    else if (D_Type == "V009") {
        $("#div_Pop_CommonDict_Title_Teft").html("选择病症");
    }
    else if (D_Type == "VHD") {
        $("#div_Pop_CommonDict_Title_Teft").html("选择商品药物");
    }
    //方法名称是：GetDataList_+控件的名称
    //D_Type:1医院,2药物分类
    var Hospital = arguments[4] ? arguments[4] : "";
    var D_Expand = arguments[3] ? arguments[3] : "";
    GetDataList_XUV_CommonDict(userControlID, hidUserControlID, D_Type, D_Expand, Hospital);
}
