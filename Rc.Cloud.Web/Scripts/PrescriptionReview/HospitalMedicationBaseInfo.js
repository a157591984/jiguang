$(function () {
    choose();

});

function Handel(sign) {
    if (sign == "1") {
        showTips('操作成功', 'HospitalMedicationBaseInfoList.aspx', '2')
        //                CloseDialog();
        //                CloseDocumentDivBG();
    }
    else if (sign == "2") {
        showTipsErr('操作失败', '4');
    }
    else {
        showTipsErr('已存在本年度报表', '4')
    }
}
function choose() {
    //医院感染管理部门
    //            $("input:radio[name='ctl00$MainContent$InfectMgmt']").change(function () {
    //                var _this = $(this);
    //                if (_this.val() == "0") {
    //                    $("#MainContent_tr_AntiGermsMgmt1").hide();
    //                    $("#MainContent_tr_AntiGermsMgmt2").hide();
    //                    $("#MainContent_tr_AntiGermsMgmt3").hide();
    //                }
    //                else {
    //                    $("#MainContent_tr_AntiGermsMgmt1").show();
    //                    $("#MainContent_tr_AntiGermsMgmt3").show();
    //                }
    //            });
    //
    $("input:radio[name='ctl00$MainContent$AntiGermsMgmt']").change(function () {
        var _this = $(this);
        if (_this.val() == "0") {
            $("#MainContent_tr_AntiGermsMgmt2").hide();
        }
        else {
            $("#MainContent_tr_AntiGermsMgmt2").show();
        }
    });

}
function CheckSave() {
    if ($("#<%=txt_BedSpace.ClientID %>").val() == "") {
        alertValue("请填写实际开放床位数！");
        this.focus();
        return false;
    }
    if ($("#<%=txt_AvgHospitalStay.ClientID %>").val() == "") {
        alertValue("请填写平均住院天数！");
        this.focus();
        return false;
    }
    if ($("#<%=txt_AllDoctors.ClientID %>").val() == "") {
        alertValue("请填写全院医师人数！");
        this.focus();
        return false;
    }
    if ($("#<%=txt_AntiGermsDoctors.ClientID %>").val() == "") {
        alertValue("请填写具有抗菌药物处方权医师人数！");
        this.focus();
        return false;
    }
    return true;
}
function CheckInput() {
    CheckSave();

    var InfectMgmt = $("input:radio[name='ctl00$MainContent$InfectMgmt']:checked");
    if (InfectMgmt.length == 0) {
        $.dialog.alert("请选择是否建立医院感染管理部门！");
        return false;
    }

    if (InfectMgmt.val() == "1") {
        var AntiGermsMgmt = $("input:radio[name='ctl00$MainContent$AntiGermsMgmt']:checked");
        if (AntiGermsMgmt.length == 0) {
            $.dialog.alert("请选择是否建立抗菌药物管理组！");
            return false;
        }
        else {
            if (AntiGermsMgmt.val() == "1") {
                if ($("#<%=txt_AntiGermsMgmt_Matter.ClientID %>").val() == "") {
                    alertValue("请填写医务管理人员名数！");
                    this.focus();
                    return false;
                }
                if ($("#<%=txt_AntiGermsMgmt_InfectMgmt.ClientID %>").val() == "") {
                    alertValue("请填写医院感染管理人员名数！");
                    this.focus();
                    return false;
                }
                if ($("#<%=txt_AntiGermsMgmt_ClinicDoctor.ClientID %>").val() == "") {
                    alertValue("请填写临床感染性疾病医师人数！");
                    this.focus();
                    return false;
                }
                if ($("#<%=txt_AntiGermsMgmt_Germ.ClientID %>").val() == "") {
                    alertValue("请填写微生物专业人员人数！");
                    this.focus();
                    return false;
                }
                if ($("#<%=txt_AntiGermsMgmt_InfectDoctor.ClientID %>").val() == "") {
                    alertValue("请填写抗感染专业临床药师人数！");
                    this.focus();
                    return false;
                }
                if ($("#<%=txt_AntiGermsMgmt_ClinicNurse.ClientID %>").val() == "") {
                    alertValue("请填写临床护士人数！");
                    this.focus();
                    return false;
                }
            }
        }
        var ClinicalMicrobiologyRoom = $("input:radio[name='ctl00$MainContent$rbClinicalMicrobiologyRoom']:checked");
        if (ClinicalMicrobiologyRoom.length == 0) {
            $.dialog.alert("请选择是否建立临床微生物室！");
            return false;
        }

    }
    var AntiBacteriaClinic = $("input:radio[name='ctl00$MainContent$AntiBacteriaClinic']:checked");
    if (AntiBacteriaClinic.length == 0) {
        $.dialog.alert("请选择是否建立抗菌药物管理工作制度！");
        return false;
    }
    var AntiGermsHierarchy = $("input:radio[name='ctl00$MainContent$AntiGermsHierarchy']:checked");
    if (AntiGermsHierarchy.length == 0) {
        $.dialog.alert("请选择是否建立抗菌药物应用分级管理！");
        return false;
    }

    if ($("#<%=txt_MedicationQuantity_Generic.ClientID%>").val == "") {
        alertValue("请填写西药品种数！");
        this.focus();
        return false;
    }
    if ($("#<%=txt_MedicationQuantity_Spec.ClientID %>").val() == "") {
        alertValue("请填写西药品规数！");
        this.focus();
        return false;
    }
    if ($("#<%=txt_MedicationQuantity_AntiGermsGeneric.ClientID %>").val() == "") {
        alertValue("请填写抗菌药物品种数！");
        this.focus();
        return false;
    }
    if ($("#<%=txt_MedicationQuantity_AntiGermsSpec.ClientID %>").val() == "") {
        alertValue("请填写抗菌药物品规数！");
        this.focus();
        return false;
    }
    return true;
}
