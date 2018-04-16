
var ImproveReviewResult = {};

ImproveReviewResult.Del = function (e, id) {
    if (confirm('确定要删除吗？')) {
        jQuery.get("../../Ajax/HospitalConfigAjax.aspx", { key: "DeleteImproveReviewResultByID", id: id, net4: Math.random() },
            function (data) {
                if (data == "1") {
                    showTips('删除成功', '', '2');
                    $(e).parent().parent().remove();
                }
                else {
                    showTipsErr('删除失败', '3');
                }
            });
    }
}

ImproveReviewResult.Save = function () {
    var id = $("#hidImproveReviewResult_ID").val();
    if (id.length > 0) {
        //修改
        ImproveReviewResult.Edit(id);
    }
    else {
        //新增
        ImproveReviewResult.Add();
    }
}

ImproveReviewResult.ShowEdit = function (e, id) {
    var info = $(e).parent().parent();
    //获取里面所有内容
    //显示到弹出层中
    var allTd = info.find("td");
    var drugId = $.trim(allTd.eq(1).find("input:hidden").val());
    var ReviewID = $.trim(allTd.eq(0).find("input:hidden").val());
    var Result = $.trim(allTd.eq(2).find("input:hidden").val());
    var Detail = $.trim(allTd.eq(3).find("input:hidden").val());
    $("#ddlDrugs").val(drugId);
    $("#ddlImproveReviewResult_ReviewID").val(ReviewID);
    if(Result=="1")
    {
    $("#rbtResult1").attr("checked",true);
    }
    else
    {
     $("#rbtResult2").attr("checked",true)
    }
    $("#txtImproveReviewResult_Detail").val(Detail);
    $("#hidImproveReviewResult_ID").val(id);
}

ImproveReviewResult.Edit = function (id) {
    //从页面中获取值
    var prescription_ID = $("#hidPrescription_ID").val();
    var prescription_Code = $("#lbBasePrescription_Code").val();
    var bacth_ID = $("#hidBacth_ID").val();
    var drugId = $("#ddlDrugs option:selected").val();
    var reviewID = $("#ddlImproveReviewResult_ReviewID option:selected").val();
    var result = $("input:radio[name='ctl00$MainContent$Result']:checked").val();
    var detail = $("#txtImproveReviewResult_Detail").val();
    //保存到数据库
    $.post("/Ajax/HospitalConfigAjax.aspx",
    { "key": "UpdateImproveReviewResult",
        "id": id,
        "prescription_ID": prescription_ID,
        "prescription_Code":prescription_Code,
        "bacth_ID": bacth_ID,
        "drugId": drugId,
        "reviewID": reviewID,
        "result": result,
        "detail": detail
    },
            function (data) {
                if (data != "0" && data != "1") {
                    showTips('保存成功', '', '2');
                    $("#" + id).remove();
                    //成功后，拼接字符串，添加到表格中
                    var strhtml = '';
                    strhtml = '<tr id="' + data + '" class="tr_con_002">';
                    strhtml += '<td class="aui_td_content">';
                    strhtml += $("#ddlImproveReviewResult_ReviewID option:selected").text();
                    strhtml += '<input type="hidden" id="hidImproveReviewResult_ReviewID" value="' + reviewID + '"> </td>';
                    strhtml += '<td>';
                    strhtml += $("#ddlDrugs option:selected").text();
                    strhtml += '<input type="hidden" name="hidDrugID" value="' + drugId + '"></td>';
                    strhtml += ' <td class="aui_td_content">';
                    if (result == "1") {
                        strhtml += '合理';
                    }
                    else {
                        strhtml += '不合理';
                    }
                    strhtml += ' <input type="hidden" id="hidImproveReviewResult_Result" value="' + result + '" /> </td>';
                    strhtml += '<td class="aui_td_content">';
                    strhtml += detail;
                    strhtml += '<input type="hidden" id="hidImproveReviewResult_Detail" value="' + detail + '" /></td>';
                    strhtml += '<td class="aui_td_content">';
                    strhtml += '<a href="javascript:void(0)" onclick=\'ImproveReviewResult.ShowEdit(this,"' + data + '")\'>修改</a> ';
                    strhtml += ' <a href="javascript:void(0)" onclick=\'ImproveReviewResult.Del(this,"' + data + '")\'>删除</a></td> </tr>';
                    $("#tbDrugList").append(strhtml);
                    $("#hidImproveReviewResult_ID").val("");
                    $("#txtImproveReviewResult_Detail").val("");

                }
                else if (data == "1") {
                    showTipsErr('此药此点评项已点评！', '3');
                }
                else {

                    showTipsErr('保存失败', '3');
                }
            });
}

ImproveReviewResult.Add = function () {
    //从页面中获取值
    var prescription_ID = $("#hidPrescription_ID").val();
    var prescription_Code = $("#lbBasePrescription_Code").val();
    var bacth_ID = $("#hidBacth_ID").val();
    var drugId = $("#ddlDrugs option:selected").val();
    var reviewID = $("#ddlImproveReviewResult_ReviewID option:selected").val();
    var result = $("input:radio[name='ctl00$MainContent$Result']:checked").val();
    var detail = $("#txtImproveReviewResult_Detail").val();
    //保存到数据库
    $.post("/Ajax/HospitalConfigAjax.aspx",
    { "key": "InsertImproveReviewResult",
        "prescription_ID": prescription_ID,
        "prescription_Code": prescription_Code,
        "bacth_ID": bacth_ID,
        "drugId": drugId,
        "reviewID": reviewID,
        "result": result,
        "detail": detail
    },
            function (data) {
                if (data != "0" && data != "1") {
                    showTips('保存成功', '', '2');
                    //成功后，拼接字符串，添加到表格中
                    $("#tbDrugList").style.display = "";
                    var strhtml = '';
                    strhtml = '<tr id="' + data + '" class="tr_con_002">';
                    strhtml += '<td class="aui_td_content">';
                    strhtml += $("#ddlImproveReviewResult_ReviewID option:selected").text();
                    strhtml += '<input type="hidden" id="hidImproveReviewResult_ReviewID" value="' + reviewID + '"> </td>';
                    strhtml += '<td>';
                    strhtml += $("#ddlDrugs option:selected").text();
                    strhtml += '<input type="hidden" name="hidDrugID" value="' + drugId + '"></td>';
                    strhtml += ' <td class="aui_td_content">';
                    if (result == "1") {
                        strhtml += '合理';
                    }
                    else {
                        strhtml += '不合理';
                    }
                    strhtml += ' <input type="hidden" id="hidImproveReviewResult_Result" value="' + result + '" /> </td>';
                    strhtml += '<td class="aui_td_content">';
                    strhtml += detail;
                    strhtml += '<input type="hidden" id="hidImproveReviewResult_Detail" value="' + detail + '" /></td>';
                    strhtml += '<td class="aui_td_content">';
                    strhtml += '<a href="javascript:void(0)" onclick=\'ImproveReviewResult.ShowEdit(this,"' + data + '")\'>修改</a> ';
                    strhtml += ' <a href="javascript:void(0)" onclick=\'ImproveReviewResult.Del(this,"' + data + '")\'>删除</a></td> </tr>';
                    $("#tbDrugList").append(strhtml)
                  
                }
                else if (data == "1") {
                    showTipsErr('此药此点评项已点评！', '3');
                }
                else {
                    showTipsErr('保存失败', '3');
                }
            });
}